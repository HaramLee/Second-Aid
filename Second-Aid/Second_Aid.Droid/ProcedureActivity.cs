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
        private string procedureId;
        private string procedureDetail;
        private string token;

        Button medicationButton;
        Button preprocedureButton;
        Button surveyButton;

        TextView Schedule;
        ImageView scheduleWaitIcon;
        ImageView scheduleDoneIcon;

        private Schedule schedule;    // A schedule is one event, binded to a patient and a procedure.

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Procedures);

            this.procedureName = Intent.GetStringExtra(Constants.PROCEDURE_KEY) ?? "No Procedure Name detected.";
            this.procedureId = Intent.GetStringExtra(Constants.PROCEDUREID_KEY) ?? "No Procedure Id detected.";
            this.token = Intent.GetStringExtra(Constants.TOKEN_KEY) ?? "No token detected.";
            this.procedureDetail = Intent.GetStringExtra(Constants.PROCEDUREDESC_KEY) ?? "No Procedure description detected.";

            medicationButton = FindViewById<Button>(Resource.Id.MedicationButton);
            preprocedureButton = FindViewById<Button>(Resource.Id.PreprocedureButton);
            surveyButton = FindViewById<Button>(Resource.Id.surveyButton);

            TextView title = FindViewById<TextView>(Resource.Id.ProcedureName);
            TextView DoctorName = FindViewById<TextView>(Resource.Id.DoctorName);
            TextView Description = FindViewById<TextView>(Resource.Id.Description);

            Schedule = FindViewById<TextView>(Resource.Id.Schedule);
            scheduleWaitIcon = FindViewById<ImageView>(Resource.Id.scheduleWaitIcon);
            scheduleDoneIcon = FindViewById<ImageView>(Resource.Id.scheduleDoneIcon);

            title.Text = procedureName;
            Description.Text = "Description: " + procedureDetail;

            getClinic();

            //medication button click action
            medicationButton.Click += (object sender, EventArgs e) =>
            {

                Intent medicationActivityIntent = new Intent(this, typeof(MedicationsActivity));
                medicationActivityIntent.PutExtra(Constants.TOKEN_KEY, token);
                medicationActivityIntent.PutExtra(Constants.PROCEDUREID_KEY, procedureId);
                StartActivity(medicationActivityIntent);

            };

            //preprocedure button click action
            preprocedureButton.Click += (object sender, EventArgs e) => {

                Intent preprocedureActivityIntent = new Intent(this, typeof(SubProcedureActivity));
                preprocedureActivityIntent.PutExtra(Constants.TOKEN_KEY, token);
                preprocedureActivityIntent.PutExtra(Constants.PROCEDUREID_KEY, procedureId);
                StartActivity(preprocedureActivityIntent);

            };

            surveyButton.Click += (object sender, EventArgs e) => {

                Intent preprocedureActivityIntent = new Intent(this, typeof(SurveyActivity));
                preprocedureActivityIntent.PutExtra(Constants.TOKEN_KEY, token);
                preprocedureActivityIntent.PutExtra(Constants.PROCEDUREID_KEY, procedureId);
                StartActivity(preprocedureActivityIntent);

            };

            getSchedule();
        }

        private async void getClinic()
        {
            using (var client = new HttpClient())
            {

                client.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", this.token));

                var response = await client.GetAsync(Constants.BASE_URL + Constants.CLINIC_URL);

                var responseString = await response.Content.ReadAsStringAsync();

                var responseMArray = JsonConvert.DeserializeObject<List<Clinic>>(responseString);

                TextView ClinicName = FindViewById<TextView>(Resource.Id.ClinicName);
                TextView PhoneNumber = FindViewById<TextView>(Resource.Id.PhoneNumber);

                ClinicName.Text = responseMArray[0].clinicAddress;
                PhoneNumber.Text = responseMArray[0].phoneNumber;

            }
        }

        private async void getSchedule()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", this.token));

                var response = await client.GetAsync(Constants.BASE_URL + Constants.SCHEDULE_URL);

                var responseString = await response.Content.ReadAsStringAsync();

                var responseMArray = JsonConvert.DeserializeObject<List<Schedule>>(responseString);

                foreach (Schedule s in responseMArray)
                {
                    if (s.procedureId.ToString() == this.procedureId)
                    {
                        this.schedule = s;
                        break;
                    }
                }                

                if (this.schedule != null && this.schedule.isCompleted)
                {
                    this.surveyButton.Visibility = ViewStates.Visible;
                    this.medicationButton.Visibility = ViewStates.Gone;
                    this.preprocedureButton.Visibility = ViewStates.Gone;
                    this.scheduleDoneIcon.Visibility = ViewStates.Visible;
                    this.scheduleWaitIcon.Visibility = ViewStates.Gone;
                    this.Schedule.Text = "Completed";
                }
                else if (this.schedule != null && !this.schedule.isCompleted)
                {
                    this.surveyButton.Visibility = ViewStates.Gone;
                    this.medicationButton.Visibility = ViewStates.Visible;
                    this.preprocedureButton.Visibility = ViewStates.Visible;
                    this.scheduleDoneIcon.Visibility = ViewStates.Gone;
                    this.scheduleWaitIcon.Visibility = ViewStates.Visible;
                    this.Schedule.Text = this.schedule.time.ToString("MMM d, yyyy h:mm tt");
                }
                else
                {
                    Console.Write("Theres no schedule bound to this procedure!");
                }

            }
        }
    }
}