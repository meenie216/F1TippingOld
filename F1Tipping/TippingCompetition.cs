//-----------------------------------------------------------------------
// <copyright file="TippingCompetition.cs" company="Weatherford International Ltd.">
// Copyright 2008, 2009, 2010, 2011  Weatherford International  All rights reserved.
// This software and documentation constitute an unpublished work and 
// contain valuable trade secrets and proprietary information belonging 
// to Weatherford. None of the foregoing material may be copied, duplicated 
// or disclosed without the express written permission of Weatherford.
// </copyright>
//-----------------------------------------------------------------------

namespace F1Tipping
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Runtime.Serialization;
    using System.Globalization;

    [DataContract]
    public class TippingCompetition
    {
        [DataMember]
        public Season Season { get; set; }
        [DataMember]
        public List<Tipster> Tipsters { get; set; }

        public TippingCompetition()
        {
            this.Season = new Season();
            this.Tipsters = new List<Tipster>();
        }

        public void AddTip(Tipster tipster, GrandPrix race, Tip tip)
        {
            // Check that Tipster exists
            if (this.Tipsters.Contains(tipster))
            {
                tipster = this.Tipsters.First(t => t.Equals(tipster));
            }
            else
            {
                this.Tipsters.Add(tipster);
            }

            bool foundRace = false;

            // Add the tip in for this race, and all following races of the season.
            foreach (var gp in this.Season.Races)
            {
                if (race == gp)
                {
                    foundRace = true;
                }

                if (foundRace)
                {
                    Tip storedTip;

                    if (!tipster.Tips.TryGetValue(gp, out storedTip))
                    {
                        tipster.Tips.Add(gp, null);
                    }

                    tipster.Tips[gp] = tip;
                }
            }
        }


        public double AverageScoreForTipstersWhoTipped(GrandPrix grandPrix)
        {
            var tips = from tipster in Tipsters
                       from tip in tipster.Tips
                       where tip.Key == grandPrix && tip.Value != null
                       select grandPrix.ScoreForTip(tip.Value);


            return tips.Count() > 0 ? tips.Average() : 0;
        }

        private void BuildAverages()
        {
            var averages = from gp in this.Season.Races
                           select new { GrandPrix = gp, AverageScore = AverageScoreForTipstersWhoTipped(gp) };

            foreach (var item in averages)
            {
                item.GrandPrix.AverageScore = item.AverageScore;
            }
        }

        public void GenerateScores()
        {
            var scores = from tipster in Tipsters
                         select new
                         {
                             Tipster = tipster,
                             Score = (from tip in tipster.Tips
                                      select tip.Key.ScoreForTip(tip.Value)).Sum()
                         };

            foreach (var item in scores)
            {
                item.Tipster.Score = item.Score;
            }
        }

        public Dictionary<Tipster, Tip> TipsForRace(GrandPrix race)
        {
            var tips = from tipster in Tipsters
                       select new
                       {
                           Tipster = tipster,
                           Tip = tipster.Tips.ContainsKey(race) ? tipster.Tips.FirstOrDefault(i => i.Key == race).Value : null
                       };

            Dictionary<Tipster, Tip> tipsByTipster = new Dictionary<Tipster, Tip>();
            foreach (var item in tips)
            {
                if (item.Tip!=null)
                {
                    tipsByTipster.Add(item.Tipster, item.Tip);
                }
            }

            return tipsByTipster;
        }

        public Dictionary<Tipster, Int32> ScoresForRace(GrandPrix race)
        {
            Dictionary<Tipster, Int32> scoresByTipster = new Dictionary<Tipster, int>();

            var scores = from tipster in Tipsters

                         select new
                         {
                             Tipster = tipster,
                             Score = tipster.Tips.Where(t => t.Key == race).Select(t => t.Value.ScoreTip(race)).FirstOrDefault()

                             //(from tip in tipster.Tips
                             //         where tip.Key == race
                             //         select race.ScoreForTip(tip.Value))
                         };

            foreach (var item in scores)
            {
                if (!scoresByTipster.ContainsKey(item.Tipster))
                {
                    scoresByTipster.Add(item.Tipster, item.Score);
                }
            }

            return scoresByTipster;
        }

        public String BuildLeaderTable()
        {
            var leaderBoard = Tipsters.OrderBy(t => (t.Score + t.InitialScore));

            StringBuilder sb = new StringBuilder();

            foreach (var item in leaderBoard)
            {
                sb.AppendFormat(CultureInfo.InvariantCulture, "{0} {1}\t{2}", item.FirstName, item.Surname, item.Score);
                sb.AppendLine();
            }

            return sb.ToString();
        }

        public String BuildSummaryForGrandPrix(GrandPrix race)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(race.GenerateSummary());

            var tips = this.TipsForRace(race).OrderBy(p => p.Key.Surname);
            sb.AppendLine("\nTips");
            foreach (var item in tips)
            {
                if (item.Value != null)
                {
                    sb.AppendFormat(CultureInfo.InvariantCulture, "{0} {1}\t{2}\t{3}\n", item.Key.FirstName, item.Key.Surname, item.Value.Qualifying.Name, item.Value.Race.Name);
                }
            }

            sb.AppendLine();

            sb.AppendLine("\nTipping Scores");
            var scores = this.ScoresForRace(race).OrderBy(p => p.Value);

            foreach (var item in scores)
            {
                sb.AppendFormat(CultureInfo.InvariantCulture, "{0} {1}\t{2}\n", item.Key.FirstName, item.Key.Surname, item.Value);

            }

            sb.AppendLine("\nLeaders Table");
            sb.Append(BuildLeaderTable());

            return sb.ToString();
        }
    }
}
