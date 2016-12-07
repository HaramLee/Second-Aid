using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Second_Aid.Droid
{
    class CustomListViewAdapter : BaseAdapter<string>
    {

        private List<string> items;
        private Context context;

        public CustomListViewAdapter(Context _context, List<string> _items)
        {
            items = _items;
            context = _context;

            if (items.Count == 0)
            {
                items.Add("There are no items in this list.");
            }
        }

        public override string this[int position]
        {
            get { return items[position]; }
        }

        public override int Count
        {
            get { return items.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;

            if (row == null)
            {
                row = LayoutInflater.From(context).Inflate(Resource.Layout.listview_row,null,false);
            }

            TextView textView = row.FindViewById<TextView>(Resource.Id.textView);
            textView.Text = items[position];

            return row;
        }
    }
}