using Firebase.Auth.Providers;
using Firebase.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Google.Cloud.Firestore;
using Google.Type;
using static Google.Cloud.Firestore.V1.StructuredAggregationQuery.Types.Aggregation.Types;
using System.Xml.Linq;


namespace TaskAPP.FireStore
{
    public class FireStore
    {
        public static class FirebaseAuthHelper
        {
            public const string firebaseAppId = "taskproject-c95cd";
            public const string firebaseApiKey = "AIzaSyDE8CAGpxXaeBptefwsI9ehx1wKH10ZAZo";

            public static FirebaseAuthClient setFirebaseAuthClient()
            {
                var response = new FirebaseAuthClient(new FirebaseAuthConfig
                {
                    ApiKey = firebaseApiKey,
                    AuthDomain = $"{firebaseAppId}.firebaseapp.com",
                    Providers = new FirebaseAuthProvider[]
                        {
                        new EmailProvider()
                        }
                });

                return response;
            }
        }
    }
}
