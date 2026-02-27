namespace OpsFlow.Application.Common.Exceptions
{
    public class IncorrectCredentialsException : Exception
    {
        public IncorrectCredentialsException()
        {
        }

        public IncorrectCredentialsException(string message) : base(message)
        {
        }

        public IncorrectCredentialsException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}