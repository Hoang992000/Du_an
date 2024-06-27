using DemoVnPay.Model;
using DemoVnPay.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketManagement.Model;

namespace DemoVnPay.Controllers
{
    [Route("api/[controller]")]
    public class VnPayController : Controller
    {
        private VnpayRepository _venpayRepository;
        private TicketRepository ticketRepo;
        public VnPayController(VnpayRepository vnrepo, TicketRepository tickrepo)
        {
            _venpayRepository = vnrepo;
            ticketRepo = tickrepo;
        }
        [HttpGet("checkGD")]
        public IActionResult CheckGD([FromQuery] string code, [FromQuery] string date)
        {
            string res = _venpayRepository.CheckTransaction(code, date, HttpContext);
            return Ok(res);
        }
        [HttpPost("updateOrder")]
        public IActionResult update([FromBody] VnpayPayment data)
        {
            VnpayReturn res = ticketRepo.updateTicket(data);
            return Ok(res);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("GetinfoVnpay")]
        public IActionResult Getinfo()
        {
            return Ok(_venpayRepository.GetVnpayInfo());
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("AddInfo")]
        public IActionResult Create([FromBody] VnPayInfo info)
        {
            var res = _venpayRepository.AddVnpayInfo(info);
            return Ok(res);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("update")]
        public VnPayInfo UpdateInfo([FromBody] VnPayInfo info)
        {
            var res = _venpayRepository.UpdateIfo(info);
            return res;
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("del")]
        public string deleteInfo(int id)
        {
            var res = _venpayRepository.DeleteInfo(id);
            return res;
        }
    }
}
