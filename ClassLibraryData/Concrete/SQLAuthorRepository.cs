using ClassLibraryData.Abstract;
using ClassLibraryData.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace ClassLibraryData.Concrete
{
    public class SQLAuthorRepository : IAuthorsRepository
    {
        SqlWorker worker;

        public IEnumerable<Author> authors
        {
            get
            {
                return this.GetAuthors();
            }
        }

        public SQLAuthorRepository()
        {
            this.worker = new SqlWorker(ConfigurationManager.ConnectionStrings["BookContext"].ConnectionString);
        }

        public List<Author> GetAuthors()
        {
            string strCommand =
                $"SELECT * FROM Author";

            List<Author> lstAuthors = null;

            SqlDataReader reader = this.worker.Execute(strCommand);
            lstAuthors = new List<Author>();
            while (reader.Read())
            {
                Author author = new Author
                {
                    Id = int.Parse(reader["Id"].ToString()),
                    Name = reader["Name"].ToString(),
                    SurName = reader["Surname"].ToString(),
                    SecondName = reader["SecondName"].ToString()
                };
                lstAuthors.Add(author);
            }

            return lstAuthors;
        }

        public int AddNewAuthor(Author author)
        {
            string strCommand =
                $"INSERT Author (Name, Surname, SecondName) VALUES(N'{author.Name}', N'{author.SurName}', N'{author.SecondName}')";
            int i = this.worker.ExecuteNonQuery(strCommand);
            return i;
        }

        public void UpdateBook(Author author)
        {
            Author temp = GetAuthorByFullName(author.Name,author.SurName, author.SecondName);
            if (temp != null)
            {
                string strCommand =
                    $"UPDATE Author SET Name='{author.Name}', Surname='{author.SurName}', SecondName='{author.SecondName}' WHERE Id='{temp.Id}'";
                this.worker.ExecuteNonQuery(strCommand);
            }
            else
            {
                this.AddNewAuthor(author);
            }
        }


        public Author GetAuthorByFullName(string strName, string strSurname, string strSecondname)
        {
            string strCommand =
                $"SELECT * FROM Author WHERE Name='{strName}' AND Surname='{strSurname}' AND SecondName='{strSecondname}'";

            Author author = null;

            SqlDataReader reader = this.worker.Execute(strCommand);

            while (reader.Read())
            {
                author = new Author
                {
                    Id = int.Parse(reader["Id"].ToString()),
                    Name = reader["Name"].ToString(),
                    SurName = reader["Surname"].ToString(),
                    SecondName = reader["SecondName"].ToString()
                };
            }

            return author;
        }

    }
}
