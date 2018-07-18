using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using ClassLibraryData.Abstract;
using ClassLibraryData.Data;

namespace ClassLibraryData.Concrete
{
    public class SQLViewModelBookAuthor : IViewModelRepository
    {
        SqlConnection connection = null;
        SqlDataAdapter adapter = null;

        private const string selectAll =
            "SELECT Book.Id, Book.Name, Book.Description, Book.Genre, Book.Price, Book.Quantity, Author.Id, Author.Name, Author.Surname, Author.SecondName FROM BookAuthor, Book, Author WHERE BookAuthor.IdBook = Book.Id AND BookAuthor.IdAuthor = Author.Id";
        private const string selectBook =
            "SELECT * FROM Book";
        private const string selectAuthor =
            "SELECT * FROM Author";
        private const string selectBookAuthor =
            "SELECT * FROM BookAuthor";

        public Dictionary<Book, List<Author>> BookAuthors
        {
            get
            {
                return GetAll();
            }
        }

        public SQLViewModelBookAuthor()
        {
            this.connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookContext"].ConnectionString);
            this.adapter = new SqlDataAdapter(selectAll, this.connection);
        }

        public Dictionary<Book, List<Author>> GetAll()
        {       
            Dictionary<Book, List<Author>> dict = new Dictionary<Book, List<Author>>();

            this.adapter.SelectCommand.CommandText = selectAll;     
            
            do
            {
                try
                {
                    DataSet ds = new DataSet();

                    this.adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];

                    for(int i = 0; i < dt.Rows.Count; i++)
                    {

                        Book book = GetBook(dt, i);

                        Author author = GetAuthor(dt, i);

                        if (dict.ContainsKey(book))
                        {
                            dict[book].Add(author);
                        }
                        else
                        {
                            List<Author> lst = new List<Author>
                            {
                                author
                            };
                            dict.Add(book, lst);
                        }
                    }
                    break;
                }
                catch (SqlException ex)
                {   }

            } while (true);

            return dict;
        }
        
        private void SetDependency(int idBook, int idAuthor)
        {
            this.adapter.InsertCommand = new SqlCommand("INSERT BookAuthor (IdBook, IdAuthor) VALUES(@idBook, @idAuthor)", this.connection);
            this.adapter.InsertCommand.Connection.Open();
            this.adapter.InsertCommand.Parameters.Add(new SqlParameter("@idBook", idBook));
            this.adapter.InsertCommand.Parameters.Add(new SqlParameter("@idAuthor", idAuthor));
            this.adapter.InsertCommand.ExecuteNonQuery();
            this.adapter.InsertCommand.Connection.Close();
        }

        public void DeleteDependency(int idBook, int idAuthor)
        {
            this.adapter.DeleteCommand = new SqlCommand("DELETE FROM BookAuthor WHERE IdBook=@idBook AND IdAuthor=@idAuthor", this.connection);
            this.adapter.DeleteCommand.Connection.Open();
            this.adapter.DeleteCommand.Parameters.Add(new SqlParameter("@idBook", idBook));
            this.adapter.DeleteCommand.Parameters.Add(new SqlParameter("@idAuthor", idAuthor));

            this.adapter.DeleteCommand.ExecuteNonQuery();
            this.adapter.DeleteCommand.Connection.Close();
        }

        private void UpdateDependency(int idBook, int idAuthor)
        {
            this.adapter.UpdateCommand = new SqlCommand("UPDATE BookAuthor SET IdBook=@idBook, IdAuthor=@idAuthor WHERE IdBook=@idBook AND IdAuthor=@idAuthor", this.connection);
            this.adapter.UpdateCommand.Connection.Open();
            this.adapter.UpdateCommand.Parameters.Add(new SqlParameter("@idBook", idBook));
            this.adapter.UpdateCommand.Parameters.Add(new SqlParameter("@idAuthor", idAuthor));   
            this.adapter.UpdateCommand.ExecuteNonQuery();
            this.adapter.UpdateCommand.Connection.Close();
        }

