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
    class MedicationInstruction
    {
        [JsonProperty("medicationInstructionId")]
        public int medicationInstructionId { get; set; }

        [JsonProperty("instruction")]
        public string instruction { get; set; }

        [JsonProperty("medicationId")]
        public int medicationId { get; set; }

    }
}