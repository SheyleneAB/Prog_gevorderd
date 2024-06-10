using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TC_BL.Exceptions;
using TC_BL.Interfaces;
using TC_BL.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TC_BL.Manager
{
    public class TCManager
    {
        private IFileProcessor fileProcessor;
        private ITCRepository TCRepository;

        public TCManager(ITCRepository tCRepository)
        {
            this.TCRepository = tCRepository;
        }
        public TCManager(IFileProcessor fileProcessor, ITCRepository itcRepositrory)
        {
            this.fileProcessor = fileProcessor;
            this.TCRepository = itcRepositrory;
        }

        

        public void UploadKlanten(string fileName)
        {
            List<Klant> klanten = fileProcessor.LeesKlanten(fileName);
            List<Klant> klantenlijst = MaakKlanten(klanten);
            foreach (Klant klant in klantenlijst)
            {
                if (!TCRepository.HeeftKlant(klant))
                    TCRepository.SchrijfKlant(klant);
            }
        }

        public void UploadProducten(string fileName)
        {
            List<Product> GelezenPro = fileProcessor.LeesProducten(fileName);
            List<Product> ProductenLijst = MaakProducten(GelezenPro);
            foreach (Product product in ProductenLijst)
            {
                if (!TCRepository.HeeftProduct(product))
                    TCRepository.SchrijfProduct(product);
            }

        }
        public void UploadOffertes(string fileName, string fileName2)
        {
            Dictionary<int, Klant> klanten = TCRepository.LeesAlleKlanten();
            Dictionary<int, Product> producten = TCRepository.LeesAlleProducten();
            Dictionary<int, Offerte> gelezenofferte = fileProcessor.LeesOffertes(fileName, fileName2, klanten, producten);

            foreach (Offerte offerte in gelezenofferte.Values)
            {
                if (!TCRepository.HeeftOfferte(offerte))
                    TCRepository.SchrijfOfferte(offerte);
            }
        }
        public void SchrijfeenOfferte(Offerte offerte)
        {
            try
            {
                if (!TCRepository.HeeftOfferte(offerte))
                    TCRepository.SchrijfOfferte(offerte);
            }
            catch (Exception)
            {
                throw new ManagerException("SchrijfeenOfferte");
            }
        }


        public List<Klant> MaakKlanten(List<Klant> gelezenklanten)
        {
            Dictionary<int, Klant> klanten = new();
            foreach (Klant klant in gelezenklanten)
            {
                if (!klanten.ContainsKey(klant.Id))
                {
                    try
                    {
                        klanten.Add(klant.Id, klant);
                    }
                    catch (DomeinException)
                    {

                    }
                }
            }
            return klanten.Values.ToList();
        }
        public List<Product> MaakProducten(List<Product> gelezenproducten)
        {
            Dictionary<int, Product> Producten = new();
            foreach (Product Product in gelezenproducten)
            {
                if (!Producten.ContainsKey(Product.Id))
                {
                    try
                    {
                        Producten.Add(Product.Id, Product);
                    }
                    catch (DomeinException)
                    {

                    }
                }
            }
            return Producten.Values.ToList();
        }

        public List<Klant> GeefAlleKlanten()
        {
            Dictionary<int, Klant> klantdic = TCRepository.LeesAlleKlanten();
            List<Klant> klantlist = new List<Klant>();
            foreach (Klant klant in klantdic.Values)
            {
                klantlist.Add(klant);
            }

            return klantlist;


        }

        public List<Product> GeefProducten()
        {
            try
            {
                Dictionary<int, Product> Producten = TCRepository.LeesAlleProducten();
                List<Product> ProductNames = new List<Product>();
                foreach (Product product in Producten.Values)
                {
                    ProductNames.Add(product);
                }
                return ProductNames;
            }
            catch (Exception)
            {
                throw new ManagerException("GeefKlantengegid");
            }
        }
        public List<Offerte> ToonOffertes(int? offerteId, Klant? klant, DateTime? datum)
        {
            try
            {
                return TCRepository.HaalOfferteOp(offerteId, klant, datum);
            }
            catch (Exception)
            {
                throw new ManagerException("Geen offertes gevonden");
            }
        }

        public void SchrijfUpdateOfferte(Offerte offerte, Dictionary<Product, int> proddel, Dictionary<Product, int> produpdate, Dictionary<Product, int> prodnew)
        {
            try
            {
                TCRepository.UpdateOfferte(offerte, proddel, produpdate, prodnew);
            }
            catch (Exception) 
            {
                throw new ManagerException("Kon geen update schrijven");
            }
        }
    }
}

   

