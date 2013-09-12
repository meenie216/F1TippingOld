using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace F1Tipping
{
	public class Tips : Dictionary<GrandPrix, Tip>
	{
		public Int32 CalculateScoreForRace(GrandPrix grandPrix)
		{
			int score = 0;
			Tip raceTip;

			if (this.TryGetValue(grandPrix, out raceTip))
			{
				score = grandPrix.ScoreForTip(raceTip);
			}

			return score;
		}

		//public Int32 CalculateScore(Season season)
		//{
		//    int score = 0;

		//    foreach (var gp in season.Races)
		//    {
		//        score+= 
		//    }
		//}
	}
}
