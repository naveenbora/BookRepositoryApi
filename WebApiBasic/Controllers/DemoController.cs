using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DAL.Model;
using DAL;
using ServiceLayer;
using Unity;

namespace WebApiBasic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        private readonly IBookRepository _BookRepository;
        private readonly IServices services;
        //IUnityContainer container = new UnityContainer();
        

        public DemoController(IBookRepository bookRepository, IServices services)
        {
            _BookRepository = bookRepository;
            this.services = services;
            
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
            var result= services.GetBookById(id);
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
        public Result Update([FromBody] Book book)
        {
            var result = services.Update(book);
            HttpContext.Response.StatusCode = result.StatusCode;
            return result;
            

        }

        
        [HttpDelete("{id}")]
        public Result Delete(int id)
        {
            var result = services.DeleteBookById(id);
            HttpContext.Response.StatusCode = result.StatusCode;
            return result;
        }
        [HttpDelete]
        public object LogDetails()
        {
            return services.GetLoggers();
        }
    }
}
