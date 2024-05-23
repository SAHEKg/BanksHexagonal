namespace BanksHexagonal.Application.InputPort;

public interface IAdminService
{
    void Login(string password);

    void Logout();

    void ChangePassword(string newPassword);

    void AddAccount(int number, int pin);
}