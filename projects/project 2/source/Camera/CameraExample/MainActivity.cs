﻿using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using System.Collections.Generic;
using Android.Content.PM;
using Android.Provider;
using System;
using System.IO;
using Android.Graphics;

namespace CameraExample
{
    [Activity(Label = "Image Manipulator", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        // Used to track the file that we're manipulating between functions
        public static Java.IO.File _file;

        // Used to track the directory that we'll be writing to between functions
        public static Java.IO.File _dir;

        //Used to track the bitmap original and copy throughout functions
        Android.Graphics.Bitmap copyBitmap = null;
        Android.Graphics.Bitmap bitmap = null;

        //If the back button is pressed go back to the main layout to start over
        public override void OnBackPressed()
        {
            SetContentView(Resource.Layout.Main);
            Toast.MakeText(this, "Back Pressed: Starting over", ToastLength.Short).Show();
            copyBitmap = null;
            bitmap = null;
            StartMainLayout();
        }

        //called on the start of the app to create the main layout to the screen
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            StartMainLayout();
        }

        //Starts the main layout, and makes the button functional
        private void StartMainLayout()
        {
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            if (IsThereAnAppToTakePictures() == true)
            {
                CreateDirectoryForPictures();
                FindViewById<Button>(Resource.Id.launchCameraButton).Click += TakePicture;
                Button openGallery = FindViewById<Button>(Resource.Id.openGallery);
                openGallery.Click += OpenGalleryClick;
            }
        }

        // Apparently, some android devices do not have a camera.  To guard against this,
        // we need to make sure that we can take pictures before we actually try to take a picture.
        private bool IsThereAnAppToTakePictures()
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            IList<ResolveInfo> availableActivities = PackageManager.QueryIntentActivities(intent, PackageInfoFlags.MatchDefaultOnly);
            return availableActivities != null && availableActivities.Count > 0;
        }

        // Creates a directory on the phone that we can place our images
        private void CreateDirectoryForPictures()
        {
            _dir = new Java.IO.File(
                Android.OS.Environment.GetExternalStoragePublicDirectory(
                    Android.OS.Environment.DirectoryPictures), "CameraExample");
            if (!_dir.Exists())
            {
                _dir.Mkdirs();
            }
        }
        
        //This function opens the camera app to take a picture
        private void TakePicture(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            _file = new Java.IO.File(_dir, string.Format("myPhoto_{0}.jpg", System.Guid.NewGuid()));
            StartActivityForResult(intent, 0);
        }

        //Opens the gallery for the user to pick a picture
        private void OpenGalleryClick(object sender, System.EventArgs e)
        {
            Toast.MakeText(this, "Button not yet implemented", ToastLength.Short).Show();
            //This code opens the gallary but I was not able to get the picture to work (W.I.P.)

            //var galleryIntent = new Intent(Intent.ActionPick, MediaStore.Images.Media.ExternalContentUri);
            //galleryIntent.SetType("image/*");
            //galleryIntent.SetAction(Intent.ActionGetContent);
            //StartActivityForResult(Intent.CreateChooser(galleryIntent, "Select Picture"), 1);
        }


        // Called automatically whenever an activity finishes
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            SetContentView(Resource.Layout.PicManip);
            bool reverted = true;
            

            Button remred = FindViewById<Button>(Resource.Id.RemRed);
            Button remgreen = FindViewById<Button>(Resource.Id.RemGreen);
            Button remblue = FindViewById<Button>(Resource.Id.RemBlue);
            Button negred = FindViewById<Button>(Resource.Id.NegRed);
            Button neggreen = FindViewById<Button>(Resource.Id.NegGreen);
            Button negblue = FindViewById<Button>(Resource.Id.NegBlue);
            Button greyscale = FindViewById<Button>(Resource.Id.Greyscale);
            Button high_cont = FindViewById<Button>(Resource.Id.HighContrast);
            Button add_noise = FindViewById<Button>(Resource.Id.AddNoise);
            Button revert = FindViewById<Button>(Resource.Id.Revert);
            Button save = FindViewById<Button>(Resource.Id.Save);

            //test to make sure the picture was found
            if ((resultCode == Result.Ok) && (data != null))
            {
                //Make image available in the gallery
                //Test to see if we came from the camera or gallery
                //If we came from galley no need to make pic available
                if (requestCode == 0)
                {
                    Intent mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
                    var contentUri = Android.Net.Uri.FromFile(_file);
                    mediaScanIntent.SetData(contentUri);
                    SendBroadcast(mediaScanIntent);
                }
            }

