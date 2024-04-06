using System.Data.SqlClient;
using Fiap.Api.Escola.Domain.Abstractions;
using Fiap.Api.Escola.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fiap.Api.Escola.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped(_ =>
            new SqlConnection(configuration.GetConnectionString("Default")));

        services.AddScoped<IAlunoRepository, AlunoRepository>();

        return services;
    }
}
