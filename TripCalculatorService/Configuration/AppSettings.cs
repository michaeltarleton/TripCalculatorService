namespace TripCalculatorService.Configuration {
    public class LogLevel {
        public string Default { get; set; }
    }
    public class Logging {
        public LogLevel LogLevel { get; set; }
    }
    public class ElasticConfig {
        public string Host { get; set; }
    }
    public class AppSettings {
        public Logging Logging { get; set; }
        public string AllowedHosts { get; set; }
        public ElasticConfig ElasticConfig { get; set; }
    }
}