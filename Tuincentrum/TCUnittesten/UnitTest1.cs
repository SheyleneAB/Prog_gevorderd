using TC_BL.Model;
using TC_BL.Exceptions;
using System.Numerics;

namespace TCUnittesten
{
    public class UnitTest1
    {

        [Fact]
        public void Offerte_WithValidKlant_ShouldCreateOfferte()
        {
            
            var klant = new Klant(1, "molly", "drongen"); 

            
            Offerte offerte = new Offerte(new DateTime(2024, 5, 29), klant, true, false);

            Assert.NotNull(offerte.Klant);
            Assert.Equal(klant, offerte.Klant);
        }

        [Fact]
        public void Offerte_WithNullKlant_ShouldThrowException()
        {
            
            Klant klant = null;


            var ex = Assert.Throws<DomeinException>(() => new Offerte(new DateTime(2024, 5, 29), klant, true, false));
            Assert.Equal("klant is null", ex.Message);
        }
        [Fact]
        public void Prijsberekenen_BasisScenario_Test()
        {

            var klant = new Klant(1, "molly", "drongen");
            Offerte offerte = new Offerte(new DateTime(2024, 5, 29), klant, true, false);
            
            var product1 = new Product ( 1, "flora", "lowies", "een bloem idk", 1000);
            var product2 = new Product(1, "flora", "lowies", "een bloem idk", 3000);

            offerte.VoegProductToe(product1, 1);
            offerte.VoegProductToe(product2, 1);

            
            double prijs = offerte.prijsberekenen();

            
            Assert.Equal(4000 * 0.95, prijs); // Verwacht: 3800 (5% korting omdat de prijs tussen 2000 en 5000 ligt)
        }

        [Fact]
        public void Prijsberekenen_AfhalenEnPlaatsen_Test()
        {
            
            var klant = new Klant(1, "molly", "drongen");
            Offerte offerte = new Offerte(new DateTime(2024, 5, 29), klant, false, true);
            var product1 = new Product(1, "flora", "lowies", "een bloem idk", 1500);

            offerte.VoegProductToe(product1, 1);

            double prijs = offerte.prijsberekenen();

            Assert.Equal((1500 + 50) * 1.15, prijs); // Verwacht: 1840 (100 voor levering, 15% toeslag voor plaatsing)
        }

        [Fact]
        public void Prijsberekenen_HoogsteKortingsscenario_Test()
        {
            
            var klant = new Klant(1, "molly", "drongen");
            Offerte offerte = new Offerte(new DateTime(2024, 5, 29), klant, false, true);
            var product1 = new Product(1, "flora", "lowies", "een bloem idk", 3000);
            var product2 = new Product(1, "flora", "lowies", "een bloem idk", 3000);

            offerte.VoegProductToe(product1, 1);
            offerte.VoegProductToe(product2, 1);

            double prijs = offerte.prijsberekenen();

            Assert.Equal(5670, prijs); // Verwacht: 5400 (10% korting omdat de prijs boven de 5000 ligt)
        }
        [Fact]
        public void VoegProductToe_ValidProduct_Success()
        {
            
            var klant = new Klant(1, "molly", "drongen");
            Offerte offerte = new Offerte(new DateTime(2024, 5, 29), klant, false, true);

            var product = new Product(1, "flora", "lowies", "een bloem idk", 1500);
            int aantal = 5;
            Assert.False(offerte.Producten.ContainsKey(product));

            offerte.VoegProductToe(product, aantal);
            var product1 = new Product(1, "flora", "lowies", "een bloem idk", 100);
            var product2 = new Product(1, "flora", "lowies", "een bloem idk", 1580);
            Assert.True((offerte.Producten.Count == 1));
            offerte.VoegProductToe(product1, aantal);
            offerte.VoegProductToe(product2, aantal);

            // Assert
            Assert.True(offerte.Producten.ContainsKey(product));
            Assert.Equal(aantal, offerte.Producten[product]);
        }

        [Fact]
        public void VoegProductToe_ProductIsNull_ThrowsException()
        {
            // Arrange
            var klant = new Klant(1, "molly", "drongen");
            Offerte offerte = new Offerte(new DateTime(2024, 5, 29), klant, false, true);
            Product product = new Product();
            // Act & Assert
            var exception = Assert.Throws<DomeinException>(() => offerte.VoegProductToe(product, 1));
            Assert.Equal("offerte-voegproducten", exception.Message);
        }

        [Fact]
        public void VoegProductToe_ProductAlreadyExists_ThrowsException()
        {
            // Arrange
            var klant = new Klant(1, "molly", "drongen");
            Offerte offerte = new Offerte(new DateTime(2024, 5, 29), klant, false, true);
            var product = new Product(1, "flora", "lowies", "een bloem idk", 1500);
            offerte.VoegProductToe(product, 1);

            // Act & Assert
            var exception = Assert.Throws<DomeinException>(() => offerte.VoegProductToe(product, 1));
            Assert.Equal("offerte-voegproducten", exception.Message);
        }

        [Fact]
        public void VerwijderProduct_ValidProduct_Success()
        {
            // Arrange
            var klant = new Klant(1, "molly", "drongen");
            Offerte offerte = new Offerte(new DateTime(2024, 5, 29), klant, false, true);
            var product = new Product(1, "flora", "lowies", "een bloem idk", 100);
            
            offerte.VoegProductToe(product, 1);

            // Act
            offerte.VerwijderProduct(product);

            // Assert
            Assert.False(offerte.Producten.ContainsKey(product));
        }

        [Fact]
        public void VerwijderProduct_ProductIsNull_ThrowsException()
        {
            // Arrange
            var klant = new Klant(1, "molly", "drongen");
            Offerte offerte = new Offerte(new DateTime(2024, 5, 29), klant, false, true);

            // Act & Assert
            var exception = Assert.Throws<DomeinException>(() => offerte.VerwijderProduct(null));
            Assert.Equal("strip-verwijderauteur", exception.Message);
        }

        [Fact]
        public void VerwijderProduct_ProductNotExists_ThrowsException()
        {
            var klant = new Klant(1, "molly", "drongen");
            // Arrange
            Offerte offerte = new Offerte(new DateTime(2024, 5, 29), klant, false, true);
            var product = new Product(1, "flora", "lowies", "een bloem idk", 1500);

            // Act & Assert
            var exception = Assert.Throws<DomeinException>(() => offerte.VerwijderProduct(product));
            Assert.Equal("strip-verwijderauteur", exception.Message);
        }

        [Fact]
        public void VerwijderProduct_OnlyOneProduct_ThrowsException()
        {
            // Arrange
            var klant = new Klant(1, "molly", "drongen");
            Offerte offerte = new Offerte(new DateTime(2024, 5, 29), klant, false, true);
            var product = new Product(1, "flora", "lowies", "een bloem idk", 1500);
            offerte.VoegProductToe(product, 1);

            // Act & Assert
            var exception = Assert.Throws<DomeinException>(() => offerte.VerwijderProduct(product));
            Assert.Equal("strip-verwijderauteur", exception.Message);
        }

    }
}