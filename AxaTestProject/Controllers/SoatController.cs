using AxaTestProject.Resources;
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
        ICreateNewSoatService CreateNewSoartService { get; set; }  

        public SoatController(ICreateNewSoatService createNewSoatService)
        {
            CreateNewSoartService= createNewSoatService;
        }

        [HttpPost]
        [Route("AddNewSoat")]
        public async Task<ActionResult> AddNewSoat(SoatDataModel soatDataModel)
        {
            try
            {
                (bool status, HttpReturnDataModel httpStatus) returnStatus = await CreateNewSoartService.CreateNewSoatAsync(soatDataModel);
                if (returnStatus.status)
                {
                    return new JsonResult(returnStatus.httpStatus);
                }
                return new JsonResult(new HttpReturnDataModel { StatusCode = System.Net.HttpStatusCode.BadRequest, Message = HttpMessages.SoatDontCreate });
            }
            catch(Exception ex)
            {
                return new JsonResult(new HttpReturnDataModel { StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = HttpMessages.SoatExeptionCreate }) ;
            }
        }
    }
}
