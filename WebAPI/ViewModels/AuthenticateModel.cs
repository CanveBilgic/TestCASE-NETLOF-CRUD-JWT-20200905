using System.ComponentModel.DataAnnotations;

namespace WebAPI.ViewModels
{
    public class AuthenticateModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}