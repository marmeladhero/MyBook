using ClassLibraryData.Data;
using System.Collections.Generic;

namespace ClassLibraryData.Abstract
{
    public interface IAuthorsRepository
    {
        IEnumerable<Author> authors { get; }
    }
}
