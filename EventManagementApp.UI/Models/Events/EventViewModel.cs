namespace EventManagementApp.UI.Models.Events
{
    public class EventViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public DateTime Time { get; set; }
        public bool IsFree { get; set; }
        public decimal? Price { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string EventType { get; set; }
    }
}
