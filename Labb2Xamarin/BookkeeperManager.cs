using System;
using System.Collections.Generic;
using Android.Widget;
using Android.Content;
using System.Linq;
using SQLite;

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
		SQLiteConnection db;

		//TODO make these private with properties...
		public Account sales = new Account ("Sales", 3000);
		public Account securities = new Account ("Securities, yield", 3670);
		public Account goodsPurchases = new Account ("Goods purchases", 4000);
		public Account officeSupplies = new Account ("Office supplies", 6010);
		public Account salaries = new Account ("Salaries", 7010);
		public Account bankAccount = new Account ("Bank account", 1930);
		public Account cash = new Account ("Cash", 1910);
		public TaxRate six = new TaxRate (0.06);
		public TaxRate twelve = new TaxRate (0.12);
		public TaxRate twentyfive = new TaxRate (0.25);


		/// <summary>
		/// Since private constructor we are not able to create a new instance of the class...
		/// </summary>
		private BookkeeperManager(){
			db = new SQLiteConnection (path + @"\database.db");
			//TODO: make sure to check if already created...
			db.CreateTable<Entry> ();
			db.CreateTable<Account> ();
			db.CreateTable<TaxRate> ();
			CreateTheAccountTable ();
			CreateTheTaxRateTable ();

			initiateLists ();
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

		private void initiateLists()
		{
			//TODO Must check if database is empty, create... else fetch...
			incomeAccounts = GetListFromDB ("income");
			expenseAccounts = GetListFromDB ("expense");
			moneyAccounts = GetListFromDB ("money");
			taxRates = new List<string>{six.ToString (), twelve.ToString (), twentyfive.ToString () };
			entries = new List<Entry>{ };
		}

		/// <summary>
		/// Gets the lists from Database
		/// </summary>
		/// <returns>The list from Database</returns>
		/// <param name="typeOfAccount">Specifies which acoounts that are fetched</param>
		private List<string> GetListFromDB (string typeOfAccount)
		{
			//This should create lists from DB SQLite, using temp for now...
			if (typeOfAccount.Equals ("income")) {
				return new List<string> 
				{
					sales.ToString (),
					securities.ToString ()
				};
			} else if (typeOfAccount.Equals ("expense")) {
				return new List<string> 
				{
					goodsPurchases.ToString (),
					officeSupplies.ToString (),
					salaries.ToString ()
				};
			} else if (typeOfAccount.Equals ("money")) {
				return new List<string> 
				{
					bankAccount.ToString (),
					cash.ToString ()
				};
			} else {
				Console.WriteLine ("Could not find list");
				return new List<string>{ };
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

		public List<string> getIncomeAccounts()
		{
			return incomeAccounts;
		}

		public List<string> getExpenseAccounts()
		{
			return expenseAccounts;
		}

		public List<string> getMoneyAccounts()
		{
			return moneyAccounts;
		}

		public List<string> getTaxRates()
		{
			return taxRates;
		}

		public List<Entry> getEntries()
		{
			return db.Table<Entry>().ToList();
			//return entries;
		}

		public string ToString()
		{
			string tempString = "";
			for (int i = 0; i < entries.Count; i++) {
				tempString += "[";
				tempString += entries [i].ToString ();
				tempString += "]   ";
			}
			return tempString;
		}

		private void CreateTheAccountTable ()
		{
			public Account sales = new Account ("Sales", 3000);
			public Account securities = new Account ("Securities, yield", 3670);
			public Account goodsPurchases = new Account ("Goods purchases", 4000);
			public Account officeSupplies = new Account ("Office supplies", 6010);
			public Account salaries = new Account ("Salaries", 7010);
			public Account bankAccount = new Account ("Bank account", 1930);
			public Account cash = new Account ("Cash", 1910);
			public TaxRate six = new TaxRate (0.06);
			public TaxRate twelve = new TaxRate (0.12);
			public TaxRate twentyfive = new TaxRate (0.25);



		}

		private void CreateTheTaxRateTable ()
		{


		}




	}
}

