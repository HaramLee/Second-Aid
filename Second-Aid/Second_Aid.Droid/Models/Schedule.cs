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
using Newtonsoft.Json;

namespace Second_Aid.Droid.Models
{
    class Schedule
    {
        [JsonProperty("scheduleId")]
        public int scheduleId { get; set; }

        [JsonProperty("time")]
        public DateTime time { get; set; }

        [JsonProperty("isCompleted")]
        public bool isCompleted { get; set; }

        [JsonProperty("patientId")]
        public string patientId{ get; set; }

        [JsonProperty("procedureId")]
        public int procedureId { get; set; }
    }
}