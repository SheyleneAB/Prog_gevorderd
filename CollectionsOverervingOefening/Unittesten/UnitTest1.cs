using Microsoft.VisualStudio.TestPlatform.Utilities;
using Overerving.BL;
using System.Security.Cryptography.X509Certificates;

namespace Unittesten
{
    public class UnitTest1
    {
        //SCHIPTESTEN public Schip(double lengte, double breedte, double tonnage, string naam)
        [Fact]
        
        public void LengteMagNietNegatiefZijn()
        {
            
            var schip = new Schip(-1, 1, 1, "hallo");

            
            Assert.Throws<Exception>(() => schip.Lengte = -1);
        }

        [Fact]
        
        public void BreedteMagNietNegatiefZijn()
        {

            var schip = new Schip(1, -1, 1, "hallo");

            Assert.Throws<Exception>(() => schip.Breedte = -1);
        }

        [Fact]
        public void TonnageMagNietNegatiefZijn()
        {

            var schip = new Schip(1, 1, -1, "hallo");


            Assert.Throws<Exception>(() => schip.Tonnage = -1);
        }

        [Fact]
        public void NaamMagNietLeegZijn()
        {

            var schip = new Schip(1, 1, 1, "");


            Assert.Throws<Exception>(() => schip.Naam = "");
        }

        [Fact]
        
        
        public void NaamMagNietNullZijn()
        {

            var schip = new Schip(1, 1, 1, null);


            Assert.Throws<Exception>(() => schip.Naam = null);
        }
        [Fact]
        //REDERIJ TESTEN
        public void VoegVlootToe_VoegtNieuweVlootToe()
        {

            Rederij rederij = new Rederij();
            
            Schip schip1 = new TankerSchip(100, 10, 5, 200, "Schip1");
            Schip schip2 = new Schip(15, 6, 300, "Schip2");
            List<Schip> schepen = new List<Schip> { schip1, schip2 };

            Vloot vloot = new Vloot("TestVloot", schepen);
            vloot.VoegSchipToe(schip1);
            vloot.VoegSchipToe(schip2);

            
            rederij.VoegVlootToe(vloot);

            
            Assert.Contains(vloot, rederij.Vloten);
        }

        [Fact]
        public void VoegVlootToe_GooitExceptionAlsVlootAlBestaat()
        {
            
            Rederij rederij = new Rederij();
            
            Schip schip1 = new TankerSchip(100, 10, 5, 200, "Schip1");
            Schip schip2 = new Schip(15, 6, 300, "Schip2");
            List<Schip> schepen = new List<Schip> { schip1, schip2 };

            Vloot vloot = new Vloot("TestVloot", schepen);
            rederij.VoegVlootToe(vloot);

            
            Assert.Throws<Exception>(() => rederij.VoegVlootToe(vloot));
        }
        [Fact]
        public void VerwijderVloot_VerwijdertVloot()
        {
            // Arrange
            Rederij rederij = new Rederij();

            Schip schip1 = new TankerSchip(100, 10, 5, 200, "Schip1");
            Schip schip2 = new Schip(15, 6, 300, "Schip2");
            List<Schip> schepen = new List<Schip> { schip1, schip2 };

            Vloot vloot = new Vloot("TestVloot", schepen);
            rederij.VoegVlootToe(vloot);

            // Act
            rederij.VerwijderVloot("TestVloot");

            // Assert
            Assert.DoesNotContain(vloot, rederij.Vloten);
        }

        [Fact]
        public void VerwijderVloot_GooitExceptionAlsVlootNietBestaat()
        {
            // Arrange
            Rederij rederij = new Rederij();

            Schip schip1 = new TankerSchip(100, 10, 5, 200, "Schip1");
            Schip schip2 = new Schip(15, 6, 300, "Schip2");
            List<Schip> schepen = new List<Schip> { schip1, schip2 };

            Vloot vloot = new Vloot("TestVloot", schepen);
            rederij.VoegVlootToe(vloot);

            // Act & Assert
            Assert.Throws<Exception>(() => rederij.VerwijderVloot("OnbestaandeVloot"));
        }

