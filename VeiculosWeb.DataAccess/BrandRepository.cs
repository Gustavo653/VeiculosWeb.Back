using VeiculosWeb.Domain.Shared;
using VeiculosWeb.Infrastructure.Repository;
using VeiculosWeb.Persistence;
using Microsoft.AspNetCore.Http;
using VeiculosWeb.Domain.CarSpecification;

namespace VeiculosWeb.DataAccess
{
    public class BrandRepository(VeiculosWebContext context) : BaseRepository<Brand, VeiculosWebContext>(context), IBrandRepository
    {
    }
}
