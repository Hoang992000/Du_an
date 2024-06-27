using DemoVnPay.Model;

namespace TicketManagement.Repository
{
    public class VehicleRepository
    {
        private DataContext _context;
        private IConfiguration _configuration;
        public VehicleRepository(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public Vehicle AddV(Vehicle loca)
        {
            _context.Vehicles.Add(loca);
            _context.SaveChanges();
            return loca;
        }
        public List<Vehicle> GetV()
        {
            var res = _context.Vehicles.ToList();
            return res;
        }
        public Vehicle UpdateV(Vehicle Loca)
        {
            var info = _context.Vehicles.Find(Loca.VehicleId);
            if (info == null)
            {
                return new Vehicle();
            }
            info.VehicleName = Loca.VehicleName;
            info.Cost = Loca.Cost;
            _context.Vehicles.Update(info);
            _context.SaveChanges();
            return info;
        }
        public string DeleteV(int id)
        {
            try
            {
                var us = _context.Vehicles.Find(id);
                _context.Vehicles.Remove(us);
                _context.SaveChanges();
                return "Success";
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra");
            }
        }
    }
}
