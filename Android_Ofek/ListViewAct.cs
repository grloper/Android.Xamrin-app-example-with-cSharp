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
    [Activity(Label = "ListViewAct")]
    public class ListViewAct : Activity
    {
        public static List<Toy> toyList { get; set; }
        ToyAdapter toyAdapter;
        ListView lv;
        Button btnAdd;
        ISharedPreferences pos;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.listview_screen);
            pos = this.GetSharedPreferences("user details1", FileCreationMode.Private);
            Android.Graphics.Bitmap bamba = Android.Graphics.BitmapFactory.DecodeResource(Application.Context.Resources, Resource.Drawable.bamba);
            Android.Graphics.Bitmap max = Android.Graphics.BitmapFactory.DecodeResource(Application.Context.Resources, Resource.Drawable.max);

            btnAdd = FindViewById<Button>(Resource.Id.btnAdd123);
            btnAdd.Click += BtnAdd_Click;
            var btnReturn = FindViewById<Button>(Resource.Id.btnFromListToMain);
            btnAdd.Click += BtnAdd_Click;
            btnReturn.Click += BtnReturn_Click;

            Toy t1 = new Toy(90, "fat", "nice toy", bamba, "Row1");
            Toy t2 = new Toy(50, "fat", "nice toy", max, "Row2");
            toyList = new System.Collections.Generic.List<Toy>();
            toyList.Add(t1);
            toyList.Add(t2);
            toyAdapter = new ToyAdapter(this, toyList);
            lv = FindViewById<ListView>(Resource.Id.lv);
            lv.Adapter = toyAdapter;
            lv.ItemClick += Lv_ItemClick;
            lv.ItemLongClick += Lv_ItemLongClick;


        }

        private void BtnReturn_Click(object sender, EventArgs e)
        {
            Toast.MakeText(this, "Switching form to Main", ToastLength.Short).Show();
            Intent i = new Intent(this, typeof(MainActivity));
            StartActivity(i);
        }

        private void Lv_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            ISharedPreferencesEditor editor = pos.Edit();
            editor.PutInt("Savepos", e.Position);
            editor.Commit();
            Android.App.AlertDialog.Builder RYS = new Android.App.AlertDialog.Builder(this);
            RYS.SetTitle("User Delete Process");
            RYS.SetMessage("Are you sure?");
            RYS.SetPositiveButton("Yes", OK_Action);
            RYS.SetNegativeButton("No", Abort_Action);
            RYS.SetCancelable(false);
            RYS.Create();
            RYS.Show();
        }

        private void Lv_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Intent intent = new Intent(this, typeof(EditActivity));
            intent.PutExtra("pos", e.Position);  // Send position  
            StartActivity(intent);
        }

        private void Abort_Action(object sender, DialogClickEventArgs e)
        {
            ISharedPreferences sp = this.GetSharedPreferences("user details1", FileCreationMode.Private);
            int User_Index = sp.GetInt("Savepos", -1);
            Toast.MakeText(this, "User #: <" + (User_Index + 1) + "> was NOT Deleted!!!!", ToastLength.Long).Show();  // The <+1> to compensate for the start from<0>
            User_Index = -1;   // Restore No Record value
            return;
        }

        private void OK_Action(object sender, DialogClickEventArgs e)
        {
            ISharedPreferences sp = this.GetSharedPreferences("user details1", FileCreationMode.Private);
            int User_Index = sp.GetInt("Savepos", -1);
            ListViewAct.toyList.RemoveAt(User_Index);
            toyAdapter.NotifyDataSetChanged();
            Toast.MakeText(this, "User #: <" + (User_Index + 1) + "> Deleted Successfully!", ToastLength.Long).Show();  // The <+1> to compensate for the start from<0>
            User_Index = -1;   // Restore No Record value
            return;
        }



        private void BtnAdd_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(EditActivity));
            StartActivity(intent);
        }
        protected override void OnResume()
        {
            base.OnResume();
            if (toyAdapter != null)
            {
                toyAdapter.NotifyDataSetChanged();
            }
        }
    }
}