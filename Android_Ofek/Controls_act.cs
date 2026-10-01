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
    [Activity(Label = "Controls_act")]
    public class Controls_act : Activity
    {
        Button btnDial, btnWeb, btnYoutube, btnMap, btnSendMail, btnSendSMS;
        EditText etContent, etSubject, etInfo;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.control);
            // Create your application here
            btnDial = FindViewById<Button>(Resource.Id.btnDail);
            btnDial.Click += OnClick;
            btnWeb = FindViewById<Button>(Resource.Id.btnWeb);
            btnWeb.Click += OnClick;
            btnYoutube = FindViewById<Button>(Resource.Id.btnYoutube);
            btnYoutube.Click += OnClick;
            btnMap = FindViewById<Button>(Resource.Id.btnMap);
            btnMap.Click += OnClick;
            btnSendMail = FindViewById<Button>(Resource.Id.btnSendEmail);
            btnSendMail.Click += OnClick;
            btnSendSMS = FindViewById<Button>(Resource.Id.btnSendSms);
            btnSendSMS.Click += OnClick;
            etContent = FindViewById<EditText>(Resource.Id.etContent);
            etSubject = FindViewById<EditText>(Resource.Id.etSubject);
            etInfo = FindViewById<EditText>(Resource.Id.etWebAddrOrPhoneNum);
            var btn = FindViewById<Button>(Resource.Id.FromControlToMain);
            btn.Click += Btn_Click;


        }

        private void Btn_Click(object sender, EventArgs e)
        {
            Toast.MakeText(this, "Switching form to Main", ToastLength.Short).Show();
            Intent i = new Intent(this, typeof(MainActivity));
            StartActivity(i);
        }

        private void OnClick(object sender, EventArgs e)
        {
            if (sender == btnDial)
            {
                Intent intent = new Intent();
                intent.SetAction(Intent.ActionDial);
                Android.Net.Uri data = Android.Net.Uri.Parse("tel:" + etInfo.Text);
                intent.SetData(data);
                StartActivity(intent);
            }
            if (sender == btnWeb)
            {

                Intent intent = new Intent();
                intent.SetAction(Intent.ActionView);
                Android.Net.Uri data = Android.Net.Uri.Parse(etInfo.Text);
                intent.SetData(data);
                StartActivity(intent);
                Finish();
            }
            if (sender == btnYoutube)
            {
                string videoId = etInfo.Text; // Queen band 
                Intent intent = new Intent();
                intent.SetAction(Intent.ActionView);
                Android.Net.Uri data = Android.Net.Uri.Parse("vnd.youtube://" + videoId);
                intent.SetData(data);
                StartActivity(intent);
            }
            if (sender == btnSendMail)
            {
                string[] emails = { "ofekv9652@gmail.com", "EitanR@gmail.com" };
                Intent intent = new Intent(Intent.ActionSend);
                intent.SetType("text/plain");
                intent.PutExtra(Intent.ExtraEmail, emails);
                intent.PutExtra(Intent.ExtraSubject, etSubject.Text);
                intent.PutExtra(Intent.ExtraText, etContent.Text);
                StartActivity(intent);
            }
            if (sender == btnSendSMS)
            {
                string message = etContent.Text;
                string phoneNumber = etInfo.Text;
                Intent intent = new Intent(Intent.ActionView, Android.Net.Uri.Parse("sms:" + phoneNumber));
                intent.PutExtra("sms_body", message);
                StartActivity(intent);
            }
            if (sender == btnMap)
            {
                Intent intent = new Intent(Intent.ActionView); intent.SetData(Android.Net.Uri.Parse("geo:" + etInfo.Text));
                StartActivity(intent);
            }



        }
    }
}