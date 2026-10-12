namespace CityInfo2.Services
{
    public class CloudMailService: IMailService
    {
       
        private string _MailTo = "admin@mycompany.com";
        private string _MailForm = "noreply@mycompany.com";

        public void SendMail(string subject, string message)
        {
            //send mail - output to console for now
            Console.WriteLine($"Mail from {_MailForm} to {_MailTo}, with {nameof(CloudMailService)}: {subject}, and {nameof(message)}: {message}");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Message: {message}");
        }
    }
}
