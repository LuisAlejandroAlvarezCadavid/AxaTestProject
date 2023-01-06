using AxaTestProject.Repositories.DataEntities;
using AxaTestProject.Repositories.DBContext;
using AxaTestProject.Repositories.Interfaces;
using AxaTestProject.Shared.Models.Login;
using Microsoft.EntityFrameworkCore;

namespace AxaTestProject.Repositories.Classes
{

    
    public class LoginRepository: ILoginRepository
    {
        public MyDBContext MyDBContext { get; set; }

        public LoginRepository(MyDBContext myDBContext)
        {
            MyDBContext = myDBContext;
        }

        public async Task<LoginUserEntity> GetUserLoginAsync(string username)
        {
            try
            {
                LoginUserEntity loginuser = await MyDBContext.LoginUserEntities.FirstOrDefaultAsync(user => user.User == username);
                if (loginuser == null) return loginuser;
               // if (loginuser.User == username && loginuser.Password == password) return true;
                return loginuser;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.Data + "\n" + ex.InnerException);
                return null;
            }
            
        }



        public async Task<LoginUserEntity> GetUserLoginPassWordAsync(string username)
        {
            try
            {
                LoginUserEntity loginuser = await MyDBContext.LoginUserEntities.FirstOrDefaultAsync(user => user.User == username);
                if (loginuser == null) return loginuser;
                // if (loginuser.User == username && loginuser.Password == password) return true;
                return loginuser;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.Data + "\n" + ex.InnerException);
                return null;
            }
        }
    }
}
