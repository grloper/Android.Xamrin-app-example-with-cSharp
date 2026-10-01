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
using Android.Telephony;

namespace Android_Ofek
{
    [BroadcastReceiver(Enabled = true)]
    [IntentFilter(new[] { Intent.ActionCall, TelephonyManager.ActionPhoneStateChanged })]


    public class BroadcastPhone : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            if (intent.Extras != null)
            {
                string state = intent.Extras.GetString(TelephonyManager.ExtraState);

                if (state == TelephonyManager.ExtraStateRinging)
                {

                    string phoneNumber = intent.GetStringExtra(TelephonyManager.ExtraIncomingNumber);
                    if (string.IsNullOrEmpty(phoneNumber))
                        phoneNumber = string.Empty;

                    Toast.MakeText(context, "Phone CALL from " + phoneNumber, ToastLength.Long).Show();

                }
                else if (state == TelephonyManager.ExtraStateOffhook)
                {
                    Toast.MakeText(context, "Call Accepted.", ToastLength.Long).Show();

                }
                else if (state == TelephonyManager.ExtraStateIdle)
                {
                    Toast.MakeText(context, "Call Denied.", ToastLength.Long).Show();

                }



            }
        }
    }
}