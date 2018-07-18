using ClassLibraryData.Data;
using System.Collections.Generic;

namespace ClassLibraryData.Abstract
{
    public interface IViewModelRepository
    {
        Dictionary<Book, List<Author>> BookAuthors { get; }
        void SaveAll(Book book, List<Author> authors);
        void RemoveAll(Book book, List<Author> authors);

        void SaveBook(Book book);
        void SaveAuthor(Author author);
    }
}
