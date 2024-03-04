using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpdrachtStripDomeinModel
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            Strip suske = new Strip("Hallo", new HashSet<Auteur>{ new Auteur("jack newton", "sdjophp@hsoapshfdp") }, new Uitgeverij("haoshos", "hsopahfs"), new List<Reeks> { new Reeks(15) });
            suske.AuteurStrip.Add(new Auteur ( "hahodsh", "fhsopahfdp" )); 
        }
    }
}
