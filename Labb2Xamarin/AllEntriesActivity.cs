using Android.App;
using Android.Widget;
using Android.OS;
using System;

namespace Labb2Xamarin
{
	[Activity (Label = "Labb2Xamarin", MainLauncher = false, Icon = "@mipmap/icon")]
	public class AllEntriesActivity : Activity
	{
		BookkeeperManager bkManager;
		ListView listViewAllEntries;
		MyAdapter myAdapter;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
			SetContentView (Resource.Layout.AllEntries);
			bkManager = BookkeeperManager.GetInstance ();
			listViewAllEntries = FindViewById<ListView> (Resource.Id.listViewAllEntries);
			myAdapter = new MyAdapter (this, bkManager.getEntries ());
			listViewAllEntries.Adapter = myAdapter;
		}
	}
}
