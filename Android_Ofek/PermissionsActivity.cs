using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.Core.App;
using AndroidX.Core.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Android_Ofek
{
    [Activity(Label = "PermissionsActivity")]
    public class PermissionsActivity : Activity
    {
        int reqCam, reqAll, reqRC, reqSMS;
        Button btnBack, btnCam, btnSMS, btnRC, btnRAP;
        AlertDialog d;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.permissions_screen);
            // Create your application here
            btnBack = FindViewById<Button>(Resource.Id.btnFromPerToMain);
            btnCam = FindViewById<Button>(Resource.Id.btnCameraPer);
            btnSMS = FindViewById<Button>(Resource.Id.btnSMSPer);
            btnRC = FindViewById<Button>(Resource.Id.btnContactsPer);
            btnRAP = FindViewById<Button>(Resource.Id.btnAllPer);
            btnBack.Click += BtnBack_Click;
            btnRAP.Click += BtnRAP_Click;
            btnCam.Click += BtnCam_Click;
            btnSMS.Click += BtnSMS_Click;
            btnRC.Click += BtnRC_Click;
            reqCam = 0;
            reqAll = 1;
            reqRC = 2;
            reqSMS = 3;



        }

        private void BtnSMS_Click(object sender, EventArgs e)
        {
            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.ReadContacts) == (int)Permission.Granted)
            {
                Toast.MakeText(this, "Send SMS permission Already Granted, moving forward", ToastLength.Short).Show();

            }
            else
            {
                Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(this);
                builder.SetTitle("SendSMS Permission Is Needed");
                builder.SetMessage("Do You Wish To Give Permissiom?");
                builder.SetCancelable(true);
                builder.SetPositiveButton("Allow", (s, e) =>
                {
                    ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.SendSms }, reqSMS);
                });
                builder.SetNegativeButton("Dont Allow", NoDeny);
                d = builder.Create();
                d.Show();
            }
        }

        private void BtnRC_Click(object sender, EventArgs e)
        {
            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.ReadContacts) == (int)Permission.Granted)
            {
                Toast.MakeText(this, "Read Contact permission Already Granted, moving forward", ToastLength.Short).Show();
            }
            else
            {
                Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(this);
                builder.SetTitle("Permission Is Needed");
                builder.SetMessage("Do You Wish To Give Permissiom?");
                builder.SetCancelable(true);
                builder.SetPositiveButton("Allow", (s, e) =>
                {
                    ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.ReadContacts }, reqRC);
                });
                builder.SetNegativeButton("Dont Allow", NoDeny);
                d = builder.Create();
                d.Show();
            }
        }

        private void NoDeny(object sender, DialogClickEventArgs e)
        {
            Toast.MakeText(this, "Permission Is Not Granted", ToastLength.Short).Show();
        }




        private void BtnRAP_Click(object sender, EventArgs e)
        {
            string[] permissionsNeed = new string[] { Manifest.Permission.Camera, Manifest.Permission.ReadContacts, Manifest.Permission.SendSms };
            ActivityCompat.RequestPermissions(this, permissionsNeed, 23);
            List<string> listMissingPermissions = new List<string>();
            for (int i = 0; i < permissionsNeed.Length; i++)
            {
                if (ContextCompat.CheckSelfPermission(this, permissionsNeed[i]) != (int)Permission.Granted)
                    listMissingPermissions.Add(permissionsNeed[i]);
            }
            if (listMissingPermissions.Count > 0)
            {
                string[] arrMissingPermissions = listMissingPermissions.ToArray();
                ActivityCompat.RequestPermissions(this, arrMissingPermissions, reqAll);
            }
            else
            {
                Toast.MakeText(this, "You have permission to camera, SMS and contacts", ToastLength.Long).Show();

            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            string permissionGranted = "";
            string status = "";
            if (requestCode == reqAll)
            {
                if (grantResults.Length > 0 && grantResults[0] == Permission.Granted)
                    Toast.MakeText(this, "Camera permission has granted", ToastLength.Long).Show();
                else
                    Toast.MakeText(this, "Camera permission was not granted ", ToastLength.Long).Show();
            }
            else if (requestCode == reqAll)
            {
                if (grantResults.Length > 0)
                {
                    for (int i = 0; i < grantResults.Length; i++)
                    {
                        if (grantResults[i] != Permission.Granted)
                            status = "DENIED";
                        else
                            status = "GRANTED";
                        permissionGranted += "\n" + permissions[i] + " : " + status;
                    }

                    Android.App.AlertDialog.Builder buildPermissions = new Android.App.AlertDialog.Builder(this);
                    buildPermissions.SetTitle("Group permission details: ");
                    buildPermissions.SetMessage(permissionGranted);
                    buildPermissions.SetCancelable(true);
                    //builder.SetPositiveButton("Allow", AllowAction);
                    buildPermissions.SetPositiveButton("0K", (senderAlert, args) =>
                    {
                        return;
                    });
                    // buildPermissions.SetNegativeButton("Deny", DenyAction);
                    d = buildPermissions.Create();
                    d.Show();
                }
            }

        }
        private void BtnCam_Click(object sender, EventArgs e)
        {

            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.Camera) == (int)Permission.Granted)
            {
                Toast.MakeText(this, "Camera permission Already Granted, moving forward", ToastLength.Short).Show();
                Intent intent = new Intent(Android.Provider.MediaStore.ActionImageCapture);
                StartActivityForResult(intent, reqCam);
            }
            else
            {
                Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(this);
                builder.SetTitle("Camera Permission Is Needed");
                builder.SetMessage("Do You Wish To Give Permissiom?");
                builder.SetCancelable(true);
                builder.SetPositiveButton("Allow", (s, e) =>
                {
                    ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.Camera }, reqCam);
                });
                builder.SetNegativeButton("Dont Allow", NoDeny);
                d = builder.Create();
                d.Show();
            }

        }

        private void DenyAction(object sender, DialogClickEventArgs e)
        {
            Toast.MakeText(this, "Permission Not Granted", ToastLength.Short).Show();
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            Toast.MakeText(this, "Switching form to Menu", ToastLength.Short).Show();
            Intent i = new Intent(this, typeof(MainActivity));
            StartActivity(i);
        }
    }
}