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
        string str_input = null;
        string str_output = null;
        bool pressed_enter = false;
        char sign = 'n';
        float lh;
        float rh;
        float total = -1;
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
            var input = FindViewById<TextView>(Resource.Id.input);


            one.Click += delegate
            {
                if(pressed_enter)
                {
                    this.str_input = null;
                }

                myVib.Vibrate(30);
                this.str_input += '1';
                this.str_output += '1';
                input.Text = this.str_output;
            };

            two.Click += delegate
            {
                if (pressed_enter)
                {
                    this.str_input = null;
                    pressed_enter = false;
                }

                myVib.Vibrate(30);
                this.str_input += '2';
                this.str_output += '2';
                input.Text = this.str_output;
            };

            three.Click += delegate
            {
                if (pressed_enter)
                {
                    this.str_input = null;
                    pressed_enter = false;
                }

                myVib.Vibrate(30);
                this.str_input += '3';
                this.str_output += '3';
                input.Text = this.str_output;
            };

            four.Click += delegate
            {
                if (pressed_enter)
                {
                    this.str_input = null;
                    pressed_enter = false;
                }

                myVib.Vibrate(30);
                this.str_input += '4';
                this.str_output += '4';
                input.Text = this.str_output;
            };

            five.Click += delegate
            {
                if (pressed_enter)
                {
                    this.str_input = null;
                    pressed_enter = false;
                }

                myVib.Vibrate(30);
                this.str_input += '5';
                this.str_output += '5';
                input.Text = this.str_output;
            };

            six.Click += delegate
            {
                if (pressed_enter)
                {
                    this.str_input = null;
                    pressed_enter = false;
                }

                myVib.Vibrate(30);
                this.str_input += '6';
                this.str_output += '6';
                input.Text = this.str_output;
            };

            seven.Click += delegate
            {
                if (pressed_enter)
                {
                    this.str_input = null;
                    pressed_enter = false;
                }

                myVib.Vibrate(30);
                this.str_input += '7';
                this.str_output += '7';
                input.Text = this.str_output;
            };

            eight.Click += delegate
            {
                if (pressed_enter)
                {
                    this.str_input = null;
                    pressed_enter = false;
                }

                myVib.Vibrate(30);
                this.str_input += '8';
                this.str_output += '8';
                input.Text = this.str_output;
            };

            nine.Click += delegate
            {
                if (pressed_enter)
                {
                    this.str_input = null;
                    pressed_enter = false;
                }

                myVib.Vibrate(30);
                this.str_input += '9';
                this.str_output += '9';
                input.Text = this.str_output;
            };

            zero.Click += delegate
            {
                if (pressed_enter)
                {
                    this.str_input = null;
                    pressed_enter = false;
                }

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
                total = 0;
                input.Text = "0";
            };

            enter.Click += delegate
            {
                myVib.Vibrate(30);
                if (str_input == null)
                {
                    input.Text = "0";
                }

                else if (float.TryParse(this.str_input, out rh))
                {
                    switch (sign)
                    {
                        case '+':
                            total = lh + rh;
                            total.ToString();
                            str_output += " = ";
                            str_output += total;
                            input.Text = str_output;
                            break;


                        case '-':
                            total = lh - rh;
                            str_output += " = ";
                            str_output += total;
                            input.Text = str_output;
                            break;


                        case '*':
                            total = lh * rh;
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

                    this.str_input = null;
                    this.str_input += total;
                    pressed_enter = true;
                    this.str_output = null;
                    sign = 'n';
                }

            };

            add.Click += delegate
            {
                myVib.Vibrate(30);

                if (sign != 'n')
                {
                    if (float.TryParse(this.str_input, out rh))
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
                if (sign != 'n')
                {
                    if (float.TryParse(this.str_input, out rh))
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

                if (sign != 'n')
                {
                    if (float.TryParse(this.str_input, out rh))
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

                if (sign != 'n')
                {
                    if (float.TryParse(this.str_input, out rh))
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

