using AxaTestProject.Repositories.Interfaces;
using AxaTestProject.Services.Interfaces;
using AxaTestProject.Shared.Models;
using System.Net;

namespace AxaTestProject.Services.Classes
{
    public class CreateNewSoatService : ICreateNewSoatService
    {
        ICreateNewSoatRepository _createNewSoatRepository { get; set; }

        public CreateNewSoatService(ICreateNewSoatRepository createNewSoatRepository) 
        { 
            _createNewSoatRepository = createNewSoatRepository;
        }

        public async Task<(bool, HttpReturnDataModel)> CreateNewSoatAsync(SoatDataModel soatDataModel)
        {
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
    }
}
