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
        public static DataTable ExecSp(string spName, List<SqlParameter> sqlParams = null)
        {
            string strConnect = @"SERVER=192.168.0.101\SQLEXPRESS;DATABASE=MobileBanking;UID=user;PASSWORD=abc555abc;Trusted_Connection=no;";
            SqlConnection connection = new SqlConnection();
            DataTable daTable = new DataTable();
            try
            {
                connection = new SqlConnection(strConnect);
                connection.Open();
                SqlCommand command = new SqlCommand(spName, connection);
                command.CommandType = CommandType.StoredProcedure;
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