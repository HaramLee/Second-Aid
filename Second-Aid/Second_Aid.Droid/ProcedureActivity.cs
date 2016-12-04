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
using Newtonsoft.Json;
using Second_Aid.Droid.Models;

namespace Second_Aid.Droid
{
    [Activity(Label = "ProcedureActivity")]
    public class ProcedureActivity : Activity
    {

        private string procedureName;
        private string token;
        TextView title;
        TextView ClinicName;
        TextView PhoneNumber;
        TextView DoctorName;
        TextView Schedule;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Procedures);

            this.procedureName = Intent.GetStringExtra(Constants.PROCEDURE_KEY) ?? "No Procedure Name detected.";
            this.token = Intent.GetStringExtra(Constants.TOKEN_KEY) ?? "No token detected.";

            Button medicationButton = FindViewById<Button>(Resource.Id.MedicationButton);
            Button preprocedureButton = FindViewById<Button>(Resource.Id.PreprocedureButton);

            title = FindViewById<TextView>(Resource.Id.ProcedureName);
            ClinicName = FindViewById<TextView>(Resource.Id.ClinicName);
            PhoneNumber = FindViewById<TextView>(Resource.Id.PhoneNumber);
            DoctorName = FindViewById<TextView>(Resource.Id.DoctorName);
            Schedule = FindViewById<TextView>(Resource.Id.Schedule);

            title.Text = procedureName;

            getClinic();

            //medication button click action
            medicationButton.Click += (object sender, EventArgs e) =>
            {

                Intent medicationActivityIntent = new Intent(this, typeof(MedicationsActivity));
                medicationActivityIntent.PutExtra(Constants.TOKEN_KEY, token);
                StartActivity(medicationActivityIntent);

            };

            //preprocedure button click action
            preprocedureButton.Click += (object sender, EventArgs e) => {
        
                Intent preprocedureActivityIntent = new Intent(this, typeof(SubProcedureActivity));
                preprocedureActivityIntent.PutExtra(Constants.TOKEN_KEY, token);
                preprocedureActivityIntent.PutExtra(Constants.PROCEDURE_KEY, procedureName);
                StartActivity(preprocedureActivityIntent);
     
            };

        }

        private async void getClinic()
        {
            using (var client = new HttpClient())
            {
                // THIS DOESN'T WORK, even when encoding token => UTF8Bytes => Base64String
                // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer ", this.token);
                client.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", this.token));

                var response = await client.GetAsync(Constants.BASE_URL + Constants.CLINIC_URL);

                var responseString = await response.Content.ReadAsStringAsync();

                var responseMArray = JsonConvert.DeserializeObject<List<Clinic>>(responseString);

                ClinicName.Text = responseMArray[0].clinicAddress;
                PhoneNumber.Text = responseMArray[0].phoneNumber;


            }
        }
    }
}