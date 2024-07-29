using VeiculosWeb.Domain.Paramedic;
using VeiculosWeb.DTO.Base;
using VeiculosWeb.Infrastructure.Repository.Paramedic;
using VeiculosWeb.Infrastructure.Service.Paramedic;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace VeiculosWeb.Service.Paramedic
{
    public class ParamedicCategoryService(IParamedicCategoryRepository paramedicCategoryRepository,
                                 IParamedicChecklistItemRepository paramedicChecklistItemRepository) : IParamedicCategoryService
    {
        public async Task<ResponseDTO> Create(BasicDTO basicDTO)
        {
            ResponseDTO responseDTO = new();
            try
            {
                var categoryExists = await paramedicCategoryRepository.GetEntities().AnyAsync(c => c.Name == basicDTO.Name);
                if (categoryExists)
                {
                    responseDTO.SetBadInput($"A categoria {basicDTO.Name} já existe!");
                    return responseDTO;
                }
                var category = new ParamedicCategory
                {
                    Name = basicDTO.Name,
                };
                category.SetCreatedAt();
                await paramedicCategoryRepository.InsertAsync(category);
                await paramedicCategoryRepository.SaveChangesAsync();
                Log.Information("Categoria persistida id: {id}", category.Id);

                responseDTO.Object = category;
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
                var category = await paramedicCategoryRepository.GetTrackedEntities().FirstOrDefaultAsync(c => c.Id == id);
                if (category == null)
                {
                    responseDTO.SetBadInput($"A categoria {basicDTO.Name} não existe!");
                    return responseDTO;
                }
                category.Name = basicDTO.Name;
                category.SetUpdatedAt();
                await paramedicCategoryRepository.SaveChangesAsync();
                Log.Information("Categoria persistida id: {id}", category.Id);

                responseDTO.Object = category;
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
                var checkListItemExists = await paramedicChecklistItemRepository.GetEntities().AnyAsync(c => c.ParamedicCategory.Id == id);
                if (checkListItemExists)
                {
                    responseDTO.SetBadInput("Não é possível apagar a categoria, já existe um item de checklist vinculado!");
                    return responseDTO;
                }

                var category = await paramedicCategoryRepository.GetTrackedEntities().FirstOrDefaultAsync(c => c.Id == id);
                if (category == null)
                {
                    responseDTO.SetBadInput($"A categoria com id: {id} não existe!");
                    return responseDTO;
                }

                paramedicCategoryRepository.Delete(category);
                await paramedicCategoryRepository.SaveChangesAsync();
                Log.Information("Categoria removida id: {id}", category.Id);

                responseDTO.Object = category;
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
                responseDTO.Object = await paramedicCategoryRepository.GetEntities().ToListAsync();
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }
            return responseDTO;
        }
    }
}