using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisStatsBL.MODEL;
using VisStatsBL.interfaces;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection.PortableExecutable;


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
                cmd.Parameters.Add(new SqlParameter("@Naam", System.Data.SqlDbType.VarChar));
                cmd.Parameters["@Naam"].Value = vissoort.Naam;
                int n = (int)cmd.ExecuteScalar();
                if (n > 0) return true; else return false;

            }
        }

        public void SchrijfVissoort(Vissoort vissoort)
        {
            string SQL = "INSERT INTO Soort(naam) VALUES(@naam) ";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
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
                cmd.Parameters.Add(new SqlParameter("@Stad", System.Data.SqlDbType.NVarChar));
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
                    cmd.Parameters.AddWithValue("@filename", fileName.Substring(fileName.LastIndexOf("\\") + 1));
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
            using (SqlCommand cmd = conn.CreateCommand())
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
                        cmd.Parameters["@haven_id"].Value = dataRecord.Haven.Id;
                        cmd.Parameters["@soort_id"].Value = dataRecord.Vissoort.Id;
                        cmd.Parameters["@gewicht"].Value = dataRecord.Gewicht;
                        cmd.Parameters["@waarde"].Value = dataRecord.Jaar;
                        cmd.ExecuteNonQuery();
                    }
                    //schrijven upload
                    cmd.CommandText = SQLupload;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@filename", fileName.Substring(fileName.LastIndexOf("\\") + 1));
                    cmd.Parameters.AddWithValue("@pad", fileName.Substring(fileName.LastIndexOf("\\") + 1));
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
                        Havens.Add(new Haven((int)reader["Id"], (string)reader["Stad"]));
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
                        soorten.Add(new Vissoort((int)reader["id"], (string)reader["naam"]));
                    }
                    return soorten;
                }
                catch (Exception ex) { throw new Exception("leessoorten", ex); }
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

        public List<Jaarvangst> LeesStatistieken(int jaar, Haven haven, List<Vissoort> vissoorten, Eenheid eenheid)
        {
            string kolom = "";
            switch (eenheid)
            {
                case Eenheid.kg: kolom = "gewicht"; break;
                case Eenheid.euro: kolom = "waarde"; break;
            }
            string paramSoorten = "";
            for (int i = 0; i < vissoorten.Count; i++) paramSoorten += $"@ps{i} ,";
            paramSoorten = paramSoorten.Remove(paramSoorten.Length - 1);
            string SQL = $"SELECT soort_id, t2.naam soortnaam, jaar, sum({kolom}) totaal, min({kolom}) minimum, max({kolom}) maximum, avg({kolom}) gemiddelde" +
                $" FROM VisStats t1 left join soort t2 on t1.soort_id = t2.id " +
                $"WHERE jaar = @jaar and soort_id IN({paramSoorten}) and haven_id = @haven_id group by t2.naam, jaar, soort_id";
            List<Jaarvangst> vangst = new();
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = SQL;
                    cmd.Parameters.AddWithValue("@jaar", jaar);
                    cmd.Parameters.AddWithValue("@haven_id", haven.Id);
                    for (int i = 0; i < vissoorten.Count; i++)
                    {
                        cmd.Parameters.AddWithValue($"@ps{i}", vissoorten[i].Id);

                    }
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        vangst.Add(new Jaarvangst((string)reader["soortnaam"], (double)reader["totaal"], (double)reader["minimum"],
                            (double)reader["maximum"], (double)reader["gemiddelde"]));
                    }
                    return vangst;
                }
                catch (Exception ex) { throw new Exception("LeesStatistieken", ex); }
            }
        }
        public List<Maandvangst> LeesMaandStatistieken(List<int> jaren, List<Haven> havens, Vissoort vissoort, Eenheid eenheid) 
        {
           
                string kolom = "";
                switch (eenheid)
                {
                    case Eenheid.kg: kolom = "gewicht"; break;
                    case Eenheid.euro: kolom = "waarde"; break;
                }

            string StringHavens = "";
            for (int i = 0; i < havens.Count; i++)
            {
                StringHavens += $" Stad = '{havens[i].Stad}' ";
                if(i != havens.Count - 1)
                {
                    StringHavens += "OR";
                }
            }
            string StringJaren = "";
            for (int i = 0; i < jaren.Count; i++)
            {
                StringJaren += $" jaar = {jaren[i]} ";
                if (i != jaren.Count - 1)
                {
                    StringJaren += "OR";
                }
            }

            string ParamJaren = "";
            for (int i = 0; i < jaren.Count; i++) ParamJaren += $"@ps{i} ,";
            ParamJaren = ParamJaren.Remove(ParamJaren.Length - 1);

            string SQL = $@"SELECT jaar jaartal, maand maandgetal, SUM({kolom}) AS totaal
                            FROM VisStats t1 WHERE soort_id = @soort_id
                            AND haven_id IN (SELECT Id FROM Haven WHERE {StringHavens})
                            AND jaar IN (SELECT DISTINCT jaar FROM VisStats WHERE {StringJaren})
                            GROUP BY jaar, maand
                            ORDER BY jaar, maand;";

            List<Maandvangst> vangst = new List<Maandvangst>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                    try
                    {
                        conn.Open();

                    // Maak een lijst van parameter-namen voor jaren
                   

                        cmd.CommandText = SQL;
                        cmd.Parameters.AddWithValue("@soort_id", vissoort.Id);
                        

                        for (int i = 0; i < jaren.Count; i++)
                        {
                            cmd.Parameters.AddWithValue($"@ps{i}", jaren[i]);
                        }

                        IDataReader reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                vangst.Add(new Maandvangst(
                                    (int)reader["jaartal"],
                                    (int)reader["maandgetal"],
                                    (double)reader["totaal"]
                                ));
                            }

                        return vangst;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("LeesMaandStatistieken", ex);
                    }
            }
        }
    }
}

