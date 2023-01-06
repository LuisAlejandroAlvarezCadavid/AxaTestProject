using AxaTestProject.Repositories.Interfaces;
using AxaTestProject.Services.Interfaces;
using AxaTestProject.Shared.Models;
using System.Net;

namespace AxaTestProject.Services.Classes
{
    public class CreateNewSoatService : ICreateNewSoatService
    {
        ICreateNewSoatRepository _createNewSoatRepository { get; set; }
        ICitiesRepository CitiesRepository { get; set; }

        public CreateNewSoatService(ICreateNewSoatRepository createNewSoatRepository, ICitiesRepository citiesRepository) 
        { 
            _createNewSoatRepository = createNewSoatRepository;
            CitiesRepository = citiesRepository;
        }

        public async Task<(bool, HttpReturnDataModel)> CreateNewSoatAsync(SoatDataModel soatDataModel)
        {
            bool soatDataStatus = await CheckDataSoat(soatDataModel);
            if (!soatDataStatus)
                return (false, new HttpReturnDataModel { StatusCode = HttpStatusCode.OK, Message = "Se agrego el Soat con Exito" });

            bool status = await _createNewSoatRepository.CreateNewSoatAsync(soatDataModel);
            if(status)
            {
                return (true, new HttpReturnDataModel { StatusCode = HttpStatusCode.OK, Message = "Se agrego el Soat con Exito" });
            }
            else
            {
                return (true, new HttpReturnDataModel { StatusCode = HttpStatusCode.OK, Message = "Se agrego el Soat con Exito" });

            }
            
        }


        private async Task<bool> CheckDataSoat(SoatDataModel soatDataModel)
        {
            if(soatDataModel.Identification is null || soatDataModel.Name is null || soatDataModel.LastName is null || soatDataModel.InitDate is null || soatDataModel.EndDate is null || soatDataModel.PlateCar is null 
                || soatDataModel.City is null)
            {
                return false;
            }
            (bool status, string city) GetCity = await CitiesRepository.GetAndCheckCityAsync(soatDataModel.City);
            if (!GetCity.status)
                return false;
            if (soatDataModel.EndDate < soatDataModel.InitDate)
                return false;
            return true;
        }
    }
}
