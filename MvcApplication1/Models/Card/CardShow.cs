using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace MvcApplication1.Models
{
    public class CardShow
    {
        [Required]
        public int Id { get; set; }

        public string Description { get; set; }

        public bool IsEditable { get; set; }

    }
}
