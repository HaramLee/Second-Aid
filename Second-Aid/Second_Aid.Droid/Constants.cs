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
    public class Constants
    {
        public const string TOKEN_KEY = "token_key";
        public const string MEDICATION_KEY = "medication_key";
        public const string PROCEDURE_KEY = "procedure_key";
        public const string PROCEDUREID_KEY = "procedureId_key";
        public const string PREPROCEDURE_KEY = "preprocedure_key";
        public const string PREPROCEDUREID_KEY = "preprocedureId_key";
        public const string PROCEDUREDESC_KEY = "proceduredesc_key";
        public const string PREPROCEDUREDESC_KEY = "preproceduredesc_key";
        
        public const string BASE_URL = "http://secondaid.azurewebsites.net";
        public const string LOGIN_URL = "/connect/token";
        public const string LOGOUT_URL = "/connect/logout";
        public const string MEDICATION_URL = "/api/medications";
        public const string PREINSTRUCTIONS_URL = "/api/PreInstructions";
        public const string SUBPROCEDURES_URL = "/api/SubProcedures";
        public const string GETPROCEDUREID_URL = "/api/PatientProcedures";
        public const string PROCEDUREID_URL = "/api/Procedures";
        public const string CLINIC_URL = "/api/Clinics";
        public const string SCHEDULE_URL = "/api/Schedules";

    }
}
