using VeiculosWeb.Domain.VehicleSpecification;
using VeiculosWeb.Infrastructure.Repository;
using VeiculosWeb.Persistence;

namespace VeiculosWeb.DataAccess
{
    public class FeatureRepository(VeiculosWebContext context) : BaseRepository<Feature, VeiculosWebContext>(context), IFeatureRepository
    {
    }
}
