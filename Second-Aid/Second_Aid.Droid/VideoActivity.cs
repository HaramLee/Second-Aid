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

using Google.YouTube.Player;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Second_Aid.Droid.Models;

namespace Second_Aid.Droid
{
    [Activity(Label = "VideoActivity")]
    public class VideoActivity : YouTubeFailureRecoveryActivity
    {

        public string token;
        public string videoID;
        TextView description;
        TextView title;
        public string videolink;
        #region implemented abstract members of YouTubeFailureRecoveryActivity

        protected override IYouTubePlayerProvider GetYouTubePlayerProvider()
        {
            return FindViewById<YouTubePlayerView>(Resource.Id.youtube_view);
        }

        #endregion

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.VideoLayout);

            this.token = Intent.GetStringExtra(Constants.TOKEN_KEY) ?? "No token detected.";
            this.videoID = Intent.GetStringExtra(Constants.VIDEO_KEY) ?? "No sub procedure name detected.";

            title = FindViewById<TextView>(Resource.Id.VideoName);
            description = FindViewById<TextView>(Resource.Id.VideoDescription);

            await setupVideo();

            YouTubePlayerView youTubeView = FindViewById<YouTubePlayerView>(Resource.Id.youtube_view);
            youTubeView.Initialize(Constants.YOUTUBE_API_KEY, this);

        }

        private async Task setupVideo()
        {
            using (var client = new HttpClient())
            {

                client.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", this.token));

                var response = await client.GetAsync(Constants.BASE_URL + Constants.VIDEO_URL);

                var responseString = await response.Content.ReadAsStringAsync();

                var responseMArray = JsonConvert.DeserializeObject<List<Video>>(responseString);

                foreach (var videoArray in responseMArray)
                {

                    if (videoArray.videoId.ToString().Equals(videoID))
                    {
                        title.Text = videoArray.title;
                        description.Text = "Description: " + videoArray.description;
                        videolink = videoArray.url;
                    }
                }
            }
        }

        public override void OnInitializationSuccess(IYouTubePlayerProvider provider, IYouTubePlayer player, bool wasRestored)
        {
            if (!wasRestored)
            {
                player.CueVideo(videolink.Substring(videolink.Length - 11));
            }
        }
    }
}