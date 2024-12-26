using Ballast.Data.DTO.Bookstore;
using Ballast.Data.DTO.BookstoreApi;
using Ballast.Data.Entities.Bookshop;
using Ballast.Service.Bookshop;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ballast.BusinessApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class BooksController : Controller
    {
        private readonly BooksManager _booksManager;

        public BooksController(IHttpContextAccessor contextAccesor)
        {
            var user = contextAccesor.HttpContext?.User?.Identity?.Name ?? "Anonymous";
            var ip = contextAccesor.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "-";
            _booksManager = new BooksManager(user, ip);
        }

        [HttpGet("")]
        public IActionResult BookCatalog()
        {
            var books = _booksManager.GetActiveBooks();
            var bookList = new BooklistDto
            {
                Books = books.Select(c => new BookDto(c)).ToList()
            };
            return Ok(bookList);
        }

        [HttpPost("create")]
        public IActionResult RegisterBook([FromBody] RegisterBook registerBook)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("Please fill up required data to register a book.");
            }
            var response = new BookOperationResponse();
            try
            {
                var book = _booksManager.Add(registerBook.Title, registerBook.Isbn, registerBook.Author, registerBook.Genre, registerBook.Description);
                response.IsSuccess = true;
                response.Message = $"Book {registerBook.Title} registered successfully.";
                response.Book = new BookDto(book);
                return Ok(response);
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = $"Book {registerBook.Title} failed registration. {e.Message}";
                return Ok(response);
            }
        }

        [HttpPost("update")]
        [HttpPatch("update")]
        public IActionResult UpdateBook([FromBody] UpdateBook updateBook)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please fill up required data to update a book.");
            }
            var response = new BookOperationResponse();
            var existingBook = _booksManager.GetBookByCode(updateBook.Id);
            if (existingBook == null)
            {
                return NotFound("The book with the given code doesn't exist.");
            }
            try
            {
                existingBook = _booksManager.Update(existingBook.Id, updateBook.Title, updateBook.Isbn, updateBook.Author, updateBook.Genre, updateBook.Description);
                response.IsSuccess = true;
                response.Message = $"Book {updateBook.Title} updated successfully.";
                response.Book = new BookDto(existingBook);
                return Ok(response);
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = $"Book {updateBook.Title} failed update. {e.Message}";
                return Ok(response);
            }
        }

        [HttpPost("delete")]
        [HttpDelete("delete")]
        public IActionResult DeleteBook([FromBody] DeleteBook deleteBook)
        {
            var book = _booksManager.GetBookByCode(deleteBook.Id);
            if (book == null)
            {
                return NotFound("The book with the given code doesn't exist.");
            }
            _booksManager.DeactivateBook(book);
            return Ok($"Book with Id {deleteBook.Id} deleted.");
        }

        protected override void Dispose(bool disposing)
        {
            _booksManager.Dispose();
            base.Dispose(disposing);
        }
    }
}
