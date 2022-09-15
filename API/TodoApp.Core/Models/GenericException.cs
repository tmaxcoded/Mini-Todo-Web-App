

namespace TodoApp.Core.Models
{
    public class GenericException: Exception
    {
        public GenericException(): base(){ }
        public GenericException(string message): base(message){ }

        public GenericException(string message, params object[] args): base(String.Format(CultureInfo.CurrentCulture, message, args)) { }

        
    }
}
