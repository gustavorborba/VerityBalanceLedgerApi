using AutoMapper;
using BalanceLedgerApi.Application.Dto;
using BalanceLedgerApi.Application.Interfaces.Repository;
using BalanceLedgerApi.Application.Interfaces.Service;
using BalanceLedgerApi.Domain.Model;
using BalanceLedgerApi.Util;

namespace BalanceLedgerApi.Application.Service
{
    public class TransactionService(ITransactionRepository _repository, ILogger<TransactionService> _logger, IMapper _mapper) : ITransactionService
    {
        public async Task<IEnumerable<Transaction>> All()
        {
           return await _repository.All();

        }

        public async Task<CommonResponseDto<TransactionDto>> Save(TransactionDto transactionDto)
        {
            try
            {
                await RetryUtilService.RetryOnExceptionAsync(() => _repository.Save(_mapper.Map<Transaction>(transactionDto)), _logger);

                return CommonResponseDto<TransactionDto>.SuccessResponse(transactionDto); ;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving transaction");
                return CommonResponseDto<TransactionDto>.ErrorResponse(ex.Message);
            }
        }
    }
}
