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
using Second_Aid.Droid.Models;
using Newtonsoft.Json;

namespace Second_Aid.Droid
{
    [Activity(Label = "PreInstruction")]
    public class PreInstructionActivity : Activity
    {
        public string token;
        public string preprocedureName;
        public string preprocedureId;
        private string subprocedureDetail;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.PreInstructionLayout);

            this.token = Intent.GetStringExtra(Constants.TOKEN_KEY) ?? "No token detected.";
            this.preprocedureName = Intent.GetStringExtra(Constants.PREPROCEDURE_KEY) ?? "No sub procedure name detected.";
            this.preprocedureId = Intent.GetStringExtra(Constants.PREPROCEDUREID_KEY) ?? "No sub procedure id detected.";
            this.subprocedureDetail = Intent.GetStringExtra(Constants.PREPROCEDUREDESC_KEY) ?? "no sub procedure description detected.";


            ListView dataDisplay = FindViewById<ListView>(Resource.Id.preinstruction_listview);
            TextView description = FindViewById<TextView>(Resource.Id.subdescription);

            description.Text = subprocedureDetail;

            var items = await getPreInstructions();
            var adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, items);
            dataDisplay.Adapter = adapter;

            dataDisplay.ItemClick += listviewClicked;
        }

        void listviewClicked(object sender, AdapterView.ItemClickEventArgs e)
        {

        }

        private async Task<List<string>> getPreInstructions()
        {
            using (var client = new HttpClient())
            {

                client.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", this.token));

                var response = await client.GetAsync(Constants.BASE_URL + Constants.PREINSTRUCTIONS_URL);

                var responseString = await response.Content.ReadAsStringAsync();

                var responseMArray = JsonConvert.DeserializeObject<List<PreInstructions>>(responseString);

                List<string> data = new List<string>();

                foreach (var preInstructions in responseMArray)
                {

                    if (preInstructions.subProcedureId.ToString().Equals(preprocedureId))
                    {
                        data.Add(preInstructions.title);
                    }
                }

                return data;
            }
        }
    }
}