using WebApplication1.Models;

namespace WebApplication1.DTO
{
    public class LoginResponseDTO
    {
        public User user { get; set; }
        public bool isValidated { get; set; }
    }
}
