using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MusicStore.MVC.Dto;
using MusicStore.MVC.Repository.Data;
using MusicStore.MVC.ViewModels;
using System;
using System.Threading.Tasks;

namespace MusicStore.MVC.Controllers
{
  [Authorize]
  public class GenresController : Controller
  {
    private readonly IUnitOfWork unitOfWork;
    private readonly ILogger logger;
    private readonly IMapper mapper;

    public GenresController(IUnitOfWork unitOfWork,
      ILogger<AlbumsController> logger,
      IMapper mapper)
    {
      this.unitOfWork = unitOfWork;
      this.logger = logger;
      this.mapper = mapper;
    }
    [Authorize]
    public async Task<IActionResult> Index()
    {
      try
      {
        var genres = await unitOfWork.Genres.GetAllAsync();
        return View(genres);
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
        await unitOfWork.Genres.DeleteAsync(id);

        if (!await this.unitOfWork.SaveAsync())
        {
          // ToDo: Implement error page
          return View("ErrorSaving");
        }

        return RedirectToAction(nameof(Index));
      }
      catch (Exception ex)
      {
        logger.LogError(ex, "An error occurred while deleting genre.");
      }
      // ToDo: Implement error page
      return View("ErrorSaving");
    }

    public IActionResult AddNewGenre()
    {
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddNewGenre(GenreForCreatingDto dto)
    {
      try
      {
        if (!ModelState.IsValid)
        {
          return View(dto);
        }

        await unitOfWork.Genres.AddAsync(dto);

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

    public async Task<IActionResult> Edit(int id)
    {
      try
      {
        var genre = await unitOfWork.Genres.GetAsync(id);
        var dto = mapper.Map<GenreForUpdatingDto>(genre);
        var vm = new EditGenreViewModel
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
    public async Task<IActionResult> Edit(int id, EditGenreViewModel vm)
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

        await unitOfWork.Genres.UpdateAsync(vm.Dto);
        await unitOfWork.SaveAsync();

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
