using VeiculosWeb.Domain.Paramedic;
using VeiculosWeb.DTO.Base;
using VeiculosWeb.DTO.Paramedic;
using VeiculosWeb.Infrastructure.Repository;
using VeiculosWeb.Infrastructure.Repository.Paramedic;
using VeiculosWeb.Infrastructure.Service.Paramedic;
using VeiculosWeb.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace VeiculosWeb.Service.Paramedic
{
    public class ParamedicChecklistReviewService(IParamedicChecklistReviewRepository paramedicChecklistReviewRepository,
                                        IParamedicChecklistRepository paramedicChecklistRepository,
                                        IParamedicCategoryRepository paramedicCategoryRepository,
                                        IParamedicChecklistItemRepository paramedicChecklistItemRepository,
                                        IUserRepository userRepository,
                                        IHttpContextAccessor httpContextAccessor,
                                        IAmbulanceRepository ambulanceRepository) : IParamedicChecklistReviewService
    {
        private ISession Session => httpContextAccessor.HttpContext!.Session;

        public async Task<ResponseDTO> Create(ParamedicChecklistReviewDTO checklistReviewDTO)
        {
            ResponseDTO responseDTO = new();
            try
            {
                checklistReviewDTO.IdUser = Guid.Parse(Session.GetString(Consts.ClaimUserId)!);

                var checklist = await paramedicChecklistRepository.GetTrackedEntities().FirstOrDefaultAsync(x => x.Id == checklistReviewDTO.IdChecklist);
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

                var checklistReview = new ParamedicChecklistReview
                {
                    IsFullReview = checklistReviewDTO.IsFullReview,
                    Observation = checklistReviewDTO.Observation,
                    ParamedicChecklist = checklist,
                    Ambulance = ambulance,
                    User = user,
                };
                checklistReview.SetCreatedAt();

                await paramedicChecklistReviewRepository.InsertAsync(checklistReview);

                await ProcessChecklistReviewItems(checklistReviewDTO, checklistReview, responseDTO);

                if (responseDTO.Code == 200)
                {
                    await paramedicChecklistReviewRepository.SaveChangesAsync();
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

        private static void ValidateCategoryExists(ParamedicChecklistReviewDTO checklistReviewDTO, Guid categoryId, ResponseDTO responseDTO)
        {
            if (checklistReviewDTO.Categories.All(x => x.Id != categoryId))
            {
                responseDTO.SetBadInput($"A categoria {categoryId} deve ser conferida");
            }
        }

        private static void ValidateItemExists(ParamedicCategoryReviewDTO category, Guid itemId, ResponseDTO responseDTO)
        {
            if (category.Items.All(i => i.Id != itemId))
            {
                responseDTO.SetBadInput($"O item {itemId} deve ser conferido");
            }
        }

        private static void ValidateSubItemsExist(ParamedicItemReviewDTO itemDTO, List<Guid>? subItemIds, ResponseDTO responseDTO)
        {
            if (subItemIds != null && subItemIds.Count != 0 && (itemDTO.SubItems == null || itemDTO.SubItems.Count == 0))
            {
                responseDTO.SetBadInput("O checklist selecionado possui subitens");
                return;
            }

            if (subItemIds == null) return;
            foreach (var subItemId in subItemIds.Where(subItemId => !itemDTO.SubItems!.Any(i => i.Id == subItemId)))
            {
                responseDTO.SetBadInput($"O item {subItemId} deve ser conferido");
                return;
            }
        }


        private async Task ProcessChecklistReviewItems(ParamedicChecklistReviewDTO checklistReviewDTO, ParamedicChecklistReview checklistReview, ResponseDTO responseDTO)
        {
            if (checklistReviewDTO.IsFullReview)
            {
                var checklist = await paramedicChecklistItemRepository.GetEntities()
                                                             .Where(x => x.ParamedicChecklistId == checklistReviewDTO.IdChecklist && x.ParamedicParentChecklistItem == null)
                                                             .Select(x => new
                                                             {
                                                                 CategoryId = x.ParamedicCategoryId,
                                                                 ItemId = x.ParamedicItemId,
                                                                 SubItemIds = x.ParamedicChecklistSubItems != null && x.ParamedicChecklistSubItems.Count > 0 ?
                                                                                x.ParamedicChecklistSubItems.Select(c => c.ParamedicItemId).ToList() : null,
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

                    ValidateSubItemsExist(itemReviewDTO, item.SubItemIds, responseDTO);
                    if (responseDTO.Code != 200) return;
                    Log.Information("Subitens validados");
                }
            }

            foreach (var categoryReviewDTO in checklistReviewDTO.Categories)
            {
                Log.Information("Processando categoria: {id} Quantidade Itens: {qtd}", categoryReviewDTO.Id, categoryReviewDTO.Items.Count());
                var category = await paramedicCategoryRepository.GetTrackedEntities()
                                                        .FirstOrDefaultAsync(x => x.Id == categoryReviewDTO.Id);
                if (category == null)
                {
                    responseDTO.SetBadInput($"A categoria {categoryReviewDTO.Id} não existe");
                    return;
                }

                foreach (var itemReviewDTO in categoryReviewDTO.Items)
                {
                    Log.Information("Processando item: {id}", itemReviewDTO.Id);
                    var checklistItem = await paramedicChecklistItemRepository.GetTrackedEntities()
                                                             .Include(x => x.ParamedicChecklistReplacedItems)
                                                             .Include(x => x.ParamedicItem)
                                                             .FirstOrDefaultAsync(x => x.ParamedicItem.Id == itemReviewDTO.Id && x.ParamedicCategory.Id == category.Id && x.ParamedicParentChecklistItemId == null);

                    if (checklistItem == null)
                    {
                        responseDTO.SetBadInput($"O item {itemReviewDTO.Id} não existe");
                        return;
                    }

                    var checklistReplacedItem = new ParamedicChecklistReplacedItem()
                    {
                        ReplacedQuantity = itemReviewDTO.ReplacedQuantity,
                        ReplenishmentQuantity = itemReviewDTO.ReplenishmentQuantity,
                        RequiredQuantity = itemReviewDTO.RequiredQuantity,
                        ParamedicChecklistItem = checklistItem,
                        ParamedicChecklistReview = checklistReview,
                        TenantId = Guid.Parse(Session.GetString(Consts.ClaimTenantId)!)
                    };
                    checklistReplacedItem.SetCreatedAt();
                    checklistItem.ParamedicChecklistReplacedItems?.Add(checklistReplacedItem);
                    Log.Information("Item {id} processado", itemReviewDTO.Id);

                    if (itemReviewDTO.SubItems != null && itemReviewDTO.SubItems.Count != 0)
                    {
                        foreach (var subItemReviewDTO in itemReviewDTO.SubItems)
                        {
                            Log.Information("Processando subItem: {id}", subItemReviewDTO.Id);
                            var subChecklistItem = await paramedicChecklistItemRepository.GetTrackedEntities()
                                         .Include(x => x.ParamedicChecklistReplacedItems)
                                         .Include(x => x.ParamedicItem)
                                         .FirstOrDefaultAsync(x => x.ParamedicItem.Id == subItemReviewDTO.Id && x.ParamedicCategory.Id == category.Id);

                            if (subChecklistItem == null)
                            {
                                responseDTO.SetBadInput($"O item {subItemReviewDTO.Id} não existe");
                                return;
                            }

                            var subChecklistReplacedItem = new ParamedicChecklistReplacedItem()
                            {
                                ReplacedQuantity = subItemReviewDTO.ReplacedQuantity,
                                ReplenishmentQuantity = subItemReviewDTO.ReplenishmentQuantity,
                                RequiredQuantity = subItemReviewDTO.RequiredQuantity,
                                ParamedicChecklistItem = subChecklistItem,
                                ParamedicChecklistReview = checklistReview,
                                TenantId = Guid.Parse(Session.GetString(Consts.ClaimTenantId)!)
                            };
                            subChecklistReplacedItem.SetCreatedAt();
                            subChecklistItem.ParamedicChecklistReplacedItems?.Add(subChecklistReplacedItem);
                            Log.Information("Subitem {id} processado", subItemReviewDTO.Id);
                        }
                    }
                }
            }
        }

        public async Task<ResponseDTO> Update(Guid id, ParamedicChecklistReviewDTO checklistReviewDTO)
        {
            ResponseDTO responseDTO = new();
            try
            {
                checklistReviewDTO.IdUser = Guid.Parse(Session.GetString(Consts.ClaimUserId)!);
                var checklistReview = await paramedicChecklistReviewRepository.GetTrackedEntities()
                                                                      .Include(x => x.ParamedicChecklistReplacedItems)
                                                                      .FirstOrDefaultAsync(c => c.Id == id);
                if (checklistReview == null)
                {
                    responseDTO.SetBadInput($"A conferência do checklist {id} não existe!");
                    return responseDTO;
                }

                var checklist = await paramedicChecklistRepository.GetTrackedEntities()
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
                checklistReview.ParamedicChecklist = checklist;

                checklistReview.ParamedicChecklistReplacedItems?.RemoveAll(_ => true);

                await ProcessChecklistReviewItems(checklistReviewDTO, checklistReview, responseDTO);

                checklistReview.SetUpdatedAt();

                if (responseDTO.Code == 200)
                {
                    await paramedicChecklistReviewRepository.SaveChangesAsync();
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
                var checklistReview = await paramedicChecklistReviewRepository.GetTrackedEntities().FirstOrDefaultAsync(c => c.Id == id);
                if (checklistReview == null)
                {
                    responseDTO.SetBadInput($"O checklist com id: {id} não existe!");
                    return responseDTO;
                }

                paramedicChecklistReviewRepository.Delete(checklistReview);
                await paramedicChecklistReviewRepository.SaveChangesAsync();
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
                responseDTO.Object = await paramedicChecklistReviewRepository.GetEntities()
                                                                     .Select(x => new
                                                                     {
                                                                         x.Id,
                                                                         x.IsFullReview,
                                                                         x.CreatedAt,
                                                                         x.UpdatedAt,
                                                                         x.Ambulance,
                                                                         x.Observation,
                                                                         User = x.User.Name,
                                                                         Checklist = x.ParamedicChecklist,
                                                                         ChecklistReviews = x.ParamedicChecklistReplacedItems != null &&
                                                                                            x.ParamedicChecklistReplacedItems.Count != 0 ?
                                                                                            x.ParamedicChecklistReplacedItems.Where(x => x.ParamedicChecklistItem.ParamedicParentChecklistItemId == null)
                                                                                                                    .Select(x => new
                                                                                                                    {
                                                                                                                        category = x.ParamedicChecklistItem.ParamedicCategory.Name,
                                                                                                                        item = x.ParamedicChecklistItem.ParamedicItem.Name,
                                                                                                                        requiredQuantity = x.RequiredQuantity,
                                                                                                                        replacedQuantity = x.ReplacedQuantity,
                                                                                                                        replenishmentQuantity = x.ReplenishmentQuantity,
                                                                                                                        subItems = x.ParamedicChecklistItem.ParamedicChecklistSubItems != null &&
                                                                                                                                   x.ParamedicChecklistItem.ParamedicChecklistSubItems.Count != 0 ?
                                                                                                                                   x.ParamedicChecklistItem.ParamedicChecklistSubItems.Select(x => new
                                                                                                                                   {
                                                                                                                                       category = x.ParamedicCategory.Name,
                                                                                                                                       item = x.ParamedicItem.Name,
                                                                                                                                       requiredQuantity = x.ParamedicChecklistReplacedItems != null &&
                                                                                                                                                        x.ParamedicChecklistReplacedItems.Count != 0 ?
                                                                                                                                                        x.ParamedicChecklistReplacedItems.FirstOrDefault()!.RequiredQuantity : int.MinValue,
                                                                                                                                       replacedQuantity = x.ParamedicChecklistReplacedItems != null &&
                                                                                                                                                        x.ParamedicChecklistReplacedItems.Count != 0 ?
                                                                                                                                                        x.ParamedicChecklistReplacedItems.FirstOrDefault()!.ReplacedQuantity : int.MinValue,
                                                                                                                                       replenishmentQuantity = x.ParamedicChecklistReplacedItems != null &&
                                                                                                                                                        x.ParamedicChecklistReplacedItems.Count != 0 ?
                                                                                                                                                        x.ParamedicChecklistReplacedItems.FirstOrDefault()!.ReplenishmentQuantity : int.MinValue,
                                                                                                                                   }) : null
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