using System.Collections.Generic;

namespace OpdrachtSchool
{
    internal class School
    {
        public SchoolBibliotheek SchoolBibliotheek { get; set;}
        public List <Persoon> Personen { get; set; }
    }
    public class SchoolBibliotheek 
    { 
        public List <Boek> Boeken { get; set; }
    }

    public class Boek
    {
        public string Auteur { get; internal set; }
        public string Titel { get; internal set; }
    }

    public class Persoon
    {

    }
}