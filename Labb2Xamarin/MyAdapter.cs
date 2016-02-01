using System;
using Android.Widget;
using Java.Util.Zip;
using Android.Views;
using Android.App;
using System.Collections.Generic;

namespace Labb2Xamarin
{
	public class MyAdapter : BaseAdapter
	{
		private Activity activity;
		private List<Entry> entries;

		public MyAdapter (Activity activity, List<Entry> entries)
		{
			this.activity = activity;
			this.entries = entries;
		}

		public override Java.Lang.Object GetItem (int position)
		{
			return new JavaObjectWrapper (){ obj = entries [position] };
		}

		public override long GetItemId (int position)
		{
			return position;
		}

		public override View GetView (int position, View convertView, ViewGroup parent)
		{
			if (convertView == null) {
				convertView = activity.LayoutInflater.Inflate (Resource.Layout.EntryLayout,parent, false);
			}

			TextView textViewDate = convertView.FindViewById<TextView> (Resource.Id.textViewEntryDate);
			TextView textViewDesc = convertView.FindViewById<TextView> (Resource.Id.textViewEntryDesc);
			TextView textViewTotal = convertView.FindViewById<TextView> (Resource.Id.textViewEntryTotal);

			Entry entry = entries[position];

			textViewDate.Text = entry.Date;
			textViewDesc.Text = entry.Description;
			textViewTotal.Text = "" + entry.TotalAmount;

			return convertView;

		}

		public override int Count {
			get {
				return entries.Count;
			}
		}
	}		
}


