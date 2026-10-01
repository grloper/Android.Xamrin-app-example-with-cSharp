using Android.App;
using Android.Content;
using Android.Graphics;
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
    [Activity(Label = "EditActivity")]
    public class EditActivity : Activity
    {
        Button btnSave, btnAddPic;
        EditText etTitle, etSubtitle, etPrice, etLocation;
        Bitmap bitmap;
        ImageView iv;
        int pos = -1;  // If no element send default is -1. Usually for adding new item 

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.edit_layout);
            // Create your application here
            pos = Intent.GetIntExtra("pos", -1);//-1 is default
            etTitle = FindViewById<EditText>(Resource.Id.etTitleEdit);
            etSubtitle = FindViewById<EditText>(Resource.Id.etSubTitleEdit);
            etPrice = FindViewById<EditText>(Resource.Id.etPriceEdit);
            etLocation = FindViewById<EditText>(Resource.Id.etLocationEdit);
            btnAddPic = FindViewById<Button>(Resource.Id.btnAddPictureEdit);
            btnSave = FindViewById<Button>(Resource.Id.btnSaveEdit);
            iv = FindViewById<ImageView>(Resource.Id.ivEdit);
            btnAddPic.Click += BtnAddPic_Click;
            btnSave.Click += BtnSave_Click;
            if (pos != -1) // update 
            {
                Toy temp = ListViewAct.toyList[pos];
                Toast.MakeText(this, "position is  " + pos, ToastLength.Long).Show();
                etTitle.Text = temp.GetTitle();
                etSubtitle.Text = temp.GetSubTitle();
                etPrice.Text = "" + temp.GetPrice();
                bitmap = temp.GetBitmap();
                etLocation.Text = temp.GetLocation();
                iv.SetImageBitmap(temp.GetBitmap());
            }
            else // create new toy 
            {
                Toast.MakeText(this, "lets add new item ", ToastLength.Long).Show();
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            int price = int.Parse(etPrice.Text);
            String title = etTitle.Text;
            String subtitle = etSubtitle.Text;
            String location = etLocation.Text;
            Toy t = null;// = new Toy
            if (pos != -1)  //update Existing item
            {
                t = new Toy(price, title, subtitle, bitmap, location);
                ListViewAct.toyList[pos] = t;
                Finish();
            }
            else // create new toy
            {
                t = new Toy(price, title, subtitle, bitmap, location);
                ListViewAct.toyList.Add(t); // Add to the end of the list 
                Finish();
            }

        }

        private void BtnAddPic_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(Android.Provider.MediaStore.ActionImageCapture);
            StartActivityForResult(intent, 0);
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == 0)//coming from camera
            {
                if (resultCode == Result.Ok)
                {
                    bitmap = (Android.Graphics.Bitmap)data.Extras.Get("data");
                    iv.SetImageBitmap(bitmap);
                }
            }

        }
    }
}