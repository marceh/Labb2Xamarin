﻿using Android.App;
using Android.Widget;
using Android.OS;

namespace Labb2Xamarin
{
	[Activity (Label = "Labb2Xamarin", MainLauncher = false, Icon = "@mipmap/icon")]
	public class CreateReportsActivity : Activity
	{

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
			SetContentView (Resource.Layout.CreateReports);

			BookkeeperManager bkManager = BookkeeperManager.GetInstance ();

			Button buttonTaxReport = FindViewById<Button> (Resource.Id.buttonTaxReport);

			buttonTaxReport.Click += delegate {
				bkManager.sendTaxReport (this);
			};

		}
	}
}
