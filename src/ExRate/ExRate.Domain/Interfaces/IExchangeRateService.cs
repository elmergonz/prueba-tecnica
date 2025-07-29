using ExRate.Domain.Entities;

namespace ExRate.Domain.Interfaces;

public interface IExchangeRateService
{
    Task<ExchangeRateResponse> GetBestDealAsync(string fromCurrency, string toCurrency, decimal amount);
}