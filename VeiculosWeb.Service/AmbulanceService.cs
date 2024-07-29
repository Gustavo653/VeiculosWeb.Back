using VeiculosWeb.Domain.Shared;
using VeiculosWeb.DTO;
using VeiculosWeb.DTO.Base;
using VeiculosWeb.Infrastructure.Repository;
using VeiculosWeb.Infrastructure.Service;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace VeiculosWeb.Service
{
    public class AmbulanceService(IAmbulanceRepository ambulanceRepository) : IAmbulanceService
    {
        public async Task<ResponseDTO> Create(AmbulanceDTO ambulanceDTO)
        {
            ResponseDTO responseDTO = new();
            try
            {
                var ambulanceExists = await ambulanceRepository.GetEntities().AnyAsync(c => c.Number == ambulanceDTO.Number);
                if (ambulanceExists)
                {
                    responseDTO.SetBadInput($"A ambulância {ambulanceDTO.Number} já existe!");
                    return responseDTO;
                }

                var ambulance = new Ambulance
                {
                    Number = ambulanceDTO.Number,
                    LicensePlate = ambulanceDTO.LicensePlate,
                };
                ambulance.SetCreatedAt();
                await ambulanceRepository.InsertAsync(ambulance);

                await ambulanceRepository.SaveChangesAsync();
                Log.Information("Ambulância persistida id: {id}", ambulance.Id);

                responseDTO.Object = ambulance;
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }
            return responseDTO;
        }

        public async Task<ResponseDTO> Update(Guid id, AmbulanceDTO ambulanceDTO)
        {
            ResponseDTO responseDTO = new();
            try
            {
                var ambulance = await ambulanceRepository.GetTrackedEntities().FirstOrDefaultAsync(c => c.Id == id);
                if (ambulance == null)
                {
                    responseDTO.SetBadInput($"A ambulância {ambulanceDTO.Number} não existe!");
                    return responseDTO;
                }

                ambulance.Number = ambulanceDTO.Number;
                ambulance.LicensePlate = ambulanceDTO.LicensePlate;
                ambulance.SetUpdatedAt();

                await ambulanceRepository.SaveChangesAsync();
                Log.Information("Ambulância persistida id: {id}", ambulance.Id);

                responseDTO.Object = ambulance;
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
                var ambulance = await ambulanceRepository.GetTrackedEntities().FirstOrDefaultAsync(c => c.Id == id);
                if (ambulance == null)
                {
                    responseDTO.SetBadInput($"A ambulância com id: {id} não existe!");
                    return responseDTO;
                }
                ambulanceRepository.Delete(ambulance);
                await ambulanceRepository.SaveChangesAsync();
                Log.Information("Ambulância removida id: {id}", ambulance.Id);

                responseDTO.Object = ambulance;
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
                responseDTO.Object = await ambulanceRepository.GetEntities().ToListAsync();
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }
            return responseDTO;
        }
    }
}
