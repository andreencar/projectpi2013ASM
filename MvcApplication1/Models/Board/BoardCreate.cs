using System.ComponentModel.DataAnnotations;


namespace MvcApplication1.Models
{
    public class BoardCreate
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsPublic { get; set; }
    }
}
