using BalanceLedgerApi.Application.Dto;
using BalanceLedgerApi.Application.Interface.Service;
using BalanceLedgerApi.Domain.Enum;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NSubstitute;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace BalanceLedgerApi.Test.Endpoints
{
    public class TransactionEndpointsTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly ITransactionService _transactionService;
        private readonly HttpClient _client;

        public TransactionEndpointsTest(WebApplicationFactory<Program> factory)
        {
            _transactionService = Substitute.For<ITransactionService>();
            _client = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddSingleton(_transactionService);
                });
            }).CreateClient();
        }

        //se der tempo, altero para gerar diretamente o token
        private async Task AuthenticateAsync()
        {
            var user = new { Email = "teste@teste.com", Password = "minhaSenha" };
            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/authenticate", content);
            var authResponse = JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authResponse);
        }

        [Fact]
        public async Task Add_ValidTransaction_ReturnOk()
        {
            // Arrange
            var transaction = new TransactionDto { Value = 100, Type = TransactionType.Credit, DateCreated = DateTime.Now };
            var commonResponse = new CommonResponseDto<TransactionDto>()
            {
                Data = transaction,
                Success = true
            };
            var content = new StringContent(JsonConvert.SerializeObject(transaction), Encoding.UTF8, "application/json");
            _transactionService.Save(Arg.Any<TransactionDto>()).Returns(commonResponse);
            await AuthenticateAsync();

            // Act
            var response = await _client.PostAsync("/add", content);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Add_InvalidTransaction_ReturnBadRequest()
        {
            // Arrange
            var content = new StringContent("", Encoding.UTF8, "application/json");
            await AuthenticateAsync();

            // Act
            var response = await _client.PostAsync("/add", content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task GetAll_Transactions_ReturnOkAndList()
        {
            // Act
            await AuthenticateAsync();
            var response = await _client.GetAsync("/all");

            // Assert
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            Assert.NotNull(content);

            var transactions = JsonConvert.DeserializeObject<List<TransactionDto>>(content);
            Assert.NotNull(transactions);
        }

        [Fact]
        public async Task GetAll_WithoutAuthentication_ReturnUnauthorized()
        {
            // Act
            var response = await _client.GetAsync("/all");

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task Add_WithoutAuthentication_ReturnUnauthorized()
        {
            // Arrange
            var transaction = new TransactionDto { Value = 100, Type = TransactionType.Credit, DateCreated = DateTime.Now };
            var content = new StringContent(JsonConvert.SerializeObject(transaction), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/add", content);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}
