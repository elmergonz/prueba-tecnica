using ExchangeRate.Models;

namespace ExchangeRate.Interfaces;

public interface IExchangeRateService
{
    Task<ExchangeRateResponse> GetBestDealAsync(ExchangeRateRequest request);

}