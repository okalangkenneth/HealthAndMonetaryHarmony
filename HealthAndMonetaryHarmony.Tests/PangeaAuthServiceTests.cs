using HealthAndMonetaryHarmony.Services;
using Moq;
using PangeaCyber.Net;
using PangeaCyber.Net.AuthN;
using PangeaCyber.Net.AuthN.Models;
using PangeaCyber.Net.AuthN.Requests;
using PangeaCyber.Net.AuthN.Results;
using System.Threading.Tasks;
using Xunit;

namespace HealthAndMonetaryHarmony.Tests
{
    public class PangeaAuthServiceTests
    {
        [Fact]
        public async Task RegisterUser_CallsAuthNClientCreateMethod()
        {
            // Arrange
            var mockAuthNClient = new Mock<AuthNClient>();

            // Mock the Create method to return a predefined response
            mockAuthNClient.Setup(client => client.User.Create(It.IsAny<UserCreateRequest>()))
                           .ReturnsAsync(new Response<UserCreateResult>
                           {
                               Result = new UserCreateResult { /* mock response data */ }
                           });

            var service = new PangeaAuthService(mockAuthNClient.Object);

            // Act
            await service.RegisterUser("test@email.com", "password123");

            // Assert
            mockAuthNClient.Verify(client => client.User.Create(It.IsAny<UserCreateRequest>()), Times.Once);
        }

        [Fact]
        public async Task LoginUser_CallsAuthNClientLoginMethod()
        {
            // Arrange
            var mockAuthNClient = new Mock<IAuthNClient>();
            mockAuthNClient.Setup(client => client.User.Login.Password(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Profile>()))
                           .ReturnsAsync((dynamic)null); // Using dynamic type here

            var service = new PangeaAuthService(mockAuthNClient.Object);

            // Act
            await service.LoginUser("test@email.com", "password123");

            // Assert
            mockAuthNClient.Verify(client => client.User.Login.Password(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Profile>()), Times.Once);
        }



        [Fact]
        public async Task UpdatePassword_CallsAuthNClientPasswordChangeMethod()
        {
            // Arrange
            var mockAuthNClient = new Mock<AuthNClient>();

            mockAuthNClient.Setup(client => client.Client.Password.Change(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                           .ReturnsAsync(new Response<object> { /* mock response data */ });

            var service = new PangeaAuthService(mockAuthNClient.Object);

            // Act
            await service.UpdatePassword("token123", "oldPassword", "newPassword");

            // Assert
            mockAuthNClient.Verify(client => client.Client.Password.Change(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }



        [Fact]
        public async Task UpdateProfile_CallsAuthNClientProfileUpdateMethod()
        {
            // Arrange
            var mockAuthNClient = new Mock<AuthNClient>();
            var profile = new Profile { { "key", "value" } };

            mockAuthNClient.Setup(client => client.User.Profile.Update(It.IsAny<UserProfileUpdateRequest>()))
                           .ReturnsAsync(new Response<ProfileUpdateResult>
                           {
                               Result = new ProfileUpdateResult { /* mock response data */ }
                           });

            var service = new PangeaAuthService(mockAuthNClient.Object);

            // Act
            await service.UpdateProfile("userId123", profile);

            // Assert
            mockAuthNClient.Verify(client => client.User.Profile.Update(It.IsAny<UserProfileUpdateRequest>()), Times.Once);
        }


    }
}
