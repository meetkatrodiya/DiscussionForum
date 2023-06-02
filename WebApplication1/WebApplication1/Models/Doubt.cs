using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Doubt
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User Users { get; set; }

        public string Question { get; set; }
        public string Description { get; set; }
        public int NoOfAnswer { get; set; }

    }
}