        public void UpdateBook(Book book)
        {
            this.adapter.UpdateCommand = new SqlCommand("UPDATE Book SET Name = @Name, Description = @Descript, Genre = @Genre, Price = @Price, Quantity = @Quantity WHERE Id = @Id", this.connection);
            this.adapter.UpdateCommand.Connection.Open();
            this.adapter.UpdateCommand.Parameters.Add(new SqlParameter("@Name", book.Name));
            this.adapter.UpdateCommand.Parameters.Add(new SqlParameter("@Descript", book.Desription));
            this.adapter.UpdateCommand.Parameters.Add(new SqlParameter("@Genre", book.Genre));
            this.adapter.UpdateCommand.Parameters.Add(new SqlParameter("@Price", book.Price));
            this.adapter.UpdateCommand.Parameters.Add(new SqlParameter("@Quantity", book.Quantity));
            this.adapter.UpdateCommand.Parameters.Add(new SqlParameter("@Id", book.Id));
            this.adapter.UpdateCommand.ExecuteNonQuery();
            this.adapter.UpdateCommand.Connection.Close();

        }


        public void DeleteAuthor(Author author)
        {
            this.adapter.DeleteCommand = new SqlCommand("DELETE FROM Author WHERE Id=@id", this.connection);
            this.adapter.DeleteCommand.Connection.Open();
            this.adapter.DeleteCommand.Parameters.Add(new SqlParameter("@id", author.Id));
            this.adapter.DeleteCommand.ExecuteNonQuery();
            this.adapter.DeleteCommand.Connection.Close();
        }

        public void DeleteBook(Book book)
        {
            this.adapter.DeleteCommand = new SqlCommand("DELETE FROM Book WHERE Id=@id", this.connection);
            this.adapter.DeleteCommand.Connection.Open();
            this.adapter.DeleteCommand.Parameters.Add(new SqlParameter("id", book.Id));
            this.adapter.DeleteCommand.ExecuteNonQuery();
            this.adapter.DeleteCommand.Connection.Close();
        }


        public void UpdateAuthor(Author author)
        {
            this.adapter.UpdateCommand = new SqlCommand("UPDATE Author SET Name=@Name, Surname=@Surname, SecondName=@Secondname WHERE Id=@Id", this.connection);
            this.adapter.UpdateCommand.Connection.Open();
            this.adapter.UpdateCommand.Parameters.Add(new SqlParameter("@Name", author.Name));
            this.adapter.UpdateCommand.Parameters.Add(new SqlParameter("@Surname", author.SurName));
            this.adapter.UpdateCommand.Parameters.Add(new SqlParameter("@Secondname", author.SecondName));
            this.adapter.UpdateCommand.Parameters.Add(new SqlParameter("@Id", author.Id));
            this.adapter.UpdateCommand.ExecuteNonQuery();
            this.adapter.UpdateCommand.Connection.Close();
        }

        public void AddBook(Book book)
        {
            this.adapter.InsertCommand = new SqlCommand("INSERT Book (Name, Description, Genre, Price, Quantity) VALUES(@name, @description, @genre, @price, @quantity)", this.connection);
            this.adapter.InsertCommand.Connection.Open();
            this.adapter.InsertCommand.Parameters.Add(new SqlParameter("name", book.Name));
            this.adapter.InsertCommand.Parameters.Add(new SqlParameter("description", book.Desription));
            this.adapter.InsertCommand.Parameters.Add(new SqlParameter("genre", book.Genre));
            this.adapter.InsertCommand.Parameters.Add(new SqlParameter("price", book.Price));
            this.adapter.InsertCommand.Parameters.Add(new SqlParameter("quantity", book.Quantity));
            this.adapter.InsertCommand.ExecuteNonQuery();
            this.adapter.InsertCommand.Connection.Close();
        }

