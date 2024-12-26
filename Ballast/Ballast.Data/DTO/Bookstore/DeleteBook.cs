using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ballast.Data.DTO.Bookstore
{
    public class DeleteBook
    {
        [Required(ErrorMessage = "Book Id is a required field for deleting.")]
        public string Id { get; set; }
    }
}
