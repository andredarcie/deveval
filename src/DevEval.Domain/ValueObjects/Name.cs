using DevEval.Common;
using System.Text.RegularExpressions;

namespace DevEval.Domain.ValueObjects
{
    /// <summary>
    /// Represents the name details of a user as a value object.
    /// </summary>
    public class Name : ValueObject
    {
        private static readonly Regex NameValidationRegex = new(@"^[A-Za-zÀ-ÖØ-öø-ÿ\s'-]+$", RegexOptions.Compiled);

        /// <summary>
        /// Gets the first name of the user.
        /// </summary>
        public string FirstName { get; }

        /// <summary>
        /// Gets the last name of the user.
        /// </summary>
        public string LastName { get; }

        /// <summary>
        /// Gets the full name by combining first and last names.
        /// </summary>
        public string FullName => $"{FirstName.Trim()} {LastName.Trim()}";

        /// <summary>
        /// Initializes a new instance of the <see cref="Name"/> class.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        public Name(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("First name cannot be empty.", nameof(firstName));

            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Last name cannot be empty.", nameof(lastName));

            if (!NameValidationRegex.IsMatch(firstName))
                throw new ArgumentException("First name contains invalid characters.", nameof(firstName));

            if (!NameValidationRegex.IsMatch(lastName))
                throw new ArgumentException("Last name contains invalid characters.", nameof(lastName));

            if (firstName.Length is < 2 or > 50)
                throw new ArgumentException("First name must be between 2 and 50 characters.", nameof(firstName));

            if (lastName.Length is < 2 or > 50)
                throw new ArgumentException("Last name must be between 2 and 50 characters.", nameof(lastName));

            FirstName = firstName;
            LastName = lastName;
        }

        private Name()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
        }

        /// <summary>
        /// Overrides the ToString method to display the full name.
        /// </summary>
        public override string ToString() => FullName;

        /// <summary>
        /// Defines the components of equality for the value object.
        /// </summary>
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FirstName;
            yield return LastName;
        }
    }
}