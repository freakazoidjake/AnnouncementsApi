using AnnouncementsApi.Dtos;
using AnnouncementsApi.Factories;
using AnnouncementsApi.Models;
using System.Text.Json;
using AnnouncementsApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace AnnouncementsApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnnouncementController : ControllerBase
{

    private readonly ILogger<AnnouncementController> _logger;
    private readonly IAnnouncementService _service;

    public AnnouncementController(ILogger<AnnouncementController> logger, IAnnouncementService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet(Name = nameof(GetAnnouncements))]
    public ActionResult<IEnumerable<AnnouncementDto>> GetAnnouncements([FromQuery] QueryParameters queryParameters)
    {
        List<Announcement> items = _service.GetAll(queryParameters).ToList();


        var paginationMetadata = new
        {
            totalCount = items.Count,
            pageSize = queryParameters.PageCount,
            currentPage = queryParameters.Page,
            totalPages = items.Count == 0 ? 0 : queryParameters.Page / items.Count
        };

        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

        return Ok(items);
    }

    [HttpGet]
    [Route("{id:int}", Name = nameof(GetSingleAnnouncement))]
    public ActionResult GetSingleAnnouncement(int id)
    {
        var model = _service.GetSingle(id);

        if (model == null)
        {
            return NotFound();
        }

        var dto = AnnouncementFactory.ToDto(model);

        return Ok(dto);
    }

    [HttpPost(Name = nameof(CreateAnnouncementAsync))]
    public async Task<ActionResult<AnnouncementDto>> CreateAnnouncementAsync([FromBody] AnnouncementDto dto)
    {
        if (dto == null)
        {
            return BadRequest();
        }

        var model = AnnouncementFactory.FromDto(dto);

        if (model == null)
        {
            return NotFound();
        }

        var changes = _service.Create(model);

        await _service.Save();

        if (changes == null)
        {
            throw new Exception("Creating an announcement failed on save.");
        }

        var returnedDto = AnnouncementFactory.ToDto(changes.Entity);

        return CreatedAtRoute(nameof(CreateAnnouncementAsync), returnedDto);
    }

    [HttpPatch("{id:int}", Name = nameof(UpdateAnnouncementAsync))]
    public async Task<ActionResult<AnnouncementDto>> UpdateAnnouncementAsync(int id, [FromBody] AnnouncementDto patchDoc)
    {
        if (patchDoc == null)
        {
            return BadRequest();
        }

        var existing = _service.GetSingle(id);

        if (existing == null)
        {
            return NotFound();
        }

        // TODO: Find better way to do this checking.
        if (patchDoc.Author != null)
        {
            existing.Author = patchDoc.Author;
        }

        if (patchDoc.Body != null)
        {
            existing.Body = patchDoc.Body;
        }

        if (patchDoc.EndDate != null)
        {
            existing.EndDate = patchDoc.EndDate;
        }

        if (patchDoc.StartDate != null)
        {
            existing.StartDate = (DateTime)patchDoc.StartDate;
        }

        if (patchDoc.Subject != null)
        {
            existing.Subject = patchDoc.Subject;
        }

        TryValidateModel(existing);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        await _service.Save();
        var updated = _service.GetSingle(existing.Id) ?? throw new Exception("Updating an announcement failed on save.");
        var dto = AnnouncementFactory.ToDto(updated);

        return Ok(dto);
    }

    [HttpDelete]
    [Route("{id:int}", Name = nameof(RemoveAnnouncementAsync))]
    public async Task<ActionResult> RemoveAnnouncementAsync(int id)
    {
        var model = _service.GetSingle(id);

        if (model == null)
        {
            return NotFound();
        }

        var didDelete = _service.Delete(id);
        await _service.Save();

        if (!didDelete)
        {
            throw new Exception("Deleting an announcement failed on save.");
        }

        return NoContent();
    }

}

