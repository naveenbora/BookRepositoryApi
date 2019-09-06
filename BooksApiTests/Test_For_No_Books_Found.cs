using System;
using Xunit;
using DAL;
using WebApiBasic;
using System.Linq;
using WebApiBasic.Controllers;
using ServiceLayer;

namespace BooksApiTests
{
    public class Test_For_No_Books_Found
    {
        [Fact]
        public void No_Books_Found()
        {
            BookRepository bookRepository = new BookRepository();
            Services services = new Services(bookRepository);
            var result = services.GetBooks();
            Assert.Equal("No Books Found", result.ErrorMessage);
            Assert.Equal(404, result.StatusCode);
        }
    }



}
