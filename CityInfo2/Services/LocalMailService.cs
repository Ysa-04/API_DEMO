namespace CityInfo2.Services
{
    public class LocalMailService(IConfiguration configuration) : IMailService
    {
        // zie appsettings.json voor email config.
        private string _MailTo = configuration["mailSettings:mailToAddress"];
        private string _MailForm = configuration["mailSettings:mailFromAddress"];

        public void SendMail(string subject, string message)
        {
            //send mail - output to console for now
            Console.WriteLine($"Mail from {_MailForm} to {_MailTo}, with {nameof(LocalMailService)}: {subject}, and {nameof(message)}: {message}");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Message: {message}");
        }
    }
}
