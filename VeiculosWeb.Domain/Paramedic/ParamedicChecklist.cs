using VeiculosWeb.Domain.Shared;

namespace VeiculosWeb.Domain.Paramedic
{
    public class ParamedicChecklist : BasicEntity
    {
        public required bool RequireFullReview { get; set; }
        public virtual List<ParamedicChecklistItem>? ParamedicChecklistItems { get; set; }
    }
}
