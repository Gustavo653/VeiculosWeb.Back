using VeiculosWeb.Domain.Driver;
using VeiculosWeb.DTO.Base;
using VeiculosWeb.Infrastructure.Repository.Driver;
using VeiculosWeb.Infrastructure.Service.Driver;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace VeiculosWeb.Service.Driver
{
    public class DriverItemService(IDriverItemRepository driverItemRepository,
        IDriverChecklistCheckedItemRepository driverChecklistCheckedItemRepository,
                             IDriverChecklistItemRepository driverChecklistItemRepository) : IDriverItemService
    {
        public async Task<ResponseDTO> Create(BasicDTO basicDTO)
        {
            ResponseDTO responseDTO = new();
            try
            {
                var itemExists = await driverItemRepository.GetEntities().AnyAsync(c => c.Name == basicDTO.Name);
                if (itemExists)
                {
                    responseDTO.SetBadInput($"O item {basicDTO.Name} já existe!");
                    return responseDTO;
                }

                var item = new DriverItem
                {
                    Name = basicDTO.Name,
                };
                item.SetCreatedAt();
                await driverItemRepository.InsertAsync(item);
                await driverItemRepository.SaveChangesAsync();
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
                var item = await driverItemRepository.GetTrackedEntities().FirstOrDefaultAsync(c => c.Id == id);
                if (item == null)
                {
                    responseDTO.SetBadInput($"O item {basicDTO.Name} não existe!");
                    return responseDTO;
                }

                item.Name = basicDTO.Name;
                item.SetUpdatedAt();
                await driverItemRepository.SaveChangesAsync();
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
                var ChecklistReplacedItemExists = await driverChecklistCheckedItemRepository.GetEntities().AnyAsync(c => c.DriverChecklistItem.DriverItem.Id == id);
                if (ChecklistReplacedItemExists)
                {
                    responseDTO.SetBadInput("Não é possível apagar o item, já existe uma reposição vinculada!");
                    return responseDTO;
                }

                var checkListItemExists = await driverChecklistItemRepository.GetEntities().AnyAsync(c => c.DriverItem.Id == id);
                if (ChecklistReplacedItemExists)
                {
                    responseDTO.SetBadInput("Não é possível apagar o item, já existe um item de checklist vinculado!");
                    return responseDTO;
                }

                var item = await driverItemRepository.GetTrackedEntities().FirstOrDefaultAsync(c => c.Id == id);
                if (item == null)
                {
                    responseDTO.SetBadInput($"O item com id: {id} não existe!");
                    return responseDTO;
                }

                driverItemRepository.Delete(item);
                await driverItemRepository.SaveChangesAsync();
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
                responseDTO.Object = await driverItemRepository.GetEntities().ToListAsync();
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }

            return responseDTO;
        }
    }
}