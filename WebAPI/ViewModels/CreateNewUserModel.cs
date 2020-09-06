using System.ComponentModel.DataAnnotations;

namespace WebAPI.ViewModels
{
    public class CreateNewUserModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }

    }
}