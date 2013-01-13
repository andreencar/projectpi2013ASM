using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1.DataBaseStructures
{
    public class Registery
    {
        public string Author { private set; get;  }
        public List<UserProperty> Users { private set; get;  }
        public Board Board { private set; get; }
        public bool IsPublic { private set; get; }


        public Registery(Board b, string username, bool canContribute)
        {
            Author = username;
            Users = new List<UserProperty>();
            Users.Add(new UserProperty(username,true));
            IsPublic = canContribute;
            Board = b;
        }
    }
}