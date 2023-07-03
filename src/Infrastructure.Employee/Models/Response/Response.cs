using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Employee.Models.Response
{
    public class Response
    {
        public int HttpStatusCode { get; set; }
        public string Message { get; set; }
        public IList<Error> Errors { get; set; }
    }
}
