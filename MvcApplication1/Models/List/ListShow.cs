using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace MvcApplication1.Models
{
    public class ListShow
    {
        
        public string Name { get; set; }

        public bool IsEditable { get; set; }
        
        public int Id { get; set; }

        public IEnumerable<CardShow> Cards { get; set; }

    }
}
