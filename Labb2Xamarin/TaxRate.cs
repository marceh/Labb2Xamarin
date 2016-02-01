using System;
using SQLite;

namespace Labb2Xamarin
{
	public class TaxRate
	{
		[PrimaryKey]
		public double Tax{ get; private set;}

		public TaxRate (double tax)
		{
			Tax = tax;
		}

		public override string ToString()
		{
			return ""+(int)(Tax*100)+"%";
		}
	}
}

