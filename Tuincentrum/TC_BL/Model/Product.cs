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

        public int Id { get; set; }
        public string Nednaam { get; set; }
        public string Wetnaam { get; set; }
        public string Beschrijving { get; set; }
        public double Prijs { get; set; }

    }
}