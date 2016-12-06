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
    [Activity(Label = "MedicationInstructionActivity")]
    public class MedicationInstructionActivity : Activity
    {

        public string token;
        public string medicationName;
        public string medicationId;
        public string medicationDesc;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.MedicationInstructionLayout);

            this.token = Intent.GetStringExtra(Constants.TOKEN_KEY) ?? "No token detected.";
            this.medicationName = Intent.GetStringExtra(Constants.MEDICATION_KEY) ?? "No sub medication name detected.";
            this.medicationId = Intent.GetStringExtra(Constants.MEDICATIONID_KEY) ?? "No sub medication id detected.";
            this.medicationDesc = Intent.GetStringExtra(Constants.MEDICATIONDESC_KEY) ?? "No sub medication description detected.";

            ListView dataDisplay = FindViewById<ListView>(Resource.Id.medicationInstruction_listview);
            TextView description = FindViewById<TextView>(Resource.Id.medDescription);
            TextView title = FindViewById<TextView>(Resource.Id.title);

            description.Text = "Description: " + medicationDesc;
            title.Text = medicationName;

            var items = await getMedicationInstructions();
            var adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, items);
            dataDisplay.Adapter = adapter;
        }

        private async Task<List<string>> getMedicationInstructions()
        {
            using (var client = new HttpClient())
            {

                client.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", this.token));

                var response = await client.GetAsync(Constants.BASE_URL + Constants.MEDICATIONINSTRUCTION_URL);

                var responseString = await response.Content.ReadAsStringAsync();

                var responseMArray = JsonConvert.DeserializeObject<List<MedicationInstruction>>(responseString);

                List<string> data = new List<string>();

                foreach (var medInstructions in responseMArray)
                {

                    if (medInstructions.medicationId.ToString().Equals(medicationId))
                    {
                        data.Add(medInstructions.instruction);
                    }
                }

                return data;
            }
        }
    }
}