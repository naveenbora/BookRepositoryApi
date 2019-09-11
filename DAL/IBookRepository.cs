using DAL.Model;
using System.Collections.Generic;

namespace DAL
{
    public interface IBookRepository
    {
        bool AddBook(Book book);
        bool ReplaceBook(Book book);
        Book GetById(int id);
        List<Book> GetAllBooks();
        bool DeleteById(int id);



    }
}
