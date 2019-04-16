using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MusicStore.MVC.Repository.Data;

namespace MusicStore.MVC.Controllers
{
  public class AlbumsController : Controller
  {
    private readonly IUnitOfWork unitOfWork;
    private readonly ILogger logger;

    public AlbumsController(IUnitOfWork unitOfWork, ILogger<AlbumsController> logger)
    {
      this.unitOfWork = unitOfWork;
      this.logger = logger;
    }
    public async Task<IActionResult> Index()
    {
      try
      {
        var albums = await unitOfWork.Albums.GetAllAsync();
        return View(albums);
      }
      catch (Exception ex)
      {
        logger.LogError(ex, "An error occurred while getting albums.");
      }
      // ToDo: Implement error page
      return View("Error");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
      try
      {
        await unitOfWork.Albums.DeleteAsync(id);

        if (!await this.unitOfWork.SaveAsync())
        {
          // ToDo: Implement error page
          return View("ErrorSaving");
        }

        return RedirectToAction(nameof(Index));
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