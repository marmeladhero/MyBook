using ClassLibraryData.Data;
using System.Collections.Generic;

namespace ClassLibraryData.Abstract
{
    public interface IBookRepository
    {
        IEnumerable<Book> Books { get; }
    }
}
