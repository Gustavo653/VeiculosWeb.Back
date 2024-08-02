using VeiculosWeb.Domain.Location;
using VeiculosWeb.Infrastructure.Repository;
using VeiculosWeb.Persistence;

namespace VeiculosWeb.DataAccess
{
    public class StateRepository(VeiculosWebContext context) : BaseRepository<State, VeiculosWebContext>(context), IStateRepository
    {
    }
}
