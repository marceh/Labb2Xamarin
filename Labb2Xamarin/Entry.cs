using System;
using Java.Sql;

namespace Labb2Xamarin
{
	public class Entry
	{

		public bool IsIncome{ get; private set;}
		public string Date{ get; private set;}
		public string Description{ get; private set;}
		public double TotalAmount{ get; private set;}
		public Account TypeAccount{ get; private set;}
		public Account MoneyAccount{ get; private set;}
		public TaxRate TaxRate{ get; private set;}

		public Entry (bool isIncome, string date, string description, double totalAmount, Account typeAccount, Account moneyAccount, TaxRate taxRate)
		{
			IsIncome = isIncome;
			Date = date;
			Description = description;
			TotalAmount = totalAmount;
			TypeAccount = typeAccount;
			MoneyAccount = moneyAccount;
			TaxRate = taxRate;
		}
	}
}