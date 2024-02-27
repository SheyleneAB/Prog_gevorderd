using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using VisStatsBL.interfaces;
using VisStatsBL.MODEL;

namespace Visstat_uploaddata
{
    public class FileProcessor : IFileProcessor
    {
        public List<string> LeesSoorten(string fileName)
        {
            try
            {
                List<string> soorten = new List<string>();
                using (StreamReader reader = new StreamReader(fileName))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        soorten.Add(line.Trim());
                    }
                }
                return soorten;
            }
            catch (Exception ex) { throw new Exception($"FileProcessor-LeesSoorten [{fileName}]"); }
        }
        public List<string> LeesHavens (string fileName)
        {
            try
            {
                List<string> havens = new List<string>();
                using (StreamReader reader = new StreamReader(fileName))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        havens.Add(line.Trim());
                    }
                }
                return havens;
            }
            catch (Exception ex) { throw new Exception($"FileProcessor-LeesHavens [{fileName}]"); }

        }
        public List<VisStatsDataRecord> LeesStatistieken(string fileName, List<Vissoort> vissoorten, List<Haven> havens)
        {
            try
            {
                Dictionary<string, Vissoort> soortenD = vissoorten.ToDictionary(x => x.Naam, x => x);
                Dictionary<string, Haven> havenD = havens.ToDictionary(x => x.Stad, x => x);
                Dictionary<(string, int, int, string), VisStatsDataRecord> data = new(); //haven, jaar, maand, soort
                using (StreamReader sr = new StreamReader(fileName))
                {
                    string line;
                    int jaar = 0, maand = 0;
                    List<string> havensTXT = new List<string>();
                    while ((line = sr.ReadLine()) != null)
                    {
                        //lees tot begin van een maand
                        if (Regex.IsMatch(line, @"^-+\d(6)-+"))
                        {
                            jaar = Int32.Parse(Regex.Match(line, @"\d(4)").Value);
                            maand = Int32.Parse(Regex.Match(line, @"(\d(2))-+").Groups[1].Value);
                            havensTXT.Clear();
                        }
                        //lees namen havens
                        else if (line.Contains("Vissoorten|Totaal van de havens"))
                        {
                            string pattern = @"\|([A-Za-z]+)\|";
                            MatchCollection matches = Regex.Matches(line, pattern);
                            foreach (Match m in matches) havensTXT.Add(m.Groups[1].Value);
                        }
                        // lees statistieken
                        else
                        {
                            string[] elements = line.Split('|');
                            //eerste lijn is lijn van vissoort
                            if (soortenD.ContainsKey(elements[0]))
                            {
                                for (int i = 0; i < havensTXT.Count; i++)
                                {
                                    if (havenD.ContainsKey(havensTXT[i]))
                                    {
                                        if (!data.ContainsKey((havensTXT[i], jaar, maand, elements[0])))
                                        {
                                            data.Add((havensTXT[i], jaar, maand, elements[0]), new VisStatsDataRecord(jaar, maand, ParseValue(elements[(i * 2) + 3]), ParseValue(elements[(i * 2) + 4]), havenD[havensTXT[i]], soortenD[elements[0]]));
                                        }
                                    }
                                }
                            }
                        }
                    } // <- missing curly brace for the while loop

                }
                return data.Values.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("fileProcessor-leesMontlyresults", ex);
            }
        }

        private double ParseValue(string value)
        {
            if (double.TryParse(value, out double d)) return d;
            else return 0.0;
        }

    }
}
    

