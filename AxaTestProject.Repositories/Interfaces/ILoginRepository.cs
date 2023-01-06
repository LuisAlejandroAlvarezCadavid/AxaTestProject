using AxaTestProject.Repositories.DataEntities;
using AxaTestProject.Shared.Models.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxaTestProject.Repositories.Interfaces
{
    public interface ILoginRepository
    {
        Task<LoginUserEntity> GetUserLoginAsync(string username);

        Task<LoginUserEntity> GetUserLoginPassWordAsync(string username);
    }
}
