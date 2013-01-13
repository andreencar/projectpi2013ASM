using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace MvcApplication1.Models
{
    public class BoardShow
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsPublic { get; set; }

        public IEnumerable<ListShow> List { get; set; }

        public bool IsEditable { get; set; }

        public int Id { get; set; }
    }
}
