using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Solution
    {
        public int Id { get; set; }

        public int DoubtRefId { get; set; }

        [ForeignKey("DoubtRefId")]
        public virtual Doubt Doubt { get; set; }
        public string Email { get; set; }

        public string Answer { get; set; }
    }
}
