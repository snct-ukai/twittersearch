using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;

namespace twittersearch
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        public TextView Result { get; private set; }
        public TextView searchword { get; private set; }

        public Button Search { get; private set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            Result = FindViewById<TextView>(Resource.Id.text);
            searchword = FindViewById<EditText>(Resource.Id.searchword);

            Search = FindViewById<Button>(Resource.Id.search);
            Search.Click += Search_Click;
        }

        private async void Search_Click(object sender, System.EventArgs e)
        {
            var token = CoreTweet.Tokens.Create(APIKeys.Apikey(), APIKeys.Apikeysecret(), APIKeys.Accesstoken(), APIKeys.Accesstokensecret());
            var search = searchword.Text;
            var result = await token.Search.TweetsAsync(count => 5, q => search);

            Result.Text = search + "\n";

            foreach (var tweet in result)
            {
                Result.Text += "\n---------------\n" + tweet.User.ScreenName + "\n\n" + tweet.Text;
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}