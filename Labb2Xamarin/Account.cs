using System;
using SQLite;

namespace Labb2Xamarin
{
	public class Account
	{

		[PrimaryKey]
		public int Number{ get; private set;}
		public string Name{ get; private set;}

		public Account ()
		{
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

