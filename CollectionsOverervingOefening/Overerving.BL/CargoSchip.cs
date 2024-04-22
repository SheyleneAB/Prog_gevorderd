namespace Overerving.BL
{
    public class CargoSchip : Schip
    {
        public CargoSchip(double lengte, double breedte, double tonnage, string naam, double cargowaarde) 
            : base(lengte, breedte, tonnage, naam)
        {

        }

        public double Cargowaarde { get; set; }
    }
}