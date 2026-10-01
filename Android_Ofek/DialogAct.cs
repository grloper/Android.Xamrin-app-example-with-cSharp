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
    [Activity(Label = "DialogAct")]
    public class DialogAct : Activity
    {
        Android.App.ProgressDialog progress;
        Android.App.AlertDialog d;
        Android.App.Dialog custom_d;
        Button btnDatePickerDialog, btnTimePickerDialog, btnAlertDialog;
        TextView tvDate, tvTime;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.dialog_screen);
            // Create your application here
            btnAlertDialog = FindViewById<Button>(Resource.Id.btnAlertDialog);
            btnDatePickerDialog = FindViewById<Button>(Resource.Id.btnDatePickerDialog);
            btnTimePickerDialog = FindViewById<Button>(Resource.Id.btnTimePickerDialog);
            var btnBack = FindViewById<Button>(Resource.Id.fromDiaToMain);
            btnBack.Click += BtnBack_Click;
            tvDate = FindViewById<TextView>(Resource.Id.tvDate);
            tvTime = FindViewById<TextView>(Resource.Id.tvTime);
            btnAlertDialog.Click += BtnAlertDialog_Click;
            btnDatePickerDialog.Click += BtnDatePickerDialog_Click;
            btnTimePickerDialog.Click += BtnTimePickerDialog_Click;
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            Toast.MakeText(this, "Switching form to Menu", ToastLength.Short).Show();
            Intent i = new Intent(this, typeof(MainActivity));
            StartActivity(i);
        }

        private void BtnTimePickerDialog_Click(object sender, EventArgs e)
        {
            DateTime today = DateTime.Today;
            TimePickerDialog timePickerDialog = new TimePickerDialog(this, OnTimeSet, today.Hour, today.Minute, true);
            timePickerDialog.Show();
        }

        private void OnTimeSet(object sender, TimePickerDialog.TimeSetEventArgs e)
        {
            tvTime.Text = e.HourOfDay.ToString() + ":" + e.Minute.ToString();
        }

        private void BtnDatePickerDialog_Click(object sender, EventArgs e)
        {
            DateTime today = DateTime.Today;
            DatePickerDialog datePickerDialog = new DatePickerDialog(this, OnDateSet, today.Year, today.Month - 1, today.Day);
            datePickerDialog.Show();
        }

        private void OnDateSet(object sender, DatePickerDialog.DateSetEventArgs e)
        {
            tvDate.Text = e.Date.ToLongDateString();
        }

        private void BtnAlertDialog_Click(object sender, EventArgs e)
        {
            Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(this);
            builder.SetTitle("Hello");
            builder.SetMessage("Your ticket is at " + tvDate.Text + " On " + tvTime.Text);
            builder.SetCancelable(false);
            builder.SetPositiveButton("Ok", OkAction);
            d = builder.Create();
            d.Show();
        }

        private void OkAction(object sender, DialogClickEventArgs e)
        {
            Toast.MakeText(this, "Ticket saved", ToastLength.Short).Show();
        }
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_dialog, menu);
            return true;
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Resource.Id.action_Time)
            {
                Toast.MakeText(this, "you selected Time Picker " + item.ItemId, ToastLength.Long).Show();
                DateTime today = DateTime.Today;
                TimePickerDialog timePickerDialog = new TimePickerDialog(this, OnTimeSet, today.Hour, today.Minute, true);
                timePickerDialog.Show();
                return true;

            }
            else if (item.ItemId == Resource.Id.action_Date)
            {
                Toast.MakeText(this, "you selected Date Picker " + item.ItemId, ToastLength.Long).Show();
                DateTime today = DateTime.Today;
                DatePickerDialog datePickerDialog = new DatePickerDialog(this, OnDateSet, today.Year, today.Month - 1, today.Day);
                datePickerDialog.Show();
                return true;
            }
            else if (item.ItemId == Resource.Id.action_Progress)
            {
                Toast.MakeText(this, "you selected Progress dialog " + item.ItemId, ToastLength.Long).Show();
                progress = new Android.App.ProgressDialog(this);
                progress.Indeterminate = true;
                progress.SetProgressStyle(Android.App.ProgressDialogStyle.Spinner);
                progress.SetMessage("Loading is Progress...");
                progress.SetCancelable(true);
                progress.Show();
                return true;
            }
            else if (item.ItemId == Resource.Id.action_Settings)
            {
                Toast.MakeText(this, "you selected Setting " + item.ItemId, ToastLength.Long).Show();
                return true;
            }
            else if (item.ItemId == Resource.Id.action_exit2)
            {
                Toast.MakeText(this, "You selected exit", ToastLength.Long).Show();
                Finish();
            }
            return base.OnOptionsItemSelected(item);

        }
    }
}
