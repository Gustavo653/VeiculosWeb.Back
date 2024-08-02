using VeiculosWeb.Domain.CarSpecification;
using VeiculosWeb.Infrastructure.Repository;
using VeiculosWeb.Persistence;

namespace VeiculosWeb.DataAccess
{
    public class GearboxRepository(VeiculosWebContext context) : BaseRepository<Gearbox, VeiculosWebContext>(context), IGearboxRepository
    {
    }
}
