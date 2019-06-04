using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MusicStore.MVC.Dto;
using MusicStore.MVC.Repository.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStore.MVC.API
{
  [Route("api/Genre")]
  [ApiController]
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
  public class GenreApiController : ControllerBase
  {
    private readonly IUnitOfWork unitOfWork;
    private readonly ILogger logger;

    public GenreApiController(IUnitOfWork unitOfWork,
      ILogger<SongApiController> logger)
    {
      this.unitOfWork = unitOfWork;
      this.logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll()
    {
      try
      {
        var genres = (await unitOfWork.Genres.GetAllAsync()).ToList();
        return Ok(genres);
      }
      catch (Exception ex)
      {
        logger.LogError(ex, "An error occurred while getting genres.");
        return StatusCode(500);
      }
    }

    [HttpPost]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> CreateGenre([FromBody]GenreForCreatingDto dto)
    {
      if (!ModelState.IsValid)
        return BadRequest();

      try
      {
        await unitOfWork.Genres.AddAsync(dto);
        if (!await unitOfWork.SaveAsync())
          throw new Exception("Creating Genre failed on save.");

        return NoContent();
      }
      catch (Exception ex)
      {
        logger.LogError(ex, "An error occurred while creating genre.", dto);
        return StatusCode(500);
      }
    }

    [HttpPut("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> UpdateeSong(
      int id,
      [FromBody]GenreForUpdatingDto dto)
    {
      if (!ModelState.IsValid)
        return BadRequest();

      try
      {
        var genre = await unitOfWork.Genres.GetAsync(id);
        if (genre == null)
          return NotFound();

        if (genre.Id != id)
          return BadRequest();

        await unitOfWork.Genres.UpdateAsync(dto);
        await unitOfWork.SaveAsync();

        return NoContent();
      }
      catch (Exception ex)
      {
        logger.LogError(ex, "An error occurred while updating genre.", dto);
        return StatusCode(500);
      }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> DeleteGenre(int id)
    {
      try
      {
        var genre = await unitOfWork.Genres.GetAsync(id);
        if (genre == null)
          return NotFound();

        await unitOfWork.Genres.DeleteAsync(id);
        if (!await unitOfWork.SaveAsync())
          throw new Exception("Deleting genre failed on save.");

        return NoContent();
      }
      catch (Exception ex)
      {
        logger.LogError(ex, $"An error occurred while deleting genre with id of {id}.");
        return StatusCode(500);
      }
    }
  }
}
