namespace DevEval.Domain.Enums
{
    /// <summary>
    /// Enum representing the possible roles assigned to a user.
    /// </summary>
    public enum UserRole
    {
        /// <summary>
        /// Represents a regular customer.
        /// </summary>
        Customer = 1,

        /// <summary>
        /// Represents a manager with higher-level privileges.
        /// </summary>
        Manager = 2,

        /// <summary>
        /// Represents an administrator with full access.
        /// </summary>
        Admin = 3
    }
}