        [Fact]
       

        
        public void VoegHavensToe_VoegtNieuweHavenToe()
        {
            // Arrange
            var rederij = new Rederij();
            var haven = "NieuweHaven";

            // Act
            rederij.VoegHavensToe(haven);

            // Assert
            Assert.Contains(haven, rederij.havens);
        }

        [Fact]
        public void VoegHavensToe_GooitExceptionAlsHavenAlBestaat()
        {
            // Arrange
            var rederij = new Rederij();
            var haven = "BestaandeHaven";
            rederij.havens = new List<string> { haven };

            // Act & Assert
            Assert.Throws<Exception>(() => rederij.VoegHavensToe(haven));
        }

        [Fact]
        public void VerwijderHavens_VerwijdertHaven()
        {
            // Arrange
            var rederij = new Rederij();
            var haven = "TeVerwijderenHaven";
            rederij.havens = new List<string> { haven };

            // Act
            rederij.VerwijderHavens(haven);

            // Assert
            Assert.DoesNotContain(haven, rederij.havens);
        }

        [Fact]
        public void VerwijderHavens_GooitExceptionAlsHavenNietBestaat()
        {
            // Arrange
            var rederij = new Rederij();
            var haven = "OnbestaandeHaven";

            // Act & Assert
            Assert.Throws<Exception>(() => rederij.VerwijderHavens(haven));
        }
        [Fact]
        public void TonnageVloten_GeeftTonnagePerVlootInDalendeVolgorde()
        {
            // Arrange
            Rederij rederij = new Rederij();
            List<Schip> schepen = new List<Schip>
            {
                new TankerSchip(volume: 150, lengte: 10, breedte: 5, tonnage: 200, naam: "Schip1"),

                new Schip(1, 1,  200, "Schip2")
            };
            List<Schip> schepen2 = new List<Schip>
            {
                new TankerSchip(volume: 100, lengte: 10, breedte: 5, tonnage: 0, naam: "Schip2"),

                new Schip(1, 1,  250, "Schip2")
            };

            Vloot vloot1 = new Vloot("Vloot1", schepen);
            Vloot vloot2 = new Vloot("Vloot2", schepen2);


            rederij.VoegVlootToe(vloot1);
            rederij.VoegVlootToe(vloot2);
            var expectedTonnagePerVloot = new Dictionary<Vloot, double>
            {
                { vloot2, 400 },
                { vloot1, 250 }
            };

            
            var result = rederij.TonnageVloten();

            
            Assert.Equal(expectedTonnagePerVloot, result);
        }

        [Fact]
        public void AantalVolume_GeeftTotaalVolumeVanTankerschepen()
        {
            
            Rederij rederij = new Rederij();
            List<Schip> schepen = new List<Schip>
            {
                new TankerSchip(volume: 150, lengte: 10, breedte: 5, tonnage: 200, naam: "Schip1"),

                new Schip(1, 1,  200, "Schip2")
            };
            List<Schip> schepen2 = new List<Schip>
            {
                new TankerSchip(volume: 100, lengte: 10, breedte: 5, tonnage: 0, naam: "Schip2"),

                new Schip(1, 1,  250, "Schip2")
            };
            
            Vloot vloot1 = new Vloot ( "Vloot1", schepen );
            Vloot vloot2 = new Vloot ( "Vloot2", schepen2 );
            
            rederij.Vloten = new List<Vloot> { vloot1, vloot2 };
            var expectedVolume = 250; // 100 + 150

            // Act
            var result = rederij.AantalVolume();

            // Assert
            Assert.Equal(expectedVolume, result);
        }

