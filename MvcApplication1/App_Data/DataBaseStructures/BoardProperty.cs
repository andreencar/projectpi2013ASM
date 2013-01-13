using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1.DataBaseStructures
{
    public class BoardProperty
    {
        public bool CanContribute { private set; get; }
        public int BoardId { private set; get; }

        public BoardProperty(int id, bool cancontribute)
        {
            BoardId = id;
            CanContribute = cancontribute;
        }
    }
}