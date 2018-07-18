using ClassLibraryData.Abstract;
using ClassLibraryData.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryData.Concrete
{
    public class SQLBookRepository : IBookRepository
    {
        SqlWorker worker;
        public IEnumerable<Book> Books
        {
            get
            {
                return this.GetBooks();
            }
        }
        
        public SQLBookRepository()
        {
            this.worker = new SqlWorker(ConfigurationManager.ConnectionStrings["BookContext"].ConnectionString);
        }

        public List<Book> GetBooks()
        {
            string strCommand =
                $"SELECT * FROM Book";

            List<Book> lstBook = null;
            using (SqlConnection connection = new SqlConnection(this.worker.strConn))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(strCommand, connection);
                SqlDataReader reader = command.ExecuteReader();

                lstBook = new List<Book>();
                while (reader.Read())
                {
                    Book book = new Book
                    {
                        Id = int.Parse(reader["Id"].ToString()),
                        Name = reader["Name"].ToString(),
                        Desription = reader["Description"].ToString(),
                        Genre = reader["Genre"].ToString(),
                        Price = decimal.Parse(reader["Price"].ToString())
                    };
                    lstBook.Add(book);
                }
            }       

            return lstBook;
        }

        public int AddNewBook(Book book)
        {
            string strCommand =
                $"INSERT Book (Name, Description, Genre, Price) VALUES(N'{book.Name}', N'{book.Desription}', N'{book.Genre}', '{book.Price}')";
            int i = this.worker.ExecuteNonQuery(strCommand);
            return i;
        }

        public void UpdateBook(Book book)
        {
            Book temp = GetBookByName(book.Name);
            if (temp != null)
            {
                string strCommand =
                    $"UPDATE Book SET Name='{book.Name}', Description='{book.Desription}', Genre='{book.Genre}', Price='{book.Price}' WHERE Id='{temp.Id}'";
                this.worker.ExecuteNonQuery(strCommand);
            }
            else
            {
                this.AddNewBook(book);
            }            
        }


        public Book GetBookByName(string strName)
        {
            string strCommand =
                $"SELECT * FROM Book WHERE Name='{strName}'";

            Book book = null;
            using (SqlConnection connection = new SqlConnection(this.worker.strConn))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(strCommand, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    book = new Book
                    {
                        Id = int.Parse(reader["Id"].ToString()),
                        Name = reader["Name"].ToString(),
                        Desription = reader["Description"].ToString(),
                        Genre = reader["Genre"].ToString(),
                        Price = decimal.Parse(reader["Price"].ToString())
                    };
                }
            }

            return book;
        }
    }
}
