using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Core.Interfaces
{
    public interface IAuthenticationManagerService
    {
        Task<bool> ValidateUser(UserForAuthentication userForAuth);

        Task<bool> ValidateUserByEmail(UserForAuthentication userForAuth);
        Task<string> CreateToken();

    }
}
