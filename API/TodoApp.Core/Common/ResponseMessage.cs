using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Core.Common
{
    public static class ResponseMessage
    {
        public static string Success = "Success";
        public static string Failed = "Failed";

        public static string _404Message = "Request data cannot be found";

        public static string CreateUsersErrorMessage = "User creating failed";

        public static string UserDoesNotExist = "User does not exist";

        public static string UnAuthorizedAccess = "Un-Authorized Access";
    }
}
