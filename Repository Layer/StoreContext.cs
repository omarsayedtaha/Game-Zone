using Core_Layer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Repository_Layer.Data.Configurations;

namespace Repository_Layer
{
    public class StoreContext:DbContext
    {
        public StoreContext()
        {

        }

        public StoreContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new GameConfiguration());

            modelBuilder.Entity<GameDevices>()
                .HasKey(e => new { e.GameId, e.DeviceId });


            modelBuilder.Entity<Category>().HasData(new List<Category>()
            { 
              new Category(){Id=1,Name="Sports"} , 
              new Category(){Id=2,Name="Action"} , 
              new Category(){Id=3, Name="Adventure"} , 
              new Category(){Id=4, Name="Racing"} , 
              new Category(){Id=5, Name="Fighting"}  ,

            });

            modelBuilder.Entity<Device>().HasData(new List<Device>()
            {
              new Device(){Id=1,Name="Playstation" , Icon="bi bi-playstation"} ,
              new Device(){Id=2,Name="xbox" , Icon="bi bi-xbox"} ,
              new Device(){Id=3,Name="Pc" , Icon="bi bi-pc-display"} ,
             
              

            });
   
        }
        public DbSet<Game> Games { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<GameDevices> GameDevices { get; set; }
    }
}