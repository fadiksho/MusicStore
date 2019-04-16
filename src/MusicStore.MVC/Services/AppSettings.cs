using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStore.MVC.Services
{
  public class AppSettings
  {
    public ConnectionStrings ConnectionStrings { get; set; }
  }

  public class ConnectionStrings
  {
    public string DefaultConnection { get; set; }
  }
}
