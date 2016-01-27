using Android.App;
using Android.Widget;
using Android.OS;

using System;
using Android.Views;
using Android.Content;
using Java.Security;
using Java.Lang;

namespace Labb2Xamarin
{
	[Activity (Label = "Labb2Xamarin", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			SetContentView (Resource.Layout.Main);

			Button button1 = FindViewById<Button> (Resource.Id.button1);
			Button button2 = FindViewById<Button> (Resource.Id.button2);
			Button button3 = FindViewById<Button> (Resource.Id.button3);

			button1.Click += delegate {
				startIntent (1);
			};

			button2.Click += delegate {
				startIntent (2);
			};

			button3.Click += delegate {
				startIntent (3);
			};


		}
			
		public void startIntent(int button)
		{
			if (button == 1) {
				Intent intent = new Intent(this, typeof(NewEntryActivity));
				StartActivity(intent);
			} else if (button == 2) {
				Intent intent = new Intent(this, typeof(AllEntriesActivity));
				StartActivity(intent);
			} else {
				Intent intent = new Intent(this, typeof(CreateReportsActivity));
				StartActivity(intent);
			}

		}

	}
}


