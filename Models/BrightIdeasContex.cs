using Microsoft.EntityFrameworkCore;

namespace BrightIdeas.Models
{
    public class BrightIdeasContext : DbContext
    {
        // constructor
        public BrightIdeasContext(DbContextOptions options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<BrightIdea> BrightIdeas { get; set; }
        public DbSet<Like> Likes { get; set; }


    }
}
