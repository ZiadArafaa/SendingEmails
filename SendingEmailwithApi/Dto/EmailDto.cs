namespace SendingEmails.Dto
{
    public class EmailDto
    {
        public string EmailTo { get; set; }
        public string Subject { get; set; }
        public string body { get; set; }
        public IList<IFormFile> files { get; set; }
    }
}
