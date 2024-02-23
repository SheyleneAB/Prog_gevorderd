

using System;

namespace OpdrachtSchool
{
    class Program
    {
        static void Main()
        {
            //we maken de school aan (met bib, autobus, lokalen en personenlijst)
            School school = new School();

            //We voegen boeken toe aan de bibliotheek
            school.SchoolBibliotheek.Boeken.Add(new() { Auteur = "Dirk", Titel = "Analyse" });
            school.SchoolBibliotheek.Boeken.Add(new() { Auteur = "Leo", Titel = "Ontwerp" });

            //We vragen een overzicht van de boeken
            Console.WriteLine("De bibliotheek van de school bevat volgende boeken: \n");
            school.SchoolBibliotheek.Boeken.ForEach(boek => 
            Console.WriteLine($" {boek.Titel} geschreven door {boek.Auteur}"));

            //we voegen twee studenten en een leerkracht toe aan de school
            school.Personen.Add(new Student("Piet"));
            school.Personen.Add(new Leerkracht("Karel"));
            school.Personen.Add(new Student("Danny"));

            //we vragen een overzicht van de personen en wat ze doen
            Console.WriteLine("\nIn de school zijn volgende personen aanwezig: \n");

            school.Personen.ForEach(persoon =>
            {
                //polymorfisme: Een persoon kan een student of een leerkracht zijn
                if (persoon is Student)
                {
                    persoon.StelUVoor();
                    ((Student)persoon).Lesvolgen();
                }
                if (persoon is Leerkracht)
                {
                    persoon.StelUVoor();
                    ((Leerkracht)persoon).Lesgeven();
                }
            });

            //We laten de autobus de dingen doen waarvan hij beloofd heeft deze te implementeren
            Console.WriteLine("\nDe school heeft een schoolbus die volgende acties kan doen: \n");
            Console.WriteLine(school.DeSchoolbus.Remmen());
            Console.WriteLine(school.DeSchoolbus.Rijden());

            //We voegen lokalen toe aan de school
            school.Klaslokalen.Add(new Klaslokaal { KlasLokaalnr = "1A" });
            school.Klaslokalen.Add(new Klaslokaal { KlasLokaalnr = "1B" });

            //We vragen een overzicht van de lokalen
            Console.WriteLine("\nIn de school bevinden zich volgende klaslokalen: \n");
            school.Klaslokalen.ForEach(lokaal => Console.WriteLine($"{lokaal.KlasLokaalnr}"));

        }
    }
}