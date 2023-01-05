using AxaTestProject.Services.Interfaces;
using AxaTestProject.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AxaTestProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SoatController : Controller
    {
        ICreateNewSoatService __createNewSoartService { get; set; }  

        public SoatController(ICreateNewSoatService createNewSoatService)
        {
            __createNewSoartService= createNewSoatService;
        }

        [HttpPost]
        [Route("AddNewSoat")]
        public async IAsyncEnumerable<(bool, HttpReturnDataModel)> AddNewSoat(SoatDataModel soatDataModel)
        {
            (bool status, HttpReturnDataModel httpStatus) returnStatus = await __createNewSoartService.CreateNewSoatAsync(soatDataModel);
            if(returnStatus.status)
            {
                yield return returnStatus;
            }
            yield return (false, new HttpReturnDataModel { StatusCode = System.Net.HttpStatusCode.NotFound, Message = "Bad Request" });
        }
    }
}
