namespace EventManagementWebApi.Models
{
    /// <summary>
    /// Step 2.1.5:Model Class for Event entity
    /// </summary>
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; } 
        public bool AllDay { get; set; }
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; } 
    }
    
}
