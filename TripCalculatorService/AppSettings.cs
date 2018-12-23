namespace TripCalculatorService
{
    public class LogLevel
    {
        string Default;
    }
    public class Logging
    {
        LogLevel LogLevel;
    }
    public class ElasticConfig
    {
        string Host;
    }
    public class AppSettings
    {
        Logging Logging;
        string AllowedHosts;
        ElasticConfig Elasticsearch;
    }
}