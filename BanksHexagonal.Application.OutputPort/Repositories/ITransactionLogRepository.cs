using BanksHexagonal.Application.Models;

namespace BanksHexagonal.Application.OutputPort;

public interface ITransactionLogRepository
{
    Task<IEnumerable<TransactionHistory>> GetHistoryForAccount(int number);

    void AddTransaction(TransactionType type, int number, int amount);
}