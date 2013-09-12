using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using F1Tipping;
using System.IO;
using System.Xml;
using System.Runtime.Serialization;

namespace TestApp
{
	class Program
	{
        static void Main(string[] args)
        {
            //RunTest();

            //Initial Setup

            TippingCompetition comp = new TippingCompetition();

            Season season = comp.Season;

            season.Races.Add(new GrandPrix() { Location = "Australian", QualifyingSessionStart = new DateTime(2013,3,16, 14,0,0) });
            season.Races.Add(new GrandPrix() { Location = "Malaysian" });
            season.Races.Add(new GrandPrix() { Location = "Chinese" });
            season.Races.Add(new GrandPrix() { Location = "Bahrain" });
            season.Races.Add(new GrandPrix() { Location = "Spainish" });
            season.Races.Add(new GrandPrix() { Location = "Monaco" });
            season.Races.Add(new GrandPrix() { Location = "Canadian" });
            season.Races.Add(new GrandPrix() { Location = "British" });
            season.Races.Add(new GrandPrix() { Location = "German" });
            season.Races.Add(new GrandPrix() { Location = "Hungarian" });
            season.Races.Add(new GrandPrix() { Location = "Belgian" });
            season.Races.Add(new GrandPrix() { Location = "Italian" });
            season.Races.Add(new GrandPrix() { Location = "Singaporean" });
            season.Races.Add(new GrandPrix() { Location = "Korean" });
            season.Races.Add(new GrandPrix() { Location = "Japanese" });
            season.Races.Add(new GrandPrix() { Location = "Indian" });
            season.Races.Add(new GrandPrix() { Location = "Abu Dhabi" });
            season.Races.Add(new GrandPrix() { Location = "United States" });
            season.Races.Add(new GrandPrix() { Location = "Brazillian" });

            season.Drivers.Add(new Driver() { Name = "Sebastian Vettel", TeamName = "Red Bull" });
            season.Drivers.Add(new Driver() { Name = "Mark Webber", TeamName = "Red Bull" });
            season.Drivers.Add(new Driver() { Name = "Fernando Alonso", TeamName = "Ferrari" });
            season.Drivers.Add(new Driver() { Name = "Felipe Massa", TeamName = "Ferrari" });
            season.Drivers.Add(new Driver() { Name = "Jenson Button", TeamName = "McLaren" });
            season.Drivers.Add(new Driver() { Name = "Sergio Perez", TeamName = "McLaren" });
            season.Drivers.Add(new Driver() { Name = "Kimi Raikkonen", TeamName = "Lotus" });
            season.Drivers.Add(new Driver() { Name = "Romain Grosjean", TeamName = "Lotus" });
            season.Drivers.Add(new Driver() { Name = "Nico Rosberg", TeamName = "Mercedes" });
            season.Drivers.Add(new Driver() { Name = "Lewis Hamilton", TeamName = "Mercedes" });
            season.Drivers.Add(new Driver() { Name = "Nico Hulkenberg", TeamName = "Sauber" });
            season.Drivers.Add(new Driver() { Name = "Esteban Guiterrez", TeamName = "Sauber" });
            season.Drivers.Add(new Driver() { Name = "Paul Di Resta", TeamName = "Force India" });
            season.Drivers.Add(new Driver() { Name = "Adrian Sutil", TeamName = "Force India" });
            season.Drivers.Add(new Driver() { Name = "Pastor Maldonado", TeamName = "Williams" });
            season.Drivers.Add(new Driver() { Name = "Valtteri Bottas", TeamName = "Williams" });
            season.Drivers.Add(new Driver() { Name = "Jean-Eric Vergne", TeamName = "Toro Rosso" });
            season.Drivers.Add(new Driver() { Name = "Daniel Ricciardo", TeamName = "Toro Rosso" });
            season.Drivers.Add(new Driver() { Name = "Charles Pic", TeamName = "Caterham" });
            season.Drivers.Add(new Driver() { Name = "Giedo van der Garde", TeamName = "Caterham" });
            season.Drivers.Add(new Driver() { Name = "Jules Bianchi", TeamName = "Marussia" });
            season.Drivers.Add(new Driver() { Name = "Max Chilton", TeamName = "Marussia" });

            PersistTippingComp(comp);
        
        }



