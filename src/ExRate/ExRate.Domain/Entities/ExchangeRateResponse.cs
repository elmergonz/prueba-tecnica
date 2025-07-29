namespace ExRate.Domain.Entities;

public class ExchangeRateResponse
{
    public string ApiName { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public decimal ExchangeRate { get; set; }
    public bool IsSuccessful { get; set; }
    public string? ErrorMessage { get; set; }
}