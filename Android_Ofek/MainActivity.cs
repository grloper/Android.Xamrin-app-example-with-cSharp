using Android.App;
using Android.OS;
using Android.Runtime;
using AndroidX.AppCompat.App;
using Android.Widget;
using System;
using Android.Content;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Views;
using Android;
using AndroidX.Core.App;
using Android.Content.PM;
using Android.Telephony;

namespace Android_Ofek
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class MainActivity : AppCompatActivity
    {
        Button btnFromMainToCalc, btnFromMainToReg, btnFromMainToLog, btnFromMainToXo, btnFromMainToCam, btnFromMainToControls, btnFromMainToMusic, btnFromMainToPer, btnFromMainToAcc, btnFromMainToListView, btnFromMainToDialog;
        private BroadcastBattery broadcastBattery;
        private BroadcastPhone broadcastPhone;
        private TextView tvBattery;
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu, menu);
            return true;
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {

            if (item.ItemId == Resource.Id.action_exit)
            {
                Toast.MakeText(this, "You selected Exit!", ToastLength.Long).Show();
                System.Diagnostics.Process.GetCurrentProcess().Kill();

            }
            return false;
        }


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            btnFromMainToCalc = FindViewById<Button>(Resource.Id.FromMainToCalc);
            btnFromMainToReg = FindViewById<Button>(Resource.Id.FromMainToReg);
            btnFromMainToLog = FindViewById<Button>(Resource.Id.FromMainToLog);
            btnFromMainToXo = FindViewById<Button>(Resource.Id.FromMainToXO);
            btnFromMainToCam = FindViewById<Button>(Resource.Id.FromMainToCam);
            btnFromMainToControls = FindViewById<Button>(Resource.Id.FromMainToControls);
            btnFromMainToMusic = FindViewById<Button>(Resource.Id.FromMainToMusic);
            btnFromMainToPer = FindViewById<Button>(Resource.Id.FromMainToPermissions);
            btnFromMainToAcc = FindViewById<Button>(Resource.Id.FromMainToAcc);
            btnFromMainToListView = FindViewById<Button>(Resource.Id.FromMainToListView);
            btnFromMainToDialog = FindViewById<Button>(Resource.Id.FromMainToDia);
            tvBattery = FindViewById<TextView>(Resource.Id.tvBatteryStat);
            broadcastBattery = new BroadcastBattery(tvBattery);
            broadcastPhone = new BroadcastPhone();
            string[] permissions = { Manifest.Permission.ReadPhoneState, Manifest.Permission.ReadCallLog };
            ActivityCompat.RequestPermissions(this, permissions, 1);
        }
        [Java.Interop.Export("NavMethod")]
        public void NavMethod(View v)
        {
            Button button = (Button)v;
            if (button == btnFromMainToLog)
            {
                Toast.MakeText(this, "Switching form to Login", ToastLength.Short).Show();
                Intent i = new Intent(this, typeof(LoginActivity));
                StartActivity(i);

            }
            if (button == btnFromMainToReg)
            {
                Toast.MakeText(this, "Switching form to Register", ToastLength.Short).Show();
                Intent i = new Intent(this, typeof(register_activity));
                StartActivity(i);

            }
            if (button == btnFromMainToCalc)
            {
                Toast.MakeText(this, "Switching form to Calculator", ToastLength.Short).Show();
                Intent i = new Intent(this, typeof(CalcActivity));
                StartActivity(i);
            }
            if (button == btnFromMainToXo)
            {
                Toast.MakeText(this, "Switching form to Tic Tac Toe", ToastLength.Short).Show();
                Intent i = new Intent(this, typeof(XoActivity));
                StartActivity(i);
            }
            if (button == btnFromMainToCam)
            {
                Toast.MakeText(this, "Switching form to Camera", ToastLength.Short).Show();
                Intent i = new Intent(this, typeof(CamActivity));
                StartActivity(i);
            }
            if (v == btnFromMainToControls)
            {
                Toast.MakeText(this, "Switching form to Controls", ToastLength.Short).Show();
                Intent i = new Intent(this, typeof(Controls_act));
                StartActivity(i);
            }
            if (v == btnFromMainToMusic)
            {
                Toast.MakeText(this, "Switching form to Music", ToastLength.Short).Show();
                Intent i = new Intent(this, typeof(ServiceActivity));
                StartActivity(i);
            }
            if (v == btnFromMainToPer)
            {
                Toast.MakeText(this, "Switching form to Permissions", ToastLength.Short).Show();
                Intent i = new Intent(this, typeof(PermissionsActivity));
                StartActivity(i);
            }
            if (v == btnFromMainToAcc)
            {
                Toast.MakeText(this, "Switching form to Accelerometer", ToastLength.Short).Show();
                Intent i = new Intent(this, typeof(AccelerometerAct));
                StartActivity(i);
            }
            if (v == btnFromMainToListView)
            {
                Toast.MakeText(this, "Switching form to List View", ToastLength.Short).Show();
                Intent i = new Intent(this, typeof(ListViewAct));
                StartActivity(i);
            }
            if (v == btnFromMainToDialog)
            {
                Toast.MakeText(this, "Switching form to Dialog", ToastLength.Short).Show();
                Intent i = new Intent(this, typeof(DialogAct));
                StartActivity(i);
            }
        }

        protected override void OnResume()
        {
            base.OnResume();
            RegisterReceiver(broadcastBattery, new IntentFilter(Intent.ActionBatteryChanged));
            RegisterReceiver(broadcastPhone, new IntentFilter(TelephonyManager.ActionPhoneStateChanged));
        }
        protected override void OnPause()
        {
            UnregisterReceiver(broadcastBattery);
            base.OnPause();
            UnregisterReceiver(broadcastPhone);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            if (requestCode == 1 && grantResults.Length > 0 && grantResults[0] == Permission.Granted)
            {
                RegisterReceiver(broadcastPhone, new IntentFilter(TelephonyManager.ActionPhoneStateChanged));
            }
            else
            {
                Toast.MakeText(this, "Please Accept Permissions", ToastLength.Short).Show();
                ActivityCompat.RequestPermissions(this, permissions, 1);

            }
        }
    }
}