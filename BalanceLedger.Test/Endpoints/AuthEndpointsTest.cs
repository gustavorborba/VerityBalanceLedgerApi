using BalanceLedgerApi.Domain.Model;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace BalanceLedgerApi.Test.Endpoints
{
    public class AuthEndpointsTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client = factory.CreateClient();

        [Fact]
        public async Task Authenticate_ValidUser_ReturnsOkWithToken()
        {
            // Arrange
            var user = new User { Email = "teste@teste.com", Password = "minhaSenha" };
            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

            // Act:
            var response = await _client.PostAsync("/authenticate", content);

            // Assert:
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Authenticate_InvalidUser_ReturnsUnauthorized()
        {
            // Arrange
            var user = new User { Email = "invalido@teste.com", Password = "senhaErrada" };
            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/authenticate", content);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task Authenticate_EmptyRequestBody_ReturnsBadRequest()
        {
            // Arrange
            var content = new StringContent("", Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/authenticate", content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Authenticate_UserWithoutPassword_ReturnsUnathorized()
        {
            // Arrange
            var user = new User { Email = "teste@teste.com", Password = "" };
            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/authenticate", content);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task Authenticate_UserWithoutEmail_ReturnsUnathorized()
        {
            // Arrange
            var user = new User { Email = "", Password = "minhaSenha" };
            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/authenticate", content);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}