using Microsoft.EntityFrameworkCore;
using Serilog;
using VeiculosWeb.Domain.VehicleSpecification;
using VeiculosWeb.DTO;
using VeiculosWeb.DTO.Base;
using VeiculosWeb.Infrastructure.Repository;
using VeiculosWeb.Infrastructure.Service;

namespace VeiculosWeb.Service
{
    public class ColorService(IColorRepository colorRepository) : IColorService
    {
        public async Task<ResponseDTO> Create(ColorDTO colorDTO)
        {
            ResponseDTO responseDTO = new();
            try
            {
                var colorExists = await colorRepository.GetEntities().AnyAsync(c => c.Name == colorDTO.Name);
                if (colorExists)
                {
                    responseDTO.SetBadInput($"A cor {colorDTO.Name} já existe!");
                    return responseDTO;
                }

                var color = new Color()
                {
                    Name = colorDTO.Name,
                };
                await colorRepository.InsertAsync(color);

                await colorRepository.SaveChangesAsync();
                Log.Information("Cor persistida id: {id}", color.Id);

                responseDTO.Object = color;
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }
            return responseDTO;
        }

        public async Task<ResponseDTO> Update(Guid id, ColorDTO colorDTO)
        {
            ResponseDTO responseDTO = new();
            try
            {
                var color = await colorRepository.GetTrackedEntities().FirstOrDefaultAsync(c => c.Id == id);
                if (color == null)
                {
                    responseDTO.SetBadInput($"A cor {colorDTO.Name} não existe!");
                    return responseDTO;
                }

                color.Name = colorDTO.Name;
                color.SetUpdatedAt();

                await colorRepository.SaveChangesAsync();
                Log.Information("Cor persistida id: {id}", color.Id);

                responseDTO.Object = color;
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
                var color = await colorRepository.GetTrackedEntities().FirstOrDefaultAsync(c => c.Id == id);
                if (color == null)
                {
                    responseDTO.SetBadInput($"A cor com id: {id} não existe!");
                    return responseDTO;
                }
                colorRepository.Delete(color);
                await colorRepository.SaveChangesAsync();
                Log.Information("Cor removida id: {id}", color.Id);

                responseDTO.Object = color;
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
                responseDTO.Object = await colorRepository.GetEntities().ToListAsync();
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }
            return responseDTO;
        }
    }
}
