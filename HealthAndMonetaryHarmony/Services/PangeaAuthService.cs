using HealthAndMonetaryHarmony.Exceptions;
using PangeaCyber.Net.AuthN;
using PangeaCyber.Net.AuthN.Models;
using PangeaCyber.Net.AuthN.Requests;
using PangeaCyber.Net.Exceptions;

namespace HealthAndMonetaryHarmony.Services
{
    public class PangeaAuthService
    {
        private readonly AuthNClient _client;

        // Constructor to initialize the AuthNClient
        public PangeaAuthService(AuthNClient client)
        {
            _client = client;
        }

        // Method to register a new user
        public async Task<object> RegisterUser(string email, string password)
        {
            try
            {
                // Create a user request with provided email and password
                var userCreateRequest = new UserCreateRequest.Builder(email, password, IDProvider.Password).Build();

                // Send the request to create the user
                var response = await _client.User.Create(userCreateRequest);
                return response;
            }
            catch (PangeaAPIException ex)
            {
                // Handle specific exception for user already existing
                if (ex.Message.Contains("UserExists"))
                    throw new UserAlreadyExistsException();
                throw;
            }
        }

        // Method to log in a user
        public async Task<object> LoginUser(string email, string password)
        {
            try
            {
                // Send the request to log in the user
                var response = await _client.User.Login.Password(email, password);
                return response;
            }
            catch (PangeaAPIException ex)
            {
                // Handle specific exception for authentication failure
                if (ex.Message.Contains("AuthenticationFailure"))
                    throw new AuthenticationFailedException();
                throw;
            }
        }

        // Method to update a user's password
        public async Task UpdatePassword(string token, string oldPassword, string newPassword)
        {
            try
            {
                // Send the request to change the password
                await _client.Client.Password.Change(token, oldPassword, newPassword);
            }
            catch (PangeaAPIException ex)
            {
                // Handle specific exception for password policy violation
                if (ex.Message.Contains("PasswordPolicyFailure"))
                    throw new PasswordPolicyViolationException();
                throw;
            }
        }

        // Method to update a user's profile
        public async Task UpdateProfile(string userId, Profile profile)
        {
            try
            {
                // Create a profile update request with provided user ID and profile
                var profileUpdateRequest = new UserProfileUpdateRequest.Builder(profile).WithID(userId).Build();

                // Send the request to update the profile
                await _client.User.Profile.Update(profileUpdateRequest);
            }
            catch (PangeaAPIException ex)
            {
                // Handle specific exception for invalid user
                if (ex.Message.Contains("InvalidUser"))
                    throw new UserNotFoundException();
                throw;
            }
        }
    }
}


