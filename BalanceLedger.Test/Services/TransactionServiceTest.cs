using AutoMapper;
using BalanceLedgerApi.Application.Dto;
using BalanceLedgerApi.Application.Interface.Repository;
using BalanceLedgerApi.Application.Service;
using BalanceLedgerApi.Domain.Model;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace BalanceLedgerApi.Test.Services
{
    public class TransactionServiceTests
    {
        private readonly ITransactionRepository _repository;
        private readonly ILogger<TransactionService> _logger;
        private readonly IMapper _mapper;
        private readonly TransactionService _transactionService;

        public TransactionServiceTests()
        {
            _repository = Substitute.For<ITransactionRepository>();
            _logger = Substitute.For<ILogger<TransactionService>>();
            _mapper = Substitute.For<IMapper>();
            _transactionService = new TransactionService(_repository, _logger, _mapper);
        }

        [Fact]
        public async Task All_ShouldReturnAllTransactions()
        {
            // Arrange
            var transactions = new List<Transaction> { new() };
            _repository.All().Returns(transactions);

            // Act
            var result = await _transactionService.All();

            // Assert
            Assert.Equal(transactions, result);
        }

        [Fact]
        public async Task Save_ShouldReturnSuccessResponse_WhenTransactionIsSaved()
        {
            // Arrange
            var transactionDto = new TransactionDto();
            var transaction = new Transaction();
            _mapper.Map<Transaction>(transactionDto).Returns(transaction);

            // Act
            var result = await _transactionService.Save(transactionDto);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(transactionDto, result.Data);
        }

        [Fact]
        public async Task Save_ShouldReturnErrorResponse_WhenExceptionIsThrown()
        {
            // Arrange
            var transactionDto = new TransactionDto();
            var exceptionMessage = "Error saving transaction";
            _mapper.Map<Transaction>(transactionDto).Returns(new Transaction());
            _repository.Save(Arg.Any<Transaction>()).Throws(new Exception(exceptionMessage));

            // Act
            var result = await _transactionService.Save(transactionDto);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(exceptionMessage, result.Error);
        }
    }
}
