namespace LoginSystem.Services.Services
{
    public interface ISecurityService
    {
        string GetHash(string password);
        bool VerifyPassword(string hashFromCash, string enteredPassword);
    }
}