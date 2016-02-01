using System;
using Java.Sql;
using SQLite;

namespace Labb2Xamarin
{
	public class Entry
	{
		[PrimaryKey, AutoIncrement]
		public int id{ get; private set;}
		public bool IsIncome{ get; set;}
		public string Date{ get; set;}
		public string Description{ get; set;}
		public double TotalAmount{ get; set;}
		public int TypeAccount{ get; set;}
		public int MoneyAccount{ get; set;}
		public TaxRate TaxRate{ get; set;}

		public Entry (/*bool isIncome, string date, string description, double totalAmount, Account typeAccount, Account moneyAccount, TaxRate taxRate*/)
		{
/*			IsIncome = isIncome;
			Date = date;
			Description = description;
			TotalAmount = totalAmount;
			TypeAccount = typeAccount;
			MoneyAccount = moneyAccount;
			TaxRate = taxRate;
*/
		}

		public override string ToString()
		{
			return "isIncome: " + IsIncome + ", date: " + Date + " description: " + Description + " totalAmount: " + TotalAmount + " typeAccount: " + TypeAccount.ToString () + " moneyAccount: " + MoneyAccount.ToString () + " taxRate: " + TaxRate.ToString ();
		}
	}
}