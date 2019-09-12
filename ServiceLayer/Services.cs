using DAL;
using DAL.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using ServiceStack.Redis;

namespace ServiceLayer
{
    public class Services:IServices
    {
        private readonly IBookRepository _BookRepository;
        private readonly BookValidationThroughFluent _BookValidationThroughFluent;
        private readonly List<Logger> _Loggers;
        RedisManagerPool manager = new RedisManagerPool("localhost:6379");
        IRedisClient Client;
        public Services(IBookRepository bookRepository)
        {
            _BookRepository = bookRepository;
            _BookValidationThroughFluent = new BookValidationThroughFluent();
            _Loggers = new List<Logger>();
   
            Client = manager.GetClient();
            Client.FlushAll();
           


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
                Logger logger = new Logger { Errors=result.ErrorMessage , Time= DateTime.Now.ToString() , Event="GetBooks" , StatusCode=result.StatusCode};
                _Loggers.Add(logger);
                
            }
            else
            {
                result.StatusCode = 200;
                result.Value = books;
                Logger logger = new Logger { Errors = result.ErrorMessage, Time = DateTime.Now.ToString(), Event = "GetBooks", StatusCode = result.StatusCode };
                _Loggers.Add(logger);
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
                Logger loggerGetBook = new Logger { Errors = result.ErrorMessage, Time = DateTime.Now.ToString(), Event = $"GetBookById{id}", StatusCode = result.StatusCode };
                _Loggers.Add(loggerGetBook);
                return result;
            }
            
            
                Logger logger1;
                Book book;
                if (Client.Get<Book>(id.ToString()) != null)
                {
                    book = Client.Get<Book>(id.ToString());
                    logger1 = new Logger { Errors = result.ErrorMessage, Time = DateTime.Now.ToString(), Event = $"Retrieved from cache", StatusCode = result.StatusCode };
                    _Loggers.Add(logger1);

                }
                else
                {
                    
                     book = _BookRepository.GetById(id);
                    if (book == null)
                    {
                        result.ErrorMessage.Add("ID Not Found");
                        result.StatusCode = 404;

                    }
                    else
                    {
                        Client.Set(book.Id.ToString(), book);
                        logger1 = new Logger { Errors = result.ErrorMessage, Time = DateTime.Now.ToString(), Event = $"Added in cache", StatusCode = result.StatusCode };
                        _Loggers.Add(logger1);
                    }
                }
                result.Value = book;
                
            
            Logger logger = new Logger { Errors = result.ErrorMessage, Time = DateTime.Now.ToString(), Event = $"GetBookById{id}", StatusCode = result.StatusCode };
            _Loggers.Add(logger);
            return result;


        }
        public Result DeleteBookById(int id)
        {
            Result result = new Result();
            if (id < 0)
            {
                result.ErrorMessage.Add("Invalid Id, Id should be a positive number");
                result.StatusCode = 400;
                Logger loggerBook = new Logger { Errors = result.ErrorMessage, Time = DateTime.Now.ToString(), Event = $"DeleteBookById{id}", StatusCode = result.StatusCode };
                _Loggers.Add(loggerBook);
                return result;
            }
            var book = _BookRepository.DeleteById(id);
            if (book == false)
            {
                
                result.ErrorMessage.Add("ID Not Found");
                result.StatusCode = 404;
                
            }
            else
            {

                Client.Remove(id.ToString());
                result.Value = "Deleted";
                
            }
            Logger logger = new Logger { Errors = result.ErrorMessage, Time = DateTime.Now.ToString(), Event = $"DeleteBookById{id}", StatusCode = result.StatusCode };
            _Loggers.Add(logger);
            return result;
        }

        public Result Update(Book book)
        {
            Result result;
            result = Validate(book);

            if (result.ErrorMessage.Count != 0)
            {
                
                return result;
            }
            if( _BookRepository.ReplaceBook(book))
            {
                Client.Remove(book.Id.ToString());
                Client.Set(book.Id.ToString(), book);
                result.Value = "Succes";
            }
            else
            {
                result.ErrorMessage.Add("No Id Match");
                result.StatusCode = 404;
            }
            Logger loggerUpdate = new Logger { Errors = result.ErrorMessage, Time = DateTime.Now.ToString(), Event = "UpdateBook", StatusCode = result.StatusCode };
            _Loggers.Add(loggerUpdate);
            return result;
        }

        

        public Result AddBook(Book book)
        {
            Result result;
            result = Validate(book);

            if (result.ErrorMessage.Count != 0)
            {
                Logger logger = new Logger { Errors = result.ErrorMessage, Time = DateTime.Now.ToString(), Event = "AddBook", StatusCode = result.StatusCode };
                _Loggers.Add(logger);
                return result;
            }
            var status = _BookRepository.AddBook(book);
            if (status == true)
            {
               
                
                result.Value = "Success";  
            }
               
            else
            {
                result.StatusCode = 404;
                result.ErrorMessage.Add("book Id already exist");
            }
            Logger loggerBook = new Logger { Errors = result.ErrorMessage, Time = DateTime.Now.ToString(), Event = "AddBook", StatusCode = result.StatusCode };
            _Loggers.Add(loggerBook);
            return result;

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
        public object GetLoggers()
        {
            FileStream file = new FileStream("Logger.Txt", FileMode.Create, FileAccess.Write);
            StreamWriter streamWriter = new StreamWriter(file);
            foreach (var logger in _Loggers)
            {
                streamWriter.WriteLine("Time:    "+logger.Time);
                if(logger.Errors.Count>0)
                    streamWriter.WriteLine("Errors:");
                var LogValue = "";
                foreach (var item in logger.Errors)
                {
                    LogValue += item + ",    ";
                }
                if(LogValue.Length>0)
                    streamWriter.WriteLine(LogValue);
                streamWriter.WriteLine("Event:     "+logger.Event);
                streamWriter.WriteLine("StatusCode:   "+logger.StatusCode.ToString());
                LogValue = "-------------------------------------------------------------\n";
                streamWriter.WriteLine(LogValue);
            }
            streamWriter.Flush();
            streamWriter.Close();
            file.Close();
            return _Loggers;
        }
    }
}
