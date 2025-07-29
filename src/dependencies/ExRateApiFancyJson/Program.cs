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
        return Results.NotFound(new ExchangeRateResponse(404, "Not Found", new Data(null)));
    }

    string jsonData = File.ReadAllText(dataFilePath);
    ExchangeRate? rateData = JsonSerializer.Deserialize<ExchangeRate>(jsonData, options);

    decimal conversion = req.quantity * (1 / rateData.rates[req.sourceCurrency]) * rateData.rates[req.targetCurrency];

    return Results.Ok(new ExchangeRateResponse(200, "Ok", new Data(Math.Round(conversion, 2))));
})
.WithName("PostExchange")
.WithOpenApi();

app.Run();

record ExchangeRateRequest(string sourceCurrency, string targetCurrency, decimal quantity);
record ExchangeRateResponse(int statusCode, string message, Data data);

record ExchangeRate(
    string provider,
    string WARNING_UPGRADE_TO_V6,
    string terms,
    string @base,
    string date,
    long time_last_updated,
    Dictionary<string, decimal> rates
);

record Data(decimal? total);
