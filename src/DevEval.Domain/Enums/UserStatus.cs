namespace DevEval.Domain.Enums
{
    /// <summary>
    /// Enum representing the possible statuses of a user.
    /// </summary>
    public enum UserStatus
    {
        /// <summary>
        /// The user is active and has full access to the system.
        /// </summary>
        Active = 1,

        /// <summary>
        /// The user is inactive and cannot access the system.
        /// </summary>
        Inactive = 2,

        /// <summary>
        /// The user is suspended due to policy violations or restrictions.
        /// </summary>
        Suspended = 3
    }
}