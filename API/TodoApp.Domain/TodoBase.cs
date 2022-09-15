
namespace TodoApp.Domain
{
    public class TodoBase
    {
        public DateTime DateCreated {get;set;}
        public string? CreatedBy {get;set;}
        public DateTime ModifiedDate{get;set;}

        public string? ModifiedBy{get;set;}
        
       
    }
}