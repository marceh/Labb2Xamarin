using System;

namespace Labb2Xamarin
{
	public class Account
	{
		public string Name{ get; private set;}
		public int Number{ get; private set;}



		public Account (string name, int number)
		{
			Name = name;
			Number = number;
		}
	}
}

