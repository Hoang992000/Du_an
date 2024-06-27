using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketManagement.Model;
using TicketManagement.Repository;

namespace TicketManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationController : Controller
    {
        private LocationReposiory repo;
        public LocationController(LocationReposiory context)
        {
            repo = context;
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("add")]
        public Location addLocation([FromBody] Location loca)
        {
            var res = repo.AddLocationInfo(loca);
            return res;
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("update")]
        public Location UpdateLocation([FromBody] Location user)
        {
            var res = repo.UpdateIfo(user);
            return res;
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("del")]
        public string deleteLocation(int id)
        {
            var res = repo.DeleteInfo(id);
            return res;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("getAll")]
        public async Task<List<Location>> getAll(int size, int num)
        {
            var res = await repo.GetLocationForAdmin(size, num);
            return res;
        }
        [HttpGet("getLocation")]
        public async Task<List<Location>> getAllForUser()
        {
            var res = await repo.GetLocationForUser();
            return res;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("coutRow")]
        public async Task<int> Cout()
        {
            var result = await repo.CoutData();
            return result;
        }
        [HttpGet("find")]
        public async Task<List<Location>> Find(string key, int size, int num)
        {
            var result = await repo.find(key, size, num);
            return result;
        }
        [HttpGet("GetAllLocationForCBB")]
        public async Task<List<Location>> getlstLocation()
        {
            var result = await repo.getlstlocation();
            return result;
        }
    }
}
