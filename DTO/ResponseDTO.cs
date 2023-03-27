using AonikenRestAPI.Models;
using System.Net;

namespace AonikenRestAPI.DTO
{
    public class ResponseDTO<T>
    {
        public T? Response { get; set; }
        public string Message { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public ResponseDTO(T data, string message, HttpStatusCode status) 
        {
            Response = data;
            Message = message;
            StatusCode = status;
        }
        public ResponseDTO(string message, HttpStatusCode status)
        {
            Message = message;
            StatusCode = status;
        }

        public static ResponseDTO<T> CreateErrorResponse(T responseObj)
        {
            ResponseDTO<T> result = new ResponseDTO<T>(responseObj, "Se produjo un error", HttpStatusCode.Conflict); //No estoy seguro del status code que debería estar devolviendo
            return result;
        }
    }
}
