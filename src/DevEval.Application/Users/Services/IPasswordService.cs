namespace DevEval.Common.Services
{
    public interface IPasswordService
    {
        string HashPassword(string password);
        bool VerifyPassword(string hashedPassword, string password);
    }
}