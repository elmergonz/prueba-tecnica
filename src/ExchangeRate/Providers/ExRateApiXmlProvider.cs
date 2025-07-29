using System.Text;
using System.Text.Json;
using System.Xml.Linq;
using System.Xml.Serialization;
using ExchangeRate.Interfaces;
using ExchangeRate.Models;

public class ExRateApiXmlProvider : IExchangeRateProvider
{
    private readonly string _providerName = "ExRateApiXml";
    private readonly HttpClient _httpClient;

    public ExRateApiXmlProvider(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ExchangeRateResponse?> GetExchangeRateAsync(ExchangeRateRequest request)
    {
        var url = $"http://localhost:28539/api/exchange";
        var payload = new XDocument(
            new XElement("ExchangeRateRequest",
                new XElement("FromCurrency", request.FromCurrency),
                new XElement("ToCurrency", request.ToCurrency),
                new XElement("Amount", request.Amount)
            )
        );

        var content = new StringContent(payload.ToString(), Encoding.UTF8, "application/xml");
        var response = await _httpClient.PostAsync(url, content);

        if (!response.IsSuccessStatusCode)
            return null;

        var responseStream = await response.Content.ReadAsStreamAsync();

        var serializer = new XmlSerializer(typeof(ExRateApiXmlResponse));

        if (serializer.Deserialize(responseStream) is not ExRateApiXmlResponse apiResult || apiResult.Result <= 0)
            return null;

        var result = new ExchangeRateResponse
        {
            ProviderName = _providerName,
            Amount = apiResult.Result
        };

        return result;
    }

    [XmlRoot("ExchangeRateResponse")]
    public class ExRateApiXmlResponse
    {
        [XmlElement("Result")]
        public decimal Result { get; set; }

        public ExRateApiXmlResponse() { }
    }
}