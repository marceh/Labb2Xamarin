using Android.App;
using Android.Widget;
using Android.OS;
using System;

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
		EditText editTextDate;
		EditText editTextDesc;
		EditText editTextTotal;
		ArrayAdapter adapterSpinnerTypeIncome;
		ArrayAdapter adapterSpinnerTypeExpense;
		ArrayAdapter adapterSpinnerTypeMoney;
		ArrayAdapter adapterSpinnerTaxRates;
		int spinnerWillNotShowToastWhenEnteringPage;

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
			editTextDate = FindViewById<EditText> (Resource.Id.editTextDate);
			editTextDesc = FindViewById<EditText> (Resource.Id.editTextDesc);
			editTextTotal = FindViewById<EditText> (Resource.Id.editTextTotal);
			adapterSpinnerTypeIncome = new ArrayAdapter (this, Android.Resource.Layout.SimpleSpinnerItem, bkManager.getIncomeAccounts());
			adapterSpinnerTypeExpense = new ArrayAdapter (this, Android.Resource.Layout.SimpleSpinnerItem, bkManager.getExpenseAccounts ());
			adapterSpinnerTypeMoney = new ArrayAdapter (this, Android.Resource.Layout.SimpleSpinnerItem, bkManager.getMoneyAccounts ());
			adapterSpinnerTaxRates = new ArrayAdapter (this, Android.Resource.Layout.SimpleSpinnerItem, bkManager.getTaxRates());
			spinnerWillNotShowToastWhenEnteringPage = 0;
			spinnerType.Adapter = adapterSpinnerTypeIncome;
			spinnerAccount.Adapter = adapterSpinnerTypeMoney;
			spinnerTax.Adapter = adapterSpinnerTaxRates;

			buttonAddEntry.Click += delegate {
				CreateEntry ();
			};

			radioButtonIncome.Click += delegate {
				spinnerWillNotShowToastWhenEnteringPage = 2;
				PushedRadioGroup();
			};

			radioButtonExpences.Click += delegate {
				spinnerWillNotShowToastWhenEnteringPage = 2;
				PushedRadioGroup();	
			};

			spinnerTax.ItemSelected += new EventHandler <AdapterView.ItemSelectedEventArgs> (SpinnerSelected);
			spinnerType.ItemSelected += new EventHandler <AdapterView.ItemSelectedEventArgs> (SpinnerSelected);
			spinnerAccount.ItemSelected += new EventHandler <AdapterView.ItemSelectedEventArgs> (SpinnerSelected);

		}

		private void SpinnerSelected(Object sender, AdapterView.ItemSelectedEventArgs e)
		{
			Spinner s = (Spinner)sender;
			if (spinnerWillNotShowToastWhenEnteringPage > 2) {
				Toast.MakeText (this, ""+s.SelectedItem.ToString()+" selected", ToastLength.Short).Show ();
			}
			spinnerWillNotShowToastWhenEnteringPage++;
		}

		private void PushedRadioGroup()
		{
			if (radioButtonIncome.Checked) {
				spinnerType.Adapter = adapterSpinnerTypeIncome;
			} else {
				spinnerType.Adapter = adapterSpinnerTypeExpense;
			}
		}

		private void CreateEntry()
		{
			Entry createdEntry = new Entry (TheIncome (), TheDate (), TheDescription (), TheTotalAmount (), TheAccount("type"), TheAccount("money"), TheTaxRate ());
			Toast.MakeText (this, "Entry created!", ToastLength.Short).Show ();
			bkManager.AddEntry (createdEntry);
			//TODO ResetMenu ();
		}

		private bool TheIncome()
		{
			return radioButtonIncome.Checked;
		}

		private string TheDate()
		{
			return editTextDate.Text;
		}

		private string TheDescription()
		{
			return editTextDesc.Text;	
		}

		//TODO understand how the f*ck this works...
		private double TheTotalAmount()
		{
			string tempText = editTextTotal.Text;
			string tempText2 = "";
			Console.WriteLine(tempText);
			Console.WriteLine ("längd: "+tempText.Length);
			for (int i = 0; i < tempText.Length; i++) {
				if (tempText[i].Equals ('.')) {
					//when fetching from editText, the dot in double cannot be read but the comma can...Weird!...
					tempText2 += ",";
				} else {
					tempText2 += tempText [i];	
				}
			}
			double tempDouble = Convert.ToDouble (tempText2);
			return tempDouble;	
		}

		private Account TheAccount(string kindOfAccount)
		{
			string tempText;
			string number = "";
			if (kindOfAccount.Equals ("type")) 
			{
				tempText = spinnerType.SelectedItem.ToString ();
				for (int i = 1; i < 4; i++) {
					number += tempText [i];
				}

				if (number.Equals ("3000")) {
					return bkManager.sales;
				} else if (number.Equals ("3670")) {
					return bkManager.securities;
				} else if (number.Equals ("4000")) {
					return bkManager.goodsPurchases;
				} else if (number.Equals ("6010")) {
					return bkManager.officeSupplies;
				} else if (number.Equals ("7010")) {
					return bkManager.salaries;
				} else {
					return new Account ("NoName", 0000);
				}
			} else {
					
				tempText = spinnerAccount.SelectedItem.ToString();
				for (int i = 1; i < 4; i++) {
					number += tempText [i];
				}

				if (number.Equals ("1930")) {
					return bkManager.bankAccount;
				} else if (number.Equals ("1910")) {
					return bkManager.cash;
				} else {
					return new Account ("NoName", 0000);
				}
			}
		}

		private TaxRate TheTaxRate()
		{
			if (spinnerTax.SelectedItem.ToString()[0].Equals ('6')) {
				return bkManager.six;
			} else if (spinnerTax.SelectedItem.ToString()[0].Equals ('1')) {
				return bkManager.twelve;
			} else if (spinnerTax.SelectedItem.ToString()[0].Equals ('2')) {
				return bkManager.twentyfive;
			} else {
				return new TaxRate (00.00);
			}
		}

	}
}


