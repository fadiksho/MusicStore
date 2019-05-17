using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MusicStore.MVC.Abstraction.Pagination;
using MusicStore.MVC.Authorization;
using MusicStore.MVC.Dto;
using MusicStore.MVC.Models;
using MusicStore.MVC.Repository.Data;
using System;
using System.Threading.Tasks;

namespace MusicStore.MVC.API
{
  [Route("api/Song")]
  [ApiController]
  public class SongApiController : ControllerBase
  {
    private readonly IUnitOfWork unitOfWork;
    private readonly ILogger logger;
    private readonly IAuthorizationService authorizationService;
    private readonly UserManager<User> userManager;
    private readonly IMapper mapper;

    public SongApiController(IUnitOfWork unitOfWork,
      ILogger<SongApiController> logger,
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

    [HttpGet]
    public async Task<ActionResult<PaggingResult<Song>>> GetSongsPage([FromQuery]PaggingQuery query)
    {
      try
      {
        var songsPage = await unitOfWork.Songs.GetSongPage(query);

        return Ok(songsPage);
      }
      catch (Exception ex)
      {
        logger.LogError(ex, "An error occurred while getting songs.");
        return StatusCode(500);
      }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Song>> GetSong(int id)
    {
      try
      {
        var song = await unitOfWork.Songs.GetAsync(id);
        if (song == null)
          return NotFound();

        return song;
      }
      catch (Exception ex)
      {
        logger.LogError(ex, "An error occurred while getting songs.");
        return StatusCode(500);
      }
    }

    [HttpPost]
    public async Task<ActionResult<Song>> CreateSong([FromBody]SongForCreatingDto dto)
    {
      if (!ModelState.IsValid)
        return BadRequest();

      try
      {
        // Validate the album if exist
        if (dto.AlbumId != null && !await unitOfWork.Albums.Exist(dto.AlbumId))
          dto.AlbumId = null;

        // Set the owner of this song to the current signedIn user
        //var currentUserId = userManager.GetUserId(User);
        //dto.OwenerId = currentUserId;

        var songEntity = await unitOfWork.Songs.AddAsync(dto);
        if (!await unitOfWork.SaveAsync())
          throw new Exception("Creating song failed on save.");

        var song = mapper.Map<Song>(songEntity);
        return song;
      }
      catch (Exception ex)
      {
        logger.LogError(ex, "An error occurred while creating song.", dto);
        return StatusCode(500);
      }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Song>> UpdateeSong(
      int id,
      [FromBody]SongForUpdatingDto dto)
    {
      if (!ModelState.IsValid)
        return BadRequest();

      try
      {
        var song = await unitOfWork.Songs.GetAsync(id);
        if (song == null)
          return NotFound();

        if (song.Id != id)
          return BadRequest();

        // Validate the album if exist
        if (dto.AlbumId != null && !await unitOfWork.Albums.Exist(dto.AlbumId))
          dto.AlbumId = null;

        //var isAuthorized = await authorizationService
        //  .AuthorizeAsync(User, song.OwenerId, AutherazationOperations.OwenResourse);
        //if (!isAuthorized.Succeeded)
        //  return Unauthorized();

        await unitOfWork.Songs.UpdateAsync(dto);
        await unitOfWork.SaveAsync();

        song = await unitOfWork.Songs.GetAsync(id);
        return song;
      }
      catch (Exception ex)
      {
        logger.LogError(ex, "An error occurred while updating song.", dto);
        return StatusCode(500);
      }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteSong(int id)
    {
      try
      {
        var song = await unitOfWork.Songs.GetAsync(id);
        if (song == null)
          return NotFound();

        //var isAuthorized = await authorizationService
        //  .AuthorizeAsync(User, song.OwenerId, AutherazationOperations.OwenResourse);
        //if (!isAuthorized.Succeeded)
        //  return Unauthorized();

        await unitOfWork.Songs.DeleteAsync(id);
        if (!await unitOfWork.SaveAsync())
          throw new Exception("Deleting song failed on save.");

        return NoContent();
      }
      catch (Exception ex)
      {
        logger.LogError(ex, $"An error occurred while deleting song with id of {id}.");
        return StatusCode(500);
      }
    }
  }
}