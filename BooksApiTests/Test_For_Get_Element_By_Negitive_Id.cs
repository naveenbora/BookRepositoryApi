using Xunit;
using DAL;
using DAL.Model;
using ServiceLayer;

namespace BooksApiTests
{
    public class Test_For_Get_Element_By_Negitive_Id
    {
        [Fact]
        public void Get_Element_By_Id_Test()
        {
            Book book1 = new Book { Id = 1, Name = "Naveen", Price = 1000, Author = "Harikrishna", Category = "Joke" };
            Book book2 = new Book { Id = 2, Name = "GreatExpectation", Price = 1000, Author = "Authut", Category = "Thriller" };
            BookRepository bookRepository = new BookRepository();
            Services services = new Services(bookRepository);
            var result = services.AddBook(book1);
            result = services.AddBook(book2);
            
            Assert.Equal("Invalid Id, Id should be a positive number", services.GetBookById(-3).ErrorMessage[0]);
            

        }
    }



}
