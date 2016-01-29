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

		/// <summary>
		/// A ToString method, with number first and name of the account after...
		/// </summary>
		/// <returns>A string perfect for display in spinners...</returns>
		public override string ToString()
		{
			return "(" + Number + ") - " + Name;
		}
	}
}

