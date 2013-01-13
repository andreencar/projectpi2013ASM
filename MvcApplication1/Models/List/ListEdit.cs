using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models.List
{
    public class ListEdit
    {
        public string Name { get; set; }
        public IEnumerable<ListShow> MovingOptions { get; set; }
    }
}