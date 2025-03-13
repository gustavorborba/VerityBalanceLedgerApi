using AutoMapper;
using BalanceLedgerApi.Application.Dto;
using BalanceLedgerApi.Application.Interface.Repository;
using BalanceLedgerApi.Application.Service;
using BalanceLedgerApi.Domain.Enum;
using BalanceLedgerApi.Domain.Model;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace BalanceLedgerApi.Test.Services
{
    public class TransactionServiceTest
    {
        private readonly ITransactionRepository _repository;
        private readonly ILogger<TransactionService> _logger;
        private readonly IMapper _mapper;
        private readonly TransactionService _transactionService;

        public TransactionServiceTest()
        {
            _repository = Substitute.For<ITransactionRepository>();
            _logger = Substitute.For<ILogger<TransactionService>>();
            _mapper = Substitute.For<IMapper>();
            _transactionService = new TransactionService(_repository, _logger, _mapper);
        }

        [Fact]
        public async Task All_ShouldReturnSuccessResponse_WhenTransactionsExist()
        {
            // Arrange
            var transactions = new List<Transaction> { new() { Type = TransactionType.Credit, Value = 100, DateCreated = DateTime.Now } };
            var transactionDtos = new List<TransactionDto> { new() { Type = TransactionType.Credit, Value = 100 } };

            _repository.All().Returns(Task.FromResult((IEnumerable<Transaction>)transactions));
            _mapper.Map<IEnumerable<TransactionDto>>(transactions).Returns(transactionDtos);

            // Act
            var result = await _transactionService.All();

            // Assert
            Assert.True(result.Success);
            Assert.Equal(transactionDtos, result.Data);
        }

        [Fact]
        public async Task All_ShouldReturnErrorResponse_WhenExceptionThrown()
        {
            // Arrange
            var exceptionMessage = "Error fetching transactions";
            _repository.All().Throws(new Exception(exceptionMessage));

            // Act
            var result = await _transactionService.All();

            // Assert
            Assert.False(result.Success);
            Assert.Equal(exceptionMessage, result.Error);
        }

        [Fact]
        public async Task Save_ShouldReturnSuccessResponse_WhenTransactionIsSaved()
        {
            // Arrange
            var transactionDto = new TransactionDto { Type = TransactionType.Credit, Value = 100 };
            var transaction = new Transaction { Type = TransactionType.Credit, Value = 100, DateCreated = DateTime.Now };

            _mapper.Map<Transaction>(transactionDto).Returns(transaction);
            _repository.Save(transaction).Returns(Task.FromResult(transaction));

            // Act
            var result = await _transactionService.Save(transactionDto);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(transactionDto, result.Data);
        }

        [Fact]
        public async Task Save_ShouldReturnErrorResponse_WhenExceptionThrown()
        {
            // Arrange
            var transactionDto = new TransactionDto { Type = TransactionType.Credit, Value = 100 };
            var exceptionMessage = "Error saving transaction";
            _repository.Save(Arg.Any<Transaction>()).Throws(new Exception(exceptionMessage));

            // Act
            var result = await _transactionService.Save(transactionDto);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(exceptionMessage, result.Error);
        }
    }
}