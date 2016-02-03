using System;
using SQLite;

namespace Labb2Xamarin
{
	public class TaxRate
	{
		[PrimaryKey,]
		public int Id{ get; set;}
		public double Tax{ get; set;}

		public TaxRate (){}

		public override string ToString()
		{
			return ""+(int)(Tax*100)+"%";
		}
	}
}

