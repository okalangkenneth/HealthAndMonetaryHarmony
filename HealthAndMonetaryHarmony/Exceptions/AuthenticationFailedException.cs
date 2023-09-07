using System;

namespace HealthAndMonetaryHarmony.Exceptions
{
    public class AuthenticationFailedException : Exception
    {
        public AuthenticationFailedException() : base("Authentication failed.")
        {
        }
    }
}
