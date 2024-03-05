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
            string SQL = "SELECT count(*) FROM upload WHERE filename = @filename";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = SQL;
                    cmd.Parameters.AddWithValue("@filename", fileName.Substring(fileName.LastIndexOf("\\") +1));
                    int n = (int)cmd.ExecuteScalar();
                    if (n > 0) return true; else return false;
                    
                }
                catch (Exception ex)
                {
                    throw new Exception("IsOpgeladen", ex);
                }
            }
        }
        public void SchrijfStatiestieken(List<VisStatsDataRecord> data, string fileName)
        {
            string SQLdata = "INSERT INTO VisStats(jaar, maand, haven_id, soort_id, gewicht, waarde) VALUES(@jaar, @maand, @haven_id, @soort_id, @gewicht, @waarde)";
            string SQLupload = "INSERT INTO upload(filename, datum, pad) VALUES (@filename, @datum, @pad)";
            using (SqlConnection conn = new SqlConnection(connectionString)) 
            using(SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    //schrijven data
                    cmd.CommandText = SQLdata;
                    cmd.Transaction = conn.BeginTransaction();
                    cmd.Parameters.Add(new SqlParameter("@jaar", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@maand", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@haven_id", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@soort_id", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@gewicht", SqlDbType.Float));
                    cmd.Parameters.Add(new SqlParameter("@waarde", SqlDbType.Float));
                    foreach (VisStatsDataRecord dataRecord in data)
                    {
                        cmd.Parameters["@jaar"].Value = dataRecord.Jaar;
                        cmd.Parameters["@maand"].Value = dataRecord.Maand;
                        cmd.Parameters["@haven_id"].Value = dataRecord.Haven;
                        cmd.Parameters["@soort_id"].Value = dataRecord.Vissoort;
                        cmd.Parameters["@gewicht"].Value = dataRecord.Gewicht;
                        cmd.Parameters["@waarde"].Value = dataRecord.Jaar;
                        cmd.ExecuteNonQuery();
                    }
                    //schrijven upload
                    cmd.CommandText = SQLupload;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@filename", fileName.Substring(fileName.LastIndexOf("\\")+1));
                    cmd.Parameters.AddWithValue("@pad", fileName.Substring(fileName.LastIndexOf("\\")+1));
                    cmd.Parameters.AddWithValue("@datum", DateTime.Now);
                    cmd.ExecuteNonQuery();
                    cmd.Transaction.Commit();

                }
                catch (Exception ex)
                {
                    cmd.Transaction.Rollback();
                    throw new Exception("SchrijfStatiestieken", ex);
                }
            }


        }
        public List<Haven> LeesHavens()
        {
            string SQL = "SELECT * FROM haven";
            List<Haven> Havens = new List<Haven>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = SQL;
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Havens.Add(new Haven((int)reader["id"], (string)reader["naam"]));
                    }
                    return Havens;
                }
                catch (Exception ex) { throw new Exception("leessoorten", ex); }
            }
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
        public List<int> LeesJaartallen()
        {
            string SQL = "SELECT DISTINCT jaar FROM visStats";
            List<int> jaren = new List<int>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = SQL;
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        jaren.Add((int)reader["jaar"]);
                    }
                    return jaren;
                }
                catch (Exception ex) { throw new Exception("leesjaren", ex); }
            }
        }
    }
}
