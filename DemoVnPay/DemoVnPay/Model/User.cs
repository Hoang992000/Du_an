using System.ComponentModel.DataAnnotations.Schema;

namespace TicketManagement.Model
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Pass { get; set; }
        [ForeignKey("Role")]
        public int RoleId { get; set; }
        public virtual Role? Role { get; set; }
        [ForeignKey("Location")]
        public int? LocationId { get; set; }
        public virtual Location? Location { get; set; }
        public bool? IsEnable { get; set; }

    }
}
