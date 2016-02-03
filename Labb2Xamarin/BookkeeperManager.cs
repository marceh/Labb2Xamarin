using System;
using System.Collections.Generic;
using Android.Widget;
using Android.Content;
using Android.App;
using Android.OS;
using System.Linq;
using SQLite;
using System.Runtime.InteropServices;
using Android.Util;
using Javax.Xml.Transform.Sax;
using Android.Net;
using Android.Content.Res;
using Labb2Xamarin;



namespace Labb2Xamarin
{
	public class BookkeeperManager 
	{
		private static BookkeeperManager bookkeeperManager = null;
		private List<string> incomeAccounts;
		private List<string> expenseAccounts;
		private List<string> moneyAccounts;
		private List<string> taxRates;
		private List<Entry> entries;
		private string path = System.Environment.GetFolderPath (System.
			Environment.SpecialFolder.Personal);
		private SQLiteConnection db;

		/// <summary>
		/// Since private constructor we are not able to create a new instance of the class...
		/// </summary>
		private BookkeeperManager(){
			db = new SQLiteConnection (path + @"\database.db");
			incomeAccounts = new List<String>();
			expenseAccounts = new List<String>();
			moneyAccounts = new List<String>();
			taxRates = new List<String>();
			entries = new List<Entry>();

			//If the database is empty we fill it with account and taxrates...
			try {
				db.Table<Account> ().Count ();
			} catch (SQLiteException e) {
				db.CreateTable<Entry> ();
				db.CreateTable<Account> ();
				db.CreateTable<TaxRate> ();
				CreateTheAccountTable ();
				CreateTheTaxRateTable ();
			}

			//The database cannot be empty due to previous try catch statement so now we fetch the database lists into our lists. 
			GetListFromDB ();
		}

		/// <summary>
		/// Returns the ONLY instance of the class, if null. we initate the ONLY instance.
		/// </summary>
		/// <returns>The instance.</returns>
		public static BookkeeperManager GetInstance() 
		{
			if (null == bookkeeperManager) {
				bookkeeperManager = new BookkeeperManager();
			}
			return bookkeeperManager;
		}

		/// <summary>
		/// Gets the lists from Database
		/// </summary>
		/// <returns>The list from Database</returns>
		/// <param name="typeOfAccount">Specifies which acoounts that are fetched</param>
		private void GetListFromDB ()
		{
			foreach (Account x in getIncomeAccounts ()) {
				incomeAccounts.Add (x.ToString ());
			}

			foreach (Account x in getExpenseAccounts ()) {
				expenseAccounts.Add (x.ToString());
			}

			foreach (Account x in getMoneyAccounts ()) {
				moneyAccounts.Add (x.ToString ());
			}

			foreach (TaxRate x in getTaxRates ()) {
				taxRates.Add (x.ToString ());
			}

			foreach (Entry x in getEntries ()) {
				entries.Add (x);
			}
		}

		/// <summary>
		/// Adds a new entry to the list and will also save it to the DB.
		/// </summary>
		/// <param name="entry">The new Entry that is collected in newEntryActivity and is added to the list.</param>
		public void AddEntry(Entry entry)
		{
			db.Insert (entry);
		}

		/*
		 *  In accounting the Account plan is organised after it's number so,
		 *  I will take advantage of the numbers to specify whether the account is of income, expense or money (asset) type.
		 * 
		 * 	Account Class 1 for assets (account number 1000-1999)
		 *	Account Class 2 for equity and debt (2000-2999)
		 *	Account Class 3 for revenues (3000-3999)
		 *	Account Class 4 for material and product costs (4000-4999)
		 *	Account Class 5 and 6 for other costs (5000-6999)
		 *	Account Class 7 of staff costs (7000-7999)
		 *	Account Class 8 Financial income and expenses (8000-8999)
		 * 
		 */

