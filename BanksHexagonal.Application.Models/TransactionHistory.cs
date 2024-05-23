namespace BanksHexagonal.Application.Models;

public record TransactionHistory(int AccountNumber, TransactionType Type, int Amount);