using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.DTO;
using Microsoft.AspNetCore.Cors;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DiscussionForumDbContext _context;
        public UserController(DiscussionForumDbContext context)
        {
            _context = context;

        }

        [HttpPost]
        [Route("Registration")]
        public async Task<SignupResponseDto> Registration(User user)
        {
            SignupResponseDto response = new SignupResponseDto();
            response.user = user;
            if (user != null)
            {
                IQueryable<User> u = _context.Users.Where(u => u.Email == user.Email);
                var s = u.SingleOrDefault();
                if(s != null)
                {
                    response.valid = false;
                    return response;
                }
                
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                response.valid = true; 
            }

            return response;
        }

        [HttpGet]
        [Route("get/{id:int}")]
        public async Task<User> getUser(int id)
        {
            User u =await _context.Users.FindAsync(id);
            return u;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<LoginResponseDTO> Login(LoginDto user)
        {
            LoginResponseDTO loginResponseDTO = new LoginResponseDTO();

            if (user != null)
            {
                IQueryable<User> u = _context.Users.Where(t => (t.Email == user.Email && t.Password == user.Password));
                Console.WriteLine(u.ToString());
                var s = u.SingleOrDefault();
                if (s != null)
                {
                    if (user.Password == s.Password && user.Email == s.Email)
                    {
                        loginResponseDTO.isValidated = true;
                        loginResponseDTO.user = s;
                        return loginResponseDTO;
                    }
                }
            }
            loginResponseDTO.isValidated = false;
            return loginResponseDTO;
        }
    }
}
