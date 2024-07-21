namespace TicketManagement.Models
{
    public class LoginRespon
    {
        public int UserId { get; set; }
        public string Token { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsBoss { get; set; }
        public string Message { get; set; }
    }
}
