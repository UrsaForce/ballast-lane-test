using System.ComponentModel.DataAnnotations;
namespace Ballast.Data.DTO.Authorization
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Username is a required field.")]
        [StringLength(40, ErrorMessage = "Username can't exceed 40 characters.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is a required field.")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Password must be have between 4 and 50 characters.")]
        public string Password { get; set; }
    }
}
