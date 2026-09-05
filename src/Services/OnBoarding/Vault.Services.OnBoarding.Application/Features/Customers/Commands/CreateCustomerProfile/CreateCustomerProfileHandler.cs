using MediatR;
using Vault.Services.OnBoarding.Application.Ports;
using Vault.Services.OnBoarding.Domain.Customer;
using Vault.Services.OnBoarding.Domain.Exceptions;

namespace Vault.Services.OnBoarding.Application.Features.Customers.Commands.CreateCustomerProfile
{
    internal sealed class CreateCustomerProfileHandler(ICustomerProfileRepository customerProfileRepository, IUnitOfWork unitOfWork, TimeProvider timeProvider) : IRequestHandler<CreateCustomerProfileCommand, Guid>
    {
        private const int MinimumAge = 18;

        public async Task<Guid> Handle(CreateCustomerProfileCommand request, CancellationToken ct)
        {
            var userId = Guid.CreateVersion7();

            if (await customerProfileRepository.ExistsForUserAsync(userId, ct))
                throw new ConflictException($"Login '{userId}' already has a customer profile.");

            var profile = CustomerProfile.Register(userId, request.FirstName, request.LastName, request.DateOfBirth, timeProvider.GetUtcNow(), MinimumAge);

            var profileId = await customerProfileRepository.AddAsync(profile, ct);
            await unitOfWork.CommitAsync(ct);

            return profileId;
        }
    }
}
