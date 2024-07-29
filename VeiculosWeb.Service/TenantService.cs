using VeiculosWeb.Domain.Base;
using VeiculosWeb.DTO.Base;
using VeiculosWeb.Infrastructure.Repository;
using VeiculosWeb.Infrastructure.Service;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace VeiculosWeb.Service
{
    public class TenantService(ITenantRepository tenantRepository) : ITenantService
    {
        public async Task<ResponseDTO> Create(BasicDTO nameDTO)
        {
            ResponseDTO responseDTO = new();
            try
            {
                var tenant = new Tenant
                {
                    Name = nameDTO.Name,
                };

                tenant.SetCreatedAt();
                await tenantRepository.InsertAsync(tenant);
                await tenantRepository.SaveChangesAsync();
                Log.Information("Tenant persistido id: {id}", tenant.Id);

                responseDTO.Object = tenant;
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }
            return responseDTO;
        }

        public async Task<ResponseDTO> Update(Guid id, BasicDTO nameDTO)
        {
            ResponseDTO responseDTO = new();
            try
            {
                var tenant = await tenantRepository.GetTrackedEntities().FirstOrDefaultAsync(c => c.Id == id);
                if (tenant == null)
                {
                    responseDTO.SetBadInput($"O tenant com id: {id} não existe!");
                    return responseDTO;
                }
                tenant.Name = nameDTO.Name;
                tenant.SetUpdatedAt();
                await tenantRepository.SaveChangesAsync();
                Log.Information("Tenant persistido id: {id}", tenant.Id);

                responseDTO.Object = tenant;
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
                var tenant = await tenantRepository.GetTrackedEntities().FirstOrDefaultAsync(c => c.Id == id);
                if (tenant == null)
                {
                    responseDTO.SetBadInput($"O tenant com id: {id} não existe!");
                    return responseDTO;
                }
                tenantRepository.Delete(tenant);
                await tenantRepository.SaveChangesAsync();
                Log.Information("Tenant removido id: {id}", tenant.Id);

                responseDTO.Object = tenant;
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
                responseDTO.Object = await tenantRepository.GetEntities().ToListAsync();
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }
            return responseDTO;
        }
    }
}