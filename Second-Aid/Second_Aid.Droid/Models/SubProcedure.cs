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
    class SubProcedure
    {
        [JsonProperty("subProcedureId")]
        public int subProcedureId { get; set; }

        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("description")]
        public string description { get; set; }

        [JsonProperty("procedureId")]
        public int procedureId { get; set; }

        [JsonProperty("procedure")]
        public Procedure procedure { get; set; }

        [JsonProperty("videos")]
        public Video[] videos { get; set; }

        [JsonProperty("preInstructions")]
        public PreInstructions[] preInstructions { get; set; }
    }
}