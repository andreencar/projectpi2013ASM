using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcApplication1.DataBaseStructures
{
    public class UserEntity
    {
        public string UserName { private set; get; }
        public string Password { set; get; }
        public string Name { set; get; }

        public UserEntity(string username, string password, string name)
        {
            UserName = username;
            Password = password;
            Name = name;
        }
    }
}
