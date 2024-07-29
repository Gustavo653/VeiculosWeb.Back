using System.ComponentModel.DataAnnotations;

namespace VeiculosWeb.DTO.Paramedic
{
    public class ParamedicChecklistReviewDTO
    {
        [Required]
        public bool IsFullReview { get; set; }
        public string? Observation { get; set; }
        [Required]
        public Guid IdAmbulance { get; set; }
        [Required]
        public Guid IdChecklist { get; set; }
        [Required]
        public virtual required IList<ParamedicCategoryReviewDTO> Categories { get; set; }
        public Guid? IdUser { get; set; }
    }

    public class ParamedicCategoryReviewDTO
    {
        [Required]
        public required Guid Id { get; set; }
        [Required]
        public virtual required IEnumerable<ParamedicItemReviewDTO> Items { get; set; }
    }

    public class ParamedicItemReviewDTO
    {
        [Required]
        public required Guid Id { get; set; }
        [Required]
        public int ReplacedQuantity { get; set; }
        [Required]
        public int RequiredQuantity { get; set; }
        [Required]
        public int ReplenishmentQuantity { get; set; }
        public List<ParamedicItemReviewDTO>? SubItems { get; set; }
    }
}