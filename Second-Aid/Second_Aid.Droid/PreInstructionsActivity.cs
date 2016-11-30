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
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Second_Aid.Droid.Models;

namespace Second_Aid.Droid
{
    [Activity(Label = "PreInstructionsActivity")]
    public class PreInstructionsActivity : Activity
    {
        public string token;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.PreInstructions);

            this.token = Intent.GetStringExtra(Constants.TOKEN_KEY) ?? "No token detected.";
            Android.Widget.Toast.MakeText(this, this.token, Android.Widget.ToastLength.Short).Show();

            ListView dataDisplay = FindViewById<ListView>(Resource.Id.data_listview);

            Button preInstructionsBtn = FindViewById<Button>(Resource.Id.preInstructions_button);
            preInstructionsBtn.Click += async (object sender, EventArgs args) =>
            {
                var items = await getPreInstructions();
                var adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, items);
                dataDisplay.Adapter = adapter;
            };
        }

        private async Task<List<string>> getPreInstructions()
        {
            using (var client = new HttpClient())
            {
                // THIS DOESN'T WORK, even when encoding token => UTF8Bytes => Base64String
                // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer ", this.token);
                client.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", this.token));

                var response = await client.GetAsync(Constants.BASE_URL + Constants.PREINSTRUCTIONS_URL);

                var responseString = await response.Content.ReadAsStringAsync();

                var responseMArray = JsonConvert.DeserializeObject<List<PreInstructions>>(responseString);

                List<string> data = new List<string>();
                foreach (var preInstruction in responseMArray)
                {
                    data.Add(preInstruction.title);
                }

                return data;
            }
        }
    }
}