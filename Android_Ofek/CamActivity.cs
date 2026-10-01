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

namespace Android_Ofek
{
    [Activity(Label = "CamActivity")]
    public class CamActivity : Activity
    {
        Intent savedata;
        ImageView ivPic;
        Button btnPic;
        Android.Graphics.Bitmap bitmap;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.camera_screen);
            // Create your application here
            ivPic = FindViewById<ImageView>(Resource.Id.ivPic);
            btnPic = FindViewById<Button>(Resource.Id.btnPic);
            btnPic.Click += BtnPic_Click;
            var btn = FindViewById<Button>(Resource.Id.FromCamToMain);
            btn.Click += Btn_Click;

        }

        private void Btn_Click(object sender, EventArgs e)
        {
            Toast.MakeText(this, "Switching form to Menu", ToastLength.Short).Show();
            Intent i = new Intent(this, typeof(MainActivity));
            StartActivity(i);
        }

        private void BtnPic_Click(object sender, EventArgs e)
        {
            Intent i = new Intent(Android.Provider.MediaStore.ActionImageCapture);
            StartActivityForResult(i, 0);

        }
        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == 0)
            {
                if (resultCode == Result.Ok)
                {
                    bitmap = (Android.Graphics.Bitmap)data.Extras.Get("data");
                    ivPic.SetImageBitmap(bitmap);

                }
            }
        }
    }
}