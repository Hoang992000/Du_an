namespace TicketManagement.Models
{
    internal class LoginRequest
    {
        public string UserName { get; set; }
        public string Pass { get; set; }
        public int LocationId { get; set; }
    }
}
