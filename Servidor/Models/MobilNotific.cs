using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;

namespace Servidor.Models
{
    public class MobilNotific
    {
        public readonly FirebaseMessaging messaging;
        private static bool isInicialized = false;


        public MobilNotific()
        {
            var credentials = GoogleCredential.FromFile("Key.json");
            if(!isInicialized)
            {
                isInicialized = true;
                FirebaseApp.Create(new AppOptions()
                {
                    Credential = credentials,
                });
                messaging = FirebaseMessaging.DefaultInstance;
            }
            else
            {
                messaging = FirebaseMessaging.DefaultInstance;
            }
        }


        public async Task SendNotific(string titol, string body, string token)
        {
            var notification = new Notification
            {
                Title = titol,
                Body = body
            };
            var message = new Message
            {
                Notification = notification,
                Token = token
            };
            var response = await messaging.SendAsync(message);
        }
    }
}
