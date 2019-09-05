using System;
using Xunit;
using DAL;
using WebApiBasic;
using System.Collections.Generic;
using System.Linq;
using DAL.Model;
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
        }
    }

}
