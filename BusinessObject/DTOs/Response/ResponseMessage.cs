using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTOs.Response
{
    public class ResponseMessage
    {
        public string? Message { get; set; }
        public object? Data { get; set; }
    }
}
