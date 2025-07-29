using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using ExchangeRate.Models;
using ExchangeRate.Interfaces;

namespace ExchangeRate.Controllers;

[ApiController]
[Route("api/exchange")]
[Produces("application/json")]
public class ExchangeController : ControllerBase
{
    private readonly IExchangeRateService _exchangeRateService;

    public ExchangeController(IExchangeRateService exchangeRateService)
    {
        _exchangeRateService = exchangeRateService;
    }

    [HttpPost]
    [Consumes("application/json")]
    public async Task<ActionResult<ExchangeRateResponse>> Post([FromBody] ExchangeRateRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var response = await _exchangeRateService.GetBestDealAsync(request);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }
}
