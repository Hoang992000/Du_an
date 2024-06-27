namespace TicketManagement.DTO
{
    public class UserRequest
    {
        public string UserName { get; set; }
        public string Pass { get; set; }
        public int? LocationId { get; set; }
    }
}
