using System.ComponentModel.DataAnnotations;

namespace JetwaysAdmin.WebAPI.Models
{
    public class LoginRequest
    {
       
        public  int UserID { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
