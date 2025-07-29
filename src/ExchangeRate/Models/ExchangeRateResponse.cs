namespace ExchangeRate.Models;

public class ExchangeRateResponse
{
    public string ProviderName { get; set; } = string.Empty;
    public decimal Amount { get; set; }
}