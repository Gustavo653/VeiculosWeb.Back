using VeiculosWeb.Domain.Base;

namespace VeiculosWeb.Domain.Shared;

public class City : BasicEntity
{
    public required State State { get; set; }
}