		/// <summary>
		/// Gets the income accounts.
		/// </summary>
		/// <returns>The income accounts.</returns>
		public List<Account> getIncomeAccounts()
		{
			return db.Table<Account> ().Where (x => x.Number >= 3000 && x.Number <= 3999).ToList (); ;
		}

		/// <summary>
		/// Gets the expense accounts.
		/// </summary>
		/// <returns>The expense accounts.</returns>
		public List<Account> getExpenseAccounts()
		{
			return db.Table<Account> ().Where (x => x.Number >= 4000 && x.Number <= 7999).ToList ();
		}

		/// <summary>
		/// Gets the money accounts.
		/// </summary>
		/// <returns>The money accounts.</returns>
		public List<Account> getMoneyAccounts()
		{
			return db.Table<Account> ().Where (x => x.Number >= 1000 && x.Number <= 1999).ToList ();
		}

		/// <summary>
		/// Gets the tax rates.
		/// </summary>
		/// <returns>The tax rates.</returns>
		public List<TaxRate> getTaxRates()
		{
			return db.Table<TaxRate> ().ToList ();
		}

		/// <summary>
		/// Gets the entries.
		/// </summary>
		/// <returns>The entries.</returns>
		public List<Entry> getEntries()
		{
			return db.Table<Entry>().ToList();
		}

		/// <summary>
		/// Returns a good sentance over the entries.
		/// </summary>
		/// <returns> Returns a good sentance over the entries</returns>
		public override string ToString()
		{
			string tempString = "";
			for (int i = 0; i < entries.Count; i++) {
				tempString += "[";
				tempString += entries [i].ToString ();
				tempString += "]   ";
			}
			return tempString;
		}

		/// <summary>
		/// Creates the account table, only the first time...
		/// </summary>
		private void CreateTheAccountTable ()
		{
			List<Account> tempList = new List<Account>
			{
				new Account() {Name=""+Resource.String.Sales, Number=3000},
				new Account() {Name=""+Resource.String.Securities_yield, Number=3670},
				new Account() {Name=""+Resource.String.Goods_purchases, Number=4000},
				new Account() {Name=""+Resource.String.Office_supplies, Number=6010},
				new Account() {Name=""+Resource.String.Salaries, Number=7010},
				new Account() {Name=""+Resource.String.Bank_account, Number=1930},
				new Account() {Name=""+Resource.String.Cash, Number=1910}
			};

			foreach (Account x in tempList) {
				db.Insert (x);
			}
		}

		/// <summary>
		/// Creates the tax rate table, only the first time...
		/// </summary>
		private void CreateTheTaxRateTable ()
		{
			db.Insert (new TaxRate() {Id=6, Tax=0.06});
			db.Insert (new TaxRate() {Id=12, Tax=0.12});
			db.Insert (new TaxRate() {Id=25, Tax=0.25});
		}

		/// <summary>
		/// Sends the tax report by mail. the Mail text is fetched from the CreateTaxReportString method
		/// </summary>
		/// <param name="context">the context that sends the intent</param>
		public void sendTaxReport(Context context)
		{
			var email = new Intent (Android.Content.Intent.ActionSend);
			email.PutExtra (Android.Content.Intent.ExtraSubject, context.Resources.GetString (Resource.String.Tax_report));
			email.PutExtra (Android.Content.Intent.ExtraText, CreateTaxReportString ());
			email.SetType ("message/rfc822");
			context.StartActivity (email);
		}

		private string CreateTaxReportString ()
		{
			List<Entry> tempList = getEntries (); 
			string tempString = "";
			double tempDouble = 0.00;

			foreach (Entry x in tempList) {
				tempString += x.EntryTaxDetails ()+"\n";
				if (x.IsIncome) {
					tempDouble += (Math.Round(x.TotalAmount-(x.TotalAmount/x.TaxRate),	2));
				} else {
					tempDouble -= (Math.Round(x.TotalAmount-(x.TotalAmount/x.TaxRate),	2));	
				}
			}
			tempString += "Total amount to pay: $" + tempDouble;
			return tempString;
		}

	}
}

