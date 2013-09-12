using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Diagnostics;

namespace F1Tipping
{
	[DataContract]
	[DebuggerDisplay("{FirstName} {Surname}")]
	public class Tipster: IEquatable<Tipster>
	{
		[DataMember]
		public string FirstName { get; set; }
		[DataMember]
		public string Surname { get; set; }
		[DataMember]
		public string emailAddress { get; set; }
		[DataMember]
		public Tips Tips { get; set; }
		[DataMember]
		public int InitialScore { get; set; }

		public int Score { get; set; }

		public Tipster()
		{
			this.Tips = new Tips();
		}

		public bool Equals(Tipster other)
		{
			// if the same name...it's the same person :)
			return (this.FirstName == other.FirstName && this.Surname == other.Surname);
		}

        public override string ToString()
        {
            return this.FirstName + " " + this.Surname;
        } 
	}
}
