using Microsoft.EntityFrameworkCore;
using Serilog;
using VeiculosWeb.Domain.Enum;
using VeiculosWeb.Domain.VehicleSpecification;
using VeiculosWeb.DTO;
using VeiculosWeb.DTO.Base;
using VeiculosWeb.Infrastructure.Repository;
using VeiculosWeb.Infrastructure.Service;

namespace VeiculosWeb.Service
{
    public class FeatureService(IFeatureRepository featureRepository) : IFeatureService
    {
        public async Task<ResponseDTO> Create(FeatureDTO featureDTO)
        {
            ResponseDTO responseDTO = new();
            try
            {
                var featureExists = await featureRepository.GetEntities().AnyAsync(c => c.Name == featureDTO.Name);
                if (featureExists)
                {
                    responseDTO.SetBadInput($"O opcional {featureDTO.Name} já existe!");
                    return responseDTO;
                }

                var feature = new Feature()
                {
                    Name = featureDTO.Name,
                    VehicleType = featureDTO.VehicleType
                };
                await featureRepository.InsertAsync(feature);

                await featureRepository.SaveChangesAsync();
                Log.Information("Opcional persistido id: {id}", feature.Id);

                responseDTO.Object = feature;
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }
            return responseDTO;
        }

        public async Task<ResponseDTO> Update(Guid id, FeatureDTO featureDTO)
        {
            ResponseDTO responseDTO = new();
            try
            {
                var feature = await featureRepository.GetTrackedEntities().FirstOrDefaultAsync(c => c.Id == id);
                if (feature == null)
                {
                    responseDTO.SetBadInput($"O opcional {featureDTO.Name} não existe!");
                    return responseDTO;
                }

                feature.Name = featureDTO.Name;
                feature.VehicleType = featureDTO.VehicleType;
                feature.SetUpdatedAt();

                await featureRepository.SaveChangesAsync();
                Log.Information("Opcional persistido id: {id}", feature.Id);

                responseDTO.Object = feature;
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
                var feature = await featureRepository.GetTrackedEntities().FirstOrDefaultAsync(c => c.Id == id);
                if (feature == null)
                {
                    responseDTO.SetBadInput($"O opcional com id: {id} não existe!");
                    return responseDTO;
                }
                featureRepository.Delete(feature);
                await featureRepository.SaveChangesAsync();
                Log.Information("Opcional removido id: {id}", feature.Id);

                responseDTO.Object = feature;
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

        public async Task<ResponseDTO> GetFeaturesByVehicleType(VehicleType vehicleType)
        {
            ResponseDTO responseDTO = new();
            try
            {
                responseDTO.Object = await featureRepository.GetEntities().Where(x => x.VehicleType == vehicleType).ToListAsync();
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }
            return responseDTO;
        }
    }
}
