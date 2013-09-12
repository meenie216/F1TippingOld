using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace F1Tipping
{
	[DataContract]
	public class Tip
	{
		[DataMember]
		public Driver Qualifying { get; set; }
		[DataMember]
		public Driver Race { get; set; }

        public Int32 ScoreTip(GrandPrix race)
        {
            return race.ScoreForTip(this);
        }
	}
}
