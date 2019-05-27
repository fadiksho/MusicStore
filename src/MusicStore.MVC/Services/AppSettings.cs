namespace MusicStore.MVC.Services
{
  public class AppSettings
  {
    public ConnectionStrings ConnectionStrings { get; set; }
    public EmailSenderOptions EmailSenderOptions { get; set; }

    public TokenSettings Token { get; set; }
  }

  public class ConnectionStrings
  {
    public string DefaultConnection { get; set; }
  }

  public class EmailSenderOptions
  {
    public string SenderName { get; set; }
    public string SenderEmail { get; set; }
    public string SendGridUser { get; set; }
    public string SendGridKey { get; set; }
  }

  public class TokenSettings
  {
    public string Key { get; set; }
    public string Issuer { get; set; }
    public string[] Audience { get; set; }
    public string AdminPassword { get; set; }
  }
}
