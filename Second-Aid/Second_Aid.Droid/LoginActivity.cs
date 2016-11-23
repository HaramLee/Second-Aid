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

namespace Second_Aid.Droid
{
    [Activity(Label = "LoginActivity")]
    public class LoginActivity : Activity
    {
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
                
            };
        }
    }
}