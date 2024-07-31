using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Serilog;
using VeiculosWeb.Domain.CarSpecification;
using VeiculosWeb.DTO.Base;
using VeiculosWeb.Infrastructure.Repository;
using VeiculosWeb.Infrastructure.Service;

namespace VeiculosWeb.Service
{
    public class BrandService(IBrandRepository brandRepository) : IBrandService
    {
        private const string UrlAPIFIPE = "https://parallelum.com.br/fipe/api/v1/carros/marcas";

        public async Task<ResponseDTO> GetList()
        {
            ResponseDTO responseDTO = new();
            try
            {
                responseDTO.Object = await brandRepository.GetEntities().OrderBy(x => x.Name).ToListAsync();
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }
            return responseDTO;
        }

        public async Task<ResponseDTO> SyncBrands()
        {
            ResponseDTO responseDTO = new();
            try
            {
                Log.Information("Buscando marcas da API");
                var apiBrands = await GetAPIBrands();
                Log.Information($"Foram encontradas {apiBrands.Count} marcas na API");
                var apiBrandsDict = apiBrands.ToDictionary(b => b.Code);

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
                            Log.Warning($"Atualizando a marca de {repositoryBrand.Name} para {apiBrand.Name}");
                            repositoryBrand.Name = apiBrand.Name;
                            brandRepository.Update(repositoryBrand);
                        }
                    }
                    else
                    {
                        Log.Warning($"Criando a marca {apiBrand.Name}");
                        brandRepository.Insert(new Brand() { Name = apiBrand.Name, Code = apiBrand.Code });
                    }
                }

                await brandRepository.SaveChangesAsync();
                responseDTO.Object = apiBrands;
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }
            return responseDTO;
        }


        private async Task<List<Brand>> GetAPIBrands()
        {
            using HttpClient client = new();
            string response = await client.GetStringAsync(UrlAPIFIPE);

            var brands = JsonConvert.DeserializeObject<IEnumerable<dynamic>>(response) ?? throw new NullReferenceException($"Não foi encontrada nenhuma marca");

            return brands.Select(b => new Brand
            {
                Code = b.codigo,
                Name = b.nome
            }).ToList();
        }
    }
}
