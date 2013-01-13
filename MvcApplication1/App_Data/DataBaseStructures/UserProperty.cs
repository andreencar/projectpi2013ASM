using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1.DataBaseStructures
{
    public class UserProperty
    {
        public bool CanEdit { private set; get; }
        public string UserName { private set; get; }

        public UserProperty(string name, bool canedit)
        {
            UserName = name;
            CanEdit = canedit;
        }
    }
}