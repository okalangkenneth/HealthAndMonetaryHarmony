namespace HealthAndMonetaryHarmony.Services
{
    public class PangeaConfigService
    {
        public string PangeaDomain { get; private set; }
        public string AuthToken { get; private set; }

        public PangeaConfigService()
        {
            PangeaDomain = Environment.GetEnvironmentVariable("PANGEA_DOMAIN");
            AuthToken = Environment.GetEnvironmentVariable("AUTHN_AUTH_TOKEN");
        }
    }

}
