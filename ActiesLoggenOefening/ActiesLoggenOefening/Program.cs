using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiesLoggenOefening
{
    internal class Program
    {
        static void Main(string[] args)
        {
            File.WriteAllLines(bestandsnaam, );
        }
    }
    public interface IDier 
    {
        string MaakGeluid();
    }
    public class kat : IDier
    {
        public string MaakGeluid()
        {
            string Geluid = "Miauw";
            return Geluid;
        }
    }
    public class hond 
    {
        SqlClientLogger 
        public string MaakGeluid()
        {
            string Geluid = "Woef";
            return Geluid;
        }
    }
}
