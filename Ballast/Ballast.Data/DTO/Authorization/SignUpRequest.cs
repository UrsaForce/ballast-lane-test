using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ballast.Data.DTO.Authorization
{
    public class SignUpRequest
    {
        [Required(ErrorMessage = "Username is a required field.")]
        [StringLength(40, ErrorMessage = "Username can't exceed 40 characters.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is a required field.")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Password must be have between 4 and 50 characters.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "First Name is a required field.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is a required field.")]
        public string LastName { get; set; }
    }
}
