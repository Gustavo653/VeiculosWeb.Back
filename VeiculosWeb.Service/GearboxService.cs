using Microsoft.EntityFrameworkCore;
using Serilog;
using VeiculosWeb.Domain.VehicleSpecification;
using VeiculosWeb.DTO;
using VeiculosWeb.DTO.Base;
using VeiculosWeb.Infrastructure.Repository;
using VeiculosWeb.Infrastructure.Service;

namespace VeiculosWeb.Service
{
    public class GearboxService(IGearboxRepository gearboxRepository) : IGearboxService
    {
        public async Task<ResponseDTO> Create(GearboxDTO gearboxDTO)
        {
            ResponseDTO responseDTO = new();
            try
            {
                var gearboxExists = await gearboxRepository.GetEntities().AnyAsync(c => c.Name == gearboxDTO.Name);
                if (gearboxExists)
                {
                    responseDTO.SetBadInput($"O câmbio {gearboxDTO.Name} já existe!");
                    return responseDTO;
                }

                var gearbox = new Gearbox()
                {
                    Name = gearboxDTO.Name,
                };
                await gearboxRepository.InsertAsync(gearbox);

                await gearboxRepository.SaveChangesAsync();
                Log.Information("Câmbio persistido id: {id}", gearbox.Id);

                responseDTO.Object = gearbox;
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }
            return responseDTO;
        }

        public async Task<ResponseDTO> Update(Guid id, GearboxDTO gearboxDTO)
        {
            ResponseDTO responseDTO = new();
            try
            {
                var gearbox = await gearboxRepository.GetTrackedEntities().FirstOrDefaultAsync(c => c.Id == id);
                if (gearbox == null)
                {
                    responseDTO.SetBadInput($"O câmbio {gearboxDTO.Name} não existe!");
                    return responseDTO;
                }

                gearbox.Name = gearboxDTO.Name;
                gearbox.SetUpdatedAt();

                await gearboxRepository.SaveChangesAsync();
                Log.Information("Câmbio persistido id: {id}", gearbox.Id);

                responseDTO.Object = gearbox;
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
                var gearbox = await gearboxRepository.GetTrackedEntities().FirstOrDefaultAsync(c => c.Id == id);
                if (gearbox == null)
                {
                    responseDTO.SetBadInput($"O câmbio com id: {id} não existe!");
                    return responseDTO;
                }
                gearboxRepository.Delete(gearbox);
                await gearboxRepository.SaveChangesAsync();
                Log.Information("Câmbio removido id: {id}", gearbox.Id);

                responseDTO.Object = gearbox;
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
                responseDTO.Object = await gearboxRepository.GetEntities().ToListAsync();
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }
            return responseDTO;
        }
    }
}
