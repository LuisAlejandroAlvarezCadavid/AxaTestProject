using AxaTestProject.Services.Interfaces;
using AxaTestProject.Shared.Models;
using System.Net;

namespace AxaTestProject.Services.Classes
{
    public class CreateNewSoatService : ICreateNewSoatService
    {
        public Task<(bool, HttpReturnDataModel)> CreateNewSoatAsync(SoatDataModel soatDataModel)
        {
            return Task.Run(() => (true, new HttpReturnDataModel { StatusCode = HttpStatusCode.OK, Message = "Se agrego el Soat con Exito"}));
        }
    }
}
