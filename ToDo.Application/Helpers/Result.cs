using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Application.Helpers
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public static Result<T> Success(T data, string message = "")
        {
            var result = new Result<T>();
            result.IsSuccess = true;    
            result.Data = data;         
            result.Message = message;   
            return result;              
        }

        public static Result<T> Failure(string message)
        {
            var result = new Result<T>();
            result.IsSuccess = false;   
            result.Message = message;   
            return result;              
        }

    }
}
