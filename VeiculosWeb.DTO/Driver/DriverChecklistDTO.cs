using VeiculosWeb.DTO.Base;
using System.ComponentModel.DataAnnotations;

namespace VeiculosWeb.DTO.Driver
{
    public class DriverChecklistDTO : BasicDTO
    {
        [Required]
        public required bool RequireFullReview { get; set; }
        [Required]
        public virtual required IEnumerable<DriverCategoryDTO> Categories { get; set; }
    }

    public class DriverCategoryDTO
    {
        [Required]
        public required Guid Id { get; set; }
        [Required]
        public virtual required IEnumerable<DriverItemDTO> Items { get; set; }
    }

    public class DriverItemDTO
    {
        [Required]
        public required Guid Id { get; set; }
        [Required]
        public required List<DriverOptionDTO> Options { get; set; }
    }

    public class DriverOptionDTO
    {
        [Required]
        public required string Name { get; set; }
        [Required]
        public bool RequireSomeAction { get; set; }
    }
}