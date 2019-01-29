using ORM_INTERFACE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ORM
{
  public  class ORM<TT> : IORM<TT>
    {
		public Type TipGetir
        {
			get
            {
                return typeof(TT);
            }
        }


        public DataTable RunSELECT()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter($"{TipGetir.Name}SELECT",Tools.baglan);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.Fill(dt);
            return dt;
        }


        public bool RunINSERT(TT entity)
        {
            //SqlCommand komut = new SqlCommand($"{TipGetir.Name}INSERT",Tools.baglan);
            //komut.CommandType = CommandType.StoredProcedure;
            // PropertyInfo [] props=TipGetir.GetProperties();
            //foreach (PropertyInfo prop in props)
            //{
            //    komut.Parameters.AddWithValue($"@{prop.Name}", prop.GetValue(entity));
            //}
            //return Tools.RunExecuteBaby(komut);
            return Tools.InsertANDUpdate<TT>(entity, "INSERT");
         }

        public bool RunUPDATE(TT entity)
        {
            //SqlCommand komut = new SqlCommand($"@{TipGetir.Name}UPDATE", Tools.baglan);
            //komut.CommandType = CommandType.StoredProcedure;

            //PropertyInfo[] props = TipGetir.GetProperties();
            //foreach (PropertyInfo prop in props)
            //{

            //    komut.Parameters.AddWithValue($"@{prop.Name}", prop.GetValue(entity));

            //}
            //return Tools.RunExecuteBaby(komut);

            return Tools.InsertANDUpdate<TT>(entity, "UPDATE");
        }


        public bool RunDELETE(int id)
        {
            SqlCommand komut = new SqlCommand($"{TipGetir.Name}DELETE", Tools.baglan);
            komut.CommandType = CommandType.StoredProcedure;

            PropertyInfo [] props =TipGetir.GetProperties();

            foreach (PropertyInfo prop in props)
            {
                komut.Parameters.AddWithValue($"@{prop.Name}",id);
            }
            return Tools.RunExecuteBaby(komut);

        }

       
      
    }
}
