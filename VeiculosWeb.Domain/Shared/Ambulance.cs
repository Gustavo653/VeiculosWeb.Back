namespace VeiculosWeb.Domain.Shared
{
    public class Ambulance : TenantBaseEntity
    {
        public required int Number { get; set; }
        public required string LicensePlate { get; set; }
    }
}