        [Fact]
        public void TotaleCargowaarde_GeeftTotaleCargowaardeVanAlleCargoschepen()
        {
            // Arrange
            Rederij rederij = new Rederij();
            List<Schip> schepen = new List<Schip>
            {
                new CargoSchip(cargowaarde: 150, lengte: 10, breedte: 5, tonnage: 200, naam: "Schip1"),

                new Schip(1, 1,  200, "Schip2")
            };
            List<Schip> schepen2 = new List<Schip>
            {
                new CargoSchip(cargowaarde: 100 , lengte: 10, breedte: 5, tonnage: 0, naam: "Schip2"),

                new Schip(1, 1,  250, "Schip2")
            };

            Vloot vloot1 = new Vloot("Vloot1", schepen);
            Vloot vloot2 = new Vloot("Vloot2", schepen2);

            
           
            rederij.Vloten = new List<Vloot> { vloot1, vloot2 };
            double expectedCargowaarde = 250; // 100 + 150

            
            double result = rederij.TotaleCargowaarde();

            
            Assert.Equal(expectedCargowaarde, result);
        }

        [Fact]
        public void BeschikbareSleepboten_GeeftTotaalAantalBeschikbareSleepboten()
        {
            // Arrange
            
            Rederij rederij = new Rederij();
            List<Schip> schepen = new List<Schip>
            {
                 new Sleepboot(1, 1,  200, "Schip1"),

                 new Schip(1, 1,  200, "Schip2")
            };
            List<Schip> schepen2 = new List<Schip>
            {
             new Sleepboot(1, 1,  250, "Schip3"),

             new Schip(1, 1,  250, "Schip4")
            };

            Vloot vloot1 = new Vloot("Vloot1", schepen);
            Vloot vloot2 = new Vloot("Vloot2", schepen2);

            rederij.Vloten = new List<Vloot> { vloot1, vloot2 };
            
            
            var expectedAantal = 2; // 1 + 1

            var result = rederij.BeschikbareSleepboten();

            Assert.Equal(expectedAantal, result);
        }

        [Fact]
        public void ZoekSchipOpNaam_VindtSchipMetGegevenNaam()
        {
            // Arrange
            Rederij rederij = new Rederij();
            List<Schip> schepen = new List<Schip>
            {
                new TankerSchip(volume: 150, lengte: 10, breedte: 5, tonnage: 200, naam: "Schip1"),

                new Schip(1, 1,  200, "Schip3")
            };
            List<Schip> schepen2 = new List<Schip>
            {
                new TankerSchip(volume: 100, lengte: 10, breedte: 5, tonnage: 0, naam: "Schip4"),

                new Schip(1, 1,  250, "Schip2")
            };

            Vloot vloot1 = new Vloot("Vloot1", schepen);
            Vloot vloot2 = new Vloot("Vloot2", schepen2);

            rederij.Vloten = new List<Vloot> { vloot1, vloot2 };
            var expectedSchip = new Schip(1, 1, 250, "Schip2");

            var result = rederij.ZoekSchipOpNaam("Schip2");

            
            Assert.Equal(expectedSchip, result);
        }

        [Fact]
        public void TotaalAantalPassagiers_GeeftTotaalAantalPassagiersOpAllePassagiersschepen()
        {
            Rederij rederij = new Rederij();
            List<Schip> schepen = new List<Schip>
            {
                 new  PassagiersSchip(aantalPassagiers: 100, lengte: 10, breedte: 5, tonnage: 200, naam: "Schip1", traject: new List<string>{"a", "b" }),

                new Schip(1, 1,  200, "Schip2")
            };
            List<Schip> schepen2 = new List<Schip>
            {
                new  PassagiersSchip(aantalPassagiers: 150, lengte: 10, breedte: 5, tonnage: 200, naam: "Schip3", traject: new List<string>{"a", "b" }),

                new Schip(1, 1,  250, "Schip4")
            };

            Vloot vloot1 = new Vloot("Vloot1", schepen);
            Vloot vloot2 = new Vloot("Vloot2", schepen2);

            rederij.Vloten = new List<Vloot> { vloot1, vloot2 };
            
            var expectedAantalPassagiers = 250; // 100 + 150

           
            var result = rederij.TotaalAantalPassagiers();

           
            Assert.Equal(expectedAantalPassagiers, result);
        }

    }
}

