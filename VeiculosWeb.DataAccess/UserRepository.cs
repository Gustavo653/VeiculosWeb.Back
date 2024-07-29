using VeiculosWeb.Domain.Base;
using VeiculosWeb.Infrastructure.Repository;
using VeiculosWeb.Persistence;

namespace VeiculosWeb.DataAccess
{
    public class UserRepository(VeiculosWebContext context) : BaseRepository<User, VeiculosWebContext>(context), IUserRepository;
}
