using System;
using System.Collections.Generic;
using Android.Widget;
using Android.Content;

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












		/// <summary>
		/// Since private constructor we are not able to create a new instance of the class...
		/// </summary>
		private BookkeeperManager(){/*should be a singleton!...*/}

		/// <summary>
		/// Returns the ONLY instance of the class, if null. we initate the ONLY instance.
		/// </summary>
		/// <returns>The instance.</returns>
		public static BookkeeperManager GetInstance() 
		{
			if (null == bookkeeperManager) {
				bookkeeperManager = new BookkeeperManager();
				bookkeeperManager.initiateLists ();
			}
			return bookkeeperManager;
		}

		private void initiateLists()
		{
			//TODO Must check if database is empty, create... else fetch...
			incomeAccounts = GetListFromDB ("income");
			expenseAccounts = GetListFromDB ("expense");
			moneyAccounts = GetListFromDB ("money");
			taxRates = new List<string>{new TaxRate(0.06).ToString (), new TaxRate(0.12).ToString (), new TaxRate(0.25).ToString () };
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
					new Account ("Sales", 3000).ToString (),
					new Account ("Securities, yield", 3670).ToString ()
				};
			} else if (typeOfAccount.Equals ("expense")) {
				return new List<string> 
				{
					new Account ("Goods purchases", 4000).ToString (),
					new Account ("Office supplies", 6010).ToString (),
					new Account ("Salaries", 7010).ToString ()
				};
			} else if (typeOfAccount.Equals ("money")) {
				return new List<string> 
				{
					new Account ("Bank account", 1930).ToString (),
					new Account ("Cash", 1910).ToString ()
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
			entries.Add (entry);
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






	}
}

