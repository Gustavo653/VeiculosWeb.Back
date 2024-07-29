using VeiculosWeb.Domain.Driver;
using VeiculosWeb.DTO.Base;
using VeiculosWeb.DTO.Driver;
using VeiculosWeb.Infrastructure.Repository.Driver;
using VeiculosWeb.Infrastructure.Service.Driver;
using VeiculosWeb.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace VeiculosWeb.Service.Driver
{
    public class DriverChecklistService(IDriverChecklistRepository driverChecklistRepository,
                                  IDriverCategoryRepository driverCategoryRepository,
                                  IDriverItemRepository driverItemRepository,
                                  IHttpContextAccessor httpContextAccessor) : IDriverChecklistService
    {
        private ISession Session => httpContextAccessor.HttpContext!.Session;

        public async Task<ResponseDTO> Remove(Guid id)
        {
            ResponseDTO responseDTO = new();
            try
            {
                var checklist = await driverChecklistRepository.GetTrackedEntities().FirstOrDefaultAsync(c => c.Id == id);
                if (checklist == null)
                {
                    responseDTO.SetBadInput($"O checklist com id: {id} não existe!");
                    return responseDTO;
                }

                driverChecklistRepository.Delete(checklist);
                await driverChecklistRepository.SaveChangesAsync();
                Log.Information("Checklist removido id: {id}", checklist.Id);

                responseDTO.Object = checklist;
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }

            return responseDTO;
        }

        public async Task<ResponseDTO> GetList()
        {
            ResponseDTO responseDTO = new();
            try
            {
                var checklists = await driverChecklistRepository.GetEntities()
                    .Include(x => x.DriverChecklistItems)!.ThenInclude(x => x.DriverItem)
                    .Include(x => x.DriverChecklistItems)!.ThenInclude(x => x.DriverCategory)
                    .Include(x => x.DriverChecklistItems)!.ThenInclude(x => x.DriverOptions)
                    .ToListAsync();

                var jsonData = checklists.Select(checklist => new
                {
                    id = checklist.Id,
                    createdAt = checklist.CreatedAt,
                    updatedAt = checklist.UpdatedAt,
                    name = checklist.Name,
                    requireFullReview = checklist.RequireFullReview,
                    categories = checklist.DriverChecklistItems?
                            .Select(item => new
                            {
                                id = item.DriverCategory.Id,
                                name = item.DriverCategory.Name,
                                items = new List<object>
                                {
                                    new
                                    {
                                        id = item.DriverItem.Id,
                                        name = item.DriverItem.Name,
                                        options = item.DriverOptions.Select(x => new { x.Id, x.Name })
                                    }
                                }
                            })
                }).GroupBy(checklist => new { checklist.id, checklist.name, checklist.createdAt, checklist.updatedAt, checklist.requireFullReview })
                    .Select(groupedChecklist => new
                    {
                        groupedChecklist.Key.id,
                        groupedChecklist.Key.createdAt,
                        groupedChecklist.Key.updatedAt,
                        groupedChecklist.Key.requireFullReview,
                        groupedChecklist.Key.name,
                        categories = groupedChecklist
                            .SelectMany(checklist => checklist.categories!)
                            .GroupBy(category => new { category.id, category.name })
                            .Select(groupedCategory => new
                            {
                                groupedCategory.Key.id,
                                groupedCategory.Key.name,
                                items = groupedCategory.SelectMany(category => category.items)
                            })
                    });

                responseDTO.Object = jsonData;
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }

            return responseDTO;
        }

        public async Task<ResponseDTO> Create(DriverChecklistDTO checklistDTO)
        {
            ResponseDTO responseDTO = new();
            try
            {
                var checklistExists = await driverChecklistRepository.GetEntities().AnyAsync(c => c.Name == checklistDTO.Name);
                if (checklistExists)
                {
                    responseDTO.SetBadInput($"O checklist {checklistDTO.Name} já existe!");
                    return responseDTO;
                }

                var checklist = new DriverChecklist
                {
                    Name = checklistDTO.Name,
                    RequireFullReview = checklistDTO.RequireFullReview,
                    DriverChecklistItems = []
                };

                await ProcessChecklistItems(checklistDTO, checklist, responseDTO);

                checklist.SetCreatedAt();
                await driverChecklistRepository.InsertAsync(checklist);

                if (responseDTO.Code == 200)
                {
                    await driverChecklistRepository.SaveChangesAsync();
                    Log.Information("Checklist persistido id: {id}", checklist.Id);
                }

                responseDTO.Object = checklistDTO;
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }

            return responseDTO;
        }

        private async Task ProcessChecklistItems(DriverChecklistDTO checklistDTO, DriverChecklist checklist, ResponseDTO responseDTO)
        {
            foreach (var categoryDTO in checklistDTO.Categories)
            {
                Log.Information("Processando categoria: {id} Quantidade Itens: {qtd}", categoryDTO.Id, categoryDTO.Items.Count());
                var category = await driverCategoryRepository.GetTrackedEntities()
                                                        .FirstOrDefaultAsync(x => x.Id == categoryDTO.Id);

                if (category == null)
                {
                    responseDTO.SetBadInput($"A categoria {categoryDTO.Id} não existe");
                    return;
                }

                foreach (var itemDTO in categoryDTO.Items)
                {
                    Log.Information("Processando item: {id}", itemDTO.Id);
                    var item = await driverItemRepository.GetTrackedEntities()
                                                   .FirstOrDefaultAsync(x => x.Id == itemDTO.Id);

                    if (item == null)
                    {
                        responseDTO.SetBadInput($"O item {itemDTO.Id} não existe");
                        return;
                    }

                    var checklistItem = new DriverChecklistItem()
                    {
                        DriverCategory = category,
                        DriverChecklist = checklist,
                        DriverItem = item,
                        DriverOptions = [],
                        TenantId = Guid.Parse(Session.GetString(Consts.ClaimTenantId)!)
                    };
                    checklistItem.SetCreatedAt();
                    checklist.DriverChecklistItems?.Add(checklistItem);
                    Log.Information("Item {id} processado", itemDTO.Id);
                    foreach (var optionDTO in itemDTO.Options)
                    {
                        Log.Information("Processando opção: {Name}", optionDTO.Name);

                        var driverOption = new DriverOption()
                        {
                            Name = optionDTO.Name,
                            RequireSomeAction = optionDTO.RequireSomeAction,
                            DriverChecklistItem = checklistItem,
                            TenantId = Guid.Parse(Session.GetString(Consts.ClaimTenantId)!)
                        };
                        driverOption.SetCreatedAt();
                        checklistItem.DriverOptions?.Add(driverOption);
                        Log.Information("Opção {Name} processado", optionDTO.Name);
                    }
                }
            }
        }

        public async Task<ResponseDTO> Update(Guid id, DriverChecklistDTO checklistDTO)
        {
            ResponseDTO responseDTO = new();
            try
            {
                var checklist = await driverChecklistRepository.GetTrackedEntities()
                                                         .Include(x => x.DriverChecklistItems)
                                                         .FirstOrDefaultAsync(c => c.Id == id);
                if (checklist == null)
                {
                    responseDTO.SetBadInput($"O checklist {checklistDTO.Name} não existe!");
                    return responseDTO;
                }

                checklist.Name = checklistDTO.Name;
                checklist.RequireFullReview = checklistDTO.RequireFullReview;
                checklist.DriverChecklistItems?.RemoveAll(_ => true);

                await ProcessChecklistItems(checklistDTO, checklist, responseDTO);

                checklist.SetUpdatedAt();

                if (responseDTO.Code == 200)
                {
                    await driverChecklistRepository.SaveChangesAsync();
                    Log.Information("Checklist persistido id: {id}", checklist.Id);
                }

                responseDTO.Object = checklist;
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }

            return responseDTO;
        }

        public async Task<ResponseDTO> GetById(Guid id)
        {
            ResponseDTO responseDTO = new();
            try
            {
                var checklist = await driverChecklistRepository.GetEntities()
                    .Include(x => x.DriverChecklistItems)!.ThenInclude(x => x.DriverItem)
                    .Include(x => x.DriverChecklistItems)!.ThenInclude(x => x.DriverCategory)
                    .Include(x => x.DriverChecklistItems)!.ThenInclude(x => x.DriverOptions)
                    .FirstOrDefaultAsync(x => x.Id == id);

                var jsonData = new
                {
                    id = checklist!.Id,
                    checklist.CreatedAt,
                    checklist.UpdatedAt,
                    name = checklist.Name,
                    requireFullReview = checklist.RequireFullReview,
                    categories = checklist.DriverChecklistItems?
                        .Select(item => new
                        {
                            id = item.DriverCategory.Id,
                            name = item.DriverCategory.Name,
                            items = new List<object>
                            {
                                new
                                {
                                    id = item.DriverItem.Id,
                                    name = item.DriverItem.Name,
                                    options = item.DriverOptions.Select(x => new { x.Id, x.Name })
                                }
                            }
                        })
                        .GroupBy(category => new { category.id, category.name })
                        .Select(groupedCategory => new
                        {
                            groupedCategory.Key.id,
                            groupedCategory.Key.name,
                            items = groupedCategory.SelectMany(category => category.items)
                        })
                };

                responseDTO.Object = jsonData;
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }

            return responseDTO;
        }
    }
}
