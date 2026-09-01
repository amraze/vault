namespace Vault.Services.OnBoarding.Domain.Exceptions
{
    /// <summary>The entity's current state refuses the operation. Values are fine; the state is not.</summary>
    public class ConflictException : BaseException
    {
        /// <summary>409: valid request, conflicting state. Retrying later may succeed.</summary>
        public override int StatusCode => 409;

        public override string Title => "Operation refused in the current state";

        public ConflictException(string message) : base(message) { }

        /// <summary>Shorthand for "Cannot {operation} from state {fromState}."</summary>
        public ConflictException(string operation, object fromState)
            : base($"Cannot {operation} from state {fromState}.") { }
    }
}
