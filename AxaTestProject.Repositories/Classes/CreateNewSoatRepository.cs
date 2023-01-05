using AxaTestProject.Repositories.Interfaces;
using AxaTestProject.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxaTestProject.Repositories.Classes
{
    public class CreateNewSoatRepository : ICreateNewSoatRepository
    {


        public Task<bool> CreateNewSoatAsync(SoatDataModel soatDataModel)
        {
            return Task.Run(() => true);
        }
    }
}
