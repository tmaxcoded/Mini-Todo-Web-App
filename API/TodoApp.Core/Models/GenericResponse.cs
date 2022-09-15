

using System.Net;

namespace TodoApp.Core.Models
{
    public class GenericResponse<T>
    {
        public GenericResponse(bool isSuccess, int responseCode, string description, T data)
        {
            IsSuccess = isSuccess;
            ResponseCode = responseCode;
            Description = description;
            Data = data;
        }

        public bool IsSuccess { get; set; }

        public int ResponseCode { get; set; }

        public string? Description { get; set; }

        public T Data { get; set; }

       
    }
}
