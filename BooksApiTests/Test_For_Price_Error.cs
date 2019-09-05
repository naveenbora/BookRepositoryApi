using Xunit;
using DAL;
using DAL.Model;
using ServiceLayer;

namespace BooksApiTests
{
    public class Test_For_Price_Error
    {
        [Fact]
        public void Price_Error_Test()
        {
            Book book = new Book { Id = 1, Name = "Naveen", Price = -1000, Author = "Harikrishna", Category = "Joke" };
            BookRepository bookRepository = new BookRepository();
            Services services = new Services(bookRepository);
            var result = services.AddBook(book);
            Assert.Equal("Price: should be a positive number.", result.ErrorMessage);
            Assert.Equal(400, result.StatusCode);
        }
    }


}
