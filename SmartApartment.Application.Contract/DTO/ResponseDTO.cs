using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartApartment.Application.Contract.DTO
{

    public enum ResponseStatus
    {
        Error,
        Success,
        Warning,
        Info
    }
   
    public class ResponseDTO
    {
        public ResponseStatus ResponseStatus { get; set; } = ResponseStatus.Success;
        public string Message { get; set; } = null;
        public Exception Ex { get; set; } = null;
        public dynamic Data { get; set; }
    }
}
