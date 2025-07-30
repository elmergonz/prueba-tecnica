using System.Text;
using System.Text.Json;
using ExchangeRate.Interfaces;
using ExchangeRate.Models;

public class ExRateApiJsonProvider : IExchangeRateProvider
{
    private readonly string _providerName = "ExRateApiJson";
    private readonly HttpClient _httpClient;

    public ExRateApiJsonProvider(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ExchangeRateResponse?> GetExchangeRateAsync(ExchangeRateRequest request)
    {
        var url = $"http://host.docker.internal:28538/api/exchange";
        var payload = new
        {
            from = request.FromCurrency,
            to = request.ToCurrency,
            value = request.Amount
        };

        var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(url, content);

        if (!response.IsSuccessStatusCode)
            return null;

        var responseBody = await response.Content.ReadAsStringAsync();

        var apiResult = JsonSerializer.Deserialize<ExRateApiJsonProviderResponse>(responseBody);

        if (apiResult == null || apiResult.rate <= 0)
            return null;

        var result = new ExchangeRateResponse
        {
            ProviderName = _providerName,
            Amount = apiResult.rate
        };

        return result;
    }

    record ExRateApiJsonProviderResponse(decimal rate);
}