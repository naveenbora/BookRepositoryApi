using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DAL.Model;
using DAL;
using ServiceLayer;

namespace WebApiBasic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        private readonly BookRepository _BookRepository;
        Services services;
        public DemoController(BookRepository bookRepository)
        {
            _BookRepository = bookRepository;
            services = new Services(bookRepository);
        }
        
        [HttpGet]
        public Result Get()
        {
            var result = services.GetBooks();
            HttpContext.Response.StatusCode = result.StatusCode;
            return result;     
        }

        
        [HttpGet("{id}")]
        public Result RetrieveById(int id)
        {
            var result= services.GetBookNameById(id);
            HttpContext.Response.StatusCode = result.StatusCode;
            return result;
        }


        [HttpPost]
        public Result AddBook([FromBody] Book book)
        {

            var result= services.AddBook(book);
            HttpContext.Response.StatusCode = result.StatusCode;
            return result;

        }


        [HttpPut]
        public bool Update([FromBody] Book book)
        {
            
            return services.Update(book);

        }


        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{

        //}
    }
}
