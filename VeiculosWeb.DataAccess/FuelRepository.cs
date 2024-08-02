using VeiculosWeb.Domain.VehicleSpecification;
using VeiculosWeb.Infrastructure.Repository;
using VeiculosWeb.Persistence;

namespace VeiculosWeb.DataAccess
{
    public class FuelRepository(VeiculosWebContext context) : BaseRepository<Fuel, VeiculosWebContext>(context), IFuelRepository
    {
    }
}
