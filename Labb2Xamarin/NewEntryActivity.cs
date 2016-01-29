using Android.App;
using Android.Widget;
using Android.OS;

namespace Labb2Xamarin
{
	[Activity (Label = "Labb2Xamarin", MainLauncher = false, Icon = "@mipmap/icon")]
	public class NewEntryActivity : Activity
	{

		BookkeeperManager bkManager;
		Button buttonAddEntry;
		RadioButton radioButtonIncome;
		RadioButton radioButtonExpences;
		Spinner spinnerType;
		Spinner spinnerTax;
		Spinner spinnerAccount;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
			SetContentView (Resource.Layout.NewEntry);

			BookkeeperManager bkManager = BookkeeperManager.GetInstance ();
			Button buttonAddEntry = FindViewById<Button> (Resource.Id.buttonAddEntry);
			RadioButton radioButtonIncome = FindViewById<RadioButton> (Resource.Id.radioButtonIncome);
			RadioButton radioButtonExpences = FindViewById<RadioButton> (Resource.Id.radioButtonExpences);
			Spinner spinnerType = FindViewById<Spinner> (Resource.Id.spinnerType);
			Spinner spinnerTax = FindViewById<Spinner> (Resource.Id.spinnerTax);
			Spinner spinnerAccount = FindViewById<Spinner> (Resource.Id.spinnerAccount);

			ArrayAdapter adapterSpinnerTypeIncome = new ArrayAdapter (this, Android.Resource.Layout.SimpleSpinnerItem, bkManager.getIncomeAccounts());
			ArrayAdapter adapterSpinnerTypeExpense = new ArrayAdapter (this, Android.Resource.Layout.SimpleSpinnerItem, bkManager.getExpenseAccounts ());
			ArrayAdapter adapterSpinnerTypeMoney = new ArrayAdapter (this, Android.Resource.Layout.SimpleSpinnerItem, bkManager.getMoneyAccounts ());
			ArrayAdapter adapterSpinnerTaxRates = new ArrayAdapter (this, Android.Resource.Layout.SimpleSpinnerItem, bkManager.getTaxRates());

			spinnerType.Adapter = adapterSpinnerTypeIncome;
			spinnerAccount.Adapter = adapterSpinnerTypeMoney;
			spinnerTax.Adapter = adapterSpinnerTaxRates;





			buttonAddEntry.Click += delegate {
				//createEntry ();
			};
		}
/*
		/// <summary>
		/// Creates the entry based on the information from the GUI, the entry is then shipped to addEntry using the manager.
		/// </summary>
		public void createEntry()
		{
			Entry createdEntry = new Entry (theIncome (), theDate (), theDescription (), theTotalAmount (), theAccount (), theTaxRate ());
			//bkManager.AddEntry (createdEntry);
		}

		private bool theIncome()
		{
			return radioButtonIncome.Checked;
		}

		private string theDate()
		{
			
		}

		private string theDescription()
		{
			
		}

		private int theTotalAmount()
		{
			
		}

		private Account theAccount()
		{
			
		}

		private TaxRate theTaxRate()
		{
			
		}
*/

	}
}


