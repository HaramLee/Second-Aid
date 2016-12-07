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

namespace Second_Aid.Droid
{
    [Activity(Label = "QuestionsActivity")]
    public class QuestionsActivity : Activity
    {
        private string token;
        private string surveyName;
        private List<string> questions;
        private Button submitButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Questions);

            this.token = Intent.GetStringExtra(Constants.TOKEN_KEY) ?? "No token detected.";
            this.surveyName = Intent.GetStringExtra(Constants.QUESTION_KEY) ?? "No name detected";
            this.questions = new List<string>(Intent.GetStringArrayListExtra(Constants.QUESTIONNAIRE_QUESTIONS_KEY));

            submitButton = FindViewById<Button>(Resource.Id.submitButton);

            submitButton.Click += (object sender, EventArgs e) =>
            {
                Finish();
            };


            var questionsAdapter = new QuestionAdapter(this, this.questions);
            var questionsListView = FindViewById<ListView>(Resource.Id.questionListView);
            questionsListView.Focusable = true;

            questionsListView.Adapter = questionsAdapter;
        }

    }
}