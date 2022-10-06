namespace SendingEmails.Services
{
    public interface IMailServices
    {
        Task SendingEmailAsync(string mailto, string sub, string body, IList<IFormFile>? attch=null);
        
    }
}
