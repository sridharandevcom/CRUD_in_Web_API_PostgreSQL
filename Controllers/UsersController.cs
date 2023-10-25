using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleWebAPI.DAL;
using SampleWebAPI.Models;

namespace SampleWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserRepository _userRepository;

        public UsersController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpGet]
        public IActionResult getAllUsers() {
            try
            {
                var users = _userRepository.Users.ToList();
                if (users.Count == 0)
                {
                    return NotFound("Users not found");
                }
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet("id")]
        public IActionResult getUser(int id)
        {
            try
            {
                var user = _userRepository.Users.Find(id);
                if (user == null)
                {
                    return NotFound("User not found");
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        public IActionResult AddUser(UsersModel request) {
            try{
                _userRepository.Add(request);
                _userRepository.SaveChanges();
                return Ok("User Created Successfully");
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult UpdateUser(UsersModel request) {
            try
            {
                if (request == null) {
                return BadRequest("Invalid data");
                }
                var user = _userRepository.Users.Find(request.userid);
                if (user == null) {
                    return NotFound($"{request.userid} is not found");
                }
                user.username = request.username;
                user.email = request.email;
                user.password = request.password;
                _userRepository.SaveChanges();
                return Ok("User Details Updated Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                var user= _userRepository.Users.Find(id);
                if (user == null)
                {
                    return NotFound($"{id} is not found");
                }
                _userRepository.Remove(user);
                _userRepository.SaveChanges();
                return Ok("User Deleted Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
