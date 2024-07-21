using System;

namespace TicketManagement.Models
{
    public class TicketModel
    {
        public string TicketCode { get; set; }
        public string VehicleNum { get; set; }
        public string VehicleType { get; set; }
        public string PaymentMethodType { get; set; }
        public DateTime TimeIn { get; set; }
        public DateTime? TimeOut { get; set; }
        public string LocationName { get; set; }
        public string StatusOder { get; set; }
        public DateTime? PaymentTime { get; set; }
        public int Cost { get; set; }
    }
    public class TicketModelSTT : TicketModel
    {
        public int STT { get; set; }

    }
}
