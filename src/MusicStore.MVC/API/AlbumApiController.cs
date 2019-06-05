using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MusicStore.MVC.Authorization;
using MusicStore.MVC.Dto;
using MusicStore.MVC.Models;
using MusicStore.MVC.Repository.Data;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MusicStore.MVC.API
{
  [Route("api/Album")]
  [ApiController]
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  public class AlbumApiController : ControllerBase
  {
    private readonly IUnitOfWork unitOfWork;
    private readonly ILogger logger;
    private readonly IAuthorizationService authorizationService;
    private readonly IMapper mapper;

    public AlbumApiController(IUnitOfWork unitOfWork,
      ILogger<SongApiController> logger,
      IAuthorizationService authorizationService,
      IMapper mapper)
    {
      this.unitOfWork = unitOfWork;
      this.logger = logger;
      this.authorizationService = authorizationService;
      this.mapper = mapper;
    }

    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetAll()
    {
      try
      {
        var albums = (await unitOfWork.Albums.GetAllAsync()).ToList();
        return Ok(albums);
      }
      catch (Exception ex)
      {
        logger.LogError(ex, "An error occurred while getting albums.");
        return StatusCode(500);
      }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(401)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetAlbum(int id)
    {
      try
      {
        var album = await unitOfWork.Albums.GetAsync(id);
        if (album == null)
          return NotFound();

        return Ok(album);
      }
      catch (Exception ex)
      {
        logger.LogError(ex, "An error occurred while getting albums.");
        return StatusCode(500);
      }
    }

    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> CreateAlbum([FromBody]AlbumForCreatingDto dto)
    {
      if (!ModelState.IsValid)
        return BadRequest();

      try
      {
        // Set the owner of this song to the current signedIn user
        var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        dto.OwenerId = currentUserId;

        var albumEntity = await unitOfWork.Albums.AddAsync(dto);
        if (!await unitOfWork.SaveAsync())
          throw new Exception("Creating Album failed on save.");

        var album = mapper.Map<Album>(albumEntity);
        return StatusCode(201, album);
      }
      catch (Exception ex)
      {
        logger.LogError(ex, "An error occurred while creating album.", dto);
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
      [FromBody]AlbumForUpdatingDto dto)
    {
      if (!ModelState.IsValid)
        return BadRequest();

      try
      {
        var album = await unitOfWork.Albums.GetAsync(id);
        if (album == null)
          return NotFound();

        if (album.Id != id)
          return BadRequest();

        var isAuthorized = await authorizationService
          .AuthorizeAsync(User, album.OwenerId, AutherazationOperations.OwenResourse);
        if (!isAuthorized.Succeeded)
          return Unauthorized();

        await unitOfWork.Albums.UpdateAsync(dto);
        await unitOfWork.SaveAsync();

        album = await unitOfWork.Albums.GetAsync(id);
        return Ok(album);
      }
      catch (Exception ex)
      {
        logger.LogError(ex, "An error occurred while updating album.", dto);
        return StatusCode(500);
      }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(401)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> DeleteAlbum(int id)
    {
      try
      {
        var album = await unitOfWork.Albums.GetAsync(id);
        if (album == null)
          return NotFound();

        var isAuthorized = await authorizationService
          .AuthorizeAsync(User, album.OwenerId, AutherazationOperations.OwenResourse);
        if (!isAuthorized.Succeeded)
          return Unauthorized();

        await unitOfWork.Albums.DeleteAsync(id);
        if (!await unitOfWork.SaveAsync())
          throw new Exception("Deleting album failed on save.");

        return NoContent();
      }
      catch (Exception ex)
      {
        logger.LogError(ex, $"An error occurred while deleting album with id of {id}.");
        return StatusCode(500);
      }
    }
  }
}
