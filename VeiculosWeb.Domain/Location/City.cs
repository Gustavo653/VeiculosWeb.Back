using VeiculosWeb.Domain.Shared;

namespace VeiculosWeb.Domain.Location;

public class City : BasicEntity
{
    public required int Code { get; set; }
    public required State State { get; set; }
}