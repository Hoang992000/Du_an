using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketManagement.DTO;
using TicketManagement.Model;
using TicketManagement.Repository;

namespace TicketManagement.Controllers
{
    [Route("api/[controller]")]
    public class UserController
    {
        UserRepository UserRepository;
        public UserController(UserRepository _userRepository)
        {
            UserRepository = _userRepository;
        }
        [HttpPost("Login")]
        public async Task<LoginRespon> Login([FromBody] UserRequest user)
        {
            LoginRespon res = await UserRepository.Login(user);
            return res;
        }
        [Authorize]
        [HttpPost("Hash")]
        public string hash(string pass)
        {
            return UserRepository.HashPass(pass);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("add")]
        public async Task<User> addUser([FromBody] UserAddUpdate user)
        {
            var res = await UserRepository.AddUser(user);
            return res;
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("update")]
        public async Task<User> UpdateUser([FromBody] User user)
        {
            var res = await UserRepository.UpdateUser(user);
            return res;
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("del")]
        public string deleteUser(int id)
        {
            var res = UserRepository.DeleteUser(id);
            return res;
        }
        //[Authorize(Roles = "Admin")]
        [HttpGet("getAllandLoca")]
        public async Task<List<User>> getAll(int size, int num)
        {
            var data = await UserRepository.getAllUserAndLocation(size, num);
            return data;
        }
        [HttpGet("searchUser")]
        public async Task<List<User>> getUser(string key, int size, int num)
        {
            var result = await UserRepository.findUser(key, size, num);
            return result;
        }
        [HttpGet("coutRow")]
        public async Task<int> Cout()
        {
            var result = await UserRepository.CoutData();
            return result;
        }
        [HttpGet("Logout")]
        public async Task<int> Logout(int userID)
        {
            int result = await UserRepository.LogOut(userID);
            return result;
        }
    }
}
