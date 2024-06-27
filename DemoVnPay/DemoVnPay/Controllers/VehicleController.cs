using DemoVnPay.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketManagement.Repository;

namespace TicketManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehicleController : Controller
    {
        private VehicleRepository repo;
        public VehicleController(VehicleRepository context)
        {
            repo = context;
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("add")]
        public Vehicle add([FromBody] Vehicle loca)
        {
            var res = repo.AddV(loca);
            return res;
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("update")]
        public Vehicle Update([FromBody] Vehicle user)
        {
            var res = repo.UpdateV(user);
            return res;
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("del")]
        public string delete(int id)
        {
            var res = repo.DeleteV(id);
            return res;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("getAll")]
        public List<Vehicle> getAll()
        {
            var res = repo.GetV();
            return res;
        }
    }
}
