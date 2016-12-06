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
    [Activity(Label = "SurveyActivity")]
    public class SurveyActivity : Activity
    {
        private string token;
        private string procedureId;
        private List<string> items = new List<string>();
        private List<string> surveyItem = new List<string>();

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.SurveyLayout);

            this.token = Intent.GetStringExtra(Constants.TOKEN_KEY) ?? "No token detected.";
            this.procedureId = Intent.GetStringExtra(Constants.PROCEDUREID_KEY) ?? "No procedure Id detected.";

            ListView dataDisplay = FindViewById<ListView>(Resource.Id.survey_listview);

            items = await getSubProcedures();
            surveyItem = await getSurvey();

            var adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, surveyItem);
            dataDisplay.Adapter = adapter;

            dataDisplay.ItemClick += listviewClicked;

        }

        void listviewClicked(object sender, AdapterView.ItemClickEventArgs e)
        {
            
        }

        private async Task<List<string>> getSurvey()
        {
            using (var client = new HttpClient())
            {

                client.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", this.token));

                var response = await client.GetAsync(Constants.BASE_URL + Constants.SUBPROCEDURES_URL);

                var responseString = await response.Content.ReadAsStringAsync();

                var responseMArray = JsonConvert.DeserializeObject<List<Survey>>(responseString);

                List<string> data = new List<string>();
                foreach (var surveyList in responseMArray)
                {

                    foreach (var subID in items)
                    {
                        if (surveyList.subProcedureId.ToString().Equals(subID))
                        {
                            data.Add(surveyList.name);
                          
                        }
                    }

                    
                }

                return data;
            }
        }

        private async Task<List<string>> getSubProcedures()
        {
            using (var client = new HttpClient())
            {

                client.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", this.token));

                var response = await client.GetAsync(Constants.BASE_URL + Constants.SUBPROCEDURES_URL);

                var responseString = await response.Content.ReadAsStringAsync();

                var responseMArray = JsonConvert.DeserializeObject<List<SubProcedure>>(responseString);

                List<string> data = new List<string>();
                foreach (var subProcedure in responseMArray)
                {

                    if (subProcedure.procedureId.ToString().Equals(procedureId))
                    {
                        data.Add(subProcedure.subProcedureId.ToString());           
                    }
                }

                return data;
            }
        }
    }
}