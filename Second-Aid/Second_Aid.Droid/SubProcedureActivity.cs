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
    [Activity(Label = "SubProcedureActivity")]
    public class SubProcedureActivity : Activity
    {
        public string token;
        public string procedureName;
        private List<string> items = new List<string>();
        private List<int> subProcedureId = new List<int>();

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.SubProcedures);

            this.token = Intent.GetStringExtra(Constants.TOKEN_KEY) ?? "No token detected.";
            this.procedureName = Intent.GetStringExtra(Constants.PROCEDURE_KEY) ?? "No procedure name detected.";

            ListView dataDisplay = FindViewById<ListView>(Resource.Id.data_listview);

            items = await getSubProcedures();
            var adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, items);
            dataDisplay.Adapter = adapter;

            dataDisplay.ItemClick += listviewClicked;

        }

        void listviewClicked(object sender, AdapterView.ItemClickEventArgs e)
        {
            Intent procedureActivityIntent = new Intent(this, typeof(PreInstructionActivity));

            procedureActivityIntent.PutExtra(Constants.PREPROCEDURE_KEY, items[e.Position]);
            procedureActivityIntent.PutExtra(Constants.PREPROCEDUREID_KEY, subProcedureId[e.Position]);
            procedureActivityIntent.PutExtra(Constants.TOKEN_KEY, token);

            StartActivity(procedureActivityIntent);
        }

        private async Task<List<string>> getSubProcedures()
        {
            using (var client = new HttpClient())
            {
                // THIS DOESN'T WORK, even when encoding token => UTF8Bytes => Base64String
                // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer ", this.token);
                client.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", this.token));

                var response = await client.GetAsync(Constants.BASE_URL + Constants.SUBPROCEDURES_URL);

                var responseString = await response.Content.ReadAsStringAsync();

                var responseMArray = JsonConvert.DeserializeObject<List<SubProcedure>>(responseString);

                List<string> data = new List<string>();
                foreach (var subProcedure in responseMArray)
                {
                    if (subProcedure.procedure.name.Equals(procedureName))
                    {
                        data.Add(subProcedure.name);
                        subProcedureId.Add(subProcedure.subProcedureId);
                    }
                }

                return data;
            }
        }
    }
}