using VeiculosWeb.DTO.Base;
using System.ComponentModel.DataAnnotations;

namespace VeiculosWeb.DTO.Paramedic
{
    public class ParamedicChecklistDTO : BasicDTO
    {
        [Required]
        public required bool RequireFullReview { get; set; }
        [Required]
        public virtual required IEnumerable<ParamedicCategoryDTO> Categories { get; set; }
    }

    public class ParamedicCategoryDTO
    {
        [Required]
        public required Guid Id { get; set; }
        [Required]
        public virtual required IEnumerable<ParamedicItemDTO> Items { get; set; }
    }

    public class ParamedicItemDTO
    {
        [Required]
        public required Guid Id { get; set; }
        [Required]
        public required int AmountRequired { get; set; }
        public List<ParamedicItemDTO>? SubItems { get; set; }
    }
}