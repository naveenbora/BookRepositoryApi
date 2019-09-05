using Xunit;
using DAL;
using DAL.Model;
using ServiceLayer;

namespace BooksApiTests
{
    public class Test_For_Adding_Book
    {
        [Fact]
        public void Add_Book_Test()
        {
            Book book = new Book { Id = 1, Name = "Naveen", Price = 1000, Author = "Harikrishna", Category = "Joke" };
            BookRepository bookRepository = new BookRepository();
            Services services = new Services(bookRepository);
            var result = services.AddBook(book);
            Assert.Equal(200, result.StatusCode);

        }
    }


}
