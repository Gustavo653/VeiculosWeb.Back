using VeiculosWeb.Domain.Shared;

namespace VeiculosWeb.Domain.Location;

public class State : BasicEntity
{
    public IList<City>? Cities { get; set; }
}