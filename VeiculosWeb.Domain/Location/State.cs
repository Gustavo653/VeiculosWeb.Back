namespace VeiculosWeb.Domain.Shared;

public class State : BasicEntity
{
    public IList<City>? Cities { get; set; }
}