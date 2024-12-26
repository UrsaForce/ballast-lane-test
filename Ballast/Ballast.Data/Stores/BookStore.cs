using Ballast.Data.Entities.Bookshop;
using LiteDB;
using System.Linq.Expressions;

namespace Ballast.Data.Stores
{
    public class BookStore : IDisposable
    {
        private readonly LiteDatabase _database;

        public BookStore()
        {
            _database = new LiteDatabase(@"ballast-booktstore.db");
        }

        private ILiteCollection<Book> Books()
        {
            return _database.GetCollection<Book>("Books");
        }

        public List<Book> GetAll()
        {
            return Books().FindAll().ToList();
        }

        public List<Book> GetByQueryList(Expression<Func<Book, bool>> query)
        {
            return Books().Find(query).ToList();
        }

        public Book GetByQuery(Expression<Func<Book, bool>> query)
        {
            return Books().FindOne(query);
        }

        public Book GetById(long id)
        {
            return Books().FindById(id);
        }

        public Book Create(Book book)
        {
            Books().Insert(book);
            return book;
        }

        public Book Update(Book book)
        {
            Books().Update(book);
            return book;
        }

        public void Delete(Book book)
        {
            Books().Delete(book.Id);
        }

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}
