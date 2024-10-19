using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Model;
     
namespace DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

        public DbSet<PostModel> Posts { get; set; }
        public DbSet<PostSectionModel> PostSections { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<TagModel> Tags { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<CommentModel> Comments { get; set; }
        public DbSet<ReactModel> Reacts { get; set; }
        public DbSet<FavPostModel> FavPosts { get; set; }
        public DbSet<ImageModel> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define relationships and constraints here (like many-to-many)
        }
    }
}
