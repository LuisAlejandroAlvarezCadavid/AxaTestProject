using AxaTestProject.Shared.Models;

namespace AxaTestProject.Services.Interfaces
{
    public interface ICreateNewSoatService
    {
        Task<(bool, HttpReturnDataModel)> CreateNewSoatAsync(SoatDataModel soatDataModel);
    }
}
