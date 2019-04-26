using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MusicStore.MVC.Dto;
using MusicStore.MVC.Repository.Data;
using MusicStore.MVC.ViewModels;

namespace MusicStore.MVC.Controllers
{
  public class AlbumsController : Controller
  {
    private readonly IUnitOfWork unitOfWork;
    private readonly ILogger logger;
    private readonly IMapper mapper;

    public AlbumsController(IUnitOfWork unitOfWork,
      ILogger<AlbumsController> logger,
      IMapper mapper)
    {
      this.unitOfWork = unitOfWork;
      this.logger = logger;
      this.mapper = mapper;
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

    public async Task<IActionResult> AlbumSuggestions(string search)
    {
      // ToDo: Create a sql quary to filter album based album name
      // This code is just to get thing done
      // as the main focuse here is Identity
      try
      {
        var albums = await unitOfWork.Albums.GetAllAsync();
        var suggestions = albums.Select(s => new { s.Id, s.Name }).Take(10);
        if (!string.IsNullOrWhiteSpace(search))
        {
          suggestions = suggestions
            .Where(s => s.Name.Contains(search, StringComparison.OrdinalIgnoreCase));
          return Json(suggestions);
        }
        return Json(suggestions);
      }
      catch (Exception ex)
      {
        logger.LogError(ex, "An error occurred while getting albums.");
      }
      return Json(null);
    }

    public IActionResult AddNewAlbum()
    {
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddNewAlbum(AlbumForCreatingDto dto)
    {
      try
      {
        if (!ModelState.IsValid)
        {
          return View(dto);
        }

        await unitOfWork.Albums.AddAsync(dto);

        if (!await unitOfWork.SaveAsync())
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

    public async Task<IActionResult> Details(int id)
    {
      try
      {
        var album = await unitOfWork.Albums.GetAsync(id);
        return View(album);
      }
      catch (Exception ex)
      {
        logger.LogError(ex, "An error occurred while getting albums.");
      }
      // ToDo: Implement error page
      return View("Error");
    }

    public async Task<IActionResult> Edit(int id)
    {
      try
      {
        var album = await unitOfWork.Albums.GetAsync(id);
        var dto = mapper.Map<AlbumForUpdatingDto>(album);
        var vm = new EditAlbumViewModel
        {
          Dto = dto
        };
        return View(vm);
      }
      catch (Exception ex)
      {
        logger.LogError(ex, "An error occurred while getting albums.");
      }
      // ToDo: Implement error page
      return View("Error");
    }

    [HttpPost]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Edit(int id, EditAlbumViewModel vm)
    {
      try
      {
        if (!ModelState.IsValid)
          return View(vm);

        if (id != vm.Dto.Id)
        {
          vm.Message = "Opps update failed please try again";
          return View(vm);
        }

        await unitOfWork.Albums.UpdateAsync(vm.Dto);
        await unitOfWork.SaveAsync();
        
        return RedirectToAction(nameof(Details), new { id });
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