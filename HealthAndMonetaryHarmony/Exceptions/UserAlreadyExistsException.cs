namespace HealthAndMonetaryHarmony.Exceptions
{
    public class UserAlreadyExistsException : Exception
    {
        public UserAlreadyExistsException() : base("A user with this email already exists.") { }
    }

}
