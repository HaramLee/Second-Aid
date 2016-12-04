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
    class Clinic
    {
        [JsonProperty("clinicId")]
        public int clinicId { get; set; }

        [JsonProperty("clinicName")]
        public string clinicName { get; set; }

        [JsonProperty("clinicAddress")]
        public string clinicAddress { get; set; }

        [JsonProperty("phoneNumber")]
        public string phoneNumber { get; set; }
    }
}