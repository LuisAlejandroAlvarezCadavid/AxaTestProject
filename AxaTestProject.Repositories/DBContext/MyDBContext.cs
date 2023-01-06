using AxaTestProject.Repositories.DataEntities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace AxaTestProject.Repositories.DBContext
{
    public class MyDBContext: DbContext
    {
        public DbSet<LoginUserEntity> LoginUserEntities { get; set; }
        public DbSet<CityEntity> CityEntities { get; set; }
        public DbSet<SoatDataEntity> SoatDataEntities { get; set; }    

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server with connection string from app settings
            base.OnConfiguring(options);
            options.UseSqlServer("Data Source=localhost;Initial Catalog=AxaTestDB;User Id=Alejandro;password=Alejo.1234;Encrypt=False");
        }


        /*
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SoatDataEntity>()
                .HasMany(ct => ct.Cities)
                .WithOne()
                .HasForeignKey(ct => ct.CityName);
        }
        */
    }
}
