using AxaTestProject.Repositories.DataEntities;
using AxaTestProject.Repositories.DBContext;
using AxaTestProject.Repositories.Interfaces;
using AxaTestProject.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxaTestProject.Repositories.Classes
{
    public class CreateNewSoatRepository : ICreateNewSoatRepository
    {

        public MyDBContext MyDBContext { get; set; }

        public CreateNewSoatRepository(MyDBContext myDBContext)
        {
            MyDBContext = myDBContext;
        }

        public async Task<bool> CreateNewSoatAsync(SoatDataModel soatDataModel)
        {
            return await Task.Run(() => true);
        }
        
    }
}
