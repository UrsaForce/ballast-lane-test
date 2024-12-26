namespace Ballast.Data.DTO.Bookstore
{
    public class BookOperationResponse
    {
        public BookDto Book { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
