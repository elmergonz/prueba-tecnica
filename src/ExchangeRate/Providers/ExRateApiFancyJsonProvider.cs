using System.Text;
using System.Text.Json;
using ExchangeRate.Interfaces;
using ExchangeRate.Models;

public class ExRateApiFancyJsonProvider : IExchangeRateProvider
{
    private readonly string _providerName = "ExRateApiFancyJson";
    private readonly HttpClient _httpClient;

    public ExRateApiFancyJsonProvider(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ExchangeRateResponse?> GetExchangeRateAsync(ExchangeRateRequest request)
    {
        var url = $"http://host.docker.internal:28540/api/exchange";
        var payload = new
        {
            sourceCurrency = request.FromCurrency,
            targetCurrency = request.ToCurrency,
            quantity = request.Amount
        };

        var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(url, content);

        if (!response.IsSuccessStatusCode)
            return null;

        var responseBody = await response.Content.ReadAsStringAsync();

        var apiResult = JsonSerializer.Deserialize<ExRateApiFancyJsonResponse>(responseBody);

        if (apiResult == null || apiResult.data.total <= 0)
            return null;

        var result = new ExchangeRateResponse
        {
            ProviderName = _providerName,
            Amount = apiResult.data.total
        };

        return result;
    }

    record ExRateApiFancyJsonResponse(int statusCode, string message, Data data);
    record Data(decimal total);
}