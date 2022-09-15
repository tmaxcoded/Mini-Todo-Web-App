using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Core.Models
{
    public class UserForRegistration
    {
        public UserForRegistration()
        {
            
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public string UserName { get; set; }
        
        public string Password { get; set; }
        public string Email { get; set; }
       
        public ICollection<string> Roles { get; set; }
    }
}
