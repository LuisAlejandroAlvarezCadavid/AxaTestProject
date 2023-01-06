using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxaTestProject.Repositories.Interfaces
{
    public interface ICitiesRepository
    {

        Task<(bool, string)> GetAndCheckCityAsync(string city);
    }
}
