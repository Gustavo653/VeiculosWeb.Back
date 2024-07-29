using VeiculosWeb.Domain.Base;
using VeiculosWeb.Domain.Shared;

namespace VeiculosWeb.Domain.Paramedic
{
    public class ParamedicChecklistReview : TenantBaseEntity
    {
        public required bool IsFullReview { get; set; }
        public string? Observation { get; set; }
        public virtual required Ambulance Ambulance { get; set; }
        public virtual required ParamedicChecklist ParamedicChecklist { get; set; }
        public virtual required User User { get; set; }
        public List<ParamedicChecklistReplacedItem>? ParamedicChecklistReplacedItems { get; set; }
    }
}
