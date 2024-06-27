using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TicketManagement.Model;

namespace DemoVnPay.Model
{
    public class Ticket
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string TicketCode { get; set; }
        public string VehicleNum { get; set; }
        [ForeignKey("Vehicle")]
        public int VehicleId { get; set; }
        public virtual Vehicle? Vehicle { get; set; }
        [ForeignKey("Method")]
        public int PaymentMethodId { get; set; }
        public virtual PaymentMethod? Method { get; set; }
        [ForeignKey("Location")]
        public int LocationId { get; set; }
        public virtual Location? Location { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User? User { get; set; }
        public DateTime TimeIn { get; set; }
        public DateTime? TimeOut { get; set; }
        public bool StatusOder { get; set; }
        public DateTime? PaymentTime { get; set; }
    }
}
