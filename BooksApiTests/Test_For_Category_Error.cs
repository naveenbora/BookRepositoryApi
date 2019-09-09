using Xunit;
using DAL;
using DAL.Model;
using ServiceLayer;

namespace BooksApiTests
{
    public class Test_For_Category_Error
    {
        [Fact]
        public void Name_Error_Test()
        {
            Book book = new Book { Id = 1, Name = "Naveen", Price = 1000, Author = "Harikrishna", Category = "J1oke" };
            BookRepository bookRepository = new BookRepository();
            Services services = new Services(bookRepository);
            var result = services.AddBook(book);
            Assert.Equal("Category: should contain only alphabets.", result.ErrorMessage[0]);
            Assert.Equal(400, result.StatusCode);
        }
    }

}
