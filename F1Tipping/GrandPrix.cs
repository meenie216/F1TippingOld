namespace F1Tipping
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Runtime.Serialization;
    using System.Diagnostics;

    [DataContract(IsReference = true)]
    [DebuggerDisplay("{Location}")]
    public class GrandPrix
    {
        [DataMember]
        public string Location { get; set; }

        [DataMember]
        public DateTime QualifyingSessionStart { get; set; }

        [DataMember]
        public List<Driver> QualifyingResults { get; set; }

        [DataMember]
        public List<Driver> RaceResults { get; set; }

        [DataMember]
        public Boolean ReadOnly { get; set; }

        public override string ToString()
        {
            return this.Location + " Grand Prix";
        }
        
        public GrandPrix()
        {
            this.QualifyingResults = new List<Driver>();
            this.RaceResults = new List<Driver>();
        }

        public Int32 QualifyingResultForDriver(Driver driver)
        {
            return QualifyingResults.Position(driver);
        }

        public Int32 RaceResultForDriver(Driver driver)
        {
            return RaceResults.Position(driver);
        }

        public Int32 ScoreForTip(Tip tip)
        {
            Int32 quallyResult = this.QualifyingResultForDriver(tip.Qualifying);
            Int32 raceResult = this.RaceResultForDriver(tip.Race);

            return quallyResult + raceResult;

        }

        public Double AverageScore { get; set; }

        public string GenerateSummary()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0} Grand Prix\n\n", this.Location);
            sb.AppendLine("Qualifying");

            for (int i = 0; i < this.QualifyingResults.Count; i++)
            {
                sb.AppendFormat("{0}.\t{1} ({2})\n", i+1, this.QualifyingResults[i].Name, this.QualifyingResults[i].TeamName);
            }

            sb.AppendLine("\nRace");

            for (int i = 0; i < this.RaceResults.Count; i++)
            {
                sb.AppendFormat("{0}.\t{1} ({2})\n", i+1, this.RaceResults[i].Name, this.RaceResults[i].TeamName);
            }


            //TODO: Add Tipping Summary

            return sb.ToString();
        }
    }
}