		static void RunTest()
		{
			
			TippingCompetition comp = new TippingCompetition();

			// Create new Season
			Season season = comp.Season;

			GrandPrix Melbourne;
			GrandPrix KL;

			season.Races.Add(Melbourne = new GrandPrix() { Location = "Australian" });
			season.Races.Add(KL = new GrandPrix() { Location = "Malaysian" });
			season.Races.Add(new GrandPrix() { Location = "Chinese" });
			season.Races.Add(new GrandPrix() { Location = "Bahrain" });

			Driver Webber;
			Driver Vettel;
			Driver Kimi;

			season.Drivers.Add(Webber = new Driver() { Name = "Mark Webber", TeamName="Red Bull"});
			season.Drivers.Add(Vettel = new Driver() { Name = "Sebastian Vettel", TeamName = "Red Bull"});
			season.Drivers.Add(Kimi = new Driver() { Name="Kimi Raikkonen", TeamName = "Lotus"});

			Tipster Matt;
			Tipster Dave;
			Tipster Hanky;

			comp.Tipsters.Add(Matt = new Tipster() { FirstName = "Matt", Surname = "Nicholls" });
			comp.Tipsters.Add(Dave = new Tipster() { FirstName = "David", Surname = "Mutch" });
			comp.Tipsters.Add(Hanky = new Tipster() { FirstName = "Daniel", Surname = "Hancock" });

			//Matt.Tips.Add(Melbourne, new Tip() { Qualifying = Webber, Race = Webber });
			//Dave.Tips.Add(Melbourne, new Tip() { Qualifying = Kimi, Race = Vettel });
			//Hanky.Tips.Add(Melbourne, new Tip() { Qualifying = Vettel, Race = Webber });

			//Matt.Tips.Add(KL, new Tip() { Qualifying = Webber, Race = Webber });
			//Dave.Tips.Add(KL, new Tip() { Qualifying = Kimi, Race = Vettel });
			////Hanky.Tips.Add(KL, new Tip() { Qualifying = Vettel, Race = Webber });

			comp.AddTip(Matt, Melbourne, new Tip() { Qualifying = Webber, Race = Webber });
			comp.AddTip(Dave, Melbourne, new Tip() { Qualifying = Kimi, Race = Vettel });
			comp.AddTip(Hanky, Melbourne, new Tip() { Qualifying = Vettel, Race = Webber });
			comp.AddTip(Matt, KL, new Tip() { Qualifying = Webber, Race = Vettel });
			comp.AddTip(Dave, KL, new Tip() { Qualifying = Kimi, Race = Vettel });

			Melbourne.QualifyingResults.Add(Webber);
			Melbourne.QualifyingResults.Add(Kimi);
			Melbourne.QualifyingResults.Add(Vettel);

			Melbourne.RaceResults.Add(Kimi);
			Melbourne.RaceResults.Add(Vettel);
			Melbourne.RaceResults.Add(Webber);

			KL.QualifyingResults.Add(Vettel);
			KL.QualifyingResults.Add(Webber);
			KL.QualifyingResults.Add(Kimi);

			KL.RaceResults.Add(Vettel);
			KL.RaceResults.Add(Webber);
			KL.RaceResults.Add(Kimi);


			comp.GenerateScores();

            PersistTippingComp(comp);

			comp = null;

            comp = DeserializeTippingCompFile(comp);

			comp.GenerateScores();
			Console.Write(comp.BuildLeaderTable());

			Console.ReadLine();
		}

        private static TippingCompetition DeserializeTippingCompFile(TippingCompetition comp)
        {
            using (Stream stream = new FileStream(@"C:\temp\tippingComp.xml", FileMode.Open, FileAccess.Read))
            {
                //using (XmlTextReader reader = XmlTextReader.Create(stream))
                //{
                DataContractSerializer serializer = new DataContractSerializer(typeof(TippingCompetition));
                comp = (TippingCompetition)serializer.ReadObject(stream);
                //}
            }
            return comp;
        }

        private static void PersistTippingComp(TippingCompetition comp)
        {
            using (Stream stream = new FileStream(@"C:\temp\tippingComp.xml", FileMode.Create, FileAccess.Write))
            {
                using (XmlWriter writer = XmlWriter.Create(stream))
                {
                    DataContractSerializer serializer = new DataContractSerializer(typeof(TippingCompetition));
                    serializer.WriteObject(writer, comp);
                }
            }
        }
	}
}