            // Display in ImageView. We will resize the bitmap to fit the display.
            // Loading the full sized image will consume too much memory
            // and cause the application to crash.
            ImageView imageView = FindViewById<ImageView>(Resource.Id.takenPictureImageView);
            int height = Resources.DisplayMetrics.HeightPixels;
            int width = imageView.Height;
            bitmap = (Android.Graphics.Bitmap)data.Extras.Get("data");
            bitmap = Android.Graphics.Bitmap.CreateScaledBitmap(bitmap, 1024, 768, true);

            //check to make sure the bitmap has data we can manipulate
            if (bitmap != null)
            {
                copyBitmap = bitmap.Copy(Android.Graphics.Bitmap.Config.Argb8888, true);
                imageView.SetImageBitmap(copyBitmap);
            }

            else
            {
                //If bitmap is null takes the user back to the original screen
                StartMainLayout();
            }

            //Removes the red in the picture by setting the red pixel to 0
            remred.Click += delegate
            {
                for (int i = 0; i < copyBitmap.Width; i++)
                {
                    for (int j = 0; j < copyBitmap.Height; j++)
                    {
                        int p = copyBitmap.GetPixel(i, j);
                        Android.Graphics.Color c = new Android.Graphics.Color(p);

                        c.R = 0;
                        copyBitmap.SetPixel(i, j, c);
                    }
                }

                imageView.SetImageBitmap(copyBitmap);
                reverted = false;
            };

            //Removes the green in the picture by setting the green pixel to 0
            remgreen.Click += delegate
            {
                for (int i = 0; i < copyBitmap.Width; i++)
                {
                    for (int j = 0; j < copyBitmap.Height; j++)
                    {
                        int p = copyBitmap.GetPixel(i, j);
                        Android.Graphics.Color c = new Android.Graphics.Color(p);

                        c.G = 0;
                        copyBitmap.SetPixel(i, j, c);
                    }
                }

                imageView.SetImageBitmap(copyBitmap);
                reverted = false;
            };

            //Removes the blue in the picture by setting the blue pixel to 0
            remblue.Click += delegate
            {
                for (int i = 0; i < copyBitmap.Width; i++)
                {
                    for (int j = 0; j < copyBitmap.Height; j++)
                    {
                        int p = copyBitmap.GetPixel(i, j);
                        Android.Graphics.Color c = new Android.Graphics.Color(p);

                        c.B = 0;
                        copyBitmap.SetPixel(i, j, c);
                    }
                }

                imageView.SetImageBitmap(copyBitmap);
                reverted = false;
            };

            //Negates the red in the picture by subtracting 255 from the current red pixels value
            negred.Click += delegate
            {
                for (int i = 0; i < copyBitmap.Width; i++)
                {
                    for (int j = 0; j < copyBitmap.Height; j++)
                    {
                        int p = copyBitmap.GetPixel(i, j);
                        Android.Graphics.Color c = new Android.Graphics.Color(p);
                        int r1 = c.R;
                        r1 = 255 - r1;
                        c.R = Convert.ToByte(r1);
                        copyBitmap.SetPixel(i, j, c);
                    }
                }

                imageView.SetImageBitmap(copyBitmap);
                reverted = false;
            };

            //Negates the green in the picture by subtracting 255 from the current green pixels value
            neggreen.Click += delegate
            {
                for (int i = 0; i < copyBitmap.Width; i++)
                {
                    for (int j = 0; j < copyBitmap.Height; j++)
                    {
                        int p = copyBitmap.GetPixel(i, j);
                        Android.Graphics.Color c = new Android.Graphics.Color(p);
                        int g1 = c.G;
                        g1 = 255 - g1;
                        c.G = Convert.ToByte(g1);
                        copyBitmap.SetPixel(i, j, c);
                    }
                }

                imageView.SetImageBitmap(copyBitmap);
                reverted = false;
            };

            //Negates the blue in the picture by subtracting 255 from the current blue pixels value
            negblue.Click += delegate
            {
                for (int i = 0; i < copyBitmap.Width; i++)
                {
                    for (int j = 0; j < copyBitmap.Height; j++)
                    {
                        int p = copyBitmap.GetPixel(i, j);
                        Android.Graphics.Color c = new Android.Graphics.Color(p);
                        int b1 = c.B;
                        b1 = 255 - b1;
                        c.B = Convert.ToByte(b1);
                        copyBitmap.SetPixel(i, j, c);
                    }
                }

                imageView.SetImageBitmap(copyBitmap);
                reverted = false;
            };

