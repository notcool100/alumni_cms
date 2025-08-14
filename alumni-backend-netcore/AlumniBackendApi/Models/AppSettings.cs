namespace AlumniBackendApi.Models
{
    public class AppSettings
    {
        public LoggingSettings Logging { get; set; }
        public string AllowedHosts { get; set; }
        public ConnectionStringsSettings ConnectionStrings { get; set; }
        public JwtSettings JWT { get; set; }
    }

    public class LoggingSettings
    {
        public LogLevelSettings LogLevel { get; set; }
    }

    public class LogLevelSettings
    {
        public string Default { get; set; }
        public string Microsoft { get; set; }
        public string MicrosoftHostingLifetime { get; set; }
    }

    public class ConnectionStringsSettings
    {
        public string DefaultConnection { get; set; }
    }

    public class JwtSettings
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Secret { get; set; }
        public int ExpiryInHours { get; set; } = 1;
    }
}