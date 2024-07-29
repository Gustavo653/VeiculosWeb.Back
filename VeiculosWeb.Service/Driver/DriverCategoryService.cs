using VeiculosWeb.Domain.Driver;
using VeiculosWeb.DTO.Base;
using VeiculosWeb.Infrastructure.Repository.Driver;
using VeiculosWeb.Infrastructure.Service.Driver;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace VeiculosWeb.Service.Driver
{
    public class DriverCategoryService(IDriverCategoryRepository driverCategoryRepository,
                                 IDriverChecklistItemRepository driverChecklistItemRepository) : IDriverCategoryService
    {
        public async Task<ResponseDTO> Create(BasicDTO basicDTO)
        {
            ResponseDTO responseDTO = new();
            try
            {
                var categoryExists = await driverCategoryRepository.GetEntities().AnyAsync(c => c.Name == basicDTO.Name);
                if (categoryExists)
                {
                    responseDTO.SetBadInput($"A categoria {basicDTO.Name} já existe!");
                    return responseDTO;
                }
                var category = new DriverCategory
                {
                    Name = basicDTO.Name,
                };
                category.SetCreatedAt();
                await driverCategoryRepository.InsertAsync(category);
                await driverCategoryRepository.SaveChangesAsync();
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
                var category = await driverCategoryRepository.GetTrackedEntities().FirstOrDefaultAsync(c => c.Id == id);
                if (category == null)
                {
                    responseDTO.SetBadInput($"A categoria {basicDTO.Name} não existe!");
                    return responseDTO;
                }
                category.Name = basicDTO.Name;
                category.SetUpdatedAt();
                await driverCategoryRepository.SaveChangesAsync();
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
                var checkListItemExists = await driverChecklistItemRepository.GetEntities().AnyAsync(c => c.DriverCategory.Id == id);
                if (checkListItemExists)
                {
                    responseDTO.SetBadInput("Não é possível apagar a categoria, já existe um item de checklist vinculado!");
                    return responseDTO;
                }

                var category = await driverCategoryRepository.GetTrackedEntities().FirstOrDefaultAsync(c => c.Id == id);
                if (category == null)
                {
                    responseDTO.SetBadInput($"A categoria com id: {id} não existe!");
                    return responseDTO;
                }

                driverCategoryRepository.Delete(category);
                await driverCategoryRepository.SaveChangesAsync();
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
                responseDTO.Object = await driverCategoryRepository.GetEntities().ToListAsync();
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }
            return responseDTO;
        }
    }
}