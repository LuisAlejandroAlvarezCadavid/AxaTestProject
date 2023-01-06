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
                return new JsonResult(new HttpReturnDataModel { StatusCode = System.Net.HttpStatusCode.BadRequest, Message = returnStatus.httpStatus.Message });
            }
            catch(Exception ex)
            {
                return new JsonResult(new HttpReturnDataModel { StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = ex.Message }) ;
            }
        }

        [HttpGet]
        [Route("GetSoatByPlate/{plate}")]
        public async Task<ActionResult> GetSoatByPlate(string plate)
        {
            try
            {
                (bool status, SoatDataModel? soatDataModel)soat = await CreateNewSoartService.GetSoatByPlateAsync(plate);
                if(soat.status && soat.soatDataModel != null)
                {
                    return Json(soat.soatDataModel);
                }
                else 
                    return new JsonResult(new HttpReturnDataModel { StatusCode = System.Net.HttpStatusCode.BadRequest, Message = HttpMessages.SoatCantRead });
            }
            catch (Exception ex) 
            {
                return new JsonResult(new HttpReturnDataModel { StatusCode = System.Net.HttpStatusCode.BadRequest, Message = HttpMessages.SoatCantRead });
            }
        }
    }
}
