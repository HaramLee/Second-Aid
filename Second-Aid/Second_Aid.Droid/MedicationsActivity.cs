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
    [Activity(Label = "MedicationsActivity")]
    public class MedicationsActivity : Activity
    {
        private string token;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.MedicationsLayout);

            this.token = Intent.GetStringExtra(Constants.TOKEN_KEY) ?? "No token detected.";

            ListView dataDisplay = FindViewById<ListView>(Resource.Id.medications_listview);

            var items = await getMedications();
            var adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, items);
            dataDisplay.Adapter = adapter;
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
    }
}