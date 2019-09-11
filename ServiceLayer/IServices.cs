using DAL.Model;

namespace ServiceLayer
{
    public interface IServices
    {
        Result GetBooks();
        Result GetBookById(int id);
        object GetLoggers();
        Result Validate(Book book);
        Result AddBook(Book book);
        Result Update(Book book);
        Result DeleteBookById(int id);
    }
}
