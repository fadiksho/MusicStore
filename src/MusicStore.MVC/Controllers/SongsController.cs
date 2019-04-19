using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MusicStore.MVC.Abstraction.Pagination;
using MusicStore.MVC.Dto;
using MusicStore.MVC.Repository.Data;
using MusicStore.MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStore.MVC.Controllers
{
  public class SongsController : Controller
  {
    private readonly IUnitOfWork unitOfWork;
    private readonly ILogger logger;
    private readonly IMapper mapper;

    public SongsController(IUnitOfWork unitOfWork,
      ILogger<SongsController> logger,
      IMapper mapper)
    {
      this.unitOfWork = unitOfWork;
      this.logger = logger;
      this.mapper = mapper;
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

    public async Task<IActionResult> AddNewSong()
    {
      try
      {
        var albums = await unitOfWork.Albums.GetAllAsync();
        var genres = await unitOfWork.Genres.GetAllAsync();
        var vm = new CreateSongViewModel
        {
          Genres = genres.Select(
            g => new CheckBoxItem
            {
              Id = g.Id,
              Name = g.Name
            }).ToList()
        };
        return View(vm);
      }
      catch (Exception ex)
      {
        logger.LogError(ex, "An error occurred while getting data.");
      }
      // ToDo: Implement error getting data
      return View("ErrorGettingData");
    }

    [HttpPost]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> AddNewSong(CreateSongViewModel vm)
    {
      try
      {
        if (!ModelState.IsValid)
        {
          return View(vm);
        }

        var selectedGenres = vm.Genres.Where(s => s.IsSelected);
        vm.Dto.GenresIds = await GetValidGenresIdsAsync(selectedGenres);

        await unitOfWork.Songs.AddAsync(vm.Dto);

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

    public async Task<IActionResult> Edit(int id)
    {
      try
      {
        var song = await unitOfWork.Songs.GetAsync(id);
        var genres = await unitOfWork.Genres.GetAllAsync();
        var songDto = mapper.Map<SongForUpdatingDto>(song);
        var genresDto = mapper.Map<List<CheckBoxItem>>(genres);
        // Select the genres that the song have
        foreach (var songGenre in song.Genres)
        {
          var selectedSong = genresDto.First(g => g.Id == songGenre.Id);
          selectedSong.IsSelected = true;
        }

        var vm = new EditSongViewModel
        {
          AlbumName = song.Album != null ? song.Album.Name : "",
          Dto = songDto,
          Genres = genresDto
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
    public async Task<IActionResult> Edit(int id, EditSongViewModel vm)
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
        // check if the selected album is exist
        if (vm.Dto.AlbumId != null && !await unitOfWork.Albums.Exist(vm.Dto.AlbumId))
        {
          vm.Message = $"The selected Album { vm.AlbumName } is no longer exist";
          return View(vm);
        }

        var selectedGenres = vm.Genres.Where(s => s.IsSelected);
        vm.Dto.GenresIds = await GetValidGenresIdsAsync(selectedGenres);

        await unitOfWork.Songs.UpdateAsync(vm.Dto);

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
      return View("Error");
    }

    /// <summary>
    /// Remove invalid Genre Ids
    /// </summary>
    /// <param name="selctedGenre"></param>
    /// <returns>Valid genres ids</returns>
    private async Task<IEnumerable<int>> GetValidGenresIdsAsync(IEnumerable<CheckBoxItem> selctedGenre)
    {
      var genres = await unitOfWork.Genres.GetAllAsync();
      
      return selctedGenre.Select(g => g.Id).Intersect(genres.Select(s => s.Id));
    }
  }
}
