using Gym.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Gym.Infrastructure.EF
{
    public class GymContext : DbContext
    {
        public GymContext(DbContextOptions<GymContext> options) : base(options) { }

        public DbSet<Exercise> Exercises { get; set; }
    }
}
