using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Android_Ofek
{
    public class ToyAdapter : BaseAdapter<Toy>
    {
        Context context;  // Context we came from
        List<Toy> objects; // List of objects to be taken into the Listview

        public ToyAdapter(Context context, System.Collections.Generic.List<Toy> objects)
        {
            this.context = context;
            this.objects = objects;
        }
        public List<Toy> GetList()
        {
            return this.objects;
        }

        public override long GetItemId(int position) // Return the position of the item
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            // Convert XML into view class to be inflated in the ListView 
            Android.Views.LayoutInflater layoutInflater = ((ListViewAct)context).LayoutInflater;

            // This Line will be executed according to the number of elements in User List.
            Android.Views.View view = layoutInflater.Inflate(Resource.Layout.custom_layout, parent, false);

            // Create references for the items in the View that was created before
            TextView tvTitle = view.FindViewById<TextView>(Resource.Id.tvTitle);
            TextView tvSubTitle = view.FindViewById<TextView>(Resource.Id.tvSubTitle);
            TextView tvPrice = view.FindViewById<TextView>(Resource.Id.tvPrice);
            ImageView ivProduct = view.FindViewById<ImageView>(Resource.Id.ivProduct);
            TextView tvLocation = view.FindViewById<TextView>(Resource.Id.tvLocation);

            Toy temp = objects[position];   // Temp points on a User in Position #Position
            if (temp != null)               // User Data is a valid record.
            {                   // Update all fields in the view.
                ivProduct.SetImageBitmap(temp.GetBitmap());
                tvPrice.Text = "" + temp.GetPrice();
                tvTitle.Text = temp.GetTitle();
                tvSubTitle.Text = temp.GetSubTitle();
                tvLocation.Text = temp.GetLocation();
            }
            return view;            // return the view to certain position
        }
        public override int Count // Return the list length  
        {
            get { return this.objects.Count; }
        }
        public override Toy this[int position] // Return object in certian position  
        {
            get
            {
                return this.objects[position];
            }
        }

    }
}