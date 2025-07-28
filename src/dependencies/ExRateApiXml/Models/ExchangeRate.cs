record ExchangeRate(
    string provider,
    string WARNING_UPGRADE_TO_V6,
    string terms,
    string @base,
    string date,
    long time_last_updated,
    Dictionary<string, decimal> rates
);