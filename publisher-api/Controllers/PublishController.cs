using Microsoft.AspNetCore.Mvc;
using publisher_api.Services;

namespace publisher_api.Controllers;

[ApiController]
[Route("[controller]")]
public class PublishController : ControllerBase
{
    private readonly ILogger<PublishController> _logger;
    private readonly ITaskPublisher _taskBus;

    public PublishController(
        ILogger<PublishController> logger,
        ITaskPublisher bus)
    {
        _logger = logger;
        _taskBus = bus;
    }

    [HttpGet("{value:int}")]
    public ActionResult Publish(int value)
    {
        _taskBus.PublishMessage(value.ToString());
        return Ok();
    }
}
