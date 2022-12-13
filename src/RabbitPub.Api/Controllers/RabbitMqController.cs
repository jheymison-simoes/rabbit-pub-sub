using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RabbitPub.Business.Exceptions;
using RabbitPub.Business.Interfaces.Services;
using RabbitPub.Business.Models;

namespace RabbitPub.Api.Controllers;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
public class RabbitMqController : BaseController<RabbitMqController>
{
    private readonly IPublishService _publishService;

    public RabbitMqController(
        ILogger<RabbitMqController> logger,
        IMapper mapper,
        IPublishService publishService) : base(logger, mapper)
    {
        _publishService = publishService;
    }

    [HttpPost("Publish")]
    public async Task<ActionResult<BaseResponse<string>>> Publish([FromBody] string message)
    {
        try
        {
            var result = await _publishService.PublishMessage(message);
            return BaseResponseSuccess(result);
        }
        catch (CustomException cEx)
        {
            return BaseResponseError(cEx.Message);
        }
        catch (Exception ex)
        {
            return BaseResponseInternalError(ex.Message);
        }
    }
}