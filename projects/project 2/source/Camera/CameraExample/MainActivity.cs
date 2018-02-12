using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using System.Collections.Generic;
using Android.Content.PM;
using Android.Provider;
using System;
using Uri = Android.Net.Uri;

namespace CameraExample
{
    [Activity(Label = "Image Manipulator", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        // Used to track the file that we're manipulating between functions
        public static Java.IO.File _file;

        // Used to track the directory that we'll be writing to between functions
        public static Java.IO.File _dir;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);


            if (IsThereAnAppToTakePictures() == true)
            {
                CreateDirectoryForPictures();
                FindViewById<Button>(Resource.Id.launchCameraButton).Click += TakePicture;
                Button openGallery = FindViewById<Button>(Resource.Id.openGallery);
                openGallery.Click += openGalleryClick;
            }


        }

        // <summary>
        // Apparently, some android devices do not have a camera.  To guard against this,
        // we need to make sure that we can take pictures before we actually try to take a picture.
        // </summary>
        // <returns></returns>
        private bool IsThereAnAppToTakePictures()
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            IList<ResolveInfo> availableActivities =
                PackageManager.QueryIntentActivities
                (intent, PackageInfoFlags.MatchDefaultOnly);
            return availableActivities != null && availableActivities.Count > 0;
        }

        // <summary>
        // Creates a directory on the phone that we can place our images
        // </summary>
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

        private void TakePicture(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            _file = new Java.IO.File(_dir, string.Format("myPhoto_{0}.jpg", System.Guid.NewGuid()));
            //android.support.v4.content.FileProvider
            //getUriForFile(getContext(), "com.mydomain.fileprovider", newFile);
            //FileProvider.GetUriForFile
            intent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(_file));
            StartActivityForResult(intent, 0);

        }

        private void openGalleryClick(object sender, System.EventArgs e)
        {
            var galleryIntent = new Intent(Intent.ActionPick, MediaStore.Images.Media.ExternalContentUri);
            galleryIntent.SetType("image/*");
            galleryIntent.SetAction(Intent.ActionGetContent);
            StartActivityForResult(Intent.CreateChooser(galleryIntent, "Select Picture"), 1);
        }

        // Called automatically whenever an activity finishes

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            SetContentView(Resource.Layout.PicManip);

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

                else
                {
                    Uri uri = data.Data;
                    //_file.SetImageURI(uri);

                }
            }

            // Display in ImageView. We will resize the bitmap to fit the display.
            // Loading the full sized image will consume too much memory
            // and cause the application to crash.
            ImageView imageView = FindViewById<ImageView>(Resource.Id.takenPictureImageView);
            int height = Resources.DisplayMetrics.HeightPixels;
            int width = imageView.Height;
            Android.Graphics.Bitmap bitmap = _file.Path.LoadAndResizeBitmap(width, height);
            Android.Graphics.Bitmap copyBitmap = bitmap.Copy(Android.Graphics.Bitmap.Config.Argb8888, true);
            imageView.SetImageBitmap(copyBitmap);

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
            };

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

            };

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
            };

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
            };

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
            };

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
            };

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
            };

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
            };

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

                        if (rand_val < 0)
                            {
                                if ((r_temp + rand_val) < 0)
                                {
                                    r_temp = 0;
                                }

                                else
                                {
                                    r_temp += rand_val;
                                }

                                if ((g_temp + rand_val) < 0)
                                {
                                    g_temp = 0;
                                }

                                else
                                {
                                    g_temp += rand_val;
                                }

                                if ((b_temp + rand_val) < 0)
                                {
                                    b_temp = 0;
                                }

                                else
                                {
                                    b_temp += rand_val;
                                }
                            }

                            else
                            {
                                if ((r_temp + rand_val) > 255)
                                {
                                    r_temp = 255;
                                }

                                else
                                {
                                    r_temp += rand_val;
                                }

                                if ((g_temp + rand_val) > 255)
                                {
                                    g_temp = 255;
                                }

                                else
                                {
                                    g_temp += rand_val;
                                }

                                if ((b_temp + rand_val) > 255)
                                {
                                    b_temp = 255;
                                }

                                else
                                {
                                    b_temp += rand_val;
                                }
                            }

                        c.R = Convert.ToByte(r_temp);
                        c.G = Convert.ToByte(g_temp);
                        c.B = Convert.ToByte(b_temp);
                        copyBitmap.SetPixel(i, j, c);
                    }
                }

                imageView.SetImageBitmap(copyBitmap);
            };

            revert.Click += delegate
            {
                if (copyBitmap != null)
                {
                    copyBitmap = bitmap.Copy(Android.Graphics.Bitmap.Config.Argb8888, true);
                    imageView.SetImageBitmap(copyBitmap);
                }
            };




            // Dispose of the Java side bitmap.
            System.GC.Collect();
        }
    }
}

