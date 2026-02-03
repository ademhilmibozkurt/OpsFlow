using OpsFlow.Domain.Enums;

namespace OpsFlow.Domain.Entities
{
    public class User : BaseEntity
    {
        private string _fullname;
        // CAUTION: email has to be unique, well formatted and case-insensitive. add later
        private string _email;
        private bool _isActive;
        private Roles _role;

        public string FullName => _fullname;
        public string Email => _email;
        public bool IsActive => _isActive;
        public Roles Role => _role;

        public User(string fullName, string email)
        {
            EnsureIsValid(fullName);
            EnsureIsValid(email);

            _fullname = fullName;
            _email    = email;
            _isActive = true;
            _role     = Roles.User;
        }

        private void EnsureIsValid(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentException($"{text} can not be empty", nameof(text));
        }

        public void ActivateUser()
        {
            ChangeState(true, "User already active. Can not activate!");
        }

        public void DeActivateUser()
        {
            ChangeState(false, "User already deactivated. Can not deactivate!");
        }

        private void ChangeState(bool to, string errorMessage)
        {
            if (_isActive == to)
            {
                throw new InvalidOperationException(errorMessage);
            }

            _isActive = to;
        } 
    }
}