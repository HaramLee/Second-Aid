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
    class PreInstructions
    {
        [JsonProperty("preInstructionId")]
        public int preInstructionId { get; set; }

        [JsonProperty("title")]
        public string title { get; set; }

        [JsonProperty("description")]
        public string description { get; set; }

        [JsonProperty("subProcedureId")]
        public int subProcedureId { get; set; }

        [JsonProperty("subProcedure")]
        public SubProcedure subProcedure { get; set; }
    }
}