using VeiculosWeb.Domain.Location;
using VeiculosWeb.Infrastructure.Repository;
using VeiculosWeb.Persistence;

namespace VeiculosWeb.DataAccess
{
    public class CityRepository(VeiculosWebContext context) : BaseRepository<City, VeiculosWebContext>(context), ICityRepository
    {
    }
}
