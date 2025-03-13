using BalanceLedgerApi.Application.Dto;
using BalanceLedgerApi.Application.Interface.Service;

namespace BalanceLedgerApi.Endpoints
{
    public static class TransactionEndpoints
    {
        public static void MapTransactionEndpoints(this IEndpointRouteBuilder routes)
        {
            routes.MapPost("/transaction/add", async (TransactionDto dto, ITransactionService service) =>
            {
                var result = await service.Save(dto);

                return result.Success ? Results.Ok(result) : Results.BadRequest(result);
            })
            .WithName("Add Transaction")
            .RequireAuthorization();

            routes.MapGet("transaction/all", async (ITransactionService service) =>
            {
                return await service.All();
            })
            .WithName("Return All saved Transactions")
            .RequireAuthorization();
        }
    }
}
