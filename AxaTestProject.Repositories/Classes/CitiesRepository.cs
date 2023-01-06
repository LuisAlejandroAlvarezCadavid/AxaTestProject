using AxaTestProject.Repositories.DataEntities;
using AxaTestProject.Repositories.DBContext;
using AxaTestProject.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxaTestProject.Repositories.Classes
{
    public class CitiesRepository: ICitiesRepository
    {

        public MyDBContext MyDBContext { get; set; }

        public CitiesRepository(MyDBContext myDBContext)
        {
            MyDBContext = myDBContext;
        }

        public async Task<(bool, string)> GetAndCheckCityAsync(string city)
        {
            CityEntity cityEntity = await MyDBContext.CityEntities.FirstOrDefaultAsync(ct => ct.CityName== city);
            if (cityEntity == null) return (false, "");
            return (true, cityEntity.CityName);
        }
    }
}
