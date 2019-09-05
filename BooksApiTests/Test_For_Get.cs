using Xunit;
using DAL;
using System.Collections.Generic;
using DAL.Model;
using ServiceLayer;

namespace BooksApiTests
{
    public class Test_For_Get
    {
        [Fact]
        public void Get_Test()
        {
            Book book1 = new Book { Id = 1, Name = "Naveen", Price = 1000, Author = "Harikrishna", Category = "Joke" };
            Book book2 = new Book { Id = 2, Name = "GreatExpectation", Price = 1000, Author = "Authut", Category = "Thriller" };
            Book book3 = new Book { Id = 3, Name = "HoundOfBaskerVille", Price = 1000, Author = "Conon", Category = "Thriller" };
            BookRepository bookRepository = new BookRepository();
            Services services = new Services(bookRepository);
            var result = services.AddBook(book1);
            result = services.AddBook(book2);
            result = services.AddBook(book3);
            var books = (List<Book>)services.GetBooks().Value;
            Assert.Equal(book2,books[1]);
            

        }
    }


}
