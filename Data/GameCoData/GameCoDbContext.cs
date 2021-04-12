using GameCo.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GameCo.Data
{
    public class GameCoDbContext : IdentityDbContext<GameCoUser, IdentityRole, string>
    {
        public virtual DbSet<GameCoGames> Games { get; set; }
        public virtual DbSet<GameCoAchievements> Achievements { get; set; }
        public virtual DbSet<GameCoRating> Ratings { get; set; }

        public GameCoDbContext(DbContextOptions<GameCoDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
