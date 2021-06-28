namespace LoginSystem.Services.Services
{
    interface ISecurityService
    {
        string GetHash(string password);
        bool VerifyPassword(string hashFromCash, string enteredPassword);
    }
}