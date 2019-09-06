using Xunit;
using DAL;
using DAL.Model;
using ServiceLayer;
using System.Collections.Generic;

namespace BooksApiTests
{
    public class Test_For_Get_Element_By__Id_Which_Is_Not_Present
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

            Assert.Equal("ID Not Found", services.GetBookById(3).ErrorMessage[0]);


        }
    }
    public class Test_For_Delete_Book_By__Id_Which_Is_Not_Present
    {
        [Fact]
        public void Delete_Book_By_Id_Test()
        {
           
            BookRepository bookRepository = new BookRepository();
            Services services = new Services(bookRepository);
            

            Assert.Equal("ID Not Found", services.DeleteBookById(3).ErrorMessage[0]);


        }
    }
    public class Test_For_Delete_Book_By__Id_
    {
        [Fact]
        public void Delete_Book_By_Id_Test()
        {
            Book book1 = new Book { Id = 1, Name = "Naveen", Price = 1000, Author = "Harikrishna", Category = "Joke" };
            Book book2 = new Book { Id = 2, Name = "GreatExpectation", Price = 1000, Author = "Authut", Category = "Thriller" };
            BookRepository bookRepository = new BookRepository();
            Services services = new Services(bookRepository);
            var result = services.AddBook(book1);
            result = services.AddBook(book2);
            result = services.DeleteBookById(2);
            var _count = 1;
            Assert.Equal(book1, ((List<Book>)(services.GetBooks().Value))[0]);
            Assert.Equal(_count, ((List<Book>)(services.GetBooks().Value)).Count);


        }
    }



}
