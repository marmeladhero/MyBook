using ClassLibraryData.Data;
using System.Collections.Generic;

namespace WebApplication.Models
{
    public class ViewModelBookAuthor
    {
        public Book bBook { set; get; }
        public List<Author> LstAuthors { set; get; }
        public string CurrentGenre { set; get; }

        public ViewModelBookAuthor()
        {

        }

        public ViewModelBookAuthor(KeyValuePair<Book, List<Author>> lst)
        {
            this.bBook = lst.Key;
            this.LstAuthors = lst.Value;
        }
    }
}