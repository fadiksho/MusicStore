using Microsoft.AspNetCore.Authentication.JwtBearer;
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
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
  public class SongApiController : ControllerBase
  {
    private readonly IUnitOfWork unitOfWork;
    private readonly ILogger logger;
    private readonly IAuthorizationService authorizationService;
    private readonly UserManager<User> userManager;

    public SongApiController(IUnitOfWork unitOfWork,
      ILogger<SongApiController> logger,
      IAuthorizationService authorizationService,
      UserManager<User> userManager)
    {
      this.unitOfWork = unitOfWork;
      this.logger = logger;
      this.authorizationService = authorizationService;
      this.userManager = userManager;
    }

    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    [AllowAnonymous]
    public async Task<ActionResult<IActionResult>> GetSongsPage([FromQuery]PaggingQuery query)
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
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetSong(int id)
    {
      try
      {
        var song = await unitOfWork.Songs.GetAsync(id);
        if (song == null)
          return NotFound();

        return Ok(song);
      }
      catch (Exception ex)
      {
        logger.LogError(ex, "An error occurred while getting songs.");
        return StatusCode(500);
      }
    }

    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> CreateSong([FromBody]SongForCreatingDto dto)
    {
      if (!ModelState.IsValid)
        return BadRequest();

      try
      {
        // Validate the album if exist
        if (dto.AlbumId != null && !await unitOfWork.Albums.Exist(dto.AlbumId))
          dto.AlbumId = null;

        // Set the owner of this song to the current signedIn user
        var currentUserId = userManager.GetUserId(User);
        dto.OwenerId = currentUserId;

        var songEntity = await unitOfWork.Songs.AddAsync(dto);
        if (!await unitOfWork.SaveAsync())
          throw new Exception("Creating song failed on save.");

        var song = await unitOfWork.Songs.GetAsync(songEntity.Id);
        return StatusCode(201, song);
      }
      catch (Exception ex)
      {
        logger.LogError(ex, "An error occurred while creating song.", dto);
        return StatusCode(500);
      }
    }

    [HttpPut("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> UpdateeSong(
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

        var isAuthorized = await authorizationService
          .AuthorizeAsync(User, song.OwenerId, AutherazationOperations.OwenResourse);
        if (!isAuthorized.Succeeded)
          return Unauthorized();

        await unitOfWork.Songs.UpdateAsync(dto);
        await unitOfWork.SaveAsync();

        song = await unitOfWork.Songs.GetAsync(id);
        return Ok(song);
      }
      catch (Exception ex)
      {
        logger.LogError(ex, "An error occurred while updating song.", dto);
        return StatusCode(500);
      }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(401)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> DeleteSong(int id)
    {
      try
      {
        var song = await unitOfWork.Songs.GetAsync(id);
        if (song == null)
          return NotFound();

        var isAuthorized = await authorizationService
          .AuthorizeAsync(User, song.OwenerId, AutherazationOperations.OwenResourse);
        if (!isAuthorized.Succeeded)
          return Unauthorized();

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