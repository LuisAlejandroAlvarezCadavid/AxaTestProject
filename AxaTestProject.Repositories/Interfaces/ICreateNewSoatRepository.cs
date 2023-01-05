using AxaTestProject.Shared.Models;

namespace AxaTestProject.Repositories.Interfaces
{
    public interface ICreateNewSoatRepository
    {
        Task<bool> CreateNewSoatAsync(SoatDataModel soatDataModel);
    }
}
