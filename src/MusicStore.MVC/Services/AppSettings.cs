using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStore.MVC.Services
{
  public class AppSettings
  {
    public ConnectionStrings ConnectionStrings { get; set; }
    public EmailSenderOptions EmailSenderOptions { get; set; }
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
}
