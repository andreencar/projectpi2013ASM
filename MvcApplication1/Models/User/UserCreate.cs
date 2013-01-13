using System.ComponentModel.DataAnnotations;


namespace MvcApplication1.Models.User
{
    public class UserCreate
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        
        public string Image { get; set; }
    }
}
