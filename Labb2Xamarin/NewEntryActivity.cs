using Android.App;
using Android.Widget;
using Android.OS;
using System;
using System.Globalization;

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
			if (editTextDate.Text.Equals ("") || editTextDesc.Text.Equals ("") || editTextTotal.Text.Equals ("")) {
				Toast.MakeText (this, Resource.String.input_fields_cannot_be_empty, ToastLength.Short).Show ();  
			} else {
				Entry createdEntry = new Entry ();
				createdEntry.IsIncome = TheIncome ();
				createdEntry.Date = TheDate ();
				createdEntry.Description = TheDescription ();
				createdEntry.TotalAmount = TheTotalAmount ();
				createdEntry.TypeAccount = TheAccount ("type");
				createdEntry.MoneyAccount = TheAccount ("money");
				createdEntry.TaxRate = TheTaxRate ();

				Toast.MakeText (this, Resource.String.entry_created, ToastLength.Short).Show ();  
				bkManager.AddEntry (createdEntry);
				ClearTextFields ();
			}
		}

		private void ClearTextFields()
		{
			editTextDate.Text = "";
			editTextDesc.Text = "";
			editTextTotal.Text = "";
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
		private double TheTotalAmount() {
	
			string tempText = editTextTotal.Text;
			double doubleTemp = double.Parse (tempText, CultureInfo.InvariantCulture);
			return doubleTemp;

		}

/*		{
			string tempText = editTextTotal.Text;
			string tempText2 = "";
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
*/
		private int TheAccount(string kindOfAccount)
		{
			string tempText;
			string number = "";
			if (kindOfAccount.Equals ("type")) 
			{
				Console.WriteLine ("Inne i type");
				tempText = spinnerType.SelectedItem.ToString ();
				for (int i = 1; i < 5; i++) {
					number += tempText [i];
				}
				Console.WriteLine ("tempText: "+tempText);
				Console.WriteLine ("number: "+number);

				if (number.Equals ("3000")) {
					return 3000;
				} else if (number.Equals ("3670")) {
					return 3670;
				} else if (number.Equals ("4000")) {
					return 4000;
				} else if (number.Equals ("6010")) {
					return 6010;
				} else if (number.Equals ("7010")) {
					return 7010;
				} else {
					return 0000;
				}
			} else {
				Console.WriteLine ("Inne i money");	
				tempText = spinnerAccount.SelectedItem.ToString();
				for (int i = 1; i < 5; i++) {
					number += tempText [i];
				}
				Console.WriteLine ("tempText: "+tempText);
				Console.WriteLine ("number: "+number);

				if (number.Equals ("1930")) {
					return 1930;
				} else if (number.Equals ("1910")) {
					return 1910;
				} else {
					return 0000;
				}
			}
		}

		private int TheTaxRate()
		{
			if (spinnerTax.SelectedItem.ToString()[0].Equals ('6')) {
				return 6;
			} else if (spinnerTax.SelectedItem.ToString()[0].Equals ('1')) {
				return 12;
			} else if (spinnerTax.SelectedItem.ToString()[0].Equals ('2')) {
				return 25;
			} else {
				return 0;
			}
		}

	}
}


