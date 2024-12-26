using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ballast.Data.DTO.BookstoreApi
{
    public class RegisterBook
    {
        [Required(ErrorMessage = "Book title is a required field.")]
        [StringLength(600, ErrorMessage = "Book title can't exceed 40 characters.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "ISBN code is a required field.")]
        [StringLength(70, ErrorMessage = "ISBN can't have more than 70 characters.")]
        public string Isbn { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Book Genre is a required field.")]
        public string Genre { get; set; }
        [Required(ErrorMessage = "Book Author is a required field.")]
        public string Author { get; set; }
    }
}
