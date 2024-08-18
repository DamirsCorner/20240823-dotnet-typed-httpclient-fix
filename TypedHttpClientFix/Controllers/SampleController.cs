using Microsoft.AspNetCore.Mvc;
using TypedHttpClientFix.Services;

namespace TypedHttpClientFix.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SampleController(SampleService sampleService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await sampleService.GetData());
    }
}
