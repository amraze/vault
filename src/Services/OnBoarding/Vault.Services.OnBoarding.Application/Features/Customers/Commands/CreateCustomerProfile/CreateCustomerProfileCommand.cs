using MediatR;

namespace Vault.Services.OnBoarding.Application.Features.Customers.Commands.CreateCustomerProfile
{
    public sealed record CreateCustomerProfileCommand : IRequest<Guid>
    {
        public required string FirstName { get; init; }
        public required string LastName { get; init; }
        public required DateOnly DateOfBirth { get; init; }
    }
}
