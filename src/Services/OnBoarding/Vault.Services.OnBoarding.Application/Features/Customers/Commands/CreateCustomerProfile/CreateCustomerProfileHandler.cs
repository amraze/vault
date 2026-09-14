using MediatR;
using Vault.Services.OnBoarding.Application.Ports;
using Vault.Services.OnBoarding.Domain.Customer;
using Vault.Services.OnBoarding.Domain.Exceptions;

namespace Vault.Services.OnBoarding.Application.Features.Customers.Commands.CreateCustomerProfile
{
    internal sealed class CreateCustomerProfileHandler(ICustomerCommandRepository customerCommandRepository, ICustomerQueryRepository customerQueryRepository, IUnitOfWork unitOfWork) : IRequestHandler<CreateCustomerProfileCommand, CreateCustomerProfileResponseDto>
    {
        private const int MinimumAge = 18;

        public async Task<CreateCustomerProfileResponseDto> Handle(CreateCustomerProfileCommand request, CancellationToken ct)
        {
            var userId = Guid.CreateVersion7();

            if (await customerQueryRepository.ExistsForUserAsync(userId, ct))
                throw new ConflictException($"Login '{userId}' already has a customer profile.");

            var profile = CustomerProfile.Register(userId, request.FirstName, request.LastName, request.DateOfBirth, DateTimeOffset.UtcNow, MinimumAge);

            var profileId = await customerCommandRepository.AddAsync(profile, ct);
            await unitOfWork.CommitAsync(ct);

            return new CreateCustomerProfileResponseDto { Id = profileId };
        }
    }
}
