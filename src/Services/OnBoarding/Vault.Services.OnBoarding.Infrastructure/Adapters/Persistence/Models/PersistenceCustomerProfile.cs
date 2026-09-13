using Vault.Services.OnBoarding.Domain.Customer.Enums;

namespace Vault.Services.OnBoarding.Infrastructure.Adapters.Persistence.Models
{
    internal class PersistenceCustomerProfile
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public KycState KycStatus { get; set; }

    }
}
