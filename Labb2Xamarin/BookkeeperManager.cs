using System;
using System.Collections.Generic;

namespace Labb2Xamarin
{
	public class BookkeeperManager 
	{
		private static BookkeeperManager bookkeeperManager = null;

		List<Account> incomeAccounts;
		List<Account> expenseAccounts;
		List<Account> moneyAccounts;
		List<TaxRate> taxRates;
		List<Entry> entries;

		public void AddEntry(Entry entry)
		{
			
		}










		private BookkeeperManager(){/*should be a singleton!...*/}

		public static BookkeeperManager GetInstance() 
		{
			if (null == bookkeeperManager) {
				bookkeeperManager = new BookkeeperManager();
			}
			return bookkeeperManager;
		}
	}
}

