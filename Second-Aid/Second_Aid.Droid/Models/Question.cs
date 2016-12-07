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
    class Question
    {
        [JsonProperty("questionId")]
        public int questionId { get; set; }

        [JsonProperty("questionBody")]
        public string questionBody { get; set; }

        [JsonProperty("subProcedureId")]
        public int subProcedureId { get; set; }
    }
}