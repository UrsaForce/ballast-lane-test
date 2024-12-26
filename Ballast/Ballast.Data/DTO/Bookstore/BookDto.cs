using Ballast.Data.Entities.Bookshop;

namespace Ballast.Data.DTO.Bookstore
{
    public class BookDto
    {
        public BookDto()
        {

        }

        public BookDto(Book book)
        {
            Id = book.Code;
            Title = book.Title;
            ISBN = book.ISBN;
            Author = book.Author;
            Genre = book.Genre;
            Description = book.Description;
        }

        public string Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public string ISBN { get; set; }
        public string Description { get; set; }
    }
}
