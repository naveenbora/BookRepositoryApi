using Xunit;
using DAL;
using DAL.Model;
using ServiceLayer;

namespace BooksApiTests
{
    public class Test_For_Update_Book_ID_Not_Present
    {
        [Fact]
        public void Update_Book_Id_Not_Present_Test()
        {
            Book book1 = new Book { Id = 1, Name = "Naveen", Price = 1000, Author = "Harikrishna", Category = "Joke" };
            

            BookRepository bookRepository = new BookRepository();
            Services services = new Services(bookRepository);
            
            var result = services.Update(book1);
            Assert.Equal(404, result.StatusCode);
            Assert.Equal("No Id Match", result.ErrorMessage[0]);

        }
    }


}
