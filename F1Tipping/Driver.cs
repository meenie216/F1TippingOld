using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Diagnostics;

namespace F1Tipping
{
	[DataContract(IsReference=true)]
	[DebuggerDisplay("{Name} ({TeamName})")]
	public class Driver
	{
		[DataMember]
		public String Name { get; set; }
		[DataMember]
		public String TeamName { get; set; }


		public override string ToString()
		{
			return this.Name + " " + this.TeamName;
		}
	}

	public static class Extension
	{
		public static int Position(this List<Driver> results, Driver driver)
		{
			return results.Select((d, index) => new { index = index, Driver = d }).Where(d => d.Driver == driver).Select(d=>d.index).FirstOrDefault();
		}
	}
}
