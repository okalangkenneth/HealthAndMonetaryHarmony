namespace HealthAndMonetaryHarmony.Exceptions
{
    public class PasswordPolicyViolationException : Exception
    {
        public PasswordPolicyViolationException() : base("Authentication failed.")
        {
        }
    }
}
