using System;

namespace Labb2Xamarin
{
	public class TaxRate
	{
		public double Tax{ get; private set;}

		public TaxRate (double tax)
		{
			Tax = tax;
		}

		public string ToString()
		{
			return ""+(int)(Tax*100)+"%";
		}
	}
}

