using DAL.Model;
using System;
using System.Collections.Generic;

namespace DAL
{
    public class BookRepository
    {
        public List<Book> books;
        public BookRepository()
        {
            books = new List<Book>();

        }

        public bool AddBook(Book book)
        {
            
                var _employee = books.Find(X => X.Id == book.Id);
                if (_employee == null)
                {
                    books.Add(book);
                    return true;
                }
                else
                    return false;            
        }
        public bool ReplaceBook( Book book)
        {
            var _book = books.Find(X => X.Id ==book.Id);
            if (_book == null)
            {
                return false;
            }
            else
            {
                var index = books.IndexOf(_book);
                books[index] = book;
                
                return true;
            }
        }

        public Book GetById(int id)
        {
            var _book = books.Find(X => X.Id == id);
            if (_book == null)
                return null;
            return _book;
        }

        public List<Book> GetAllBooks()
        {
            return books;
        }

        public bool DeleteById(int id)
        {
            var _book = books.Find(X => X.Id == id);
            if (_book == null)
            {
                return false;
            }
            else
            {
                var index = books.IndexOf(_book);
                books.RemoveAt(index);

                return true;
            }
                
            
        }
    }
}
