using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MusicStore.MVC.Authorization;
using MusicStore.MVC.Dto;
using MusicStore.MVC.Models;
using MusicStore.MVC.Repository.Data;
using MusicStore.MVC.ViewModels;

namespace MusicStore.MVC.Controllers
{
  [Authorize]
  public class AlbumsController : Controller
  {
    private readonly IUnitOfWork unitOfWork;
    private readonly ILogger logger;
    private readonly IAuthorizationService authorizationService;
    private readonly UserManager<User> userManager;
    private readonly IMapper mapper;

    public AlbumsController(IUnitOfWork unitOfWork,
      ILogger<AlbumsController> logger,
      IAuthorizationService authorizationService,
      UserManager<User> userManager,
      IMapper mapper)
    {
      this.unitOfWork = unitOfWork;
      this.logger = logger;
      this.authorizationService = authorizationService;
      this.userManager = userManager;
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
        var album = await unitOfWork.Albums.GetAsync(id);

        // temporary solutions tracking
        // https://github.com/aspnet/AspNetCore.Docs/issues/10393
        var isAuthorized = await authorizationService
          .AuthorizeAsync(User, album, AutherazationOperations.OwenResourse);
        if (!isAuthorized.Succeeded)
        {
          return RedirectToAction("AccessDenied", "Users");
        }

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
        var albums = (await unitOfWork.Albums.GetAllAsync()).ToList();
        for (int i = albums.Count - 1; i >= 0; i--)
        {
          // temporary solutions tracking
          // https://github.com/aspnet/AspNetCore.Docs/issues/10393
          var isAuthorized = await authorizationService
          .AuthorizeAsync(User, albums[i].OwenerId, AutherazationOperations.OwenResourse);
          if (!isAuthorized.Succeeded ||
            !string.IsNullOrWhiteSpace(search) &&
            !albums[i].Name.Contains(search, StringComparison.OrdinalIgnoreCase))
          {
            albums.RemoveAt(i);
          }
        }
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

        var currentUserId = userManager.GetUserId(User);
        dto.OwenerId = currentUserId;

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

        // temporary solutions tracking
        // https://github.com/aspnet/AspNetCore.Docs/issues/10393
        var isAuthorized = await authorizationService
          .AuthorizeAsync(User, album, AutherazationOperations.OwenResourse);
        if (!isAuthorized.Succeeded)
        {
          return RedirectToAction("AccessDenied", "Users");
        }

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