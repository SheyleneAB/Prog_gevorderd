using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisStatsBL.MODEL;
using VisStatsBL.interfaces;
using Microsoft.Data.SqlClient;


namespace Visstat_SQL
{
    public class VisStatRepository : IVisStatRepository
    {
        private string connectionString;

        public VisStatRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public bool HeeftVissoort(Vissoort vissoort)
        {
            string SQL = "SELECT count(*) FROM soort WHERE naam = @naam";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = SQL;
                cmd.Parameters.Add(new SqlParameter("@naam", System.Data.SqlDbType.NVarChar));
                cmd.Parameters["@naam"].Value = vissoort.Naam;
                int n = (int)cmd.ExecuteScalar();
                if (n > 0) return true; else return false;

            }
        }

        public void SchrijfVissoort( Vissoort vissoort )
        {
            string SQL = "INSERT INTO Soort(naam) VALUES(@naam) ";
            using(SqlConnection conn= new SqlConnection(connectionString))
            using(SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = SQL;
                cmd.Parameters.Add(new SqlParameter("@naam", System.Data.SqlDbType.NVarChar));
                cmd.Parameters["@naam"].Value = vissoort.Naam;
                cmd.ExecuteNonQuery();
            }
        }

        public bool HeeftHaven(Haven haven)
        {
            string SQL = "SELECT count(*) FROM soort WHERE naam = @naam";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = SQL;
                cmd.Parameters.Add(new SqlParameter("@naam", System.Data.SqlDbType.NVarChar));
                cmd.Parameters["@naam"].Value = haven.Naam;
                int n = (int)cmd.ExecuteScalar();
                if (n > 0) return true; else return false;

            }
        }

        public void SchrijfHaven(Haven haven)
        {
            string SQL = "INSERT INTO Soort(naam) VALUES(@naam) ";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = SQL;
                cmd.Parameters.Add(new SqlParameter("@naam", System.Data.SqlDbType.NVarChar));
                cmd.Parameters["@naam"].Value = haven.Naam;
                cmd.ExecuteNonQuery();
            }
        }
    }
}
