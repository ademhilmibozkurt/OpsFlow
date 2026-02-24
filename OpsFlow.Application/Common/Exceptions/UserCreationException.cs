namespace OpsFlow.Application.Common.Exceptions
{
    public class UserCreationException : Exception
    {
        public UserCreationException()
        {
        }

        public UserCreationException(string message) : base(message)
        {
        }

        public UserCreationException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}