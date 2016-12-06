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
        private IList<string> medicationId = new List<string>();
        private List<string> items = new List<string>();
        private List<string> itemID = new List<string>();
        private List<string> itemDesc = new List<string>();

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.MedicationsLayout);

            this.token = Intent.GetStringExtra(Constants.TOKEN_KEY) ?? "No token detected.";
            this.medicationId = Intent.GetStringArrayListExtra(Constants.MEDICATION_KEY);

            ListView dataDisplay = FindViewById<ListView>(Resource.Id.medications_listview);

            items = await getMedications();

            var adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, items);
            dataDisplay.Adapter = adapter;
            dataDisplay.ItemClick += listviewClicked;

        }

        void listviewClicked(object sender, AdapterView.ItemClickEventArgs e)
        {
            Intent medicationInstructionActivityIntent = new Intent(this, typeof(MedicationInstructionActivity));
            medicationInstructionActivityIntent.PutExtra(Constants.TOKEN_KEY, token);
            medicationInstructionActivityIntent.PutExtra(Constants.MEDICATION_KEY, items[e.Position]);
            medicationInstructionActivityIntent.PutExtra(Constants.MEDICATIONID_KEY, itemID[e.Position]);
            medicationInstructionActivityIntent.PutExtra(Constants.MEDICATIONDESC_KEY, itemDesc[e.Position]);
            StartActivity(medicationInstructionActivityIntent);
        }

        private async Task<List<string>> getMedications()
        {
            using (var client = new HttpClient())
            {

                client.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", this.token));

                var response = await client.GetAsync(Constants.BASE_URL + Constants.MEDICATION_URL);

                var responseString = await response.Content.ReadAsStringAsync();

                var responseMArray = JsonConvert.DeserializeObject<List<Medication>>(responseString);

                List<string> data = new List<string>();
                foreach (var medication in responseMArray)
                {
                    foreach (var id in medicationId)
                    {
                        if (id.ToString().Equals(medication.Id))
                        {

                            data.Add(medication.Name);
                            itemID.Add(medication.Id);
                            itemDesc.Add(medication.Description);
                        }
                    }

                }

                return data;
            }
        }
    }
}