using System;
using System.Collections.Generic;
using Android.Widget;
using Android.Content;

namespace Labb2Xamarin
{
	public class BookkeeperManager 
	{
		private static BookkeeperManager bookkeeperManager = null;
		private List<Account> incomeAccounts;
		private List<Account> expenseAccounts;
		private List<Account> moneyAccounts;
		private List<TaxRate> taxRates;
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
			}
			return bookkeeperManager;
		}

		public void initiateLists()
		{
			//TODO Must check if database is empty, create... else fetch...
			incomeAccounts = GetListFromDB ("income");
			incomeAccounts = GetListFromDB ("expense");
			incomeAccounts = GetListFromDB ("money");
			taxRates = new List<TaxRate>{new TaxRate(0.06), new TaxRate(0.12), new TaxRate(0.25) };
			entries = new List<Entry>{ };
		}

		/// <summary>
		/// Gets the lists from Database
		/// </summary>
		/// <returns>The list from Database</returns>
		/// <param name="typeOfAccount">Specifies which acoounts that are fetched</param>
		private List<Account> GetListFromDB (string typeOfAccount)
		{
			//This should create lists from DB SQLite, using temp for now...
			if (typeOfAccount.Equals ("income")) {
				return new List<Account> 
				{
					new Account ("Sales", 3000),
					new Account ("Securities, yield", 3670)
				};
			} else if (typeOfAccount.Equals ("expense")) {
				return new List<Account> 
				{
					new Account ("Goods purchases", 4000),
					new Account ("Office supplies", 6010),
					new Account ("Salaries", 7010)
				};
			} else if (typeOfAccount.Equals ("money")) {
				return new List<Account> 
				{
					new Account ("Bank account", 1930),
					new Account ("Cash", 1910)
				};
			} else {
				Console.WriteLine ("Could not find list");
				return new List<Account>{ };
			}
		}

		/// <summary>
		/// Adds a new entry to the list and will also save it to the DB.
		/// </summary>
		/// <param name="entry">The new Entry that is collected in newEntryActivity and is added to the list.</param>
		public void AddEntry(Entry entry)
		{

		}



	}
}

