namespace Vault.Services.OnBoarding.Domain.Exceptions
{
    /// <summary>
    /// Base for exceptions that represent a refused business outcome rather than a caller bug.
    /// Each subclass declares how it should surface, so the API's exception handler never
    /// needs a type switch.
    /// </summary>
    /// <remarks>
    /// StatusCode is a plain int on purpose: the domain must not reference
    /// Microsoft.AspNetCore.Http. The API translates it.
    /// </remarks>
    public abstract class BaseException : Exception
    {
        public abstract int StatusCode { get; }

        public abstract string Title { get; }

        protected BaseException(string message) : base(message) { }
    }
}
