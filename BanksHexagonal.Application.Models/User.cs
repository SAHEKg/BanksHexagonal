namespace BanksHexagonal.Application.Models;

public record User
{
    private User() { }

    public sealed record AccountHolder(int AccountNumber, int Pin) : User;

    public sealed record Admin : User;
}
