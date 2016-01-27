using System;

namespace Labb2Xamarin
{
	public class Account
	{
		public String Name{private get; private set;}
		public int Number{private get; private set;}



		public Account (String name, int number)
		{
			Name = name;
			Number = number;
		}
	}
}

