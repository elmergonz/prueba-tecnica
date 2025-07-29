using ExchangeRate.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IExchangeRateService, ExchangeRateService>();

builder.Services.AddScoped<IExchangeRateProvider, ExRateApiJsonProvider>();
builder.Services.AddHttpClient<ExRateApiJsonProvider>();

builder.Services.AddScoped<IExchangeRateProvider, ExRateApiFancyJsonProvider>();
builder.Services.AddHttpClient<ExRateApiFancyJsonProvider>();

builder.Services.AddScoped<IExchangeRateProvider, ExRateApiXmlProvider>();
builder.Services.AddHttpClient<ExRateApiXmlProvider>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
