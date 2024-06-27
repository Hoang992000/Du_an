using DemoVnPay.DTO;
using DemoVnPay.Model;
using DemoVnPay.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketManagement.DTO;

namespace DemoVnPay.Controllers
{
    [Route("api/[controller]")]
    public class TicketController : Controller
    {
        TicketRepository ticketRepo;
        public TicketController(TicketRepository repo)
        {
            ticketRepo = repo;
        }
        [Authorize]
        [HttpPost("CreateTicket")]
        public async Task<ActionResult> createTicket([FromBody] TicketRequest value)
        {
            Ticket res = await ticketRepo.AddTicket(value);
            if (res != null)
            {
                return Ok(res);
            }

            return BadRequest("somethings went wrong");
        }
        [Authorize]
        [HttpGet("checkTicket")]
        public async Task<Ticket> Check([FromQuery] string code, [FromQuery] bool cash)
        {
            Ticket res = await ticketRepo.CheckTicketAsync(code, cash);
            return res;
        }
        //[Authorize(Roles = "Admin")]
        [HttpGet("report")]
        public async Task<IActionResult> Report([FromQuery] int size, [FromQuery] int num, [FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            IQueryable<TicketRespon> data = await ticketRepo.GetReportTicket(size, num, from, to);
            //var res = PaginationHelper.GetPagedData<TicketRespon>(data, page, pageSize);
            return Ok(data);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("findTicket")]
        public async Task<IActionResult> Find([FromQuery] string key)
        {
            IQueryable<TicketRespon> data = await ticketRepo.FindTicketByNum(key);
            return Ok(data);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("getHomeData")]
        public async Task<List<ReportModel>> gethomedata()
        {
            return await ticketRepo.GetHomeData();
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("getTotalTicketToday")]
        public async Task<int> gettotaltickettoday()
        {
            int result = await ticketRepo.GetTotalTicketToday();
            return result;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("getTotalLocationToday")]
        public async Task<int> gettotallocationtoday()
        {
            return await ticketRepo.GetLoactionToday();
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("coutRow")]
        public async Task<int> Cout()
        {
            var result = await ticketRepo.CoutData();
            return result;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("reportByTime")]
        public async Task<List<TicketRespon>> getReportByLoca(int locaid, int vehiid, DateTime from, DateTime to)
        {
            var result = await ticketRepo.GetReportTicke(locaid, vehiid, from, to);
            return result;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("Revenue")]
        public async Task<int> getReportByVehi(int locaid, int vehiid, DateTime from, DateTime to)
        {
            var result = await ticketRepo.GetRevenue(locaid, vehiid, from, to);
            return result;
        }
    }
}
