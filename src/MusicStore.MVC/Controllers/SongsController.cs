using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MusicStore.MVC.Abstraction.Pagination;
using MusicStore.MVC.Repository.Data;
using System;
using System.Threading.Tasks;

namespace MusicStore.MVC.Controllers
{
  public class SongsController : Controller
  {
    private readonly IUnitOfWork unitOfWork;
    private readonly ILogger logger;

    public SongsController(IUnitOfWork unitOfWork, ILogger<SongsController> logger)
    {
      this.unitOfWork = unitOfWork;
      this.logger = logger;
    }
    public async Task<IActionResult> Index(int page = 1)
    {
      try
      {
        var songsPage = await unitOfWork.Songs.GetSongPage(new PaggingQuery { Page = page });
        
        return View(songsPage);
      }
      catch (Exception ex)
      {
        logger.LogError(ex, "An error occurred while getting songs.");
      }
      // ToDo: Implement error getting data
      return View("ErrorGettingData");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id, int page)
    {
      try
      {
        await unitOfWork.Songs.DeleteAsync(id);

        if (!await this.unitOfWork.SaveAsync())
        {
          // ToDo: Implement error page
          return View("ErrorSaving");
        }

        return RedirectToAction(nameof(Index), new { page });
      }
      catch (Exception ex)
      {
        logger.LogError(ex, "An error occurred while deleting song.");
      }
      // ToDo: Implement error page
      return View("ErrorSaving");
    }
  }
}
