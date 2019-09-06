using Xunit;
using DAL;
using DAL.Model;
using ServiceLayer;

namespace BooksApiTests
{
    public class Test_For_Update_Book
    {
        [Fact]
        public void Update_Book_Test()
        {
            Book book1 = new Book { Id = 1, Name = "Naveen", Price = 1000, Author = "Harikrishna", Category = "Joke" };
            Book book2 = new Book { Id = 1, Name = "NaveenReddy", Price = 1000, Author = "Harikrishna", Category = "Joke" };

            BookRepository bookRepository = new BookRepository();
            Services services = new Services(bookRepository);
            var result = services.AddBook(book1);
             result = services.Update(book2);
            Assert.Equal(200, result.StatusCode);
            
        }
    }

}
