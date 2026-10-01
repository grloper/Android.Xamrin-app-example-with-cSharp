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
    [Activity(Label = "StatsAct")]
    public class StatsAct : Activity
    {
        private int xWins, oWins, ties, clicks;
        private Button btnClear, btnReturn;
        private TextView tvX, tvO, tvTies, tvClicks;
        private ISharedPreferences sp;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.stats_screen);

            // Create your application here
            sp = this.GetSharedPreferences("xo_details", FileCreationMode.Private);
            xWins = Intent.GetIntExtra("xWins", 0);
            oWins = Intent.GetIntExtra("oWins", 0);
            ties = Intent.GetIntExtra("ties", 0);
            clicks = Intent.GetIntExtra("clicks", 0);
            //xWins += sp.GetInt("xWins", 0);
            //oWins += sp.GetInt("oWins", 0);
            //ties += sp.GetInt("Ties", 0);
            //clicks += sp.GetInt("Clicks", 0);
            tvClicks = FindViewById<TextView>(Resource.Id.tvClicks);
            tvX = FindViewById<TextView>(Resource.Id.tvX);
            tvO = FindViewById<TextView>(Resource.Id.tvO);
            tvTies = FindViewById<TextView>(Resource.Id.tvTies);
            tvTies.Text = "Ties: " + ties;
            tvO.Text = "O Wins: " + oWins;
            tvX.Text = "X Wins: " + xWins;
            tvClicks.Text = "Clicks: " + clicks;
            btnClear = FindViewById<Button>(Resource.Id.btnClearStats);
            btnClear.Click += BtnClear_Click;
            btnReturn = FindViewById<Button>(Resource.Id.btnFromStatToXo);
            btnReturn.Click += BtnReturn_Click;
        }

        private void BtnReturn_Click(object sender, EventArgs e)
        {
            Toast.MakeText(this, "Switching form to Game", ToastLength.Short).Show();
            Intent i = new Intent(this, typeof(XoActivity));
            i.PutExtra("xWins", xWins);
            i.PutExtra("oWins", oWins);
            i.PutExtra("clicks", clicks);
            i.PutExtra("ties", ties);
            StartActivity(i);
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            Android.App.AlertDialog.Builder RYS = new Android.App.AlertDialog.Builder(this);
            RYS.SetTitle("User Delete Process");
            RYS.SetMessage("Are you sure?");
            RYS.SetPositiveButton("Yes", OK_Action);
            RYS.SetNegativeButton("No", Abort_Action);
            RYS.SetCancelable(false);
            RYS.Create();
            RYS.Show();

        }

        private void Abort_Action(object sender, DialogClickEventArgs e)
        {
            Toast.MakeText(this, "Action Denied", ToastLength.Short).Show();

        }

        private void OK_Action(object sender, DialogClickEventArgs e)
        {
            tvClicks.Text = "Ties: 0";
            tvO.Text = "O Wins: 0";
            tvX.Text = "X Wins: 0";
            tvClicks.Text = "Clicks: 0";
            ties = 0;
            clicks = 0;
            xWins = 0;
            oWins = 0;
        }
    }
}