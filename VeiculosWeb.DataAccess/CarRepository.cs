using VeiculosWeb.Domain.Location;
using VeiculosWeb.Domain.Vehicles;
using VeiculosWeb.Infrastructure.Repository;
using VeiculosWeb.Persistence;

namespace VeiculosWeb.DataAccess
{
    public class CarRepository(VeiculosWebContext context) : BaseRepository<Car, VeiculosWebContext>(context), ICarRepository
    {
    }
}
