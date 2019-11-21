using Google.Apis.Auth.OAuth2;
using System;
using System.IO;
using System.Threading.Tasks;

namespace GCPConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var jsonFilePath = AppDomain.CurrentDomain.BaseDirectory + "mydemoapp-eyfkym-a9cc9278d409.json";
            var scopes = new string[]
            {
              "https://www.googleapis.com/auth/cloud-platform",
              "https://www.googleapis.com/auth/dialogflow"
            };

            var token = GetAccessTokenFromJSONKey(jsonFilePath, scopes);
            Console.WriteLine($"{token}");
            Console.ReadLine();
        }

        private static string GetAccessTokenFromJSONKey(string jsonKeyFilePath, params string[] scopes)
        {
            return GetAccessTokenFromJSONKeyAsync(jsonKeyFilePath, scopes).Result;
        }
        private static async Task<string> GetAccessTokenFromJSONKeyAsync(string jsonKeyFilePath, params string[] scopes)
        {
            using (var stream = new FileStream(jsonKeyFilePath, FileMode.Open, FileAccess.Read))
            {
                return await GoogleCredential
                    .FromStream(stream) // Loads key file  
                    .CreateScoped(scopes) // Gathers scopes requested  
                    .UnderlyingCredential // Gets the credentials  
                    .GetAccessTokenForRequestAsync(); // Gets the Access Token  
            }
        }
    }
}
