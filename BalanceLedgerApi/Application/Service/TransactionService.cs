using AutoMapper;
using BalanceLedgerApi.Application.Dto;
using BalanceLedgerApi.Application.Interface.Repository;
using BalanceLedgerApi.Application.Interface.Service;
using BalanceLedgerApi.Domain.Model;
using BalanceLedgerApi.Util;

namespace BalanceLedgerApi.Application.Service
{
    public class TransactionService(ITransactionRepository _repository, ILogger<TransactionService> _logger, IMapper _mapper) : ITransactionService
    {
        public async Task<CommonResponseDto<IEnumerable<TransactionDto>>> All()
        {
            try
            {
                var transactions = await _repository.All();

                var transactionDtos = _mapper.Map<IEnumerable<TransactionDto>>(transactions);

                return CommonResponseDto<IEnumerable<TransactionDto>>.SuccessResponse(transactionDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting transaction");
                return CommonResponseDto<IEnumerable<TransactionDto>>.ErrorResponse(ex.Message);
            }

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
