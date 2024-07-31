using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Serilog;
using VeiculosWeb.Domain.CarSpecification;
using VeiculosWeb.Domain.Enum;
using VeiculosWeb.DTO.Base;
using VeiculosWeb.Infrastructure.Repository;
using VeiculosWeb.Infrastructure.Service;
using VeiculosWeb.Utils;

namespace VeiculosWeb.Service
{
    public class BrandService(IBrandRepository brandRepository) : IBrandService
    {
        public async Task<ResponseDTO> GetBrands(VehicleType vehicleType)
        {
            ResponseDTO responseDTO = new();
            try
            {
                responseDTO.Object = await brandRepository
                    .GetEntities()
                    .Where(x => x.VehicleType == vehicleType)
                    .Select(x => new
                    {
                        x.Id,
                        x.Code,
                        x.Name,
                        ModelsCount = x.Models.Count,
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

        public Task<ResponseDTO> SyncBrands()
        {
            ResponseDTO responseDTO = new();
            try
            {
                Log.Information("Processando motos");
                Hangfire.BackgroundJob.Enqueue(() => CreateOrUpdateBrands(VehicleType.Bike));
                Log.Information("Processando carros");
                Hangfire.BackgroundJob.Enqueue(() => CreateOrUpdateBrands(VehicleType.Car));
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }

            return Task.FromResult(responseDTO);
        }

        private async Task CreateOrUpdateBrands(VehicleType vehicleType)
        {
            Log.Information("Buscando marcas da API");
            var apiBrands = await GetAPIBrands(vehicleType);
            Log.Information($"Foram encontradas {apiBrands.Count} marcas na API");

            Log.Information("Buscando marcas do banco");
            var repositoryBrands = await brandRepository.GetEntities().ToListAsync();
            Log.Information($"Foram encontradas {repositoryBrands.Count} marcas no banco");
            var repositoryBrandsDict = repositoryBrands.ToDictionary(b => b.Code);

            Log.Information("Validando as marcas");
            foreach (var apiBrand in apiBrands)
            {
                if (repositoryBrandsDict.TryGetValue(apiBrand.Code, out var repositoryBrand))
                {
                    if (repositoryBrand.Name != apiBrand.Name)
                    {
                        Log.Warning($"Atualizando a marca {repositoryBrand.Code} de {repositoryBrand.Name} para {apiBrand.Name}");
                        repositoryBrand.Name = apiBrand.Name;
                        brandRepository.Update(repositoryBrand);
                    }
                }
                else
                {
                    Log.Warning($"Criando a marca {apiBrand.Name}");
                    await brandRepository.InsertAsync(new Brand { Name = apiBrand.Name, Code = apiBrand.Code, VehicleType = vehicleType});
                }
            }

            await brandRepository.SaveChangesAsync();
        }

        private async Task<List<(int Code, string Name, VehicleType Type)>> GetAPIBrands(VehicleType vehicleType)
        {
            using HttpClient client = new();
            var url = $"{Consts.UrlBaseAPIFipe}{(vehicleType == VehicleType.Bike ? "motos" : "carros")}/marcas";
            string response = await client.GetStringAsync(url);

            var brands = JsonConvert.DeserializeObject<IEnumerable<BrandDTO>>(response) ?? 
                         throw new NullReferenceException($"Não foi encontrada nenhuma marca");

            return brands.Select(b => (
                Code: b.Codigo,
                Name: b.Nome,
                Type: vehicleType
            )).ToList();
        }

        private record BrandDTO
        {
            public int Codigo { get; set; }
            public string Nome { get; set; }
        }
    }
}