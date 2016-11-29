using System;
using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Net.Http;

namespace Second_Aid.Droid
{
    [Activity(Label = "LoginActivity")]
    public class LoginActivity : Activity
    {
        private const string BASE_URL = "http://2aid.azurewebsites.net";
        private const string TOKEN_URL = "/connect/token";

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.login);

            //Initializing button from layout
            Button login = FindViewById<Button>(Resource.Id.login);

            var usernameInput = FindViewById<TextView>(Resource.Id.userName);
            var passwordInput = FindViewById<TextView>(Resource.Id.password);

            //Login button click action
            login.Click += (object sender, EventArgs e) => {
                var username = usernameInput.Text.ToString();
                var password = passwordInput.Text.ToString(); 
                Android.Widget.Toast.MakeText(this, "Login Button Clicked with credentials " + username + password, Android.Widget.ToastLength.Short).Show();

                Login(BASE_URL + TOKEN_URL, username, password);

            };
        }

        private async void Login(string url, string username, string password)
        {
            using (var client = new HttpClient())
            {
                var values = new Dictionary<string, string>
                {
                   { "grant_type", "password" },
                   { "username", username },
                   { "password", password }
                };

                var content = new FormUrlEncodedContent(values);

                var response = await client.PostAsync(url, content);

                var responseString = await response.Content.ReadAsStringAsync();
                Console.Write(responseString);

                Android.Widget.Toast.MakeText(this, responseString, Android.Widget.ToastLength.Short).Show();

            }


            /*
            using (WebClient client = new WebClient())
            {
                string json = "{'grant_type':'password', 'username': " + username + ", 'password': " + password + "}";
                Console.Write(json);
                var result = client.UploadString(url, json);
                Console.Write(result);
            }*/
        }
    }
}