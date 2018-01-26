using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections;

namespace App1
{
    [Activity(Label = "Calculator", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        string str_input;
        string str_output;
        char sign = 'n';
        float lh;
        float rh = 0;
        float total;
        private Vibrator myVib;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            myVib = (Vibrator)GetSystemService(VibratorService);

            Button one = FindViewById<Button>(Resource.Id.button1);
            Button two = FindViewById<Button>(Resource.Id.button2);
            Button three = FindViewById<Button>(Resource.Id.button3);
            Button four = FindViewById<Button>(Resource.Id.button4);
            Button five = FindViewById<Button>(Resource.Id.button5);
            Button six = FindViewById<Button>(Resource.Id.button6);
            Button seven = FindViewById<Button>(Resource.Id.button7);
            Button eight = FindViewById<Button>(Resource.Id.button8);
            Button nine = FindViewById<Button>(Resource.Id.button9);
            Button zero = FindViewById<Button>(Resource.Id.button10);
            Button divide = FindViewById<Button>(Resource.Id.button11);
            Button multiply = FindViewById<Button>(Resource.Id.button12);
            Button add = FindViewById<Button>(Resource.Id.button13);
            Button minus = FindViewById<Button>(Resource.Id.button14);
            Button clear = FindViewById<Button>(Resource.Id.button15);
            Button enter = FindViewById<Button>(Resource.Id.button16);

            var input = FindViewById<EditText>(Resource.Id.input);


            one.Click += delegate
            {
                myVib.Vibrate(30);
                this.str_input += '1';
                this.str_output += '1';
                input.Text = this.str_output;
            };

            two.Click += delegate
            {
                myVib.Vibrate(30);
                this.str_input += '2';
                this.str_output += '2';
                input.Text = this.str_output;
            };

            three.Click += delegate
            {
                myVib.Vibrate(30);
                this.str_input += '3';
                this.str_output += '3';
                input.Text = this.str_output;
            };

            four.Click += delegate
            {
                myVib.Vibrate(30);
                this.str_input += '4';
                this.str_output += '4';
                input.Text = this.str_output;
            };

            five.Click += delegate
            {
                myVib.Vibrate(30);
                this.str_input += '5';
                this.str_output += '5';
                input.Text = this.str_output;
            };

            six.Click += delegate
            {
                myVib.Vibrate(30);
                this.str_input += '6';
                this.str_output += '6';
                input.Text = this.str_output;
            };

            seven.Click += delegate
            {
                myVib.Vibrate(30);
                this.str_input += '7';
                this.str_output += '7';
                input.Text = this.str_output;
            };

            eight.Click += delegate
            {
                myVib.Vibrate(30);
                this.str_input += '8';
                this.str_output += '8';
                input.Text = this.str_output;
            };

            nine.Click += delegate
            {
                myVib.Vibrate(30);
                this.str_input += '9';
                this.str_output += '9';
                input.Text = this.str_output;
            };

            zero.Click += delegate
            {
                myVib.Vibrate(30);
                this.str_input += '0';
                this.str_output += '0';
                input.Text = this.str_output;
            };

            clear.Click += delegate
            {
                myVib.Vibrate(30);
                this.str_input = null;
                this.str_output = null;
                input.Text = this.str_input;
            };

            enter.Click += delegate
            {
                myVib.Vibrate(30);
<<<<<<< HEAD:projects/project 1/source/App1/App1/App1/MainActivity.cs
                if (float.TryParse(this.str_input, out rh))
=======
                if (int.TryParse(this.str_input, out rh))
>>>>>>> bee53a42b2c4ca67979ca72c0c4a74f49087efd4:projects/project 1/source/App1/App1/MainActivity.cs
                {
                    switch (sign)
                    {
                        case '+':

<<<<<<< HEAD:projects/project 1/source/App1/App1/App1/MainActivity.cs
                            total = lh + rh;
=======
                        total.ToString();

                        str_output += " = ";
                        str_output += total;
                        input.Text = str_output;
                    }
>>>>>>> bee53a42b2c4ca67979ca72c0c4a74f49087efd4:projects/project 1/source/App1/App1/MainActivity.cs

                            total.ToString();

                            str_output += " = ";
                            str_output += total;
                            input.Text = str_output;
                            break;


                        case '-':
                            total = lh - rh;

<<<<<<< HEAD:projects/project 1/source/App1/App1/App1/MainActivity.cs
                            str_output += " = ";
                            str_output += total;
                            input.Text = str_output;
                            break;


                        case '*':

                            total = lh * rh;
=======
                    else if (sign == '/')
                    {
                        total = lh / rh;
>>>>>>> bee53a42b2c4ca67979ca72c0c4a74f49087efd4:projects/project 1/source/App1/App1/MainActivity.cs

                            str_output += " = ";
                            str_output += total;
                            input.Text = str_output;
                            break;

                        default:
                            total = lh / rh;

                            str_output += " = ";
                            str_output += total;
                            input.Text = str_output;
                            break;
                    }

                    else
                    {
                        if (int.TryParse(this.str_input, out total)) 
                        {
                            str_output += " = ";
                            str_output += total;
                            input.Text = str_output;
                        }
                    }
                }

                this.str_input = null;
                this.str_output = null;
                sign = 'n';

            };

            add.Click += delegate
            {
                myVib.Vibrate(30);
<<<<<<< HEAD:projects/project 1/source/App1/App1/App1/MainActivity.cs

                if (sign != 'n')
                {
                    if (float.TryParse(this.str_input, out rh))
=======
                //if (!lh_full)
                //{
                if (int.TryParse(this.str_input, out lh))
>>>>>>> bee53a42b2c4ca67979ca72c0c4a74f49087efd4:projects/project 1/source/App1/App1/MainActivity.cs
                    {
                        switch (sign)
                        {
                            case '+':

                                total = lh + rh;

                                str_output = null;
                                lh = total;
                                str_output += total;
                                str_output += " + ";
                                input.Text = str_output;
                                str_input = null;
                                sign = '+';
                                break;


                            case '-':
                                total = lh - rh;

                                str_output = null;
                                lh = total;
                                str_output += total;
                                str_output += " + ";
                                input.Text = str_output;
                                str_input = null;
                                sign = '+';
                                break;


                            case '*':

                                total = lh * rh;

                                str_output = null;
                                lh = total;
                                str_output += total;
                                str_output += " + ";
                                input.Text = str_output;
                                str_input = null;
                                sign = '+';
                                break;

                            default:
                                total = lh / rh;

                                str_output = null;
                                lh = total;
                                str_output += total;
                                str_output += " + ";
                                input.Text = str_output;
                                str_input = null;
                                sign = '+';
                                break;
                        }
                    }
                }

                else
                {
                    if (float.TryParse(this.str_input, out lh))
                        sign = '+';
                    str_output = str_input;
                    str_output += " + ";
                    input.Text = str_output;
                    this.str_input = null;
                }
            };

            minus.Click += delegate
            {
                myVib.Vibrate(30);
<<<<<<< HEAD:projects/project 1/source/App1/App1/App1/MainActivity.cs
                if (sign != 'n')
                {
                    if (float.TryParse(this.str_input, out rh))
=======
                //if (!lh_full)
                //{
                if (int.TryParse(this.str_input, out lh))
>>>>>>> bee53a42b2c4ca67979ca72c0c4a74f49087efd4:projects/project 1/source/App1/App1/MainActivity.cs
                    {
                        switch (sign)
                        {
                            case '+':

                                total = lh + rh;

                                str_output = null;
                                lh = total;
                                str_output += total;
                                str_output += " - ";
                                input.Text = str_output;
                                str_input = null;
                                sign = '-';
                                break;


                            case '-':
                                total = lh - rh;

                                str_output = null;
                                lh = total;
                                str_output += total;
                                str_output += " - ";
                                input.Text = str_output;
                                str_input = null;
                                sign = '-';
                                break;


                            case '*':

                                total = lh * rh;

                                str_output = null;
                                lh = total;
                                str_output += total;
                                str_output += " - ";
                                input.Text = str_output;
                                str_input = null;
                                sign = '-';
                                break;

                            default:
                                total = lh / rh;

                                str_output = null;
                                lh = total;
                                str_output += total;
                                str_output += " - ";
                                input.Text = str_output;
                                str_input = null;
                                sign = '-';
                                break;
                        }
                    }
                }
                else
                {
                    if (float.TryParse(this.str_input, out lh))

                        sign = '-';
                    str_output = str_input;
                    str_output += " - ";
                    input.Text = str_output;
                    this.str_input = null;
                }
            };

            multiply.Click += delegate
            {
                myVib.Vibrate(30);
<<<<<<< HEAD:projects/project 1/source/App1/App1/App1/MainActivity.cs

                if (sign != 'n')
                {
                    if (float.TryParse(this.str_input, out rh))
=======
                //if (!lh_full)
                //{
                if (int.TryParse(this.str_input, out lh))
>>>>>>> bee53a42b2c4ca67979ca72c0c4a74f49087efd4:projects/project 1/source/App1/App1/MainActivity.cs
                    {
                        switch (sign)
                        {
                            case '+':

                                total = lh + rh;

                                str_output = null;
                                lh = total;
                                str_output += total;
                                str_output += " * ";
                                input.Text = str_output;
                                str_input = null;
                                sign = '*';
                                break;


                            case '-':
                                total = lh - rh;

                                str_output = null;
                                lh = total;
                                str_output += total;
                                str_output += " * ";
                                input.Text = str_output;
                                str_input = null;
                                sign = '*';
                                break;


                            case '*':

                                total = lh * rh;

                                str_output = null;
                                lh = total;
                                str_output += total;
                                str_output += " * ";
                                input.Text = str_output;
                                str_input = null;
                                sign = '*';
                                break;

                            default:
                                total = lh / rh;

                                str_output = null;
                                lh = total;
                                str_output += total;
                                str_output += " * ";
                                input.Text = str_output;
                                str_input = null;
                                sign = '*';
                                break;
                        }
                    }
                }

                else
                {
                    if (float.TryParse(this.str_input, out lh))

                        sign = '*';
                    str_output = str_input;
                    str_output += " * ";
                    input.Text = str_output;
                    this.str_input = null;
                }
            };

            divide.Click += delegate
            {
                myVib.Vibrate(30);
<<<<<<< HEAD:projects/project 1/source/App1/App1/App1/MainActivity.cs

                if (sign != 'n')
                {
                    if (float.TryParse(this.str_input, out rh))
=======
                //if (!lh_full)
                //{
                if (int.TryParse(this.str_input, out lh))
>>>>>>> bee53a42b2c4ca67979ca72c0c4a74f49087efd4:projects/project 1/source/App1/App1/MainActivity.cs
                    {
                        switch (sign)
                        {
                            case '+':

                                total = lh + rh;

                                str_output = null;
                                lh = total;
                                str_output += total;
                                str_output += " / ";
                                input.Text = str_output;
                                str_input = null;
                                sign = '/';
                                break;


                            case '-':
                                total = lh - rh;

                                str_output = null;
                                lh = total;
                                str_output += total;
                                str_output += " / ";
                                input.Text = str_output;
                                str_input = null;
                                sign = '/';
                                break;


                            case '*':

                                total = lh * rh;

                                str_output = null;
                                lh = total;
                                str_output += total;
                                str_output += " / ";
                                input.Text = str_output;
                                str_input = null;
                                sign = '/';
                                break;

                            default:
                                total = lh / rh;

                                str_output = null;
                                lh = total;
                                str_output += total;
                                str_output += " / ";
                                input.Text = str_output;
                                str_input = null;
                                sign = '/';
                                break;
                        }
                    }
                }

                else
                {
                    if (float.TryParse(this.str_input, out lh))
                        
                        sign = '/';
                    str_output = str_input;
                    str_output += " / ";
                    input.Text = str_output;
                    this.str_input = null;
                }
            };



            // SetContentView (Resource.Layout.Main);
        }
    }
}

