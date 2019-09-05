using DAL;
using DAL.Model;
using System;
using System.Linq;

namespace ServiceLayer
{
    public class Services
    {
        private readonly BookRepository _BookRepository;

        public Services(BookRepository bookRepository)
        {
            _BookRepository = bookRepository;
        }


        public Result GetBooks()
        {
            var books = _BookRepository.GetAllBooks();
            Result result = new Result();
            if (books.Count == 0)
            {

                result.ErrorMessage = "No Books Found";
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
        public Result GetBookNameById(int id)
        {
            Result result = new Result();
            if(id<0)
            {
                result.ErrorMessage = "Invalid Id, Id should be a positive number";
                result.StatusCode = 400;
                return result;
            }
            var book = _BookRepository.GetById(id);
            if (book == null)
            {
                result.ErrorMessage = "ID Not Found";
                result.StatusCode = 404;
                return result;
            }
            else
            {
                result.Value = book;
                return result;
            }
                


        }

        public bool Update(Book book)
        {
            return _BookRepository.ReplaceEmployee(book);
        }

        public Result AddBook(Book book)
        {
            Result result = new Result();
            if (!book.Name.All(X => char.IsLetter(X) || X == ' ' || X == '.') || !book.Author.All(X => char.IsLetter(X) || X == ' ' || X == '.') || !book.Category.All(X => char.IsLetter(X) || X == ' ' || X == '.'))
            {
                result.ErrorMessage = "Name, Category and Author: should contain only alphabets.";
                result.StatusCode = 400;
                return result;
            }
            else if (book.Id < 0)
            {
                result.ErrorMessage = "Id: should be a positive integer.";
                result.StatusCode = 400;
                return result;
            }
                
            else if (book.Price < 0)
            {
                result.ErrorMessage = "Price: should be a positive number.";
                result.StatusCode = 400;
                return result;
            }
             
            var status = _BookRepository.AddBook(book);
            if (status == true)
            {

                return result;
            }
               
            else
            {
                result.StatusCode = 404;
                result.ErrorMessage = "book already exist";
                return result;
            }
                
        }
    }
}
