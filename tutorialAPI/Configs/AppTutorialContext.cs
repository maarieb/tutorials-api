using Microsoft.EntityFrameworkCore;
using tutorialAPI.Models;

namespace tutorialAPI.Configs
{
    public class AppTutorialContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Author> Authors {  get; set; }
        public DbSet<Comment> Comments {  get; set; }
        public DbSet<Tutorial> Tutorials {  get; set; }
        public DbSet<User> Users { get; set; }

        public AppTutorialContext(DbContextOptions<AppTutorialContext> options) : base(options)
        {

        }

    }
}
