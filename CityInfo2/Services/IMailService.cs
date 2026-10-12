namespace CityInfo2.Services
{
    public interface IMailService
    {
        void SendMail(string subject, string message);
    }
}
