using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Serilog;
using VeiculosWeb.Domain.Enum;
using VeiculosWeb.Domain.VehicleSpecification;
using VeiculosWeb.DTO.Base;
using VeiculosWeb.Infrastructure.Repository;
using VeiculosWeb.Infrastructure.Service;
using VeiculosWeb.Utils;

namespace VeiculosWeb.Service
{
    public class ModelService(IBrandRepository brandRepository, IModelRepository modelRepository) : IModelService
    {
        public async Task<ResponseDTO> GetModelsByBrand(VehicleType vehicleType, Guid brandId)
        {
            ResponseDTO responseDTO = new();
            try
            {
                responseDTO.Object = await modelRepository
                    .GetEntities()
                    .Where(x => x.BrandId == brandId && x.VehicleType == vehicleType)
                    .Select(x => new
                    {
                        x.Id,
                        x.Code,
                        x.Name,
                        x.CreatedAt,
                        x.UpdatedAt
                    })
                    .OrderBy(x => x.Name)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }
            return responseDTO;
        }

        public Task<ResponseDTO> SyncModels()
        {
            ResponseDTO responseDTO = new();
            try
            {
                Log.Information("Processando motos");
                Hangfire.BackgroundJob.Enqueue(() => CreateOrUpdateModels(VehicleType.Bike));
                Log.Information("Processando carros");
                Hangfire.BackgroundJob.Enqueue(() => CreateOrUpdateModels(VehicleType.Car));
                Log.Information("Processando motos");
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }
            return Task.FromResult(responseDTO);
        }

        public async Task CreateOrUpdateModels(VehicleType vehicleType)
        {
            Log.Information("Buscando marcas do banco");
            var brands = await brandRepository.GetTrackedEntities().Where(x => x.VehicleType == vehicleType).ToListAsync();
            foreach (var brand in brands)
            {
                var apiModels = await GetAPIModels(vehicleType, brand.Code);
                Log.Information($"Foram encontrados {apiModels.Count} modelos na API");

                Log.Information($"Buscando modelos do banco para a marca {brand.Name}");
                var repositoryModels = await modelRepository.GetEntities().Where(x => x.Brand == brand).ToListAsync();
                Log.Information($"Foram encontrados {repositoryModels.Count} modelos no banco para a marca {brand.Name}");
                var repositoryModelsDict = repositoryModels.ToDictionary(b => b.Code);

                Log.Information("Validando os modelos");
                foreach (var apiModel in apiModels)
                {
                    if (repositoryModelsDict.TryGetValue(apiModel.Codigo, out var repositoryModel))
                    {
                        if (repositoryModel.Name != apiModel.Nome)
                        {
                            Log.Warning($"Atualizando o modelo {repositoryModel.Code} de {repositoryModel.Name} para {apiModel.Nome}");
                            repositoryModel.Name = apiModel.Nome;
                            modelRepository.Update(repositoryModel);
                        }
                    }
                    else
                    {
                        Log.Warning($"Criando o modelo {apiModel.Nome}");
                        await modelRepository.InsertAsync(new Model() { Name = apiModel.Nome, Code = apiModel.Codigo, Brand = brand, VehicleType = vehicleType });
                    }
                }
                await modelRepository.SaveChangesAsync();
            }
        }

        private async Task<List<ModelDTO>> GetAPIModels(VehicleType vehicleType, int brandCode)
        {
            using HttpClient client = new();
            var url = $"{Consts.UrlBaseFipe}{(vehicleType == VehicleType.Bike ? "motos" : "carros")}/marcas/{brandCode}/modelos";

            string response = await client.GetStringAsync(url);
            var models = JsonConvert.DeserializeObject<ModelsResponseDTO>(response) ??
                         throw new NullReferenceException("Não foi encontrado nenhum modelo");

            return models.Modelos;
        }

        private class ModelDTO
        {
            public int Codigo { get; set; }
            public string Nome { get; set; }
        }

        private class ModelsResponseDTO
        {
            public List<ModelDTO> Modelos { get; set; }
        }
    }
}
