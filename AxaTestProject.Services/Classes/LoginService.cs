using AxaTestProject.Repositories.DataEntities;
using AxaTestProject.Repositories.Interfaces;
using AxaTestProject.Services.Interfaces;

namespace AxaTestProject.Services.Classes
{
    public class LoginService : ILoginService
    {
        ILoginRepository LoginRepository { get; set; }

        public LoginService(ILoginRepository loginRepository)
        {
            LoginRepository = loginRepository;
        }

        public async Task<bool> GetUserLoginAsync(string username)
        {
            LoginUserEntity loginUserEntity = await LoginRepository.GetUserLoginAsync(username);
            return loginUserEntity == null;
        }

        public async Task<LoginUserEntity> GetUserLoginPassWordAsync(string username)
        {
            LoginUserEntity loginUserEntity = await LoginRepository.GetUserLoginPassWordAsync(username);
            return loginUserEntity;
        }
    }
}
