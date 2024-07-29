using VeiculosWeb.Domain.Paramedic;
using VeiculosWeb.DTO.Base;
using VeiculosWeb.DTO.Paramedic;
using VeiculosWeb.Infrastructure.Repository.Paramedic;
using VeiculosWeb.Infrastructure.Service.Paramedic;
using VeiculosWeb.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace VeiculosWeb.Service.Paramedic
{
    public class ParamedicChecklistService(IParamedicChecklistRepository paramedicChecklistRepository,
                                  IParamedicCategoryRepository paramedicCategoryRepository,
                                  IParamedicItemRepository paramedicItemRepository,
                                  IHttpContextAccessor httpContextAccessor) : IParamedicChecklistService
    {
        private ISession Session => httpContextAccessor.HttpContext!.Session;

        public async Task<ResponseDTO> Remove(Guid id)
        {
            ResponseDTO responseDTO = new();
            try
            {
                var checklist = await paramedicChecklistRepository.GetTrackedEntities().FirstOrDefaultAsync(c => c.Id == id);
                if (checklist == null)
                {
                    responseDTO.SetBadInput($"O checklist com id: {id} não existe!");
                    return responseDTO;
                }

                paramedicChecklistRepository.Delete(checklist);
                await paramedicChecklistRepository.SaveChangesAsync();
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
                var checklists = await paramedicChecklistRepository.GetEntities()
                    .Include(x => x.ParamedicChecklistItems)!.ThenInclude(x => x.ParamedicItem)
                    .Include(x => x.ParamedicChecklistItems)!.ThenInclude(x => x.ParamedicChecklistSubItems)!.ThenInclude(x => x.ParamedicItem)
                    .Include(x => x.ParamedicChecklistItems)!.ThenInclude(x => x.ParamedicCategory)
                    .ToListAsync();

                var jsonData = checklists.Select(checklist => new
                {
                    id = checklist.Id,
                    createdAt = checklist.CreatedAt,
                    updatedAt = checklist.UpdatedAt,
                    name = checklist.Name,
                    requireFullReview = checklist.RequireFullReview,
                    categories = checklist.ParamedicChecklistItems?
                            .Where(x => x.ParamedicParentChecklistItemId == null)
                            .Select(item => new
                            {
                                id = item.ParamedicCategory.Id,
                                name = item.ParamedicCategory.Name,
                                items = new List<object>
                                {
                                    new
                                    {
                                        id = item.ParamedicItem.Id,
                                        name = item.ParamedicItem.Name,
                                        amountRequired = item.RequiredQuantity,
                                        subItems = item.ParamedicChecklistSubItems != null && item.ParamedicChecklistSubItems.Count != 0 ? item.ParamedicChecklistSubItems.Select(x=>new
                                        {
                                            id = x.ParamedicItem.Id,
                                            name = x.ParamedicItem.Name,
                                            amountRequired = x.RequiredQuantity
                                        }) : []
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

        public async Task<ResponseDTO> Create(ParamedicChecklistDTO checklistDTO)
        {
            ResponseDTO responseDTO = new();
            try
            {
                var checklistExists = await paramedicChecklistRepository.GetEntities().AnyAsync(c => c.Name == checklistDTO.Name);
                if (checklistExists)
                {
                    responseDTO.SetBadInput($"O checklist {checklistDTO.Name} já existe!");
                    return responseDTO;
                }

                var checklist = new ParamedicChecklist
                {
                    Name = checklistDTO.Name,
                    RequireFullReview = checklistDTO.RequireFullReview,
                    ParamedicChecklistItems = []
                };

                await ProcessChecklistItems(checklistDTO, checklist, responseDTO);

                checklist.SetCreatedAt();
                await paramedicChecklistRepository.InsertAsync(checklist);

                if (responseDTO.Code == 200)
                {
                    await paramedicChecklistRepository.SaveChangesAsync();
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

        private async Task ProcessChecklistItems(ParamedicChecklistDTO checklistDTO, ParamedicChecklist checklist, ResponseDTO responseDTO)
        {
            foreach (var categoryDTO in checklistDTO.Categories)
            {
                Log.Information("Processando categoria: {id} Quantidade Itens: {qtd}", categoryDTO.Id, categoryDTO.Items.Count());
                var category = await paramedicCategoryRepository.GetTrackedEntities()
                                                        .FirstOrDefaultAsync(x => x.Id == categoryDTO.Id);

                if (category == null)
                {
                    responseDTO.SetBadInput($"A categoria {categoryDTO.Id} não existe");
                    return;
                }

                foreach (var itemDTO in categoryDTO.Items)
                {
                    Log.Information("Processando item: {id}", itemDTO.Id);
                    var item = await paramedicItemRepository.GetTrackedEntities()
                                                    .FirstOrDefaultAsync(x => x.Id == itemDTO.Id) ??
                                                    throw new Exception();

                    if (item == null)
                    {
                        responseDTO.SetBadInput($"O item {itemDTO.Id} não existe");
                        return;
                    }

                    var checklistItem = new ParamedicChecklistItem()
                    {
                        ParamedicCategory = category,
                        ParamedicChecklist = checklist,
                        ParamedicItem = item,
                        RequiredQuantity = itemDTO.AmountRequired,
                        TenantId = Guid.Parse(Session.GetString(Consts.ClaimTenantId)!)
                    };
                    checklistItem.SetCreatedAt();
                    checklist.ParamedicChecklistItems?.Add(checklistItem);
                    Log.Information("Item {id} processado", itemDTO.Id);

                    if (itemDTO.SubItems != null && itemDTO.SubItems.Count != 0)
                    {
                        foreach (var subItemDTO in itemDTO.SubItems)
                        {
                            Log.Information("Processando subItem: {id}", subItemDTO.Id);
                            var subItem = await paramedicItemRepository.GetTrackedEntities()
                                                    .FirstOrDefaultAsync(x => x.Id == subItemDTO.Id);

                            if (subItem == null)
                            {
                                responseDTO.SetBadInput($"O item {subItemDTO.Id} não existe");
                                return;
                            }

                            var subChecklistItem = new ParamedicChecklistItem()
                            {
                                ParamedicCategory = category,
                                ParamedicChecklist = checklist,
                                ParamedicItem = subItem,
                                ParamedicParentChecklistItem = checklistItem,
                                RequiredQuantity = subItemDTO.AmountRequired,
                                TenantId = Guid.Parse(Session.GetString(Consts.ClaimTenantId)!)
                            };
                            subChecklistItem.SetCreatedAt();
                            checklist.ParamedicChecklistItems?.Add(subChecklistItem);
                            Log.Information("Subitem {id} processado", subItemDTO.Id);
                        }
                    }
                }
            }
        }

        public async Task<ResponseDTO> Update(Guid id, ParamedicChecklistDTO checklistDTO)
        {
            ResponseDTO responseDTO = new();
            try
            {
                var checklist = await paramedicChecklistRepository.GetTrackedEntities()
                                                         .Include(x => x.ParamedicChecklistItems)
                                                         .FirstOrDefaultAsync(c => c.Id == id);
                if (checklist == null)
                {
                    responseDTO.SetBadInput($"O checklist {checklistDTO.Name} não existe!");
                    return responseDTO;
                }

                checklist.Name = checklistDTO.Name;
                checklist.RequireFullReview = checklistDTO.RequireFullReview;
                checklist.ParamedicChecklistItems?.RemoveAll(_ => true);

                await ProcessChecklistItems(checklistDTO, checklist, responseDTO);

                checklist.SetUpdatedAt();

                if (responseDTO.Code == 200)
                {
                    await paramedicChecklistRepository.SaveChangesAsync();
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
                var checklist = await paramedicChecklistRepository.GetEntities()
                    .Include(x => x.ParamedicChecklistItems)!.ThenInclude(x => x.ParamedicItem)
                    .Include(x => x.ParamedicChecklistItems)!.ThenInclude(x => x.ParamedicCategory)
                    .FirstOrDefaultAsync(x => x.Id == id);

                var jsonData = new
                {
                    id = checklist!.Id,
                    checklist.CreatedAt,
                    checklist.UpdatedAt,
                    name = checklist.Name,
                    requireFullReview = checklist.RequireFullReview,
                    categories = checklist.ParamedicChecklistItems?
                        .Where(x => x.ParamedicParentChecklistItemId == null)
                        .Select(item => new
                        {
                            id = item.ParamedicCategory.Id,
                            name = item.ParamedicCategory.Name,
                            items = new List<object>
                            {
                                new
                                {
                                    id = item.ParamedicItem.Id,
                                    name = item.ParamedicItem.Name,
                                    amountRequired = item.RequiredQuantity,
                                    subItems = item.ParamedicChecklistSubItems != null && item.ParamedicChecklistSubItems.Count != 0 ? item.ParamedicChecklistSubItems.Select(x=>new
                                    {
                                        id = x.ParamedicItem.Id,
                                        name = x.ParamedicItem.Name,
                                        amountRequired = x.RequiredQuantity
                                    }) : []
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
