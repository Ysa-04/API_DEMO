namespace CityInfo2.Services
{
    public class CloudMailService(IConfiguration configuration) : IMailService
    {
       //moet lezen uit developmentfile 
        private string _MailTo = configuration["mailSettings:mailToAddress"]; 
        private string _MailForm = configuration["mailSettings:mailFromAddress"];

        public void SendMail(string subject, string message)
        {
            //send mail - output to console for now
            Console.WriteLine($"Mail from {_MailForm} to {_MailTo}, with {nameof(CloudMailService)}: {subject}, and {nameof(message)}: {message}");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Message: {message}");
        }
    }
}
