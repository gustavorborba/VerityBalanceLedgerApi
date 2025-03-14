using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace BalanceLedgerApi.Endpoints
{
    public static class AuthEndpoints
    {
        public static void MapAuthEndpoints(this IEndpointRouteBuilder routes)
        {
            routes.MapPost("auth/authenticate", [AllowAnonymous] (User user, IOptions<AppSettings> appSettings) =>
            {
                //Feito para teste, se der tempo irei fazer a autenticação completa com criptografia e controle de usuário
                if (user.Email == "teste@teste.com" && user.Password == "minhaSenha")
                    return Results.Ok(JwtBearerService.GenerateToken(user, appSettings.Value.JwtToken));

                return Results.Unauthorized();
            })
            .WithName("User Authentication");
        }
    }
}
