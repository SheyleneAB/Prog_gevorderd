using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC_BL.Model;


namespace TC_SQL
{
    internal class main
    {
        static void Main(string[] args)
        {
            TCRepository repository = new TCRepository(@"Data Source=Radion\sqlexpress;Initial Catalog=Tuin;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
            List<Offerte> alles = repository.LeesAlleOffertes();
            foreach (Offerte offerte in alles) { 
                Console.WriteLine(offerte);
            }
        }
    }
}
