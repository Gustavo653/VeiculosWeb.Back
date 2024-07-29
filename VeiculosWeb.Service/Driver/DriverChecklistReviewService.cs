using Hangfire;
using VeiculosWeb.Domain.Driver;
using VeiculosWeb.DTO.Base;
using VeiculosWeb.DTO.Driver;
using VeiculosWeb.Infrastructure.Repository;
using VeiculosWeb.Infrastructure.Repository.Driver;
using VeiculosWeb.Infrastructure.Service.Driver;
using VeiculosWeb.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace VeiculosWeb.Service.Driver
{
    public class DriverChecklistReviewService(IDriverChecklistReviewRepository driverChecklistReviewRepository,
                                        IDriverChecklistRepository driverChecklistRepository,
                                        IDriverCategoryRepository driverCategoryRepository,
                                        IDriverChecklistItemRepository driverChecklistItemRepository,
                                        IUserRepository userRepository,
                                        IGoogleCloudStorageService googleCloudStorageService,
                                        IHttpContextAccessor httpContextAccessor,
                                        IAmbulanceRepository ambulanceRepository) : IDriverChecklistReviewService
    {
        private ISession Session => httpContextAccessor.HttpContext!.Session;

        public async Task<ResponseDTO> Create(DriverChecklistReviewDTO checklistReviewDTO)
        {
            ResponseDTO responseDTO = new();
            try
            {
                checklistReviewDTO.IdUser = Guid.Parse(Session.GetString(Consts.ClaimUserId)!);

                var checklist = await driverChecklistRepository.GetTrackedEntities().FirstOrDefaultAsync(x => x.Id == checklistReviewDTO.IdChecklist);
                if (checklist == null)
                {
                    responseDTO.SetBadInput($"O checklist {checklistReviewDTO.IdChecklist} não existe!");
                    return responseDTO;
                }

                if (checklist.RequireFullReview && !checklistReviewDTO.IsFullReview)
                {
                    responseDTO.SetBadInput("O checklist deve ser completo!");
                    return responseDTO;
                }

                var user = await userRepository.GetTrackedEntities().FirstOrDefaultAsync(x => x.Id == checklistReviewDTO.IdUser.ToString());
                if (user == null)
                {
                    responseDTO.SetBadInput($"O usuário {checklistReviewDTO.IdUser} não existe!");
                    return responseDTO;
                }

                var ambulance = await ambulanceRepository.GetTrackedEntities().FirstOrDefaultAsync(x => x.Id == checklistReviewDTO.IdAmbulance);
                if (ambulance == null)
                {
                    responseDTO.SetBadInput($"A ambulância {checklistReviewDTO.IdAmbulance} não existe!");
                    return responseDTO;
                }

                var checklistReview = new DriverChecklistReview
                {
                    IsFullReview = checklistReviewDTO.IsFullReview,
                    Observation = checklistReviewDTO.Observation,
                    DriverChecklist = checklist,
                    Ambulance = ambulance,
                    DriverChecklistCheckedItems = [],
                    User = user,
                };
                checklistReview.SetCreatedAt();

                await driverChecklistReviewRepository.InsertAsync(checklistReview);

                await ProcessChecklistReviewItems(checklistReviewDTO, checklistReview, responseDTO);

                if (responseDTO.Code == 200)
                {
                    await driverChecklistReviewRepository.SaveChangesAsync();
                    Log.Information("ChecklistReview persistido id: {id}", checklistReview.Id);
                }

                responseDTO.Object = checklistReviewDTO;
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }

            return responseDTO;
        }

        private static void ValidateCategoryExists(DriverChecklistReviewDTO checklistReviewDTO, Guid categoryId, ResponseDTO responseDTO)
        {
            if (checklistReviewDTO.Categories.All(x => x.Id != categoryId))
            {
                responseDTO.SetBadInput($"A categoria {categoryId} deve ser conferida");
            }
        }

        private static void ValidateItemExists(DriverCategoryReviewDTO category, Guid itemId, ResponseDTO responseDTO)
        {
            if (category.Items.All(i => i.Id != itemId))
            {
                responseDTO.SetBadInput($"O item {itemId} deve ser conferido");
            }
        }

        private static void ValidateOptionExists(DriverItemReviewDTO itemDTO, IEnumerable<Guid> optionIds, ResponseDTO responseDTO)
        {
            if (optionIds.All(x => x != itemDTO.IdOption))
            {
                responseDTO.SetBadInput($"A opção {itemDTO.IdOption} não é válida");
            }
        }

        private async Task ProcessChecklistReviewItems(DriverChecklistReviewDTO checklistReviewDTO, DriverChecklistReview checklistReview, ResponseDTO responseDTO)
        {
            if (checklistReviewDTO.IsFullReview)
            {
                var checklist = await driverChecklistItemRepository.GetEntities()
                                                             .Where(x => x.DriverChecklistId == checklistReviewDTO.IdChecklist)
                                                             .Select(x => new
                                                             {
                                                                 CategoryId = x.DriverCategoryId,
                                                                 ItemId = x.DriverItemId,
                                                                 OptionsIds = x.DriverOptions.Select(x => x.Id)
                                                             })
                                                             .ToListAsync();

                foreach (var item in checklist)
                {
                    ValidateCategoryExists(checklistReviewDTO, item.CategoryId, responseDTO);
                    if (responseDTO.Code != 200) return;
                    Log.Information("Categorias validadas");

                    var categoryReviewDTO = checklistReviewDTO.Categories.First(x => x.Id == item.CategoryId);

                    ValidateItemExists(categoryReviewDTO, item.ItemId, responseDTO);
                    if (responseDTO.Code != 200) return;
                    Log.Information("Itens validados");

                    var itemReviewDTO = categoryReviewDTO.Items.First(i => i.Id == item.ItemId);

                    ValidateOptionExists(itemReviewDTO, item.OptionsIds, responseDTO);
                    if (responseDTO.Code != 200) return;
                    Log.Information("Opções validadas");
                }
            }

            foreach (var categoryReviewDTO in checklistReviewDTO.Categories)
            {
                Log.Information("Processando categoria: {id} Quantidade Itens: {qtd}", categoryReviewDTO.Id, categoryReviewDTO.Items.Count());
                var category = await driverCategoryRepository.GetTrackedEntities()
                                                        .FirstOrDefaultAsync(x => x.Id == categoryReviewDTO.Id);
                if (category == null)
                {
                    responseDTO.SetBadInput($"A categoria {categoryReviewDTO.Id} não existe");
                    return;
                }

                foreach (var itemReviewDTO in categoryReviewDTO.Items)
                {
                    Log.Information("Processando item: {id}", itemReviewDTO.Id);
                    var checklistItem = await driverChecklistItemRepository.GetTrackedEntities()
                                                             .Include(x => x.DriverChecklistCheckedItems)
                                                             .Include(x => x.DriverItem)
                                                             .Include(x => x.DriverOptions)
                                                             .FirstOrDefaultAsync(x => x.DriverItem.Id == itemReviewDTO.Id && x.DriverCategory.Id == category.Id);

                    if (checklistItem == null)
                    {
                        responseDTO.SetBadInput($"O item {itemReviewDTO.Id} não existe");
                        return;
                    }

                    var driverChecklistCheckedItem = new DriverChecklistCheckedItem()
                    {
                        DriverOption = checklistItem.DriverOptions.FirstOrDefault(x => x.Id == itemReviewDTO.IdOption)!,
                        DriverChecklistItem = checklistItem,
                        DriverChecklistReview = checklistReview,
                        DriverMedias = [],
                        TenantId = Guid.Parse(Session.GetString(Consts.ClaimTenantId)!)
                    };
                    driverChecklistCheckedItem.SetCreatedAt();
                    checklistReview.DriverChecklistCheckedItems!.Add(driverChecklistCheckedItem);
                    Log.Information("Item {id} processado", itemReviewDTO.Id);

                    foreach (var item in itemReviewDTO.Medias)
                    {
                        Log.Information("Processando mídia: {id}", item.FileName);

                        var driverMediaId = Guid.NewGuid();
                        var url = await googleCloudStorageService.UploadFileToGcsAsync(item, $"{driverMediaId}{Path.GetExtension(item.FileName)}");
                        //BackgroundJob.Enqueue(() => googleCloudStorageService.UploadFileToGcsAsync(item, mediaName));

                        var driverMedia = new DriverMedia()
                        {
                            Id = driverMediaId,
                            DriverChecklistCheckedItem = driverChecklistCheckedItem,
                            Url = url,
                            TenantId = Guid.Parse(Session.GetString(Consts.ClaimTenantId)!)
                        };
                        driverMedia.SetCreatedAt();
                        driverChecklistCheckedItem.DriverMedias?.Add(driverMedia);
                        Log.Information("Mídia {id} processada", item.FileName);
                    }
                }
            }
        }

        public async Task<ResponseDTO> Update(Guid id, DriverChecklistReviewDTO checklistReviewDTO)
        {
            ResponseDTO responseDTO = new();
            try
            {
                checklistReviewDTO.IdUser = Guid.Parse(Session.GetString(Consts.ClaimUserId)!);
                var checklistReview = await driverChecklistReviewRepository.GetTrackedEntities()
                                                                      .Include(x => x.DriverChecklistCheckedItems)!.ThenInclude(x => x.DriverMedias)
                                                                      .FirstOrDefaultAsync(c => c.Id == id);
                if (checklistReview == null)
                {
                    responseDTO.SetBadInput($"A conferência do checklist {id} não existe!");
                    return responseDTO;
                }

                var checklist = await driverChecklistRepository.GetTrackedEntities()
                                                          .FirstOrDefaultAsync(x => x.Id == checklistReviewDTO.IdChecklist);
                if (checklist == null)
                {
                    responseDTO.SetBadInput($"O checklist {checklistReviewDTO.IdChecklist} não existe!");
                    return responseDTO;
                }

                if (checklist.RequireFullReview && checklistReviewDTO.IsFullReview)
                {
                    responseDTO.SetBadInput("O checklist deve ser completo!");
                    return responseDTO;
                }

                checklistReview.Observation = checklistReviewDTO.Observation;
                checklistReview.DriverChecklist = checklist;

                foreach (var item in checklistReview.DriverChecklistCheckedItems!)
                {
                    foreach (var media in item.DriverMedias!)
                    {
                        BackgroundJob.Enqueue(() => googleCloudStorageService.DeleteFileFromGcsAsync(media.Url));
                    }
                }

                checklistReview.DriverChecklistCheckedItems?.RemoveAll(_ => true);

                await ProcessChecklistReviewItems(checklistReviewDTO, checklistReview, responseDTO);

                checklistReview.SetUpdatedAt();

                if (responseDTO.Code == 200)
                {
                    await driverChecklistReviewRepository.SaveChangesAsync();
                    Log.Information("ChecklistReview persistido id: {id}", checklistReview.Id);
                }

                responseDTO.Object = checklistReview;
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }

            return responseDTO;
        }

        public async Task<ResponseDTO> Remove(Guid id)
        {
            ResponseDTO responseDTO = new();
            try
            {
                var checklistReview = await driverChecklistReviewRepository.GetTrackedEntities().FirstOrDefaultAsync(c => c.Id == id);
                if (checklistReview == null)
                {
                    responseDTO.SetBadInput($"O checklist com id: {id} não existe!");
                    return responseDTO;
                }

                driverChecklistReviewRepository.Delete(checklistReview);
                await driverChecklistReviewRepository.SaveChangesAsync();
                Log.Information("ChecklistReview removido id: {id}", checklistReview.Id);

                responseDTO.Object = checklistReview;
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }

            return responseDTO;
        }

        public async Task<ResponseDTO> GetList(int? takeLast)
        {
            ResponseDTO responseDTO = new();
            try
            {
                responseDTO.Object = await driverChecklistReviewRepository.GetEntities()
                                                                     .Select(x => new
                                                                     {
                                                                         x.Id,
                                                                         x.IsFullReview,
                                                                         x.CreatedAt,
                                                                         x.UpdatedAt,
                                                                         x.Ambulance,
                                                                         x.Observation,
                                                                         User = x.User.Name,
                                                                         Checklist = x.DriverChecklist,
                                                                         ChecklistReviews = x.DriverChecklistCheckedItems != null &&
                                                                                            x.DriverChecklistCheckedItems.Count != 0 ?
                                                                                            x.DriverChecklistCheckedItems
                                                                                                                    .Select(x => new
                                                                                                                    {
                                                                                                                        category = x.DriverChecklistItem.DriverCategory.Name,
                                                                                                                        item = x.DriverChecklistItem.DriverItem.Name,
                                                                                                                        option = x.DriverOption,
                                                                                                                        medias = x.DriverMedias
                                                                                                                    }) : null
                                                                     })
                                                                     .OrderByDescending(x => x.Id)
                                                                     .Take(takeLast ?? 1000)
                                                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }

            return responseDTO;
        }

        public Task<ResponseDTO> GetList()
        {
            throw new NotImplementedException();
        }
    }
}