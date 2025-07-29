using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/exchange", (ExchangeRateRequest req) =>
{
    string dataFilePath = "./data/rates.json";
    var options = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true
    };

    if (!File.Exists(dataFilePath))
    {
        return Results.NotFound(dataFilePath);
    }

    string jsonData = File.ReadAllText(dataFilePath);
    ExchangeRate? rateData = JsonSerializer.Deserialize<ExchangeRate>(jsonData, options);

    decimal conversion = req.value * (1 / rateData.rates[req.from]) * rateData.rates[req.to];

    return Results.Ok(new ExchangeRateResponse(Math.Round(conversion, 2)));
})
.WithName("PostExchange")
.WithOpenApi();

app.Run();

record ExchangeRateRequest(string from, string to, decimal value);
record ExchangeRateResponse(decimal rate);

record ExchangeRate(
    string provider,
    string WARNING_UPGRADE_TO_V6,
    string terms,
    string @base,
    string date,
    long time_last_updated,
    Dictionary<string, decimal> rates
);
