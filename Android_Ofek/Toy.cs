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
using Android.Graphics;

namespace Android_Ofek
{
    public class Toy : BasicPrice
    {
        private String title;
        private String subTitle;
        private Android.Graphics.Bitmap bitmap; //Picture 
        private String location;

        public Toy(int price, String title, String subTitle, Bitmap bitmap, String location) : base(price)
        {
            this.title = title;
            this.subTitle = subTitle;
            this.bitmap = bitmap;
            this.location = location;

        }

        public string GetTitle()
        {
            return this.title;
        }
        public string GetSubTitle()
        {
            return this.subTitle;
        }
        public Android.Graphics.Bitmap GetBitmap()
        {
            return this.bitmap;
        }
        public string GetLocation()
        {
            return this.location;
        }

        public void SetTitle(string t)
        {
            this.title = t;
        }
        public void SetSubTitle(string st)
        {
            this.subTitle = st;
        }
        public void SetBitMap(Bitmap b)
        {
            this.bitmap = b;
        }
        public void SetLocation(string l)
        {
            this.location = l;
        }
    }
}