using VeiculosWeb.Domain.Shared;

namespace VeiculosWeb.Domain.Paramedic
{
    public class ParamedicChecklistReplacedItem : TenantBaseEntity
    {
        public required int ReplacedQuantity { get; set; } // Quantidade reposta
        public required int RequiredQuantity { get; set; } // Quantidade requirida
        public required int ReplenishmentQuantity { get; set; } // Quantidade a repor
        public Guid ParamedicChecklistReviewId { get; set; }
        public virtual required ParamedicChecklistReview ParamedicChecklistReview { get; set; }
        public Guid ParamedicChecklistItemId { get; set; }
        public virtual required ParamedicChecklistItem ParamedicChecklistItem { get; set; }
    }
}
