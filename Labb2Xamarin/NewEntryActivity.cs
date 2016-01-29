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
		ArrayAdapter adapterSpinnerTypeIncome;
		ArrayAdapter adapterSpinnerTypeExpense;
		ArrayAdapter adapterSpinnerTypeMoney;
		ArrayAdapter adapterSpinnerTaxRates;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
			SetContentView (Resource.Layout.NewEntry);

			bkManager = BookkeeperManager.GetInstance ();
			buttonAddEntry = FindViewById<Button> (Resource.Id.buttonAddEntry);
			radioButtonIncome = FindViewById<RadioButton> (Resource.Id.radioButtonIncome);
			radioButtonExpences = FindViewById<RadioButton> (Resource.Id.radioButtonExpences);
			spinnerType = FindViewById<Spinner> (Resource.Id.spinnerType);
			spinnerTax = FindViewById<Spinner> (Resource.Id.spinnerTax);
			spinnerAccount = FindViewById<Spinner> (Resource.Id.spinnerAccount);
			adapterSpinnerTypeIncome = new ArrayAdapter (this, Android.Resource.Layout.SimpleSpinnerItem, bkManager.getIncomeAccounts());
			adapterSpinnerTypeExpense = new ArrayAdapter (this, Android.Resource.Layout.SimpleSpinnerItem, bkManager.getExpenseAccounts ());
			adapterSpinnerTypeMoney = new ArrayAdapter (this, Android.Resource.Layout.SimpleSpinnerItem, bkManager.getMoneyAccounts ());
			adapterSpinnerTaxRates = new ArrayAdapter (this, Android.Resource.Layout.SimpleSpinnerItem, bkManager.getTaxRates());

			spinnerType.Adapter = adapterSpinnerTypeIncome;
			spinnerAccount.Adapter = adapterSpinnerTypeMoney;
			spinnerTax.Adapter = adapterSpinnerTaxRates;





			buttonAddEntry.Click += delegate {
				//createEntry ();
			};

			radioButtonIncome.Click += delegate {
				pushedRadioGroup();
			};

			radioButtonExpences.Click += delegate {
				pushedRadioGroup();	
			};

		}

		private void pushedRadioGroup()
		{
			if (radioButtonIncome.Checked) {
				spinnerType.Adapter = adapterSpinnerTypeIncome;
			} else {
				spinnerType.Adapter = adapterSpinnerTypeExpense;
			}
		}






/*

		private void createEntry()
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


