using System.ComponentModel.DataAnnotations;

namespace EventManagerBlazorApp.Models
{
    public class EventDto
    {
        [Required]
        public string Title { get; set; }
        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;
        [Required]
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime? EndDate { get; set; }
        public bool AllDay { get; set; }
    }
}
