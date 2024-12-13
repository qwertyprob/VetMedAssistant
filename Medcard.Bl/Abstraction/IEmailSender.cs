namespace Medcard.Bl.Abstraction
{
    public interface IEmailSender
    {
         Task SendEmailAsync();
    }
}