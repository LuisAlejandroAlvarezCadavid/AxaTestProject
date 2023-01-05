using AxaTestProject.Repositories.DataEntities;
using Microsoft.EntityFrameworkCore;

namespace AxaTestProject.Repositories.DBContext
{
    public class MyDBContext: DbContext
    {
        DbSet<LoginUserEntity> LoginUserEntities { get; set; }
        DbSet<CityEntity> CityEntities { get; set; }
        DbSet<SoatDataEntity> SoatDataEntities { get; set; }    

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server with connection string from app settings
            base.OnConfiguring(options);
            options.UseSqlServer("Data Source=localhost;Initial Catalog=AxaTestDB;User Id=Alejandro;password=Alejo.1234;Encrypt=False");
        }
    }
}
