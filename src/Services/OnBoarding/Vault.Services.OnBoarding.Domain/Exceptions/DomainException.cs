namespace Vault.Services.OnBoarding.Domain.Exceptions
{
    /// <summary>The values are incoherent whatever the state — they could never be accepted.</summary>
    public class DomainException : BaseException
    {
        /// <summary>422: parsed fine, but the content breaks a model rule. Retrying will always fail.</summary>
        public override int StatusCode => 422;

        public override string Title => "Invalid domain values";

        public DomainException(string message) : base(message) { }
    }
}
