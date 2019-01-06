using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.Dtos
{
    //DTO's: often use to map the main models (like "User" class) into simpler objects that ultimately get returend or displayed by the view
    public class UserForRegisterDTO
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "You must specify password between 4 and 8 characters")]
        public string Password { get; set; }
    }
}