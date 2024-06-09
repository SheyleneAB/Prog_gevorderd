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
using System.Collections;

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
            string SQL = "SELECT count(*) from Offerte where datum = @Datum and klantid = " +
                "@Klantid and afhalenbool = @Afhalenbool and plaatsenbool = @Plaatsenbool";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = SQL;
                
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
            string SQL = "INSERT INTO offerte(datum, klantid, afhalenbool, plaatsenbool) output INSERTED.ID VALUES( @Datum, @Klantid, @Afhalenbool, @Plaatsenbool)";
            string SQLprod = "INSERT INTO offerteklantaantal(offerteid, productid, aantal) VALUES(@OfferteId, @ProductId, @Aantal)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    int offerteId;
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = transaction;
                            cmd.CommandText = SQL;

                            cmd.Parameters.Add(new SqlParameter("@Datum", System.Data.SqlDbType.DateTime)).Value = offerte.Datum;
                            cmd.Parameters.Add(new SqlParameter("@Klantid", System.Data.SqlDbType.Int)).Value = offerte.Klant.Id;
                            cmd.Parameters.Add(new SqlParameter("@Afhalenbool", System.Data.SqlDbType.Bit)).Value = offerte.AfhalenBool;
                            cmd.Parameters.Add(new SqlParameter("@Plaatsenbool", System.Data.SqlDbType.Bit)).Value = offerte.PlaatsenBool;

                            offerteId = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        using (SqlCommand cmdProd = conn.CreateCommand())
                        {
                            cmdProd.Transaction = transaction;
                            cmdProd.CommandText = SQLprod;

                            cmdProd.Parameters.Add(new SqlParameter("@OfferteId", System.Data.SqlDbType.Int));
                            cmdProd.Parameters.Add(new SqlParameter("@ProductId", System.Data.SqlDbType.Int));
                            cmdProd.Parameters.Add(new SqlParameter("@Aantal", System.Data.SqlDbType.Int));

                            foreach (var producten in offerte.Producten)
                            {
                                cmdProd.Parameters["@OfferteId"].Value = offerteId;
                                cmdProd.Parameters["@ProductId"].Value = producten.Key.Id;
                                cmdProd.Parameters["@Aantal"].Value = producten.Value;

                                cmdProd.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        public List<Offerte> HaalOfferteOp(int? offerteId, Klant? klant, DateTime? datum)
        {
            string SQL = "SELECT * FROM offerte o JOIN offerteklantaantal ok ON o.id = ok.offerteid JOIN product p ON ok.productid = p.id Join klant k on o.klantid = k.id ";
            if (offerteId.HasValue)
            {
                SQL += " WHERE o.id = @Id ;";
            }
            else if (klant != null)
            {
                SQL += " WHERE o.klantid = @Klantid ;";
            }
            else if (datum.HasValue)
            {
                SQL += " WHERE o.datum = @Datum ;";
            }

            List<Offerte> offertes = new List<Offerte>();
            Dictionary<int, Offerte> offerteMap = new Dictionary<int, Offerte>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = SQL;
                if (offerteId.HasValue)
                {
                    command.Parameters.AddWithValue("@Id", offerteId.Value);
                }
                else if (klant != null)
                {
                    command.Parameters.AddWithValue("@Klantid", klant.Id);
                }
                else if (datum.HasValue)
                {
                    command.Parameters.AddWithValue("@Datum", datum.Value);
                }

                connection.Open();
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(reader.GetOrdinal("OfferteId"));
                        if (!offerteMap.TryGetValue(id, out Offerte offerte))
                        {
                            offerte = new Offerte
                            {
                                Id = id,
                                Datum = reader.GetDateTime(reader.GetOrdinal("datum")),
                                Klant = new Klant { Id = reader.GetInt32(reader.GetOrdinal("klantid")), 
                                    Naam = reader.GetString(reader.GetOrdinal("naam")), 
                                    Adres = reader.GetString(reader.GetOrdinal("adres"))
                                },
                                AfhalenBool = reader.GetBoolean(reader.GetOrdinal("afhalenbool")),
                                PlaatsenBool = reader.GetBoolean(reader.GetOrdinal("plaatsenbool")),
                                
                            };
                            offertes.Add(offerte);
                            offerteMap[id] = offerte;
                        }

                        if (!reader.IsDBNull(reader.GetOrdinal("ProductId")))
                        {
                            Product product = new Product
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("ProductId")),
                                Nednaam = reader.GetString(reader.GetOrdinal("nednaam")),
                                Wetnaam = reader.GetString(reader.GetOrdinal("wetnaam")),
                                Beschrijving = reader.GetString(reader.GetOrdinal("beschrijving")),
                                Prijs = reader.GetDouble(reader.GetOrdinal("prijs"))
                            };
                            int aantal = reader.GetInt32(reader.GetOrdinal("aantal"));
                            offerte.VoegProductToe(product, aantal);
                        }
                    }
                }
            }
            return offertes;
        }
 
        public Dictionary<int, Product> LeesAlleProducten()
        {
            string SQL = "SELECT * FROM Product";
            Dictionary<int, Product> IdProductenlijst = new Dictionary<int, Product>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = SQL;
                    using (IDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(reader.GetOrdinal("id"));
                            string nednaam = reader.GetString(reader.GetOrdinal("nednaam"));
                            string wetnaam = reader.GetString(reader.GetOrdinal("wetnaam"));
                            string beschrijving = reader.GetString(reader.GetOrdinal("beschrijving"));
                            double prijs = reader.GetDouble(reader.GetOrdinal("prijs"));

                            Product product = new Product(id, nednaam, wetnaam, beschrijving, prijs);
                            IdProductenlijst.Add(product.Id, product);
                        }
                    }
                    return IdProductenlijst;
                }
                catch (Exception ex)
                {
                    throw new Exception("leessoorten", ex);
                }
            }
        }

        public Klantengeg LeesKlantengegnaam(string klantnaam)
        {
         // HERSCHRIJVEN  
            Klantengeg klantengeg = null;
            string SQL = "SELECT k.id AS klant_id, k.naam, k.adres, o.id AS offerte_id, " +
                         "o.datum, o.plaatsenbool, o.afhalenbool, ok.productid, ok.aantal, p.prijs AS product_prijs " +
                         "FROM klant k " +
                         "JOIN offerte o ON k.id = o.klantid " +
                         "JOIN offerteklantaantal ok ON o.id = ok.offerteid " +
                         "JOIN product p ON ok.productid = p.id " +
                         "WHERE k.naam = @Naam;";

            Dictionary<int, Product> products = LeesAlleProducten();

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = SQL;
                    cmd.Parameters.AddWithValue("@Naam", klantnaam);

                    using (IDataReader reader = cmd.ExecuteReader())
                    {
                        List<Offerte> offertes = new List<Offerte>();
                        while (reader.Read())
                        {
                            if (klantengeg == null)
                            {
                                int klant_id = reader.GetInt32(reader.GetOrdinal("klant_id"));
                                string naam = reader.GetString(reader.GetOrdinal("naam"));
                                string adres = reader.GetString(reader.GetOrdinal("adres"));
                                klantengeg = new Klantengeg(klant_id, naam, adres, 0, new List<Offerte>(), 0.0);
                            }

                            int offerte_id = reader.GetInt32(reader.GetOrdinal("offerte_id"));
                            DateTime datum = reader.GetDateTime(reader.GetOrdinal("datum"));
                            bool plaatsenBool = reader.GetBoolean(reader.GetOrdinal("plaatsenbool"));
                            bool afhalenBool = reader.GetBoolean(reader.GetOrdinal("afhalenbool"));

                            Offerte offerte = offertes.FirstOrDefault(o => o.Id == offerte_id);
                            if (offerte == null)
                            {
                                offerte = new Offerte(datum, new Klant { Id = klantengeg.Id, Naam = klantengeg.Naam, Adres = klantengeg.Adres }, afhalenBool, plaatsenBool)
                                {
                                    Id = offerte_id
                                };
                                offertes.Add(offerte);
                            }

                            int productid = reader.GetInt32(reader.GetOrdinal("productid"));
                            int aantal = reader.GetInt32(reader.GetOrdinal("aantal"));
                            double product_prijs = reader.GetDouble(reader.GetOrdinal("product_prijs"));

                            if (products.TryGetValue(productid, out Product product))
                            {
                                product.Prijs = product_prijs;
                                if (!offerte.Producten.ContainsKey(product))
                                {
                                    offerte.VoegProductToe(product, aantal);
                                }
                            }
                        }

                        if (klantengeg != null)
                        {
                            double totaalprijs = 0;
                            foreach (var offerte in offertes)
                            {
                                totaalprijs += offerte.prijsberekenen();
                            }

                            klantengeg.Aantaloffertes = offertes.Count;
                            klantengeg.Offertenummber = offertes;
                            klantengeg.TotaalPrijs = totaalprijs;
                        }

                        return klantengeg;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error reading customer data", ex);
                }
            }
        }
       
        public Klantengeg LeesKlantengegid(int klantId)
        {
            //HERSCHRIJVEN
            Klantengeg klantengeg = null;
            string SQL = "SELECT k.id AS klant_id, k.naam, k.adres, o.id AS offerte_id, " +
                         "o.datum, o.plaatsenbool, o.afhalenbool, ok.productid, ok.aantal, p.prijs AS product_prijs " +
                         "FROM klant k " +
                         "JOIN offerte o ON k.id = o.klantid " +
                         "JOIN offerteklantaantal ok ON o.id = ok.offerteid " +
                         "JOIN product p ON ok.productid = p.id " +
                         "WHERE k.id = @Id;";

            Dictionary<int, Product> products = LeesAlleProducten();

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = SQL;
                    cmd.Parameters.AddWithValue("@Id", klantId);

                    using (IDataReader reader = cmd.ExecuteReader())
                    {
                        List<Offerte> offertes = new List<Offerte>();
                        while (reader.Read())
                        {
                            if (klantengeg == null)
                            {
                                int klant_id = reader.GetInt32(reader.GetOrdinal("klant_id"));
                                string naam = reader.GetString(reader.GetOrdinal("naam"));
                                string adres = reader.GetString(reader.GetOrdinal("adres"));
                                klantengeg = new Klantengeg(klant_id, naam, adres, 0, new List<Offerte>(), 0.0);
                            }

                            int offerte_id = reader.GetInt32(reader.GetOrdinal("offerte_id"));
                            DateTime datum = reader.GetDateTime(reader.GetOrdinal("datum"));
                            bool plaatsenBool = reader.GetBoolean(reader.GetOrdinal("plaatsenbool"));
                            bool afhalenBool = reader.GetBoolean(reader.GetOrdinal("afhalenbool"));

                            Offerte offerte = offertes.FirstOrDefault(o => o.Id == offerte_id);
                            if (offerte == null)
                            {
                                offerte = new Offerte(datum, new Klant { Id = klantId, Naam = klantengeg.Naam, Adres = klantengeg.Adres }, afhalenBool, plaatsenBool)
                                {
                                    Id = offerte_id
                                };
                                offertes.Add(offerte);
                            }

                            int productid = reader.GetInt32(reader.GetOrdinal("productid"));
                            int aantal = reader.GetInt32(reader.GetOrdinal("aantal"));
                            double product_prijs = reader.GetDouble(reader.GetOrdinal("product_prijs"));

                            if (products.TryGetValue(productid, out Product product))
                            {
                                product.Prijs = product_prijs;
                                if (!offerte.Producten.ContainsKey(product))
                                {
                                    offerte.VoegProductToe(product, aantal);
                                }
                            }
                        }

                        if (klantengeg != null)
                        {
                            double totaalprijs = 0;
                            foreach (var offerte in offertes)
                            {
                                totaalprijs += offerte.prijsberekenen();
                            }

                            klantengeg.Aantaloffertes = offertes.Count;
                            klantengeg.Offertenummber = offertes;
                            klantengeg.TotaalPrijs = totaalprijs;
                        }

                        return klantengeg;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error reading customer data", ex);
                }
            }
        }

        public void UpdateOfferte(Offerte offerte, Dictionary<Product, int> proddel, Dictionary<Product, int> produpdate, Dictionary<Product, int> prodnew)
        {
            string SQLUpdateOfferte = "UPDATE offerte SET datum = @Datum, afhalenbool = @Afhalenbool, plaatsenbool = @Plaatsenbool WHERE id = @OfferteId";
            string SQLDeleteProducts = "DELETE FROM offerteklantaantal WHERE offerteid = @OfferteId AND productid = @ProductId";
            string SQLInsertProducts = "INSERT INTO offerteklantaantal (offerteid, productid, aantal) VALUES (@OfferteId, @ProductId, @Aantal)";
            string SQLUpdateProducts = "UPDATE offerteklantaantal SET aantal = @Aantal WHERE offerteid = @OfferteId AND productid = @ProductId";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand(SQLUpdateOfferte, conn, transaction))
                        {
                            cmd.Parameters.Add(new SqlParameter("@Datum", System.Data.SqlDbType.DateTime)).Value = offerte.Datum;
                            cmd.Parameters.Add(new SqlParameter("@Afhalenbool", System.Data.SqlDbType.Bit)).Value = offerte.AfhalenBool;
                            cmd.Parameters.Add(new SqlParameter("@Plaatsenbool", System.Data.SqlDbType.Bit)).Value = offerte.PlaatsenBool;
                            cmd.Parameters.Add(new SqlParameter("@OfferteId", System.Data.SqlDbType.Int)).Value = offerte.Id;

                            cmd.ExecuteNonQuery();
                        }

                        if (proddel.Count > 0)
                        {
                            using (SqlCommand cmdDel = new SqlCommand(SQLDeleteProducts, conn, transaction))
                            {
                                cmdDel.Parameters.Add(new SqlParameter("@OfferteId", System.Data.SqlDbType.Int));
                                cmdDel.Parameters.Add(new SqlParameter("@ProductId", System.Data.SqlDbType.Int));

                                foreach (var kvp in proddel)
                                {
                                    cmdDel.Parameters["@OfferteId"].Value = offerte.Id;
                                    cmdDel.Parameters["@ProductId"].Value = kvp.Key.Id;

                                    cmdDel.ExecuteNonQuery();
                                }
                            }
                        }

                        if (prodnew.Count > 0)
                        {
                            using (SqlCommand cmdIns = new SqlCommand(SQLInsertProducts, conn, transaction))
                            {
                                cmdIns.Parameters.Add(new SqlParameter("@OfferteId", System.Data.SqlDbType.Int));
                                cmdIns.Parameters.Add(new SqlParameter("@ProductId", System.Data.SqlDbType.Int));
                                cmdIns.Parameters.Add(new SqlParameter("@Aantal", System.Data.SqlDbType.Int));

                                foreach (var kvp in prodnew)
                                {
                                    cmdIns.Parameters["@OfferteId"].Value = offerte.Id;
                                    cmdIns.Parameters["@ProductId"].Value = kvp.Key.Id;
                                    cmdIns.Parameters["@Aantal"].Value = kvp.Value;

                                    cmdIns.ExecuteNonQuery();
                                }
                            }
                        }

                        if (produpdate.Count > 0)
                        {
                            using (SqlCommand cmdUpd = new SqlCommand(SQLUpdateProducts, conn, transaction))
                            {
                                cmdUpd.Parameters.Add(new SqlParameter("@OfferteId", System.Data.SqlDbType.Int));
                                cmdUpd.Parameters.Add(new SqlParameter("@ProductId", System.Data.SqlDbType.Int));
                                cmdUpd.Parameters.Add(new SqlParameter("@Aantal", System.Data.SqlDbType.Int));

                                foreach (var kvp in produpdate)
                                {
                                    cmdUpd.Parameters["@OfferteId"].Value = offerte.Id;
                                    cmdUpd.Parameters["@ProductId"].Value = kvp.Key.Id;
                                    cmdUpd.Parameters["@Aantal"].Value = kvp.Value;

                                    cmdUpd.ExecuteNonQuery();
                                }
                            }
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception("Ongeldige input: " + ex.Message);
                    }
                }
            }
        }
    }  
}
