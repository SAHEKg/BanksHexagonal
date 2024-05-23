namespace BanksHexagonal.Application.InputPort;

public interface IAccountHolderService
{
    void Login(int number, int pin);

    void Logout();
}