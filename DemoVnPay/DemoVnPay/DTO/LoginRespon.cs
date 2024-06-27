namespace TicketManagement.DTO
{
    public class LoginRespon
    {
        public int UserId { get; set; }
        public string Token { get; set; }
        public bool IsAdmin { get; set; }
        public string Message { get; set; }
    }
}
