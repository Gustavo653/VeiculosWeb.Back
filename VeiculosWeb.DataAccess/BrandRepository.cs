using VeiculosWeb.Domain.CarSpecification;
using VeiculosWeb.Infrastructure.Repository;
using VeiculosWeb.Persistence;

namespace VeiculosWeb.DataAccess
{
    public class BrandRepository(VeiculosWebContext context) : BaseRepository<Brand, VeiculosWebContext>(context), IBrandRepository
    {
    }
}
