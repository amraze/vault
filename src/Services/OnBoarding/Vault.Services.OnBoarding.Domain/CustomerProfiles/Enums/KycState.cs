namespace Vault.Services.OnBoarding.Domain.CustomerProfiles.Enums
{
    /// <summary>KycState different states a customer's KYC status can be in.</summary>
    public enum KycState
    {
        Unverified = 1,
        Pending = 2,
        Verified = 3,
        Rejected = 4
    }
}
