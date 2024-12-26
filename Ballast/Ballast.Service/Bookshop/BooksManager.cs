using Ballast.Data.Entities.Bookshop;
using Ballast.Data.Stores;

namespace Ballast.Service.Bookshop
{
    public class BooksManager : IDisposable
    {
        private readonly BookStore _bookStore;
        private readonly string _user;
        private readonly string _ip;

        public BooksManager(string user, string ip)
        {
            _bookStore = new BookStore();
            _user = user;
            _ip = ip;
        }

        public List<Book> GetActiveBooks()
        {
            return _bookStore.GetByQueryList(c => c.IsActive);
        }

        public Book? GetBookByCode(string code)
        {
            code = code.Trim().ToLower();
            return _bookStore.GetByQuery(c => c.IsActive && c.Code == code);
        }

        private bool IsbnIsUnique(string isbn, long? existingId = null)
        {
            isbn = isbn.Trim();
            return _bookStore.GetByQuery(c => c.IsActive && c.ISBN == isbn && c.Id != existingId) == null;
        }

        public Book Add(string title, string isbn, string author, string genre, string? description = null)
        {
            if (!IsbnIsUnique(isbn))
                throw new Exception($"There is already a book with the same ISBN code: {isbn}.");
            var book = new Book
            {
                Code = GenerateBookCode(),
                Title = title.Trim(),
                ISBN = isbn.Trim(),
                Author = author.Trim(),
                Genre = genre.Trim(),
                Description = description,
                RegistrationTimestamp = DateTime.Now,
                IsActive = true,
                RegistrationUser = _user,
                RegistrationIp = _ip
            };
            return _bookStore.Create(book);
        }

        public Book Update(long id, string title, string isbn, string author, string genre, string? description = null)
        {
            var book = _bookStore.GetById(id);
            if (book == null || !book.IsActive)
                throw new Exception($"The book with Id {id} doesn't exist in catalog.");
            if (!IsbnIsUnique(isbn, id))
                throw new Exception($"There is another book with the same ISBN code: {isbn}.");
            book.Title = title.Trim();
            book.ISBN = isbn.Trim();
            book.Author = author.Trim();
            book.Genre = genre.Trim();
            book.Description = description;
            book.UpdateTimestamp = DateTime.Now;
            book.UpdateUser = _user;
            book.UpdateIp = _ip;
            return _bookStore.Update(book);
        }

        public void DeactivateBook(Book book)
        {
            book.IsActive = false;
            _bookStore.Update(book);
        }

        private string GenerateBookCode()
        {
            return DateTime.Today.Year.ToString() + "-" + Guid.NewGuid().ToString().Substring(0, 12).ToLower();
        }

        public void Dispose()
        {
            _bookStore.Dispose();
        }
    }
}