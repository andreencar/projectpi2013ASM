using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models.User
{
    public class UserModel
    {
        public bool IsPhoto { set; get; }
        public string Photo { set; get; }
        public string Comment { set; get; }
        public string Email { set; get; }
        public string UserName { set; get; }
        public string Name { set; get; }
    }
}