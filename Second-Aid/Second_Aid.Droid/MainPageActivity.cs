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
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Second_Aid.Droid.Models;
using System.Threading.Tasks;

namespace Second_Aid.Droid
{
    [Activity(Label = "MainPageActivity")]
    public class MainPageActivity : Activity
    {
        private string token;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.MainPageLayout);

            this.token = Intent.GetStringExtra(Constants.TOKEN_KEY) ?? "No token detected.";
            Android.Widget.Toast.MakeText(this, this.token, Android.Widget.ToastLength.Short).Show();

            ListView dataDisplay = FindViewById<ListView>(Resource.Id.data_listview);

            Button logoutBtn = FindViewById<Button>(Resource.Id.logout_button);
            logoutBtn.Click += (object sender, EventArgs args) =>
            {
                logout();
            };

            Button medicationsBtn = FindViewById<Button>(Resource.Id.medications_button);
            medicationsBtn.Click += async (object sender, EventArgs args) =>
            {
                var items = await getMedications();
                var adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, items);
                dataDisplay.Adapter = adapter;
            };
        }

        private async Task<List<string>> getMedications()
        {
            using (var client = new HttpClient())
            {
                // THIS DOESN'T WORK, even when encoding token => UTF8Bytes => Base64String
                // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer ", this.token);
                client.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", this.token));

                var response = await client.GetAsync(Constants.BASE_URL + Constants.MEDICATION_URL);

                var responseString = await response.Content.ReadAsStringAsync();

                var responseMArray = JsonConvert.DeserializeObject<List<Medication>>(responseString);

                List<string> data = new List<string>();
                foreach (var medication in responseMArray)
                {
                    data.Add(medication.Name);
                }

                return data; 
            }
        }

        public override void OnBackPressed()
        {
            logout();
        }

        private void logout()
        {
            // logout? connect/logout doesn't seem to work
            Finish();
        }

    }
}