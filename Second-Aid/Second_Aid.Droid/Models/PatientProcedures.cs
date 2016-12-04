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
    class PatientProcedures
    {
        [JsonProperty("patientProcedureId")]
        public string patientProcedureId { get; set; }

        [JsonProperty("procedureId")]
        public string procedureId { get; set; }

        [JsonProperty("patientId")]
        public string PatientId { get; set; }

        [JsonProperty("medicationId")]
        public int MedicationId { get; set; }

    }
}