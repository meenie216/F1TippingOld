//-----------------------------------------------------------------------
// <copyright file="Season.cs" company="Weatherford International Ltd.">
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

	[DataContract]
	public class Season
	{

		[DataMember]
		public List<GrandPrix> Races { get; set; }

		[DataMember]
		public List<Driver> Drivers { get; set; }

		public Season()
		{
			this.Races = new List<GrandPrix>();
			this.Drivers = new List<Driver>();
		}
	}
}
