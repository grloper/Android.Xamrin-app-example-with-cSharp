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
    [Activity(Label = "CalcActivity")]
    public class CalcActivity : Activity
    {
        private TextView tvResult;
        private string[] numbers = new string[2];
        private string @operator;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.calc_screen);
            tvResult = FindViewById<TextView>(Resource.Id.tvResult);
            var btnMenu = FindViewById<Button>(Resource.Id.FromCalcToMain);
            btnMenu.Click += BtnMenu_Click;

            // Create your application here
        }

        private void BtnMenu_Click(object sender, EventArgs e)
        {
            Toast.MakeText(this, "Switching form to Menu", ToastLength.Short).Show();
            Intent i = new Intent(this, typeof(MainActivity));
            StartActivity(i);
        }
        [Java.Interop.Export("ButtonClick")]
        public void ButtonClick(View v)
        {
            Button button = (Button)v;
            if ("0123456789.".Contains(button.Text))
                AddDigitOrDecimalPoint(button.Text);
            else if ("/*+-".Contains(button.Text))
                AddOperator(button.Text);
            else if ("=" == button.Text)
                Calculate();
            else
                Erase();
        }

        private void Erase()
        {
            numbers[0] = numbers[1] = null;
            @operator = null;
            UpdateCalculatorText();
        }



        private void AddOperator(string value)
        {
            if (numbers[1] != null)
            {
                Calculate(value);
                return;
            }

            @operator = value;

            UpdateCalculatorText();
        }

        private void AddDigitOrDecimalPoint(string value)
        {
            int index = @operator == null ? 0 : 1;

            if (value == "." && (numbers[index] ?? "").Contains("."))
                return;

            if (value == "." && string.IsNullOrEmpty(numbers[index])) numbers[index] = "0";
            numbers[index] += value;

            UpdateCalculatorText();
        }

        private void UpdateCalculatorText() => tvResult.Text = $"{numbers[0]} {@operator} {numbers[1]}";



        private void Calculate(string newOperator = null)
        {
            string result;
            if (CalculatorMath.TryCalculate(numbers[0], @operator, numbers[1], out result))
            {
                numbers[0] = result;
                @operator = newOperator;
                numbers[1] = null;
                UpdateCalculatorText();
            }
            else
            {
                Toast.MakeText(this, "Enter two valid numbers; division by zero is not allowed.", ToastLength.Short).Show();
            }
        }
    }
}
