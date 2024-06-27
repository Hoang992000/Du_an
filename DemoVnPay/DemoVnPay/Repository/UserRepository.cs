using DemoVnPay.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TicketManagement.DTO;
using TicketManagement.Model;

namespace TicketManagement.Repository
{
    public class UserRepository
    {
        private DataContext _context;
        private IConfiguration _configuration;
        public UserRepository(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        private string GenToken(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:SecretKey").Value!);

            var decentralization = _context.Roles.FirstOrDefault(x => x.RoleId == user.RoleId);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.UserId.ToString()),
                    new Claim("Username", user.UserName),
                    new Claim("RoleId", user.RoleId.ToString()),
                    new Claim(ClaimTypes.Role, decentralization?.RoleName ?? "")
                }),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = jwtTokenHandler.CreateToken(tokenDescription);
            var accessToken = jwtTokenHandler.WriteToken(token);
            return accessToken;
        }
        public string HashPass(string password)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            return hashedPassword;
        }
        public async Task<LoginRespon> Login(UserRequest user)
        {
            User user1 = await _context.Users.Where(x => EF.Functions.Collate(x.UserName, "SQL_Latin1_General_CP1_CS_AS") == user.UserName).FirstOrDefaultAsync();
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    if (user1 != null)
                    {
                        if (user1.LocationId == null)
                        {
                            if (user.LocationId != 0)
                            {
                                user1.LocationId = user.LocationId.Value;
                                Location loca = _context.Locations.Find(user.LocationId);
                                loca.IsActive = true;
                                _context.Locations.Update(loca);
                                _context.Users.Update(user1);
                                _context.SaveChangesAsync();
                            }
                            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(user.Pass, user1.Pass);
                            if (isPasswordValid)
                            {
                                string token = GenToken(user1);
                                await transaction.CommitAsync();
                                return new LoginRespon() { UserId = user1.UserId, Token = token, IsAdmin = user1.RoleId == 1 ? true : false, Message = "Đăng nhập thành công" };
                            }
                            else
                            {
                                await transaction.RollbackAsync();
                                return new LoginRespon() { Message = "Đăng nhập nhất bại, mật khẩu không đúng" };
                            }
                        }
                        else
                        {
                            await transaction.RollbackAsync();
                            return new LoginRespon() { Message = "Đăng nhập thất bại, tài khoản đang được sử dụng ở thiết bị khác" };
                        }
                    }
                    return new LoginRespon() { Message = "Đăng nhập thất bại, tài khoản không tồn tại" };
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception("Đăng nhập thất bại");
                }
            }

        }
        public async Task<User> AddUser(UserAddUpdate user)
        {
            try
            {
                User user1 = await _context.Users.Where(x => EF.Functions.Collate(x.UserName, "SQL_Latin1_General_CP1_CS_AS") == user.UserName).FirstOrDefaultAsync();
                if (user1 == null)
                {
                    string hashPass = BCrypt.Net.BCrypt.HashPassword(user.Pass);
                    User res = new User() { UserName = user.UserName, Pass = hashPass, RoleId = user.RoleId };
                    _context.Users.Add(res);
                    _context.SaveChanges();
                    return res;
                }
                return new User() { UserName = "Tài khoản đã tồn tại" };
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra");
            }
        }
        public async Task<User> UpdateUser(User user)
        {
            try
            {
                var us = _context.Users.Find(user.UserId);
                if (us != null)
                {
                    User user1 = await _context.Users.Where(x => EF.Functions.Collate(x.UserName, "SQL_Latin1_General_CP1_CS_AS") == user.UserName).FirstOrDefaultAsync();
                    if (user1 == null || user1.UserId == user.UserId)
                    {
                        string hashPass = BCrypt.Net.BCrypt.HashPassword(user.Pass);
                        us.UserName = user.UserName;
                        us.Pass = hashPass;
                        us.RoleId = user.RoleId;
                        _context.Update(us);
                        _context.SaveChanges();
                        return us;
                    }
                    return new User() { UserName = "UserName đã tồn tại..." };
                }
                return new User() { UserName = "Tài khoản Không tồn tại" };
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra");
            }
        }
        public string DeleteUser(int id)
        {
            try
            {
                var us = _context.Users.Find(id);
                us.IsEnable = false;
                _context.Users.Update(us);
                _context.SaveChanges();
                return "Success";
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra");
            }
        }
        public async Task<List<User>> getAllUserAndLocation(int size, int num)
        {
            List<User> res = await _context.Users.Include(x => x.Location).Skip((num - 1) * size).Take(size).ToListAsync();
            return res;
        }
        public async Task<List<User>> findUser(string key, int size, int num)
        {
            List<User> res = await _context.Users.Include(x => x.Location).Where(x => x.UserName.Contains(key)).Skip((num - 1) * size).Take(size).ToListAsync();
            return res;
        }
        public async Task<int> CoutData()
        {
            int result = await _context.Users.CountAsync();
            return result;
        }
        public async Task<int> LogOut(int id)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var user = await _context.Users.FindAsync(id);
                    var location = await _context.Locations.FindAsync(user.LocationId);

                    user.LocationId = null;
                    location.IsActive = false;

                    _context.Users.Update(user);
                    _context.Locations.Update(location);

                    await _context.SaveChangesAsync(); // Lưu các thay đổi vào cơ sở dữ liệu

                    await transaction.CommitAsync(); // Xác nhận giao dịch nếu tất cả các thao tác đều thành công
                    return 1;
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync(); // Hủy bỏ giao dịch nếu có bất kỳ lỗi nào xảy ra
                    return 0;
                }
            }
        }
    }
}
