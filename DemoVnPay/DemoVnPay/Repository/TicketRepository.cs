using DemoVnPay.Controllers;
using DemoVnPay.DTO;
using DemoVnPay.Model;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using TicketManagement.DTO;
using TicketManagement.Model;

namespace DemoVnPay.Repository
{
    public class TicketRepository
    {
        private DataContext _context;
        private IConfiguration _configuration;
        private readonly ILogger<WeatherForecastController> _logger;
        public TicketRepository(DataContext context, IConfiguration configuration, ILogger<WeatherForecastController> logger)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger;
        }
        private string GenCheckSum(DataVnpayQr vnpay, string secretKey)
        {
            string result = string.Join("|", vnpay.appId, vnpay.merchantName, vnpay.serviceCode, vnpay.countryCode, vnpay.masterMerCode,
            vnpay.merchantType, vnpay.merchantCode, vnpay.terminalId, vnpay.payType, vnpay.productId, vnpay.txnId, vnpay.amount, vnpay.tipAndFee, vnpay.ccy, vnpay.expDate, secretKey);
            using (MD5 md5 = MD5.Create())
            {
                // Chuyển đổi chuỗi thành mảng byte
                byte[] inputBytes = Encoding.ASCII.GetBytes(result);
                // Tạo giá trị băm từ mảng byte
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Chuyển đổi mảng byte thành chuỗi hex
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }
                return (sb.ToString().ToUpper());
            }
        }
        private string GenCode(int id)
        {
            var typeVe = _context.Vehicles.Find(id).VehicleName;
            string res = typeVe + DateTime.Now.ToString("yyyyMMddHHmmss");
            return res;
        }
        private async Task<string> GetQrCodeVnpay(string code)
        {
            var ticket = await _context.Tickets.Include(x => x.Vehicle).FirstOrDefaultAsync(x => x.TicketCode == code);
            var LstInfo = await _context.vnPayInfos.ToListAsync();
            VnPayInfo Info = LstInfo[0];
            DataVnpayQr vnpayQr = new DataVnpayQr()
            {
                appId = Info.appId,
                merchantName = Info.merchantName,
                serviceCode = Info.merchantCode,
                countryCode = "VN",
                masterMerCode = "A000000775",
                merchantType = Info.merchantType,
                merchantCode = Info.merchantCode,
                payloadFormat = "",
                terminalId = Info.terminalId,
                payType = "03",
                productId = "",
                txnId = code,
                amount = ticket.Vehicle.Cost.ToString(),
                tipAndFee = "",
                ccy = "704",
                expDate = "",
                desc = "",
                mobile = "",
                billNumber = code,
                consumerID = "",
                purpose = ""
            };
            string checksum = GenCheckSum(vnpayQr, Info.secretKey);
            vnpayQr.checksum = checksum;
            //goi api get qr
            string url = "https://doitac-tran.vnpaytest.vn/QRCreateAPIRestV2/rest/CreateQrcodeApi/createQrcode";
            HttpClient _httpClient = new HttpClient();
            VnPayQrReturn result = new VnPayQrReturn();
            var jsonStr = JsonConvert.SerializeObject(vnpayQr);
            HttpContent content = new StringContent(jsonStr, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, content);
            string jsonResult = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {

                result = JsonConvert.DeserializeObject<VnPayQrReturn>(jsonResult);
                return result.data;
            }
            return "hello";
        }
        public async Task<Ticket> AddTicket(TicketRequest ticket)
        {
            Ticket ticket1 = new Ticket()
            {
                TicketCode = GenCode(ticket.VehicleId),
                TimeIn = DateTime.Now,
                VehicleNum = ticket.VehicleNum,
                VehicleId = ticket.VehicleId,
                PaymentMethodId = 1,
                UserId = ticket.UserId,
                LocationId = ticket.LocationId,
                StatusOder = false
            };
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    _context.Tickets.Add(ticket1);
                    await _context.SaveChangesAsync();
                    string Qr = await GetQrCodeVnpay(ticket1.TicketCode);
                    TicketReturn result = new TicketReturn()
                    {
                        TicketCode = ticket1.TicketCode,
                        TimeIn = ticket1.TimeIn,
                        VehicleNum = ticket1.VehicleNum,
                        VehicleId = ticket1.VehicleId,
                        PaymentMethodId = ticket1.PaymentMethodId,
                        UserId = ticket1.UserId,
                        LocationId = ticket1.LocationId,
                        StatusOder = ticket1.StatusOder,
                        QrData = Qr
                    };
                    await transaction.CommitAsync();
                    //_logger.LogInformation($"Qr:{result.QrData}");
                    return result;

                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync(); // Hủy bỏ giao dịch nếu có bất kỳ lỗi nào xảy ra
                    return null;
                }
            }
        }
        public async Task<Ticket> CheckTicketAsync(string TicketCode, bool cash)
        {
            var tick = await _context.Tickets.Include(x => x.Vehicle).Include(x => x.Method).SingleOrDefaultAsync(x => x.TicketCode == TicketCode);
            if (tick != null)
            {
                if (tick.StatusOder == true)
                {
                    tick.TimeOut = DateTime.Now;
                    _context.Update(tick);
                    await _context.SaveChangesAsync();
                }
                else if (cash)
                {
                    tick.TimeOut = DateTime.Now;
                    tick.StatusOder = true;
                    tick.PaymentTime = DateTime.Now;
                    _context.Update(tick);
                    await _context.SaveChangesAsync();
                }
                return tick;
            }
            return new Ticket()
            {
                TicketCode = "000"
            };
        }
        public async Task<IQueryable<TicketRespon>> FindTicketByNum(string num)
        {
            List<Ticket> lsttick = new List<Ticket>();
            lsttick = await _context.Tickets.Include(x => x.Vehicle).Include(x => x.Method).OrderBy(x => x.TimeIn).Where(x => x.VehicleNum.Contains(num)).ToListAsync();
            List<TicketRespon> res = new List<TicketRespon>();
            for (int i = 0; i < lsttick.Count(); i++)
            {
                TicketRespon respon = new TicketRespon()
                {
                    TicketCode = lsttick[i].TicketCode,
                    TimeIn = lsttick[i].TimeIn,
                    TimeOut = lsttick[i].TimeOut,
                    PaymentTime = lsttick[i].PaymentTime,
                    VehicleType = lsttick[i].Vehicle.VehicleName,
                    VehicleNum = lsttick[i].VehicleNum,
                    PaymentMethodType = lsttick[i].Method.PaymentMethodCode,
                    StatusOder = lsttick[i].StatusOder ? "Đã thanh toán" : "Chưa thanh toán",
                };
                res.Add(respon);
            }
            return res.AsQueryable();
        }
        public async Task<IQueryable<TicketRespon>> GetReportTicket(int size, int num, DateTime from, DateTime to)
        {
            List<Ticket> lsttick = new List<Ticket>();
            if (from == new DateTime(1, 1, 1) || to == new DateTime(1, 1, 1))
            {
                lsttick = await _context.Tickets.Include(x => x.Vehicle).Include(x => x.Method).Include(x => x.Location).OrderByDescending(x => x.TimeIn).Skip((num - 1) * size).Take(size).ToListAsync();
            }
            else
            {
                lsttick = await _context.Tickets.Include(x => x.Vehicle).Include(x => x.Method).Include(x => x.Location).Where(x => x.TimeIn >= from && x.TimeIn <= to).OrderByDescending(x => x.TimeIn).Skip((num - 1) * size).Take(size).ToListAsync();
            }
            List<TicketRespon> res = new List<TicketRespon>();
            for (int i = 0; i < lsttick.Count(); i++)
            {
                TicketRespon respon = new TicketRespon()
                {
                    TicketCode = lsttick[i].TicketCode,
                    TimeIn = lsttick[i].TimeIn,
                    TimeOut = lsttick[i].TimeOut,
                    PaymentTime = lsttick[i].PaymentTime,
                    VehicleType = lsttick[i].Vehicle.VehicleName,
                    VehicleNum = lsttick[i].VehicleNum,
                    PaymentMethodType = lsttick[i].Method.PaymentMethodCode,
                    StatusOder = lsttick[i].StatusOder ? "Đã thanh toán" : "Chưa thanh toán",
                    LocationName = lsttick[i].Location.Locationname,
                    Cost = lsttick[i].Vehicle.Cost,
                };
                res.Add(respon);
            }
            return res.AsQueryable();
        }
        public VnpayReturn updateTicket(VnpayPayment data)
        {
            _logger.LogInformation($"DataVnpay send:{data.txnId}");
            _logger.LogInformation($"CheckSum send:{data.checksum}");
            Ticket ticket = _context.Tickets.Find(data.txnId);
            if (ticket == null)
            {
                _logger.LogInformation($"khong tim thay ma ve");
                return new VnpayReturn() { code = "04", message = "đơn hàng không tồn tại" };
            }
            else
            {
                _logger.LogInformation($"ticket tuong ung voi ma txnid:{ticket.TicketCode}");
                ticket.StatusOder = true;
                ticket.PaymentTime = DateTime.Now;
                ticket.PaymentMethodId = 2;
                _context.Tickets.Update(ticket);
                _context.SaveChangesAsync();
                _logger.LogInformation($"da cap nhat trang thai ve thanh cong");
                return new VnpayReturn() { code = "00", message = "Đặt hàng thành công" };
            }

        }

        public async Task<List<ReportModel>> GetHomeData()
        {
            List<ReportModel> result = new List<ReportModel>();
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    await connection.OpenAsync();

                    using (var command = new SqlCommand("GetUserTicketCountsForToday", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                result.Add(new ReportModel()
                                {
                                    STT = result.Count + 1,
                                    UserName = reader["UserName"].ToString(),
                                    Location = reader["LocationName"].ToString(),
                                    TotalTicket = (int)reader["TotalTicket"]
                                });

                            }
                        }
                    }
                    return result;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }
        }

        public async Task<int> GetTotalTicketToday()
        {
            int result = await _context.Tickets.Where(x => x.TimeIn.Date == DateTime.Today).CountAsync();
            //var a = _context.Tickets.Find("Car20240603160816");
            //var tmp1 = a.TimeIn.ToShortDateString();
            //var tmp2 = DateTime.Now.ToShortDateString();
            return result;
        }
        public async Task<int> GetLoactionToday()
        {
            var result = _context.Tickets
            .Where(t => t.TimeIn.Date == DateTime.Now.Date)
            .GroupBy(t => t.LocationId)
            .Select(g => new { LocationId = g.Key });
            return result.Count();
        }
        public async Task<int> CoutData()
        {
            int result = await _context.Tickets.CountAsync();
            return result;
        }
        public async Task<List<TicketRespon>> GetReportTicke(int locationID, int vehicleID, DateTime from, DateTime to)
        {
            from = DateTime.Parse(from.ToShortDateString());
            to = DateTime.Parse(to.ToShortDateString());
            List<TicketRespon> result = new List<TicketRespon>();
            string sqlCommand = "";
            if (locationID == 0 && vehicleID == 0)
            {
                sqlCommand = "GetTicketsByDate";
            }
            else if (locationID == 0)
            {
                sqlCommand = "GetTicketsByVehicleAndDate";
            }
            else if (vehicleID == 0)
            {
                sqlCommand = "GetTicketsByVehicleAndDate";
            }
            else
            {
                sqlCommand = "GetTicketsByAllLocationAndVehicleAndDate";
            }
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    await connection.OpenAsync();

                    using (var command = new SqlCommand(sqlCommand, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        if (locationID == 0 && vehicleID == 0)
                        {

                        }
                        else if (locationID == 0)
                        {
                            command.Parameters.Add("@VehicleId", SqlDbType.Int).Value = vehicleID;
                        }
                        else if (vehicleID == 0)
                        {
                            command.Parameters.Add("@LocationId", SqlDbType.Int).Value = locationID;
                        }
                        else
                        {
                            command.Parameters.Add("@LocationId", SqlDbType.Int).Value = locationID;
                            command.Parameters.Add("@VehicleId", SqlDbType.Int).Value = vehicleID;
                        }
                        command.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = from;
                        command.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = to;
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                result.Add(new TicketRespon()
                                {
                                    STT = result.Count + 1,
                                    TicketCode = reader["TicketCode"].ToString(),
                                    VehicleNum = reader["VehicleNum"].ToString(),
                                    VehicleType = reader["VehicleName"].ToString(),
                                    PaymentMethodType = reader["PaymentMethodCode"].ToString(),
                                    LocationName = reader["LocationName"].ToString(),
                                    TimeIn = DateTime.Parse(reader["TimeIn"].ToString()),
                                    Cost = int.Parse(reader["Cost"].ToString()),
                                    TimeOut = reader["TimeOut"] == DBNull.Value ? (DateTime?)null : DateTime.Parse(reader["TimeOut"].ToString()),
                                    StatusOder = reader["StatusOder"].ToString() == "True" ? "Đã thanh toán" : "Chưa thanh toán",
                                    PaymentTime = reader["PaymentTime"] == DBNull.Value ? (DateTime?)null : DateTime.Parse(reader["PaymentTime"].ToString()),
                                });
                            }
                        }
                        reader.Close();
                        return result;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
        public async Task<int> GetRevenue(int locationID, int vehicleID, DateTime from, DateTime to)
        {
            from = DateTime.Parse(from.ToShortDateString());
            to = DateTime.Parse(to.ToShortDateString());
            int result = 0;
            string sqlCommand = "";
            if (locationID == 0 && vehicleID == 0)
            {
                sqlCommand = "GetRevenueByDate";
            }
            else if (locationID == 0)
            {
                sqlCommand = "GetRevenueByVehicleAndDate";
            }
            else if (vehicleID == 0)
            {
                sqlCommand = "GetRevenueByVehicleAndDate";
            }
            else
            {
                sqlCommand = "GetRevenueByLocationAndVehicleAndDate";
            }
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    await connection.OpenAsync();

                    using (var command = new SqlCommand(sqlCommand, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        if (locationID == 0 && vehicleID == 0)
                        { }
                        else if (locationID == 0)
                        {
                            command.Parameters.Add("@VehicleId", SqlDbType.Int).Value = vehicleID;
                        }
                        else if (vehicleID == 0)
                        {
                            command.Parameters.Add("@LocationId", SqlDbType.Int).Value = locationID;
                        }
                        else
                        {
                            command.Parameters.Add("@LocationId", SqlDbType.Int).Value = locationID;
                            command.Parameters.Add("@VehicleId", SqlDbType.Int).Value = vehicleID;
                        }
                        command.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = from;
                        command.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = to;
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        if (await reader.ReadAsync()) // Chỉ cần đọc một dòng nếu bạn biết thủ tục chỉ trả về một dòng
                        {
                            if (reader["Revenue"] != DBNull.Value)
                            {
                                string revenueStr = reader["Revenue"].ToString();
                                Console.WriteLine("Log loi: " + revenueStr);
                                result = int.Parse(revenueStr);
                            }
                            else
                            {
                                Console.WriteLine("Log loi: Revenue is null");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No rows found.");
                        }
                        reader.Close();
                        return result;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
        //public async Task<List<TicketRespon>> GetReportForVehicle(int vehicleID, DateTime from, DateTime to)
        //{
        //    List<TicketRespon> result = new List<TicketRespon>();
        //    string sqlCommand = "";
        //    if (from == new DateTime(1, 1, 1) || to == new DateTime(1, 1, 1))
        //    {
        //        sqlCommand = "GetTicketsByVehicle";
        //    }
        //    else
        //    {
        //        sqlCommand = "GetTicketsByVehicleAndDate";
        //    }
        //    using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        //    {
        //        try
        //        {
        //            await connection.OpenAsync();

        //            using (var command = new SqlCommand(sqlCommand, connection))
        //            {
        //                command.CommandType = CommandType.StoredProcedure;
        //                if (from == new DateTime(1, 1, 1) || to == new DateTime(1, 1, 1))
        //                {
        //                    command.Parameters.Add("@VehicleId", SqlDbType.Int).Value = vehicleID;
        //                }
        //                else
        //                {
        //                    command.Parameters.Add("@VehicleId", SqlDbType.Int).Value = vehicleID;
        //                    command.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = from;
        //                    command.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = to;
        //                }
        //                SqlDataReader reader = await command.ExecuteReaderAsync();
        //                if (reader.HasRows)
        //                {
        //                    while (reader.Read())
        //                    {
        //                        //DateTime Out = new DateTime();
        //                        //bool check=DateTime.TryParse(reader["TimeOut"].ToString(),out Out);
        //                        //if(!check) Out=DateTime.
        //                        result.Add(new TicketRespon()
        //                        {
        //                            STT = result.Count + 1,
        //                            TicketCode = reader["TicketCode"].ToString(),
        //                            VehicleNum = reader["VehicleNum"].ToString(),
        //                            VehicleType = reader["VehicleName"].ToString(),
        //                            PaymentMethodType = reader["PaymentMethodCode"].ToString(),
        //                            LocationName = reader["LocationName"].ToString(),
        //                            TimeIn = DateTime.Parse(reader["TimeIn"].ToString()),
        //                            TimeOut = reader["TimeOut"] == DBNull.Value ? (DateTime?)null : DateTime.Parse(reader["TimeOut"].ToString()),
        //                            StatusOder = reader["StatusOder"].ToString() == "1" ? "Đã thanh toán" : "Chưa thanh toán",
        //                            PaymentTime = reader["PaymentTime"] == DBNull.Value ? (DateTime?)null : DateTime.Parse(reader["PaymentTime"].ToString()),
        //                        });
        //                    }
        //                }
        //                return result;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception(ex.Message);
        //        }
        //    }
        //}

    }
}
