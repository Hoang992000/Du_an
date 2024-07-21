namespace TicketManagement.Models
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Pass { get; set; }
        public int RoleId { get; set; }
        public LocationModel Location { get; set; }
        public string FullName { get; set; }
        public bool Status { get; set; }
    }
    public class PageUserModel : UserModel
    {
        public int STT { get; set; }
        public string RoleName { get; set; }
        public string LocationName { get; set; }
    }
}
