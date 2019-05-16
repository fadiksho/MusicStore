using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MusicStore.MVC.Dto;
using MusicStore.MVC.Models;
using MusicStore.MVC.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStore.MVC.API
{
  [Route("api/Genre")]
  [ApiController]
  public class GenreApiController : ControllerBase
  {
    private readonly IUnitOfWork unitOfWork;
    private readonly ILogger logger;
    private readonly IAuthorizationService authorizationService;
    private readonly UserManager<User> userManager;
    private readonly IMapper mapper;

    public GenreApiController(IUnitOfWork unitOfWork,
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
    public async Task<ActionResult<List<Genre>>> GetAll()
    {
      try
      {
        var genres = (await unitOfWork.Genres.GetAllAsync()).ToList();
        return genres;
      }
      catch (Exception ex)
      {
        logger.LogError(ex, "An error occurred while getting genres.");
        return StatusCode(500);
      }
    }

    [HttpPost]
    public async Task<ActionResult> CreateGenre([FromBody]GenreForCreatingDto dto)
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
    public async Task<ActionResult> UpdateeSong(
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
    public async Task<ActionResult> DeleteGenre(int id)
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
