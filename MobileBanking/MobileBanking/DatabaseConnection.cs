using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace MobileBanking
{
    public static class DatabaseConnection
    {
        private const string connectionString = @"SERVER=5.12.236.72,1433\SQLEXPRESS;DATABASE=MobileBanking;UID=user;PASSWORD=abc555abc;Trusted_Connection=no;";

        public static DataTable ExecSp(string spName, List<SqlParameter> sqlParams = null)
        {
            string strConnect = connectionString;
            SqlConnection connection = new SqlConnection();
            DataTable daTable = new DataTable();
            try
            {
                connection = new SqlConnection(strConnect);
                connection.Open();
                SqlCommand command = new SqlCommand(spName, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddRange(sqlParams.ToArray());
                SqlCommand command2 = connection.CreateCommand();
                SqlDataReader dataReader = command.ExecuteReader();
                daTable.Load(dataReader);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            return daTable;
        }
    }
}