            //Converts the picture to greyscale by averaging all the pixels values
            //Then setting the each pixel to the average
            greyscale.Click += delegate
            {
                for (int i = 0; i < copyBitmap.Width; i++)
                {
                    for (int j = 0; j < copyBitmap.Height; j++)
                    {
                        int average = 0;
                        int p = copyBitmap.GetPixel(i, j);
                        Android.Graphics.Color c = new Android.Graphics.Color(p);
                        int r_temp = c.R;
                        int g_temp = c.G;
                        int b_temp = c.B;

                        average = (r_temp + g_temp + b_temp) / 3;

                        c.R = Convert.ToByte(average);
                        c.G = Convert.ToByte(average);
                        c.B = Convert.ToByte(average);
                        copyBitmap.SetPixel(i, j, c);
                    }
                }
                imageView.SetImageBitmap(copyBitmap);
                reverted = false;
            };

            //Converts the picture to high contrast
            //If the pixel > 127.5, then set to 255, else set to 0
            high_cont.Click += delegate
            {
                for (int i = 0; i < copyBitmap.Width; i++)
                {
                    for (int j = 0; j < copyBitmap.Height; j++)
                    {
                        int check_num = 255 / 2;
                        int p = copyBitmap.GetPixel(i, j);
                        Android.Graphics.Color c = new Android.Graphics.Color(p);
                        int r_temp = c.R;
                        int g_temp = c.G;
                        int b_temp = c.B;

                        if (r_temp > check_num)
                        {
                            r_temp = 255;
                        }

                        else
                        {
                            r_temp = 0;
                        }

                        if (g_temp > check_num)
                        {
                            g_temp = 255;
                        }

                        else
                        {
                            g_temp = 0;
                        }

                        if (b_temp > check_num)
                        {
                            b_temp = 255;
                        }

                        else
                        {
                            b_temp = 0;
                        }

                        c.R = Convert.ToByte(r_temp);
                        c.G = Convert.ToByte(g_temp);
                        c.B = Convert.ToByte(b_temp);
                        copyBitmap.SetPixel(i, j, c);
                    }
                }
                imageView.SetImageBitmap(copyBitmap);
                reverted = false;
            };

            //Adds random noise to the picture
            //Get a random value from -10 - 10, and add it to the pixels value 
            //making sure the pixel value doesn't go over 255 or under 0.
            add_noise.Click += delegate
            {
                Random rnd = new Random();

                for (int i = 0; i < copyBitmap.Width; i++)
                {
                    for (int j = 0; j < copyBitmap.Height; j++)
                    {
                        int p = copyBitmap.GetPixel(i, j);
                        Android.Graphics.Color c = new Android.Graphics.Color(p);
                        int rand_val = rnd.Next(-10, 11);
                        int r_temp = c.R;
                        int g_temp = c.G;
                        int b_temp = c.B;

                        r_temp += rand_val;
                        g_temp += rand_val;
                        b_temp += rand_val;

                        if(r_temp > 255)
                        {
                            r_temp = 255;
                        }
                        else if (r_temp<0)
                        {
                            r_temp = 0;
                        }

                        if (g_temp > 255)
                        {
                            g_temp = 255;
                        }
                        else if (g_temp < 0)
                        {
                            g_temp = 0;
                        }

                        if (b_temp > 255)
                        {
                            b_temp = 255;
                        }
                        else if (b_temp < 0)
                        {
                            b_temp = 0;
                        }

                        c.R = Convert.ToByte(r_temp);
                        c.G = Convert.ToByte(g_temp);
                        c.B = Convert.ToByte(b_temp);
                        copyBitmap.SetPixel(i, j, c);
                    }
                }

                imageView.SetImageBitmap(copyBitmap);
                reverted = false;
            };

            //Button used to revert the image effects back to the orginal picture
            revert.Click += delegate
            {
                if (bitmap != null && !reverted)
                {
                    copyBitmap = bitmap.Copy(Android.Graphics.Bitmap.Config.Argb8888, true);
                    imageView.SetImageBitmap(copyBitmap);
                    reverted = true;
                }

                else if(reverted)
                {
                    Toast.MakeText(this, "The picture is the original.", ToastLength.Short).Show();
                }
            };

            //Saves the image to the users gallery
            save.Click += delegate
            {
                var sdCardPath = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
                var filePath = System.IO.Path.Combine(sdCardPath, "ImageManip_{0}.png");
                var stream = new FileStream(filePath, FileMode.Create);
                copyBitmap.Compress(Bitmap.CompressFormat.Png, 100, stream);
                stream.Close();
                Toast.MakeText(this, "The picture was saved to gallery.", ToastLength.Short).Show();

                StartMainLayout();
            };

            // Dispose of the Java side bitmap.
            System.GC.Collect();
        }
    }
}

