using VeiculosWeb.Domain.Paramedic;
using VeiculosWeb.DTO.Base;
using VeiculosWeb.Infrastructure.Repository.Paramedic;
using VeiculosWeb.Infrastructure.Service.Paramedic;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace VeiculosWeb.Service.Paramedic
{
    public class ParamedicItemService(IParamedicItemRepository paramedicItemRepository,
                             IParamedicChecklistReplacedItemRepository paramedicChecklistReplacedItemRepository,
                             IParamedicChecklistItemRepository paramedicChecklistItemRepository) : IParamedicItemService
    {
        public async Task<ResponseDTO> Create(BasicDTO basicDTO)
        {
            ResponseDTO responseDTO = new();
            try
            {
                var itemExists = await paramedicItemRepository.GetEntities().AnyAsync(c => c.Name == basicDTO.Name);
                if (itemExists)
                {
                    responseDTO.SetBadInput($"O item {basicDTO.Name} já existe!");
                    return responseDTO;
                }

                var item = new ParamedicItem
                {
                    Name = basicDTO.Name,
                };
                item.SetCreatedAt();
                await paramedicItemRepository.InsertAsync(item);
                await paramedicItemRepository.SaveChangesAsync();
                Log.Information("Item persistido id: {id}", item.Id);

                responseDTO.Object = item;
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }

            return responseDTO;
        }

        public async Task<ResponseDTO> Update(Guid id, BasicDTO basicDTO)
        {
            ResponseDTO responseDTO = new();
            try
            {
                var item = await paramedicItemRepository.GetTrackedEntities().FirstOrDefaultAsync(c => c.Id == id);
                if (item == null)
                {
                    responseDTO.SetBadInput($"O item {basicDTO.Name} não existe!");
                    return responseDTO;
                }

                item.Name = basicDTO.Name;
                item.SetUpdatedAt();
                await paramedicItemRepository.SaveChangesAsync();
                Log.Information("Item persistido id: {id}", item.Id);

                responseDTO.Object = item;
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
                var ChecklistReplacedItemExists = await paramedicChecklistReplacedItemRepository.GetEntities().AnyAsync(c => c.ParamedicChecklistItem.ParamedicItem.Id == id);
                if (ChecklistReplacedItemExists)
                {
                    responseDTO.SetBadInput("Não é possível apagar o item, já existe uma reposição vinculada!");
                    return responseDTO;
                }

                var checkListItemExists = await paramedicChecklistItemRepository.GetEntities().AnyAsync(c => c.ParamedicItem.Id == id);
                if (ChecklistReplacedItemExists)
                {
                    responseDTO.SetBadInput("Não é possível apagar o item, já existe um item de checklist vinculado!");
                    return responseDTO;
                }

                var item = await paramedicItemRepository.GetTrackedEntities().FirstOrDefaultAsync(c => c.Id == id);
                if (item == null)
                {
                    responseDTO.SetBadInput($"O item com id: {id} não existe!");
                    return responseDTO;
                }

                paramedicItemRepository.Delete(item);
                await paramedicItemRepository.SaveChangesAsync();
                Log.Information("Item removido id: {id}", item.Id);

                responseDTO.Object = item;
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
                responseDTO.Object = await paramedicItemRepository.GetEntities().ToListAsync();
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }

            return responseDTO;
        }
    }
}