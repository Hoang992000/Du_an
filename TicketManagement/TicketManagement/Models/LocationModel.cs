namespace TicketManagement.Models
{
    public class LocationModel
    {
        public int LocationId { get; set; }
        public string Locationname { get; set; }
        public bool? IsActive { get; set; }
    }
    public class LocationModelPage : LocationModel
    {
        public int STT { get; set; }
        public string Status { get; set; }
    }
}
