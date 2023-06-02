using WebApplication1.Models;

namespace WebApplication1.DTO
{
    public class SignupResponseDto
    {
        public User user { get; set; }
        public bool valid { get; set; } 
    }
}
