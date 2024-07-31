using VeiculosWeb.Domain.Shared;

namespace VeiculosWeb.Domain.Location;

public class City : BasicEntity
{
    public required State State { get; set; }
}