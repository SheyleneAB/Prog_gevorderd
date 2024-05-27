using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC_BL.Model;
using TC_BL.Interfaces;
using System.Numerics;
using System.Data;

namespace TC_SQL
{
    public class TCRepository : ITCRepository
    {
        private string connectionString;

        public TCRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public bool HeeftKlant(Klant klant)
        {
            string SQL = "SELECT count(*) from Klant WHERE naam = @Naam and id = @Id and adres = @Adres";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = SQL;
                cmd.Parameters.Add(new SqlParameter("@Naam", System.Data.SqlDbType.VarChar));
                cmd.Parameters["@Naam"].Value = klant.Naam;
                cmd.Parameters.Add(new SqlParameter("@Id", System.Data.SqlDbType.Int));
                cmd.Parameters["@Id"].Value = klant.Id;
                cmd.Parameters.Add(new SqlParameter("@Adres",System.Data.SqlDbType.VarChar));
                cmd.Parameters["@Adres"].Value = klant.Adres;
                int n = (int)cmd.ExecuteScalar();
                if (n > 0) return true; else return false;

            }
        }

        public bool HeeftProduct(Product product)
        {
            string SQL = "SELECT count(*) from Product WHERE id = @Id and nednaam = @Nednaam and wetnaam = @Wetnaam" +
                " and beschrijving = @Beschrijving and prijs = @Prijs";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = SQL;
                cmd.Parameters.Add(new SqlParameter("@Id", System.Data.SqlDbType.Int));
                cmd.Parameters["@Id"].Value = product.Id;
                cmd.Parameters.Add(new SqlParameter("@Nednaam", System.Data.SqlDbType.NVarChar));
                cmd.Parameters["@Nednaam"].Value = product.Nednaam;
                cmd.Parameters.Add(new SqlParameter("@Wetnaam", System.Data.SqlDbType.NVarChar));
                cmd.Parameters["@Wetnaam"].Value = product.Wetnaam;
                cmd.Parameters.Add(new SqlParameter("@Beschrijving", System.Data.SqlDbType.NVarChar));
                cmd.Parameters["@Beschrijving"].Value = product.Beschrijving;
                cmd.Parameters.Add(new SqlParameter("@Prijs", System.Data.SqlDbType.Float));
                cmd.Parameters["@Prijs"].Value = product.Prijs;
                int n = (int)cmd.ExecuteScalar();
                if (n > 0) return true; else return false;

            }
        }

        public void SchrijfKlant(Klant klant)
        {
            string SQL = "INSERT INTO Klant(naam, id, adres) VALUES(@Naam, @Id, @Adres)";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = SQL;
                cmd.Parameters.Add(new SqlParameter("@Naam", System.Data.SqlDbType.NVarChar));
                cmd.Parameters["@Naam"].Value = klant.Naam;
                cmd.Parameters.Add(new SqlParameter("@Id", System.Data.SqlDbType.Int));
                cmd.Parameters["@Id"].Value = klant.Id;
                cmd.Parameters.Add(new SqlParameter("@Adres", System.Data.SqlDbType.NVarChar));
                cmd.Parameters["@Adres"].Value = klant.Adres;
                cmd.ExecuteNonQuery();
            }
           
        }

        public void SchrijfProduct(Product product)
        {
            string SQL = "INSERT INTO Product( id, nednaam, wetnaam, beschrijving, prijs) VALUES( @Id, @Nednaam, " +
                "@Wetnaam, @Beschrijving, @Prijs)";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = SQL;
                cmd.Parameters.Add(new SqlParameter("@Id", System.Data.SqlDbType.Int));
                cmd.Parameters["@Id"].Value = product.Id;
                cmd.Parameters.Add(new SqlParameter("@Nednaam", System.Data.SqlDbType.NVarChar));
                cmd.Parameters["@Nednaam"].Value = product.Nednaam;
                cmd.Parameters.Add(new SqlParameter("@Wetnaam", System.Data.SqlDbType.NVarChar));
                cmd.Parameters["@Wetnaam"].Value = product.Wetnaam;
                cmd.Parameters.Add(new SqlParameter("@Beschrijving", System.Data.SqlDbType.NVarChar));
                cmd.Parameters["@Beschrijving"].Value = product.Beschrijving;
                cmd.Parameters.Add(new SqlParameter("@Prijs", System.Data.SqlDbType.Float));
                cmd.Parameters["@Prijs"].Value = product.Prijs;
                cmd.ExecuteNonQuery();
            }
        }
        public Dictionary<int, Klant> LeesAlleKlanten ()
        {
            string SQL = "SELECT * FROM Klant";
            List<Klant> klanten = new List<Klant>();
            Dictionary<int, Klant> IdKlantenlijst = new Dictionary<int, Klant>();
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
                        klanten.Add(new Klant((int)reader["id"], (string)reader["naam"], (string)reader["adres"]));
                    }
                    foreach (Klant klant in klanten)
                    {
                        IdKlantenlijst.Add(klant.Id, klant);
                    }
                    return IdKlantenlijst;
                }
                catch (Exception ex) { throw new Exception("leessoorten", ex); }
            }
        }

        public bool HeeftOfferte(Offerte offerte)
        {
            string SQL = "SELECT count(*) from Offerte WHERE id = @Id and datum = @Datum and klantid = " +
                "@Klantid and afhalenbool = @Afhalenbool and plaatsenbool = @Plaatsenbool";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = SQL;
                cmd.Parameters.Add(new SqlParameter("@Id", System.Data.SqlDbType.Int));
                cmd.Parameters["@Id"].Value = offerte.Id;
                cmd.Parameters.Add(new SqlParameter("@Datum", System.Data.SqlDbType.DateTime));
                cmd.Parameters["@Datum"].Value = offerte.Datum;
                cmd.Parameters.Add(new SqlParameter("@Klantid", System.Data.SqlDbType.Int));
                cmd.Parameters["@Klantid"].Value = offerte.Klant.Id;
                cmd.Parameters.Add(new SqlParameter("@Afhalenbool", System.Data.SqlDbType.Bit));
                cmd.Parameters["@Afhalenbool"].Value = offerte.AfhalenBool;
                cmd.Parameters.Add(new SqlParameter("@Plaatsenbool", System.Data.SqlDbType.Bit));
                cmd.Parameters["@Plaatsenbool"].Value = offerte.PlaatsenBool;

                int n = (int)cmd.ExecuteScalar();
                if (n > 0) return true; else return false;
            }
        }

        public void SchrijfOfferte(Offerte offerte)
        {
            string SQL = "INSERT INTO offerte( id, datum, klantid, afhalenbool, plaatsenbool) VALUES( @Id, @Datum, " +
               "@Klantid, @Afhalenbool, @Plaatsenbool)";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = SQL;
                cmd.Parameters.Add(new SqlParameter("@Id", System.Data.SqlDbType.Int));
                cmd.Parameters["@Id"].Value = offerte.Id;
                cmd.Parameters.Add(new SqlParameter("@Datum", System.Data.SqlDbType.DateTime));
                cmd.Parameters["@Datum"].Value = offerte.Datum;
                cmd.Parameters.Add(new SqlParameter("@Klantid", System.Data.SqlDbType.Int));
                cmd.Parameters["@Klantid"].Value = offerte.Klant.Id;
                cmd.Parameters.Add(new SqlParameter("@Afhalenbool", System.Data.SqlDbType.Bit));
                cmd.Parameters["@Afhalenbool"].Value = offerte.AfhalenBool;
                cmd.Parameters.Add(new SqlParameter("@Plaatsenbool", System.Data.SqlDbType.Bit));
                cmd.Parameters["@Plaatsenbool"].Value = offerte.PlaatsenBool;

                cmd.ExecuteNonQuery();
            }
        }
    }
}
