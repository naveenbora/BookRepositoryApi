
using DAL;
using DAL.Model;
using System;
using System.Linq;

namespace ServiceLayer
{
    public class Services
    {
        private readonly BookRepository _BookRepository;
        private readonly BookValidationThroughFluent _BookValidationThroughFluent;

        public Services(BookRepository bookRepository)
        {
            _BookRepository = bookRepository;
            _BookValidationThroughFluent = new BookValidationThroughFluent();
        }


        public Result GetBooks()
        {
            var books = _BookRepository.GetAllBooks();
            Result result = new Result();
            if (books.Count == 0)
            {

                result.ErrorMessage.Add("No Books Found");
                result.StatusCode = 404;
                result.Value = null;
                
            }
            else
            {
                result.StatusCode = 200;
                result.Value = books;
            }
            return result;
        }
        public Result GetBookById(int id)
        {
            Result result = new Result();
            if(id<0)
            {
                result.ErrorMessage.Add("Invalid Id, Id should be a positive number");
                result.StatusCode = 400;
                return result;
            }
            var book = _BookRepository.GetById(id);
            if (book == null)
            {
                result.ErrorMessage.Add("ID Not Found");
                result.StatusCode = 404;
                return result;
            }
            else
            {
                result.Value = book;
                return result;
            }
                


        }
        public Result DeleteBookById(int id)
        {
            Result result = new Result();
            if (id < 0)
            {
                result.ErrorMessage.Add("Invalid Id, Id should be a positive number");
                result.StatusCode = 400;
                return result;
            }
            var book = _BookRepository.DeleteById(id);
            if (book == false)
            {
                result.ErrorMessage.Add("ID Not Found");
                result.StatusCode = 404;
                return result;
            }
            else
            {
                result.Value = "Deleted";
                return result;
            }


        }

        public Result Update(Book book)
        {
            Result result;
            result = Validate(book);

            if (result.ErrorMessage.Count!=0)
                return result;
            if( _BookRepository.ReplaceBook(book))
            {
                result.Value = "Succes";
                return result;

            }
            else
            {
                result.ErrorMessage.Add("No Id Match");
                result.StatusCode = 404;
                return result;
            }
        }

        

        public Result AddBook(Book book)
        {
            Result result;
            result = Validate(book);

            if (result.ErrorMessage.Count!=0)
                return result;
             
            var status = _BookRepository.AddBook(book);
            if (status == true)
            {
                result.Value = "Success";
                return result;
            }
               
            else
            {
                result.StatusCode = 404;
                result.ErrorMessage.Add("book Id already exist");
                return result;
            }
                
        }
        public Result Validate(Book book)
        {
            Result result = new Result();
            var resultFluent = _BookValidationThroughFluent.Validate(book);
            if (!resultFluent.IsValid)
            {
                foreach (var failure in resultFluent.Errors)
                {
                    result.ErrorMessage.Add(failure.ErrorMessage);
                }
                result.StatusCode = 400;              
            }
            return result;
        }
    }
}
