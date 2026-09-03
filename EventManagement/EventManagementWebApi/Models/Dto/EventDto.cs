using System.ComponentModel.DataAnnotations;

namespace EventManagementWebApi.Models
{
    public class EventDto
    {
        //[Required(ErrorMessage = "Title is required")]
        [Required]
        public string Title { get; set; }
        //[MaxLength(500, ErrorMessage = "Description is can not be more than 500 charecters")]
        [MaxLength(500)]
        public string Description { get; set; }
       // [Required(ErrorMessage = "Start Date is required")]
        [Required]
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool AllDay { get; set; }
    }
}
