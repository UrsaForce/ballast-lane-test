using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ballast.Data.DTO.Bookstore
{
    public class BooklistDto
    {
        public BooklistDto()
        {
            Books = new List<BookDto>();
        }

        public List<BookDto> Books { get; set; }
        public int Count
        {
            get
            {
                return Books.Count;
            }
        }
        public string Query { get; set; }
    }
}
