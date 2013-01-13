using System.ComponentModel.DataAnnotations;


namespace MvcApplication1.Models.User
{
    public class LoginUser
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

    }
}
