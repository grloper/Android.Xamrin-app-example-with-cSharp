using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using AndroidX.AppCompat.App;
using Android.Widget;
using System;

namespace Android_Ofek
{

    [Activity(Label = "ServiceActivity")]
    public class ServiceActivity : Activity
    {
        private Switch musicSwitch;
        private Intent music;
        private bool isPlaying;
        private Button btn;
        private ISharedPreferences sp;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.service_screen);
            sp = this.GetSharedPreferences("details", FileCreationMode.Private);
            music = new Intent(this, typeof(MusicService));
            btn = FindViewById<Button>(Resource.Id.btn123);
            btn.Click += Btn_Click;
            var btnBack = FindViewById<Button>(Resource.Id.FromMusicToMain);
            btnBack.Click += Btn_Click1;
            musicSwitch = FindViewById<Switch>(Resource.Id.switch1);
            musicSwitch.CheckedChange += MusicSwitch_CheckedChange;
            if (!isPlaying)
            {
                isPlaying = sp.GetBoolean("isplay", false);
            }
            musicSwitch.Checked = isPlaying;
            if (isPlaying)
            {
                StartService(music);
            }
            else
            {
                StopService(music);
            }
        }

        private void Btn_Click1(object sender, EventArgs e)
        {
            Toast.MakeText(this, "Switching form to Menu", ToastLength.Short).Show();
            Intent i = new Intent(this, typeof(MainActivity));
            ISharedPreferencesEditor editor = sp.Edit();
            editor.PutBoolean("isplay", isPlaying);
            editor.Commit();
            StartActivity(i);
        }

        private void MusicSwitch_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            isPlaying = e.IsChecked;
            if (isPlaying)
            {
                StartService(music);
            }
            else
            {
                StopService(music);
            }
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            if (musicSwitch.Checked)
            {
                ISharedPreferencesEditor editor = sp.Edit();
                editor.PutBoolean("reset", true);
                editor.Commit();
                StartService(music);
            }
            else
            {
                ISharedPreferencesEditor editor = sp.Edit();
                editor.PutBoolean("reset", true);
                editor.Commit();
                StopService(music);
            }
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
        }
    }
}