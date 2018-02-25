using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using System.Collections.Generic;
using Android.Content.PM;
using Android.Provider;
using Android.Support.V7.App;
using System;

namespace GoogleApiExample
{
    [Activity(Label = "iFindIt", Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        int total_points = 0;
        int count = 0;
        Android.Graphics.Bitmap bitmap;
        ImageView imageView;
        private Vibrator myVib;
        string GivenWord;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            myVib = (Vibrator)GetSystemService(VibratorService);

            StartMainLayout();
        }

        //Starts the main layout, and makes the button functional
        private void StartMainLayout()
        {
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            ImageButton startGame = FindViewById<ImageButton>(Resource.Id.start_game);

            startGame.Click += delegate
            {
                startFindIt();
            };
        }

        //If the back button is pressed go back to the main layout to start over
        public override void OnBackPressed()
        {
            myVib.Vibrate(30);
            SetContentView(Resource.Layout.Main);
            Toast.MakeText(this, "Back Pressed: Starting over", ToastLength.Short).Show();
            StartMainLayout();
        }

        /// Apparently, some android devices do not have a camera.  To guard against this,
        /// we need to make sure that we can take pictures before we actually try to take a picture.
        private bool IsThereAnAppToTakePictures()
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            IList<ResolveInfo> availableActivities =
                PackageManager.QueryIntentActivities
                (intent, PackageInfoFlags.MatchDefaultOnly);
            return availableActivities != null && availableActivities.Count > 0;
        }

        private void startFindIt()
        {
            SetContentView(Resource.Layout.GoFind);
            TextView whatToFind = (TextView)FindViewById(Resource.Id.whatToFind);


            String[] WordList = { "technology", "computer keyboard", "basketball" , "circle", "car", "umbrella"};

            Random rnd = new Random();
            int rand = rnd.Next(0,3);

            whatToFind.Text = (WordList[rand]);
            GivenWord = WordList[rand];

            if (IsThereAnAppToTakePictures() == true)
            {
                FindViewById<Button>(Resource.Id.go).Click += TakePicture;
            }
           

        }


        private void TakePicture(object sender, System.EventArgs e)
        {
            myVib.Vibrate(30);
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            StartActivityForResult(intent, 0);
        }

        // Called automatically whenever an activity finishes
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            
            int rank = -1;
            float score = 0;
            base.OnActivityResult(requestCode, resultCode, data);
            SetContentView(Resource.Layout.Results);

            TextView result = (TextView)FindViewById(Resource.Id.result);
            TextView percentage = (TextView)FindViewById(Resource.Id.percentage);

            //Test to make sure user took a picture
            if (data != null)
            {
                // Display in ImageView. We will resize the bitmap to fit the display.
                // Loading the full sized image will consume too much memory
                // and cause the application to crash.
                imageView = FindViewById<ImageView>(Resource.Id.takenPictureImageView);
                int height = Resources.DisplayMetrics.HeightPixels;
                int width = imageView.Height;

                //AC: workaround for not passing actual files
                bitmap = (Android.Graphics.Bitmap)data.Extras.Get("data");
            }

            else
            {
                Toast.MakeText(this, "Error: Starting over", ToastLength.Short).Show();
                StartMainLayout();
            }

            //Test to make sure we have the bitmap
            if (bitmap != null)
            {
                //convert bitmap into stream to be sent to Google API
                string bitmapString = "";
                using (var stream = new System.IO.MemoryStream())
                {
                    bitmap.Compress(Android.Graphics.Bitmap.CompressFormat.Jpeg, 95, stream);
                    var bytes = stream.ToArray();
                    bitmapString = System.Convert.ToBase64String(bytes);
                }

                //credential is stored in "assets" folder
                string credPath = "API-Game-7e75c497f0b6.json";
                Google.Apis.Auth.OAuth2.GoogleCredential cred;

                //Load credentials into object form
                using (var stream = Assets.Open(credPath))
                {
                    cred = Google.Apis.Auth.OAuth2.GoogleCredential.FromStream(stream);
                }
                cred = cred.CreateScoped(Google.Apis.Vision.v1.VisionService.Scope.CloudPlatform);

                // By default, the library client will authenticate 
                // using the service account file (created in the Google Developers 
                // Console) specified by the GOOGLE_APPLICATION_CREDENTIALS 
                // environment variable. We are specifying our own credentials via json file.
                var client = new Google.Apis.Vision.v1.VisionService(new Google.Apis.Services.BaseClientService.Initializer()
                {
                    ApplicationName = "api-game-195221",
                    HttpClientInitializer = cred
                });

                //set up request
                var request = new Google.Apis.Vision.v1.Data.AnnotateImageRequest();
                request.Image = new Google.Apis.Vision.v1.Data.Image();
                request.Image.Content = bitmapString;

                //tell google that we want to perform label detection
                request.Features = new List<Google.Apis.Vision.v1.Data.Feature>();
                request.Features.Add(new Google.Apis.Vision.v1.Data.Feature() { Type = "LABEL_DETECTION" });

                //add to list of items to send to google
                var batch = new Google.Apis.Vision.v1.Data.BatchAnnotateImagesRequest();
                batch.Requests = new List<Google.Apis.Vision.v1.Data.AnnotateImageRequest>();
                batch.Requests.Add(request);


                //send request.  Note that I'm calling execute() here, but you might want to use
                //ExecuteAsync instead
                var apiResult = client.Images.Annotate(batch).Execute();

                count = apiResult.Responses[0].LabelAnnotations.Count;

                for (int i = 0; i < count; i++)
                {
                    if (GivenWord == apiResult.Responses[0].LabelAnnotations[i].Description)
                    {
                        rank = i;
                    }
                }

                score = (float)(apiResult.Responses[0].LabelAnnotations[rank].Score);
                score *= 100;


                if (rank != -1)
                {
                    result.Text = ("Correct!! +10 Points");
                    percentage.Text = ("Your picture was " + score + "% accurate!" );
                    total_points += 10;

                    if (total_points >= 100)
                    {
                        //implement

                    }
                }

                else
                {
                    result.Text = ("LOSER!!");
                }

                imageView.SetImageBitmap(bitmap);
                bitmap = null;
            }

            // Dispose of the Java side bitmap.
            System.GC.Collect();
        }
    }
}

