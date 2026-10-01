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
    [Activity(Label = "register_activity")]
    public class register_activity : Activity
    {
        private EditText etPass1, etPass2, etUsername;
        private Button btnRegister, btnEye, btnFromRegToMain;
        private TextView tvRegister;
        private bool EyeClosed = false, isValid = false;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.register_screen);
            // Create your application here
            etPass1 = FindViewById<EditText>(Resource.Id.etPassReg1);
            etPass2 = FindViewById<EditText>(Resource.Id.etPassReg2);
            etUsername = FindViewById<EditText>(Resource.Id.etUserReg);
            //if clicked on register
            btnRegister = FindViewById<Button>(Resource.Id.btnSubmitReg);
            btnRegister.Click += BtnRegister_Click;
            //if clicked on "show pass:
            btnEye = FindViewById<Button>(Resource.Id.btnEyeReg);
            btnEye.Click += BtnEye_Click;
            etPass1.TextChanged += EtPass1_TextChanged;
            //return to Main
            btnFromRegToMain = FindViewById<Button>(Resource.Id.btnFromRegToMain);
            btnFromRegToMain.Click += BtnFromRegToMain_Click;

            tvRegister = FindViewById<TextView>(Resource.Id.tvReg);
            tvRegister.Text = Intent.GetStringExtra("headline") ?? "Create Account";
        }

        private void BtnFromRegToMain_Click(object sender, EventArgs e)
        {
            Toast.MakeText(this, "Switching form to Menu", ToastLength.Short).Show();
            Intent i = new Intent(this, typeof(MainActivity));
            StartActivity(i);
        }

        private void EtPass1_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            var tvHasNumber = FindViewById<TextView>(Resource.Id.anReg);
            var tvUpperChar = FindViewById<TextView>(Resource.Id.ucReg);
            var tvMin8 = FindViewById<TextView>(Resource.Id.min8Reg);
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMiniChars = new Regex(@".{8,}");
            if (!hasNumber.IsMatch(etPass1.Text))
                tvHasNumber.SetTextColor(Color.Red);
            else
                tvHasNumber.SetTextColor(Color.Green);
            if (!hasUpperChar.IsMatch(etPass1.Text))
                tvUpperChar.SetTextColor(Color.Red);
            else
                tvUpperChar.SetTextColor(Color.Green);
            if (!hasMiniChars.IsMatch(etPass1.Text))
                tvMin8.SetTextColor(Color.Red);
            else
                tvMin8.SetTextColor(Color.Green);

        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            ISharedPreferences sp = this.GetSharedPreferences("user details", FileCreationMode.Private);
            string strUserName = sp.GetString("Username" + etUsername.Text, null);
            isValid = IsValidPass(etPass1.Text);
            //valid username&&password

            if (strUserName == null)
            {
                if (etUsername.Text != "" && etPass1.Text == etPass2.Text && etPass1.Text != "" && isValid)
                {

                    sp = this.GetSharedPreferences("user details", FileCreationMode.Private);
                    ISharedPreferencesEditor editor = sp.Edit();
                    editor.PutString("detail", etUsername.Text);
                    editor.PutString("Username" + etUsername.Text, etUsername.Text);
                    editor.PutString("Password" + etUsername.Text, etPass1.Text);
                    editor.Commit();
                    //return to the last acticity
                    SetResult(Result.Ok);
                    Finish();

                }
                else
                    Toast.MakeText(this, "Please Provide Valid Username && Passwords ", ToastLength.Short).Show();
            }
            else
            {
                Toast.MakeText(this, "Username Already Exist", ToastLength.Short).Show();
            }





        }

        private void BtnEye_Click(object sender, EventArgs e)
        {
            if (EyeClosed)
            {
                btnEye.Text = "Show Password";
                etPass1.TransformationMethod = PasswordTransformationMethod.Instance;
                etPass2.TransformationMethod = PasswordTransformationMethod.Instance;
                EyeClosed = !EyeClosed;
            }
            else
            {
                btnEye.Text = "Hide Password";
                etPass1.TransformationMethod = HideReturnsTransformationMethod.Instance;
                etPass2.TransformationMethod = HideReturnsTransformationMethod.Instance;
                EyeClosed = !EyeClosed;
            }
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
    }
}