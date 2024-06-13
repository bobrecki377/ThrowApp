using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ThrowApp.Models;

namespace ThrowApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ThrowApp.Models.DiscusThrow> DiscusThrow { get; set; } = default!;
        public DbSet<ThrowApp.Models.HammerThrow> HammerThrow { get; set; } = default!;
        public DbSet<ThrowApp.Models.JavelinThrow> JavelinThrow { get; set; } = default!;
        public DbSet<ThrowApp.Models.ShotPut> ShotPut { get; set; } = default!;
    }
}
