using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using System.Collections.Generic;
using Android.Content.PM;
using Android.Provider;
using Android.Support.V7.App;
using System;
using System.Linq;
using System.Timers;

namespace GoogleApiExample
{
    [Activity(Label = "iFindIt", Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        int total_points = 0;
        Android.Graphics.Bitmap bitmap;
        ImageView imageView;
        private Vibrator myVib;
        string GivenWord;
        Timer timer;
        bool timer_start = false;
        int min = 0;
        int sec = 0;


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
                myVib.Vibrate(30);
                StartFindIt();
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

        private void StartFindIt()
        {
            string CapWord;
            SetContentView(Resource.Layout.GoFind);
            TextView whatToFind = (TextView)FindViewById(Resource.Id.whatToFind);
            ImageView pictureToTake = (ImageView)FindViewById(Resource.Id.pictureToTake);

            String[] WordList = { "technology", "computer keyboard", "house", "circle", "car", "umbrella", "bottle", "clock" };

            Random rnd = new Random();
            int rand = rnd.Next(0, 8);

            //sets the givenword to the random word selected
            GivenWord = WordList[rand];

            ///Capitalize the first letter in the string
            CapWord = GivenWord.First().ToString().ToUpper() + GivenWord.Substring(1);
            whatToFind.Text = (CapWord);

            switch (GivenWord)
            {
                case "clock":
                    pictureToTake.SetImageResource(Resource.Drawable.clock);
                    break;
                case "technology":
                    pictureToTake.SetImageResource(Resource.Drawable.technology);
                    break;
                case "computer keyboard":
                    pictureToTake.SetImageResource(Resource.Drawable.keyboard);
                    break;
                case "house":
                    pictureToTake.SetImageResource(Resource.Drawable.house);
                    break;
                case "circle":
                    pictureToTake.SetImageResource(Resource.Drawable.circle);
                    break;
                case "car":
                    pictureToTake.SetImageResource(Resource.Drawable.car);
                    break;
                case "umbrella":
                    pictureToTake.SetImageResource(Resource.Drawable.umbrella);
                    break;
                case "bottle":
                    pictureToTake.SetImageResource(Resource.Drawable.bottle);
                    break;
                default:
                    break;
            }



            if (IsThereAnAppToTakePictures() == true)
            {
                //starts timer for how long it takes the user to win
                if (!timer_start)
                {
                    timer = new Timer();
                    timer.Interval = 1000;
                    timer.Elapsed += Timer_Elapsed;
                    timer.Start();
                    timer_start = true;
                }

                FindViewById<ImageButton>(Resource.Id.start).Click += TakePicture;
            }


        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            sec++;
            if (sec == 60)
            {
                min++;
                sec = 0;
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
            bool found = false;
            base.OnActivityResult(requestCode, resultCode, data);
            SetContentView(Resource.Layout.Results);

            TextView result = (TextView)FindViewById(Resource.Id.result);
            TextView percentage = (TextView)FindViewById(Resource.Id.percentage);
            ImageButton nextTurn = (ImageButton)FindViewById(Resource.Id.nextTurn);

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

                for (int i = 0; i < apiResult.Responses[0].LabelAnnotations.Count; i++)
                {
                    if (GivenWord == apiResult.Responses[0].LabelAnnotations[i].Description)
                    {
                        rank = i;
                        found = true;
                    }
                }

                //check to make sure the user found the object
                if (found)
                {
                    score = (float)(apiResult.Responses[0].LabelAnnotations[rank].Score);
                    score *= 100;
                    result.Text = ("Correct!! +10 Points");
                    percentage.Text = ("Your picture was " + score + "% accurate!");
                    total_points += 10;
                }

                else
                {
                    result.Text = ("You did not take a picture of a " + GivenWord);
                }

                nextTurn.Click += delegate
                {
                    myVib.Vibrate(30);

                    if (total_points >= 30)
                    {
                        Gameover();
                    }

                    else
                    {
                        StartFindIt();
                    }
                };

                imageView.SetImageBitmap(bitmap);
                bitmap = null;
            }

            // Dispose of the Java side bitmap.
            System.GC.Collect();
        }

        //used to start the game over screen
        private void Gameover()
        {
            SetContentView(Resource.Layout.GameOver);
            TextView timer_view = (TextView)FindViewById(Resource.Id.timer);
            ImageButton startGame = FindViewById<ImageButton>(Resource.Id.start_game);

            timer.Stop();
            timer_view.Text = "It took you ";
            if (min == 1)
            {
                timer_view.Text += min + " minute " + sec + " seconds to find the items! Good Job!";
            }

            else if (min > 1)
            {
                timer_view.Text += min + " minutes " + sec + " seconds to find the items! Good Job!";
            }

            else
            {
                timer_view.Text += sec + " seconds to find the items! Good Job!";
            }

            timer.Dispose();
            timer = null;
            timer_start = false;
            total_points = 0;
            min = 0;
            sec = 0;

            startGame.Click += delegate
            {
                myVib.Vibrate(30);
                StartMainLayout();
            };
        }
    }
}


