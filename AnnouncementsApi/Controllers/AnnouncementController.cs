using Microsoft.AspNetCore.Mvc;

namespace AnnouncementsApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AnnouncementController : ControllerBase
{

    private readonly ILogger<AnnouncementController> _logger;

    public AnnouncementController(ILogger<AnnouncementController> logger)
    {
        _logger = logger;
    }

}

