using System.ComponentModel.DataAnnotations;

namespace EventManagementWebApi.Models
{
    public class EventDto
    {
        [Required]
        public string Title { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool AllDay { get; set; }
    }
}
