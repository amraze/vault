using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vault.Services.OnBoarding.Application.Features.Customers.Commands.CreateCustomerProfile;

namespace Vault.Services.OnBoarding.Api.Endpoints
{
    public static class CustomerEndpoints
    {
        public static IEndpointRouteBuilder MapCustomers(this IEndpointRouteBuilder endpoints)
        {
            var group = endpoints.MapGroup("/customers").WithTags("Customers");

            group.MapPost("/", async ([FromBody] CreateCustomerProfileCommand command, ISender sender, CancellationToken ct) =>
            {
                var response = await sender.Send(command, ct);
                return Results.Created($"/customers/{response.Id}", response);
            })
            .WithName("CreateCustomer").WithSummary("Create a new customer").Produces(StatusCodes.Status201Created).Produces(StatusCodes.Status400BadRequest);

            return endpoints;
        }
    }
}
