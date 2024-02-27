using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisStatsBL.MODEL;
using VisStatsBL.interfaces;
using Microsoft.Data.SqlClient;
using System.Data;


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
            string SQL = "SELECT count(*) FROM haven WHERE stad = @stad";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = SQL;
                cmd.Parameters.Add(new SqlParameter("@stad", System.Data.SqlDbType.NVarChar));
                cmd.Parameters["@stad"].Value = haven.Stad;
                int n = (int)cmd.ExecuteScalar();
                if (n > 0) return true; else return false;

            }
        }

        public void SchrijfHaven(Haven haven)
        {
            string SQL = "INSERT INTO haven(stad) VALUES(@Stad) ";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = SQL;
                cmd.Parameters.Add(new SqlParameter("@stad", System.Data.SqlDbType.NVarChar));
                cmd.Parameters["@stad"].Value = haven.Stad;
                cmd.ExecuteNonQuery();
            }
        }
        public bool IsOpgeladen(string fileName)
        {
            throw new NotImplementedException();
        }
        public void SchrijfStatiestieken(List<VisStatsDataRecord> data, string fileName)
        {
            throw new NotImplementedException();
        }
        public List<Haven> LeesHavens()
        {
            throw new NotImplementedException();
        }
        public List<Vissoort> LeesVissoorten()
        {
            string SQL = "SELECT * FROM Soort";
            List<Vissoort> soorten = new List<Vissoort>();
            using(SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = SQL;
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        soorten.Add(new Vissoort((int)reader["id"], (string)reader["naam"]));
                    }
                    return soorten;
                }
                catch(Exception ex) { throw new Exception("leessoorten", ex); }
            }

        }
    }
}