        private void AddAuthor(Author author)
        {
            if (author.SecondName == null)
            {
                this.adapter.InsertCommand = new SqlCommand("INSERT Author (Name, Surname) VALUES(@name, @surname)", this.connection);

            }
            else
            {
                this.adapter.InsertCommand = new SqlCommand("INSERT Author (Name, Surname, SecondName) VALUES(@name, @surname, @secondname)", this.connection);
                this.adapter.InsertCommand.Parameters.Add(new SqlParameter("secondname", author.SecondName));
            }
            this.adapter.InsertCommand.Connection.Open();
            this.adapter.InsertCommand.Parameters.Add(new SqlParameter("name", author.Name));
            this.adapter.InsertCommand.Parameters.Add(new SqlParameter("surname", author.SurName));
            this.adapter.InsertCommand.ExecuteNonQuery();
            this.adapter.InsertCommand.Connection.Close();
        }
        
        private Book GetBook(DataTable dt, int row)
        {
            var cells = dt.Rows[row].ItemArray;
            
            Book book = new Book
            {
                Id = int.Parse(cells[0].ToString()),
                Name = cells[1].ToString(),
                Desription = cells[2].ToString(),
                Genre = cells[3].ToString(),
                Price = decimal.Parse(cells[4].ToString()),
                Quantity = int.Parse(cells[5].ToString())
            };

            return book;
        }
        private Author GetAuthor(DataTable dt, int row)
        {
            var cells = dt.Rows[row].ItemArray;

            Author author = new Author
            {
                Id = int.Parse(cells[6].ToString()),
                Name = cells[7].ToString(),
                SurName = cells[8].ToString(),
                SecondName = cells[9].ToString()
            };

            return author;
        }

        public void RemoveAll(Book book, List<Author> authors)
        {
            int iBook = GetBookID(book.Name);
            
            foreach (var author in authors)
            {
                try
                {
                    int iAuthor = GetAuthorID(author.Name, author.SurName, author.SecondName);
                    this.DeleteDependency(iBook, iAuthor);
                    this.DeleteAuthor(author);
                }
                catch (System.Exception)
                {
                    
                }
            }
            this.DeleteBook(book);
        }

        public void SaveBook(Book book)
        {
            if(book.Id  == 0)
            {
                this.AddBook(book);
            }
            else
            {
                this.UpdateBook(book);
            }
        }

        public void SaveAll(Book book, List<Author>authors)
        {
            if (book.Id == 0)
            {
                this.AddBook(book);                
            }
            else
            {
                this.UpdateBook(book);
            }

            foreach (var author in authors)
            {
                if (author.Id == 0)
                {
                    this.AddAuthor(author);

                    int iAuthor = GetAuthorID(author.Name, author.SurName, author.SecondName);
                    int iBook = GetBookID(book.Name);
                    SetDependency(iBook,iAuthor);
                }
                else
                {
                    this.UpdateAuthor(author);
                    int iAuthor = GetAuthorID(author.Name, author.SurName, author.SecondName);
                    int iBook = GetBookID(book.Name);
                    this.UpdateDependency(iBook, iAuthor);
                }
            }
        }       

        public int GetBookID(string strName)
        {
            string strCommand =
                $"SELECT Id FROM Book WHERE Name='{strName}'";


            this.adapter.SelectCommand.CommandText = strCommand;
            DataSet ds = new DataSet();
            this.adapter.Fill(ds);
            DataTable dt = ds.Tables[0];
            var cells = dt.Rows[0].ItemArray;
            int id = (int)cells[0];
            return id;
        }

        public int GetAuthorID(string strName, string strSurname, string strSecondname)
        {
            string strCommand = "";

            if (strSecondname != null)
            {
                strCommand =
                            $"SELECT Id FROM Author WHERE Name='{strName}' AND Surname='{strSurname}' AND SecondName='{strSecondname}'";
            }
            else
            {
                strCommand =
                             $"SELECT Id FROM Author WHERE Name='{strName}' AND Surname='{strSurname}'";

            }

            this.adapter.SelectCommand.CommandText = strCommand;
            DataSet ds = new DataSet();
            this.adapter.Fill(ds);
            DataTable dt = ds.Tables[0];
            
            var cells = dt.Rows[0].ItemArray;
            int id = (int)cells[0];
            return id;
        }

        public void SaveAuthor(Author author)
        {
            if (author.Id == 0)
            {
                this.AddAuthor(author);                
            }
            else
            {
                this.UpdateAuthor(author);                
            }
        }
    }
}
