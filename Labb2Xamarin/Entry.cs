using System;
using Java.Sql;

namespace Labb2Xamarin
{
	public class Entry
	{

		public bool IsIncome{ get; private set;}
		public string Date{ get; private set;}
		public string Description{ get; private set;}
		public int TotalAmount{ get; private set;}
		public Account Account{ get; private set;}
		public TaxRate TaxRate{ get; private set;}

		public Entry (bool isIncome, string date, string description, int totalAmount, Account account, TaxRate taxRate)
		{
			IsIncome = isIncome;
			Date = date;
			Description = description;
			TotalAmount = totalAmount;
			Account = account;
			TaxRate = taxRate;
		}
	}
}