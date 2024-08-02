using VeiculosWeb.Domain.CarSpecification;
using VeiculosWeb.Infrastructure.Repository;
using VeiculosWeb.Persistence;

namespace VeiculosWeb.DataAccess
{
    public class ColorRepository(VeiculosWebContext context) : BaseRepository<Color, VeiculosWebContext>(context), IColorRepository
    {
    }
}
