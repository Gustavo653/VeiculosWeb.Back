using VeiculosWeb.Domain.Shared;

namespace VeiculosWeb.Domain.Paramedic
{
    public class ParamedicChecklistItem : TenantBaseEntity
    {
        public required int RequiredQuantity { get; set; }
        public Guid ParamedicItemId { get; set; }
        public virtual required ParamedicItem ParamedicItem { get; set; }
        public Guid ParamedicCategoryId { get; set; }
        public virtual required ParamedicCategory ParamedicCategory { get; set; }
        public Guid ParamedicChecklistId { get; set; }
        public virtual required ParamedicChecklist ParamedicChecklist { get; set; }
        public Guid? ParamedicParentChecklistItemId { get; set; }
        public virtual ParamedicChecklistItem? ParamedicParentChecklistItem { get; set; }
        public virtual List<ParamedicChecklistItem>? ParamedicChecklistSubItems { get; set; }
        public virtual List<ParamedicChecklistReplacedItem>? ParamedicChecklistReplacedItems { get; set; }
    }
}
