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
using Android.Text.Method;
using Android.Graphics;
using System.Text.RegularExpressions;

namespace Android_Ofek
{
    [Activity(Label = "LoginActivity")]
    public class LoginActivity : Activity
    {
        private EditText etPass, etUsername;
        private Button btnLogin, btnEye, btnFromLogToMain;
        private bool EyeClosed = false, isValid = false;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.login_screen);

            // Create your application here
            etPass = FindViewById<EditText>(Resource.Id.etPassLog);
            etUsername = FindViewById<EditText>(Resource.Id.etUserLog);
            //if clicked on register
            btnLogin = FindViewById<Button>(Resource.Id.btnSubmitLog);
            btnLogin.Click += BtnLogin_Click;
            //if clicked on "show pass:
            btnEye = FindViewById<Button>(Resource.Id.btnEyeLog);
            btnEye.Click += BtnEye_Click;
            etPass.TextChanged += EtPass_TextChanged;
            //return to menu
            btnFromLogToMain = FindViewById<Button>(Resource.Id.btnFromLogToMain);
            btnFromLogToMain.Click += BtnFromLogToMain_Click;

        }

        private void BtnFromLogToMain_Click(object sender, EventArgs e)
        {
            Toast.MakeText(this, "Switching form to Menu", ToastLength.Short).Show();
            Intent i = new Intent(this, typeof(MainActivity));
            StartActivity(i);
        }

        [Java.Interop.Export("onRegister")]

        public void onRegister(View v)
        {
            Toast.MakeText(this, "Switching form to Register", ToastLength.Short).Show();
            Intent i = new Intent(this, typeof(register_activity));
            i.PutExtra("headline", "Sign Up");
            StartActivityForResult(i, 363);
        }
        private void EtPass_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            var tvHasNumber = FindViewById<TextView>(Resource.Id.anLog);
            var tvUpperChar = FindViewById<TextView>(Resource.Id.ucLog);
            var tvMin8 = FindViewById<TextView>(Resource.Id.min8Log);
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMiniChars = new Regex(@".{8,}");
            if (!hasNumber.IsMatch(etPass.Text))
            {
                tvHasNumber.SetTextColor(Color.Red);
            }
            else
            {
                tvHasNumber.SetTextColor(Color.Green);

            }
            if (!hasUpperChar.IsMatch(etPass.Text))
            {
                tvUpperChar.SetTextColor(Color.Red);
            }
            else
            {
                tvUpperChar.SetTextColor(Color.Green);
            }
            if (!hasMiniChars.IsMatch(etPass.Text))
            {
                tvMin8.SetTextColor(Color.Red);
            }
            else
            {
                tvMin8.SetTextColor(Color.Green);
            }
        }

        private void BtnEye_Click(object sender, EventArgs e)
        {
            if (EyeClosed)
            {
                btnEye.Text = "Show Password";
                etPass.TransformationMethod = PasswordTransformationMethod.Instance;
                EyeClosed = !EyeClosed;
            }
            else
            {
                btnEye.Text = "Hide Password";
                etPass.TransformationMethod = HideReturnsTransformationMethod.Instance;
                EyeClosed = !EyeClosed;
            }
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            ISharedPreferences sp = this.GetSharedPreferences("user details", FileCreationMode.Private);
            string strUserName = sp.GetString("Username" + etUsername.Text, null);
            string strPassword = sp.GetString("Password" + etUsername.Text, null);
            isValid = IsValidPass(etPass.Text);
            if (etUsername.Text != "" && etPass.Text != "" && isValid)
            {
                if (strUserName == etUsername.Text && strPassword == etPass.Text)
                    Toast.MakeText(this, "Welcome, " + etUsername.Text+"!", ToastLength.Short).Show();
                else
                    Toast.MakeText(this, "Please Provide Valid Username && Passwords ", ToastLength.Short).Show();
            }
            else
                Toast.MakeText(this, "Please Provide Valid Username && Passwords ", ToastLength.Short).Show();
        }
        private static bool IsValidPass(string s)
        {
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMiniChars = new Regex(@".{8,}");
            if (hasMiniChars.IsMatch(s) && hasUpperChar.IsMatch(s) && hasNumber.IsMatch(s))
            {
                return true;
            }
            return false;
        }
        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == 363)
            {
                if (resultCode == Result.Ok)
                {
                    Toast.MakeText(this, "Sign-Up Accepted", ToastLength.Short).Show();
                    ISharedPreferences sp = this.GetSharedPreferences("user details", FileCreationMode.Private);
                    string username= sp.GetString("detail", "");
                    etUsername.Text = sp.GetString("Username" + username, "");
                    etPass.Text = sp.GetString("Password" + username, "");

                }
            }
        }
    }
}