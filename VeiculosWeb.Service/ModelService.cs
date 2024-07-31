using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Serilog;
using VeiculosWeb.Domain.CarSpecification;
using VeiculosWeb.DTO.Base;
using VeiculosWeb.Infrastructure.Repository;
using VeiculosWeb.Infrastructure.Service;
using VeiculosWeb.Utils;

namespace VeiculosWeb.Service
{
    public class ModelService(IBrandRepository brandRepository, IModelRepository modelRepository) : IModelService
    {
        private const string UrlAPIFIPE = $"{Consts.UrlBaseAPIFipe}carros/marcas";

        public async Task<ResponseDTO> GetList()
        {
            ResponseDTO responseDTO = new();
            try
            {
                responseDTO.Object = await modelRepository
                    .GetEntities()
                    .Include(x => x.Brand)
                    .OrderBy(x => x.Name)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }
            return responseDTO;
        }

        public async Task<ResponseDTO> SyncModels()
        {
            ResponseDTO responseDTO = new();
            try
            {
                Log.Information("Buscando marcas do banco");
                var brands = await brandRepository.GetTrackedEntities().ToListAsync();
                if (brands.Count == 0)
                {
                    responseDTO.SetBadInput($"Não há nenhuma marca cadastrada");
                    return responseDTO;
                }

                foreach (var brand in brands)
                {
                    var apiModels = await GetAPIModels(brand.Code);
                    Log.Information($"Foram encontrados {apiModels.Count} modelos na API");

                    Log.Information($"Buscando modelos do banco para a marca {brand.Name}");
                    var repositoryModels = await modelRepository.GetEntities().Where(x => x.Brand == brand).ToListAsync();
                    Log.Information($"Foram encontrados {repositoryModels.Count} modelos no banco para a marca {brand.Name}");
                    var repositoryModelsDict = repositoryModels.ToDictionary(b => b.Code);
                    
                    Log.Information("Validando os modelos");
                    foreach (var apiModel in apiModels)
                    {
                        if (repositoryModelsDict.TryGetValue(apiModel.Code, out var repositoryModel))
                        {
                            if (repositoryModel.Name != apiModel.Name)
                            {
                                Log.Warning($"Atualizando o modelo {repositoryModel.Code} de {repositoryModel.Name} para {apiModel.Name}");
                                repositoryModel.Name = apiModel.Name;
                                modelRepository.Update(repositoryModel);
                            }
                        }
                        else
                        {
                            Log.Warning($"Criando o modelo {apiModel.Name}");
                            await modelRepository.InsertAsync(new Model() { Name = apiModel.Name, Code = apiModel.Code, Brand = brand});
                        }
                    }
                    await modelRepository.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }
            return responseDTO;
        }


        private async Task<List<(int Code, string Name)>> GetAPIModels(int brand)
        {
            using HttpClient client = new();
            string response = await client.GetStringAsync($"{UrlAPIFIPE}/{brand}/modelos");

            var models = JsonConvert.DeserializeObject<ModelsResponseDTO>(response) ?? throw new NullReferenceException($"Não foi encontrado nenhum modelo");
            
            return models.Modelos.Select(b => (
                Code: b.Codigo,
                Name: b.Nome
            )).ToList();
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
