using VeiculosWeb.Domain.CarSpecification;
using VeiculosWeb.Infrastructure.Repository;
using VeiculosWeb.Persistence;

namespace VeiculosWeb.DataAccess
{
    public class ModelRepository(VeiculosWebContext context) : BaseRepository<Model, VeiculosWebContext>(context), IModelRepository
    {
    }
}
