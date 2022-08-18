using Microsoft.EntityFrameworkCore;

namespace Scheduler.Api.Models
{
    public class SchedulerDbContext : DbContext
    {
        public SchedulerDbContext() 
        {
        }

        public virtual DbSet<RoomModel> Rooms { get; set; } = null!;
        public virtual DbSet<EventModel> Events { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("server=us-cdbr-east-06.cleardb.net;database=heroku_ab83ed6cb724d59;uid=bfba1fd4932260;pwd=bde49b32");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RoomModel>().HasData(
                new RoomModel { Id = 1, Name = "Sala 1"},
                new RoomModel { Id = 2, Name = "Sala 2" },
                new RoomModel { Id = 3, Name = "Sala 3" },
                new RoomModel { Id = 4, Name = "Sala 4" },
                new RoomModel { Id = 5, Name = "Sala 5" },
                new RoomModel { Id = 6, Name = "Sala 6" },
                new RoomModel { Id = 7, Name = "Sala 7" },
                new RoomModel { Id = 8, Name = "Sala 8" },
                new RoomModel { Id = 9, Name = "Sala 9" },
                new RoomModel { Id = 10, Name = "Sala 10" },
                new RoomModel { Id = 11, Name = "Sala 11" },
                new RoomModel { Id = 12, Name = "Sala 12" }


            );
        }

    }
}
