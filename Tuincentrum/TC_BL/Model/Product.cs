using TC_BL.Exceptions;

namespace TC_BL.Model
{
    public class Product
    {
        public Product() { }
        public Product(int id, string nednaam, string wetnaam, string beschrijving, double prijs)
        {
            Id = id;
            Nednaam = nednaam;
            Wetnaam = wetnaam;
            Beschrijving = beschrijving;
            Prijs = prijs;
        }
        private int id;
        private string nednaam;
        private string wetnaam;
        private string beschrijving;
        private double prijs;

        public int Id { get{ return id; } set { if (value < 0) throw new DomeinException("SetProductid"); id = value; } }
        public string Nednaam { get { return nednaam; } set 
            { if (string.IsNullOrEmpty(value)) throw new DomeinException("nednaam is null"); nednaam = value; } }
        public string Wetnaam { get { return wetnaam; } set 
            { if (string.IsNullOrEmpty(value)) throw new DomeinException("wetnaam is null"); wetnaam = value; } }
        public string Beschrijving { get { return beschrijving; } set 
            { if (string.IsNullOrEmpty(value)) throw new DomeinException("beschrijving is null"); beschrijving = value; } }
        public double Prijs { get { return prijs; } set { if (value < 0) throw new DomeinException("prijs"); prijs = value; } }

    }
}