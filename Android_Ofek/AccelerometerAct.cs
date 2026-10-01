using Android.App;
using Android.Content;
using Android.Hardware;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.Lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Android_Ofek
{

    [Activity(Label = "AccelerometerAct", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class AccelerometerAct : Activity, ISensorEventListener
    {
        private ImageView ball;
        private SensorManager senMngr;
        FrameLayout frm;
        private float dx, dy, dz;
        TextView tvDebug, tvDebug2;

        public void OnAccuracyChanged(Sensor sensor, [GeneratedEnum] SensorStatus accuracy) { }


        public void OnSensorChanged(SensorEvent e)
        {

            Sensor mySens = e.Sensor;
            dx = e.Values[0];
            dy = e.Values[1];
            dz = e.Values[2];
            dx *= 10;
            dy *= 10;
            dz *= 10;
            try
            {
                float a = (float)2.0 / dx;
            }
            catch (InterruptedException err)
            {
                err.PrintStackTrace();
            }
            ball.SetX(ball.GetX() - dx);
            ball.SetY(ball.GetY() + dy);
            if (ball.GetX() < 0) //left wall
            {
                ball.SetX(0);
            }
            if (ball.GetX() > frm.Width - ball.Width)
            {
                ball.SetX(frm.Width - ball.Width);
            }
            if (ball.GetY() < 0)//top roof
            {
                ball.SetY(0);
            }
            if (ball.GetY() > frm.Height - ball.Height)
            {
                ball.SetY(frm.Height - ball.Height);
            }

            tvDebug.Text = "x= " + dx + ", y=" + dy + ", z=" + dz;
            tvDebug2.Text = "Width: " + frm.Width + ", Height: " + frm.Height;

        }
        protected override void OnDestroy()
        {
            senMngr.UnregisterListener(this);
            base.OnDestroy();

        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.accelerometer_screen);
            // Create your application here
            dx = 0; dy = 0; dz = 0;
            var returnBack = FindViewById<Button>(Resource.Id.FromAccToMain);
            returnBack.Click += ReturnBack_Click;
            ball = FindViewById<ImageView>(Resource.Id.ivBall);
            frm = FindViewById<FrameLayout>(Resource.Id.frm);
            ball.Visibility = Android.Views.ViewStates.Visible;
            senMngr = (SensorManager)GetSystemService(Context.SensorService);
            senMngr.RegisterListener(this, senMngr.GetDefaultSensor(SensorType.Accelerometer), SensorDelay.Normal);
            tvDebug = FindViewById<TextView>(Resource.Id.tvCheck1);
            tvDebug2 = FindViewById<TextView>(Resource.Id.tvCheck2);
        }

        private void ReturnBack_Click(object sender, EventArgs e)
        {
            Toast.MakeText(this, "Switching form to Main", ToastLength.Short).Show();
            Intent i = new Intent(this, typeof(MainActivity));
            StartActivity(i);
        }
    }
}