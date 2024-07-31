using Microsoft.EntityFrameworkCore;
using Serilog;
using VeiculosWeb.Domain.CarSpecification;
using VeiculosWeb.DTO;
using VeiculosWeb.DTO.Base;
using VeiculosWeb.Infrastructure.Repository;
using VeiculosWeb.Infrastructure.Service;

namespace VeiculosWeb.Service
{
    public class FuelService(IFuelRepository fuelRepository) : IFuelService
    {
        public async Task<ResponseDTO> Create(FuelDTO fuelDTO)
        {
            ResponseDTO responseDTO = new();
            try
            {
                var fuelExists = await fuelRepository.GetEntities().AnyAsync(c => c.Name == fuelDTO.Name);
                if (fuelExists)
                {
                    responseDTO.SetBadInput($"O combustível {fuelDTO.Name} já existe!");
                    return responseDTO;
                }

                var fuel = new Fuel()
                {
                    Name = fuelDTO.Name,
                };
                await fuelRepository.InsertAsync(fuel);

                await fuelRepository.SaveChangesAsync();
                Log.Information("Combustível persistido id: {id}", fuel.Id);

                responseDTO.Object = fuel;
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }
            return responseDTO;
        }

        public async Task<ResponseDTO> Update(Guid id, FuelDTO fuelDTO)
        {
            ResponseDTO responseDTO = new();
            try
            {
                var fuel = await fuelRepository.GetTrackedEntities().FirstOrDefaultAsync(c => c.Id == id);
                if (fuel == null)
                {
                    responseDTO.SetBadInput($"O combustível {fuelDTO.Name} não existe!");
                    return responseDTO;
                }

                fuel.Name = fuelDTO.Name;
                fuel.SetUpdatedAt();

                await fuelRepository.SaveChangesAsync();
                Log.Information("Combustível persistido id: {id}", fuel.Id);

                responseDTO.Object = fuel;
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
                var fuel = await fuelRepository.GetTrackedEntities().FirstOrDefaultAsync(c => c.Id == id);
                if (fuel == null)
                {
                    responseDTO.SetBadInput($"O combustível com id: {id} não existe!");
                    return responseDTO;
                }
                fuelRepository.Delete(fuel);
                await fuelRepository.SaveChangesAsync();
                Log.Information("Combustível removido id: {id}", fuel.Id);

                responseDTO.Object = fuel;
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
                responseDTO.Object = await fuelRepository.GetEntities().ToListAsync();
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }
            return responseDTO;
        }
    }
}
