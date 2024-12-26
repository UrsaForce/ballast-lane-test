using Ballast.Data.DTO.BookstoreApi;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ballast.Data.DTO.Bookstore
{
    public class UpdateBook : RegisterBook
    {
        [Required(ErrorMessage = "Book Id is a required field for updating.")]
        public string Id { get; set; }
    }
}
