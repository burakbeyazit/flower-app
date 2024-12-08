using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CicekApp.Application.Models.Response
{
    public class ExceptionResponseModel
    {
        public string Message { get; set; }
        public int Error { get; set; }
    }
}