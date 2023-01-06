using AxaTestProject.Repositories.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxaTestProject.Services.Interfaces
{
    public interface ILoginService
    {
        Task<bool> GetUserLoginAsync(string username);

        Task<LoginUserEntity> GetUserLoginPassWordAsync(string username);
    }
}
