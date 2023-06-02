using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class DiscussionForumDbContext : DbContext
    {
        public DiscussionForumDbContext(DbContextOptions<DiscussionForumDbContext> options):base(options) 
        { 

        }
        public DbSet<User> Users { get; set; } = null;
        public DbSet<Doubt> Doubts { get; set; } = null;
        public DbSet<Solution> Solutions { get; set; } = null;
    }

}
