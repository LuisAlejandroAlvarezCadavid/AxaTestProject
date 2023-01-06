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
            try
            {
                await Task.Run(() => {
                    MyDBContext.Add<SoatDataEntity>(new SoatDataEntity
                    {
                        Identification = soatDataModel?.Identification ?? 0,
                        Name = soatDataModel?.Name ?? "",
                        LastName = soatDataModel?.LastName ?? "",
                        InitDate = soatDataModel?.InitDate ?? DateTime.Parse("1000-1-1"),
                        EndDate = soatDataModel?.EndDate ?? DateTime.Parse("1000-1-1"),
                        PlateCar = soatDataModel?.PlateCar ?? "",
                        CityName = soatDataModel?.City ?? ""
                    });
                    MyDBContext.SaveChanges();
                });
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.InnerException + "\n" + ex.StackTrace);
                return false;
            }
        }       

        public async Task<SoatDataModel?> GetSoatByPlateAsync(string plate)
        {
            SoatDataEntity? soatDataEntity = await MyDBContext.SoatDataEntities.FirstOrDefaultAsync(so => so.PlateCar== plate);
            return GetSoatDataModel(soatDataEntity);
        }


        private SoatDataModel? GetSoatDataModel(SoatDataEntity? soatDataEntity)
        {           
            if (soatDataEntity != null)
            {
                return new SoatDataModel
                {
                    Identification = soatDataEntity?.Identification ?? 0,
                    Name = soatDataEntity?.Name ?? string.Empty,
                    LastName = soatDataEntity?.LastName ?? string.Empty,
                    InitDate = soatDataEntity?.InitDate ?? DateTime.Parse("1000-1-1"),
                    EndDate = soatDataEntity?.EndDate ?? DateTime.Parse("1000-1-1"),
                    PlateCar = soatDataEntity?.PlateCar ?? string.Empty,
                    City = soatDataEntity?.CityName ?? string.Empty
                };
            }
            else
                return null;   
            
        }

    }
}
