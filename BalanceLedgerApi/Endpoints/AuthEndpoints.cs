using BalanceLedgerApi.Domain.Model;
using BalanceLedgerApi.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace BalanceLedgerApi.Endpoints
{
    public static class AuthEndpoints
    {
        public static void MapAuthEndpoints(this IEndpointRouteBuilder routes)
        {
            routes.MapPost("/authenticate/", [AllowAnonymous] (User user, IOptions<AppSettings> appSettings) =>
            {
                //Eu sei, se der tempo irei concluir :C
                if (user.Email == "teste@teste.com" && user.Password == "minhaSenha")
                    return Results.Ok(JwtBearerService.GenerateToken(user, appSettings.Value.JwtToken));

                return Results.Unauthorized();
            });
        }
    }
}
