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
    class Survey
    {
        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("subProcedureId")]
        public int subProcedureId { get; set; }

        [JsonProperty("subProcedure")]
        public SubProcedure subProcedure { get; set; }

        [JsonProperty("questions")]
        public Question[] questions { get; set; }

        [JsonProperty("createdBy")]
        public string createdBy { get; set; }

        [JsonProperty("dateCreated")]
        public DateTime dateCreated { get; set; }
    }
}