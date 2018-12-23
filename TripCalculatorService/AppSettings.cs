namespace TripCalculatorService
{
    public class LogLevel
    {
        public string Default;
    }
    public class Logging
    {
        public LogLevel LogLevel;
    }
    public class ElasticConfig
    {
        public string Host;
    }
    public class AppSettings
    {
        public Logging Logging;
        public string AllowedHosts;
        public ElasticConfig ElasticConfig;
    }
}