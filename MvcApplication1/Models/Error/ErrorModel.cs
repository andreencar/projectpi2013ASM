using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models
{
    public class ErrorModel
    {
        public int Code { get; set; }

        public string Description { get; set; }

        public ErrorModel(int Code, string Description) {
            this.Code = Code;
            this.Description = Description;
        }
    }
}