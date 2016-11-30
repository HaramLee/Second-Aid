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
        public const string BASE_URL = "http://testaid.azurewebsites.net";
        public const string LOGIN_URL = "/connect/token";
        public const string LOGOUT_URL = "/connect/logout";
        public const string MEDICATION_URL = "/api/medications";
        public const string PREINSTRUCTIONS_URL = "/api/PreInstructions";
        public const string SUBPROCEDURES_URL = "/api/SubProcedures";
    }
}