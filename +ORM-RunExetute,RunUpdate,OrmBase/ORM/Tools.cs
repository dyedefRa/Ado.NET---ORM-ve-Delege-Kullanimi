using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Reflection;

namespace ORM
{
    static class Tools
    {
        private static SqlConnection _baglan = new SqlConnection(ConfigurationManager.ConnectionStrings["baglantiKablosu"].ConnectionString);

        public static SqlConnection baglan
        {
            get { return _baglan; }
            set { _baglan = value; }
        }

        public static bool RunExecuteBaby(SqlCommand komut)
        {
            try
            {
                if (komut.Connection.State!=ConnectionState.Open)
                {
                    komut.Connection.Open();

                }

                return komut.ExecuteNonQuery() > 0;
                
            }
            catch 
            {

                return false;
            }
            finally
            {
                if (komut.Connection.State!=ConnectionState.Closed)
                {
                    komut.Connection.Close();

                }
            }
        }

        public static bool InsertANDUpdate<T>(T entity,string iORu)
      {
            Type tip = typeof(T);

            SqlCommand komut = new SqlCommand($"{tip.Name}{iORu}", Tools.baglan);
            komut.CommandType = CommandType.StoredProcedure;
            PropertyInfo[] props = tip.GetProperties();
            foreach (PropertyInfo prop in props)
            {
                komut.Parameters.AddWithValue($"@{prop.Name}", prop.GetValue(entity));
            }
            return Tools.RunExecuteBaby(komut);
        }
    }
}
