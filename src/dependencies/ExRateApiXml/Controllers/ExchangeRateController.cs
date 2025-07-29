using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using XmlApiDemo.Models;

namespace XmlApiDemo.Controllers;

[ApiController]
[Route("exchange")]
[Produces("application/xml")]
public class ExchangeController : ControllerBase
{
    [HttpPost]
    [Consumes("application/xml")]
    public ActionResult<ExchangeRateResponse> Post([FromBody] ExchangeRateRequest request)
    {
        string dataFilePath = "./data/rates.json";
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        if (!System.IO.File.Exists(dataFilePath))
        {
            return NotFound(new ExchangeRateResponse
            {
                Result = 0
            });
        }

        string jsonData = System.IO.File.ReadAllText(dataFilePath);
        ExchangeRate? rateData = JsonSerializer.Deserialize<ExchangeRate>(jsonData, options);

        decimal conversion = request.Amount * (1 / rateData.rates[request.FromCurrency]) * rateData.rates[request.ToCurrency];

        return Ok(new ExchangeRateResponse
        {
            Result = Math.Round(conversion, 2)
        });
    }
}
