using System;
using System.Collections.Generic;
using Android.Widget;
using Android.Content;
using System.Linq;
using SQLite;
using System.Runtime.InteropServices;
using Android.Util;
using Javax.Xml.Transform.Sax;

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

			Console.WriteLine ("inne i construktor för BKMANAGER");
			//If the database is empty we fill it with account and taxrates...
			try {
				db.Table<Account> ().Count ();
				Console.WriteLine ("db.Table<Account> ().Count ():    "+db.Table<Account> ().Count ());
				Console.WriteLine ("db.Table<TaxRate> ().Count ():    "+db.Table<TaxRate> ().Count ());
				Console.WriteLine ("db.Table<Entry> ().Count ():    "+db.Table<Entry> ().Count ());
			} catch (SQLiteException e) {
				Console.WriteLine ("inne i Exception");
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
			//entries.Add(entry);
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

		public List<Account> getIncomeAccounts()
		{
			return db.Table<Account> ().Where (x => x.Number >= 3000 && x.Number <= 3999).ToList (); ;
		}

		public List<Account> getExpenseAccounts()
		{
			return db.Table<Account> ().Where (x => x.Number >= 4000 && x.Number <= 7999).ToList ();
		}

		public List<Account> getMoneyAccounts()
		{
			return db.Table<Account> ().Where (x => x.Number >= 1000 && x.Number <= 1999).ToList ();
		}

		public List<TaxRate> getTaxRates()
		{
			return db.Table<TaxRate> ().ToList ();
		}

		public List<Entry> getEntries()
		{
			return db.Table<Entry>().ToList();
		}

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

			Console.WriteLine ("inne i Create AccountTable");
			List<Account> tempList = new List<Account>
			{
				new Account() {Name="Sales", Number=3000},
				new Account() {Name="Securities, yield", Number=3670},
				new Account() {Name="Goods purchases", Number=4000},
				new Account() {Name="Office supplies", Number=6010},
				new Account() {Name="Salaries", Number=7010},
				new Account() {Name="Bank account", Number=1930},
				new Account() {Name="Cash", Number=1910}
			};

			foreach (Account x in tempList) {
				Console.WriteLine ("element i templist: "+x.ToString ());
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
			db.Insert ( new TaxRate() {Id=25, Tax=0.25});
		}
	}
}

