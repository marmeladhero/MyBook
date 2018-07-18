using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryData
{
    class SqlWorker
    {
        public string strConn { get; private set; }

        public SqlWorker(string strConn)
        {
            this.strConn = strConn;
        }
           


        public SqlDataReader Execute(string strCommand)
        {
            SqlDataReader reader = null;

            using (SqlConnection connection = new SqlConnection(this.strConn))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(strCommand, connection);
                reader = command.ExecuteReader();
            }

            return reader;
        }

        public DataTable GetTable(string strCommand)
        {
            DataTable dt = null;
            using (SqlConnection connection = new SqlConnection(this.strConn))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(strCommand, connection);
                adapter.Fill(dt);
            }
            return dt;
        }
        
        public int ExecuteNonQuery(string strCommand)
        {
            int i = -1;
            using (SqlConnection connection = new SqlConnection(this.strConn))
            {
                connection.Open();

                using (SqlCommand comm = new SqlCommand(strCommand, connection))
                {
                    i = comm.ExecuteNonQuery();
                }
            }

            return i;
        }
    }
}
