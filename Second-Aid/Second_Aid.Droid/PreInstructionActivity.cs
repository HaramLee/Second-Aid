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
        private string preprocedureDetail;
        private List<string> videoID = new List<string>();

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.PreInstructionLayout);

            this.token = Intent.GetStringExtra(Constants.TOKEN_KEY) ?? "No token detected.";
            this.preprocedureName = Intent.GetStringExtra(Constants.PREPROCEDURE_KEY) ?? "No sub procedure name detected.";
            this.preprocedureId = Intent.GetStringExtra(Constants.PREPROCEDUREID_KEY) ?? "No sub procedure id detected.";
            this.preprocedureDetail = Intent.GetStringExtra(Constants.PREPROCEDUREDESC_KEY) ?? "no sub procedure description detected.";


            ListView dataDisplay = FindViewById<ListView>(Resource.Id.preinstruction_listview);
            ListView videoDisplay = FindViewById<ListView>(Resource.Id.video_listview);
            TextView description = FindViewById<TextView>(Resource.Id.subdescription);
            TextView title = FindViewById<TextView>(Resource.Id.desc);

            description.Text = "Description: " + preprocedureDetail;
            title.Text = preprocedureName;


            var videoItems = await getVideoList();
            CustomListViewAdapter adapterVideo = new CustomListViewAdapter(this, videoItems);
            videoDisplay.Adapter = adapterVideo;
            videoDisplay.ItemClick += videoListviewClicked;

            var items = await getPreInstructions();
            CustomListViewAdapter adapter = new CustomListViewAdapter(this, items);
            dataDisplay.Adapter = adapter;
            dataDisplay.ItemClick += listviewClicked;
        }

        void listviewClicked(object sender, AdapterView.ItemClickEventArgs e)
        {
            
        }

        void videoListviewClicked(object sender, AdapterView.ItemClickEventArgs e)
        {
            Intent videoActivityIntent = new Intent(this, typeof(VideoActivity));
            videoActivityIntent.PutExtra(Constants.TOKEN_KEY, token);
            videoActivityIntent.PutExtra(Constants.VIDEO_KEY, videoID[e.Position]);
            StartActivity(videoActivityIntent);
        }

        private async Task<List<string>> getVideoList()
        {
            using (var client = new HttpClient())
            {

                client.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", this.token));

                var response = await client.GetAsync(Constants.BASE_URL + Constants.VIDEO_URL);

                var responseString = await response.Content.ReadAsStringAsync();

                var responseMArray = JsonConvert.DeserializeObject<List<Video>>(responseString);

                List<string> data = new List<string>();

                foreach (var videoArray in responseMArray)
                {

                    if (videoArray.subProcedureId.ToString().Equals(preprocedureId))
                    {
                        data.Add(videoArray.title);
                        videoID.Add(videoArray.videoId.ToString());
                    }
                }

                return data;
            }
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