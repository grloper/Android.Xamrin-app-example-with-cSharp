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
    [Activity(Label = "XoActivity")]
    public class XoActivity : Activity
    {
        private int xWins, oWins, ties, clicks;
        private Button btnFromXoToMain, btnReset, btnStats;
        private Button[,] btns;
        private string player;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.xo_screen);

            // Create your application here
            btnFromXoToMain = FindViewById<Button>(Resource.Id.btnFromXoToMain);
            InitBoard();
            btnReset = FindViewById<Button>(Resource.Id.btnFromXoToMain);
            btnReset.LongClick += OnLongClick;
            btnStats = FindViewById<Button>(Resource.Id.btnStatisticsXO);
            btnStats.Click += BtnStats_Click;
            xWins = Intent.GetIntExtra("xWins", 0);
            oWins = Intent.GetIntExtra("oWins", 0);
            ties = Intent.GetIntExtra("ties", 0);
            clicks = Intent.GetIntExtra("clicks", 0);

        }

        private void BtnStats_Click(object sender, EventArgs e)
        {
            Toast.MakeText(this, "Switching form to Stats", ToastLength.Short).Show();
            Intent i = new Intent(this, typeof(StatsAct));
            i.PutExtra("xWins", xWins);
            i.PutExtra("oWins", oWins);
            i.PutExtra("clicks", clicks);
            i.PutExtra("ties", ties);
            StartActivity(i);
        }

        private void OnLongClick(object sender, View.LongClickEventArgs e)
        {
            ClearBaord();
        }

        [Java.Interop.Export("ButtonClick")]
        public void ButtonClick(View v)
        {
            Button button = (Button)v;
            if (button == btnFromXoToMain)
            {
                Toast.MakeText(this, "Switching form to Menu", ToastLength.Short).Show();
                Intent i = new Intent(this, typeof(MainActivity));
                StartActivity(i);
            }

        }
        private void InitBoard()
        {
            int index = 0;
            player = "X";
            btns = new Button[3, 3];
            for (int i = 0; i < btns.GetLength(0); i++)
            {
                for (int k = 0; k < btns.GetLength(1); k++)
                {
                    int resID = base.Resources.GetIdentifier("btnXoNum" + index, "id", PackageName);
                    btns[i, k] = FindViewById<Button>(resID);
                    btns[i, k].Enabled = true;
                    btns[i, k].Text = "";
                    btns[i, k].Click += OnClick;
                    index++;
                }
            }
        }
        private void OnClick(object sender, EventArgs e)
        {
            clicks++;

            ((Button)sender).Text = player;
            ((Button)sender).Enabled = false;
            if (IsWon())
            {
                Android.App.AlertDialog.Builder RYS = new Android.App.AlertDialog.Builder(this);
                RYS.SetTitle(player + " Wins!");
                RYS.SetMessage("Do You Wish To Clear The Board?");
                RYS.SetPositiveButton("Yes", OK_Action);
                RYS.SetNegativeButton("No", Abort_Action);
                RYS.SetCancelable(false);
                RYS.Create();
                RYS.Show();
                if (player == "X")
                {
                    xWins++;
                }
                else
                {
                    oWins++;
                }
            }
            else if (IsTie())
            {
                Android.App.AlertDialog.Builder RYS = new Android.App.AlertDialog.Builder(this);
                RYS.SetTitle("Tie!");
                RYS.SetMessage("Do You Wish To Clear The Board?");
                RYS.SetPositiveButton("Yes", OK_Action);
                RYS.SetNegativeButton("No", Abort_Action);
                RYS.SetCancelable(false);
                RYS.Create();
                RYS.Show();
                ties++;
            }
            else
            {
                if (player == "X")
                    player = "O";
                else
                    player = "X";
            }
        }

        private void Abort_Action(object sender, DialogClickEventArgs e)
        {
            Toast.MakeText(this, "Board still visible", ToastLength.Short).Show();
        }

        private void OK_Action(object sender, DialogClickEventArgs e)
        {
            ClearBaord();
            Toast.MakeText(this, "Board Cleared", ToastLength.Short).Show();

        }

        private void ClearBaord()
        {
            player = "X";
            for (int i = 0; i < btns.GetLength(0); i++)
                for (int k = 0; k < btns.GetLength(1); k++)
                {
                    btns[i, k].Enabled = true;
                    btns[i, k].Text = "";
                }
        }

        private bool IsWon()
        {
            if (btns[0, 0].Text != "" && btns[0, 0].Text == btns[1, 1].Text && btns[2, 2].Text == btns[0, 0].Text)
            {
                return true;
            }
            else if (btns[2, 0].Text != "" && btns[2, 0].Text == btns[1, 1].Text && btns[2, 0].Text == btns[0, 2].Text)
            {
                return true;
            }
            for (int i = 0; i < btns.GetLength(0); i++)
            {
                if (btns[i, 0].Text != "" && btns[i, 0].Text == btns[i, 1].Text && btns[i, 0].Text == btns[i, 2].Text)
                {
                    return true;
                }
                else if (btns[0, i].Text != "" && btns[0, i].Text == btns[1, i].Text && btns[0, i].Text == btns[2, i].Text)
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsTie()
        {//false means keep playing true means found a tie
            for (int i = 0; i < btns.GetLength(0); i++)
                for (int k = 0; k < btns.GetLength(1); k++)
                    if (btns[i, k].Text == "")
                        return false;
            return true;
        }
    }
}