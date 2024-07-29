using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace VeiculosWeb.DTO.Driver
{
    public class DriverChecklistReviewDTO
    {
        [Required]
        public bool IsFullReview { get; set; }
        public string? Observation { get; set; }
        [Required]
        public Guid IdAmbulance { get; set; }
        [Required]
        public Guid IdChecklist { get; set; }
        [Required]
        public virtual required IList<DriverCategoryReviewDTO> Categories { get; set; }
        public Guid? IdUser { get; set; }
    }

    public class DriverCategoryReviewDTO
    {
        [Required]
        public required Guid Id { get; set; }
        [Required]
        public virtual required IEnumerable<DriverItemReviewDTO> Items { get; set; }
    }

    public class DriverItemReviewDTO
    {
        [Required]
        public required Guid Id { get; set; }
        [Required]
        public required Guid IdOption { get; set; }
        [Required]
        public required List<IFormFile> Medias { get; set; }
    }
}