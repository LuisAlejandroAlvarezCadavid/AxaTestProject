using AxaTestProject.Repositories.DataEntities;
using AxaTestProject.Repositories.Interfaces;
using AxaTestProject.Services.Interfaces;
using AxaTestProject.Shared.Exeptions;
using AxaTestProject.Shared.Models;
using System.Net;

namespace AxaTestProject.Services.Classes
{
    public class CreateNewSoatService : ICreateNewSoatService
    {
        ICreateNewSoatRepository CreateNewSoatRepository { get; set; }
        ICitiesRepository CitiesRepository { get; set; }

        public CreateNewSoatService(ICreateNewSoatRepository createNewSoatRepository, ICitiesRepository citiesRepository) 
        { 
            CreateNewSoatRepository = createNewSoatRepository;
            CitiesRepository = citiesRepository;
        }

        public async Task<(bool, HttpReturnDataModel)> CreateNewSoatAsync(SoatDataModel soatDataModel)
        {
            (bool status, string message)soatDataStatus = await CheckDataSoat(soatDataModel);
            if (!soatDataStatus.status)
                return (false, new HttpReturnDataModel { StatusCode = HttpStatusCode.BadRequest, Message = soatDataStatus.message });

            bool status = await CreateNewSoatRepository.CreateNewSoatAsync(soatDataModel);
            if(status)
            {
                return (true, new HttpReturnDataModel { StatusCode = HttpStatusCode.OK, Message = ServicesMessages.CreateSoatSuccessful });
            }
            else
            {
                return (true, new HttpReturnDataModel { StatusCode = HttpStatusCode.OK, Message = ServicesMessages.CreateSoatHasFailed });

            }
            
        }

        public async Task<(bool, SoatDataModel?)> GetSoatByPlateAsync(string plate)
        {
            if (string.IsNullOrEmpty(plate))
                return (false, null);
            else
            {
                return (true, await CreateNewSoatRepository.GetSoatByPlateAsync(plate));
            }
        }


        private async Task<(bool, string)> CheckDataSoat(SoatDataModel soatDataModel)
        {
            if(soatDataModel.Identification is null || soatDataModel.Name is null || soatDataModel.LastName is null || soatDataModel.InitDate is null || soatDataModel.EndDate is null || soatDataModel.PlateCar is null 
                || soatDataModel.City is null)
            {
                return (false, ServicesMessages.NeedMoreSoatInformation);
            }
            (bool status, string city) GetCity = await CitiesRepository.GetAndCheckCityAsync(soatDataModel.City);
            if (!GetCity.status)
                throw new CityExistExeption(ServicesMessages.DontExistTheCity, this.GetType());
            if (soatDataModel.EndDate < soatDataModel.InitDate)
                return (false, ServicesMessages.EndDateIsVeryLate);
            return (true, "");
        }
    }
}
