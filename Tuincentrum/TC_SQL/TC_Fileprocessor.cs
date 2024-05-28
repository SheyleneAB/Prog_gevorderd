using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TC_BL.Interfaces;
using TC_BL.Model;

namespace TC_SQL
{
    public class TC_Fileprocessor : IFileProcessor
    {
        public List<Klant> LeesKlanten(string fileName)
        {
            try
            {
                List<Klant> klanten = new List<Klant>();
                using (StreamReader reader = new StreamReader(fileName))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string pattern = @"(\d+)\|([^|]+)\|([^|]+)";
                        Match match = Regex.Match(line, pattern);

                        if (match.Success)
                        {
                            int id = int.Parse(match.Groups[1].Value);
                            string name = match.Groups[2].Value.Trim();
                            string address = match.Groups[3].Value.Trim();

                            klanten.Add(new Klant
                            {
                                Id = id,
                                Naam = name,
                                Adres = address
                            });
                        }
                    }
                }
                return klanten;
            }
            catch (Exception ex) { throw new Exception($"FileProcessor-LeesKlanten [{fileName}]"); }
        }
        public List<Product> LeesProducten(string fileName)
        {
            try
            {
                List<Product> producten = new List<Product>();
                using (StreamReader reader = new StreamReader(fileName))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string pattern = @"^\s*(\d+)\|([^|]+)\|('[^']*'|\S+)\|(\d+|[\d-]+)\|(.+)$";


                        Match match = Regex.Match(line, pattern);

                        if (match.Success)
                        {
                            int id = int.Parse(match.Groups[1].Value);
                            string nedname = match.Groups[2].Value;
                            string wetnaam = match.Groups[3].Value;
                            double prijs = double.Parse(match.Groups[4].Value);
                            string beschrijving = match.Groups[5].Value;

                            producten.Add(new Product
                            {
                                Id = id,
                                Nednaam = nedname,
                                Wetnaam = wetnaam,
                                Beschrijving = beschrijving,
                                Prijs = prijs


                            });
                        }
                    }
                }
                return producten;
            }
            catch (Exception ex) { throw new Exception($"FileProcessor-LeesProducten [{fileName}]"); }

        }
        public Dictionary<int, Offerte> LeesOffertes(string fileName, string fileName2, 
            Dictionary<int, Klant> klanten, Dictionary<int,Product> alleproducten)
        {
            Dictionary<int, Offerte> offertes = new Dictionary<int, Offerte>();
            try
            {

                using (StreamReader reader = new StreamReader(fileName))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string pattern = @"(\d+)\|(\d{1,2}/\d{1,2}/\d{4})\|(\d+)\|(True|False)\|(True|False)";
                        Match match = Regex.Match(line, pattern);

                        if (match.Success)
                        {
                            int id = int.Parse(match.Groups[1].Value);
                            DateTime datum = DateTime.Parse(match.Groups[2].Value.Trim());
                            int klantid = int.Parse(match.Groups[3].Value.Trim());
                            bool afhalenbool = bool.Parse(match.Groups[4].Value.Trim());
                            bool plaatsenbool = bool.Parse(match.Groups[5].Value.Trim());


                            offertes.Add(id,(new Offerte
                            {
                                Id = id,
                                Datum = datum,
                                Klant = klanten[klantid],
                                AfhalenBool = afhalenbool,
                                PlaatsenBool = plaatsenbool
                            }));
                        }
                    }
                }
                LeesOfferteProducten(offertes,fileName2, alleproducten);


                
                return offertes;
            }
            catch (Exception ex)
            {
                throw new Exception($"FileProcessor-LeesOffertes [{fileName}]");
            }
        }
        public void LeesOfferteProducten(Dictionary<int, Offerte> offertes,string fileName2, Dictionary<int, Product> alleproducten)
        {
            try
            {
                Dictionary<Product, int> producten = new Dictionary<Product, int>();
                using (StreamReader reader2 = new StreamReader(fileName2))
                {
                    string line;
                    while ((line = reader2.ReadLine()) != null)
                    {
                        string pattern = @"^(\d+)\|(\d+)\|(\d+)$";

                        Match match = Regex.Match(line, pattern);

                        if (match.Success)
                        {
                            int id = int.Parse(match.Groups[1].Value);
                            int productid = int.Parse(match.Groups[2].Value);
                            int aantal = int.Parse(match.Groups[3].Value);

                            offertes[id].VoegProductToe(alleproducten[productid], aantal);
                        }
                    }
                }
            }
            catch (Exception ex) { throw new Exception($"FileProcessor-LeesOfferteProducten [{fileName2}]"); }

        }
       

    }
}
            
             
           
    
            



            
        

    

