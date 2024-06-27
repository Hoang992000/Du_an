using DemoVnPay.Model;
using Microsoft.EntityFrameworkCore;
using TicketManagement.Model;

namespace TicketManagement.Repository
{
    public class LocationReposiory
    {
        private DataContext _context;
        private IConfiguration _configuration;
        public LocationReposiory(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public Location AddLocationInfo(Location loca)
        {
            var check = _context.Locations.FirstOrDefault(x => x.Locationname == loca.Locationname);
            if (check != null)
            {
                return null;
            }
            _context.Locations.Add(loca);
            _context.SaveChanges();
            return loca;
        }
        public async Task<List<Location>> GetLocationForUser()
        {
            var res = await _context.Locations.Where(x => x.IsActive == false).ToListAsync();
            return res;
        }
        public async Task<List<Location>> GetLocationForAdmin(int size, int num)
        {
            var res = await _context.Locations.Skip((num - 1) * size).Take(size).ToListAsync();
            return res;
        }
        public Location UpdateIfo(Location Loca)
        {
            var info = _context.Locations.Find(Loca.LocationId);
            if (info == null)
            {
                return null;
            }
            info.Locationname = Loca.Locationname;
            _context.Locations.Update(info);
            _context.SaveChanges();
            return info;
        }
        public string DeleteInfo(int id)
        {
            try
            {
                var us = _context.Locations.Find(id);
                _context.Locations.Remove(us);
                _context.SaveChanges();
                return "Success";
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra");
            }
        }
        public async Task<List<Location>> find(string key, int size, int num)
        {
            List<Location> res = await _context.Locations.Where(x => x.Locationname.Contains(key)).Skip((num - 1) * size).Take(size).ToListAsync();
            return res;
        }
        public async Task<int> CoutData()
        {
            int result = await _context.Locations.CountAsync();
            return result;
        }
        public async Task<List<Location>> getlstlocation()
        {
            var result = await _context.Locations.Where(x => x.IsActive == false).ToListAsync();
            return result;
        }
    }
}
