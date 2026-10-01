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
using Android.Media;
using AndroidX.Core.App;

namespace Android_Ofek
{
    [Service]
    public class MusicService : Service
    {
        Attribute service;
        private ISharedPreferences sp;
        //bool isPlaying;
        private string chId = "ChannelId";
        private MediaPlayer mediaPlayer;

        public override IBinder OnBind(Intent intent) { return null; }
        public override void OnCreate()
        {
            base.OnCreate();
            mediaPlayer = MediaPlayer.Create(this, Resource.Raw.song);
            mediaPlayer.Looping = true;
            mediaPlayer.SetVolume(100, 100);
            mediaPlayer.Completion += SongEnded;
            sp = this.GetSharedPreferences("details", FileCreationMode.Private);
        }

        private void SongEnded(object sender, EventArgs e)
        {
            Toast.MakeText(this, "song ended", ToastLength.Short).Show();
            this.OnDestroy();
        }
        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            int position = sp.GetInt("position", 0); //פקודת המשך מאיפה שנעצר
            mediaPlayer.SeekTo(position);
            bool reset = sp.GetBoolean("reset", false);
            if (reset)
                RestartSong();

            mediaPlayer.Start();
            return base.OnStartCommand(intent, flags, startId);
        }

        public void RestartSong()
        {
            mediaPlayer.SeekTo(0);
            ISharedPreferencesEditor editor = sp.Edit();
            editor.PutBoolean("reset", false);
            editor.Commit();

        }

        public override void OnDestroy()
        {
            ISharedPreferencesEditor editor = sp.Edit();
            editor.PutInt("position", mediaPlayer.CurrentPosition);
            editor.Commit();
            mediaPlayer.Stop();
            mediaPlayer.Release();
            base.OnDestroy();

        }
    }
}