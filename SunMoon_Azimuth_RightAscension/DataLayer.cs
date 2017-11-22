using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Sql;
using System.Data.SqlClient;

namespace SunMoon_Azimuth_RightAscension
{
    class DataLayer
    {
        SqlConnection connect = null;
        public DataLayer()
        {
            connect = new SqlConnection("Data Source = DESKTOP - N8GGAKM; Initial Catalog = ''Solar Tracking''; Integrated Security = True");
            Console.WriteLine("Connected");
        }

        public void CreateTable()
        {
            String queryString = "CREATE TABLE [dbo].[Movement]([X - value][varchar](50) NULL,[Y-value] [varchar] (50) NULL) ON[PRIMARY]";
            connect.Open();
            SqlCommand command = new SqlCommand(queryString, connect);
            command.Connection.Open();
            command.ExecuteNonQuery();
            connect.Close();

        }
        public void InsertData()
        {
            Senddata sd = new Senddata();
            String queryString = "INSERT INTO [dbo].[Movement]([X - value] ,[Y - value])VALUES(<"+sd.X+" - value, varchar(50),>,< "+sd.Y+" - value, varchar(50),>)";
            connect.Open();
            SqlCommand command = new SqlCommand(queryString, connect);
            command.Connection.Open();
            command.ExecuteNonQuery();
            command.Connection.Close();
            connect.Close();
        }
        

        
    }
}
