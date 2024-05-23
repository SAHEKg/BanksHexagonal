using BanksHexagonal.Application.Models;

namespace BanksHexagonal.Application.InputPort;

public interface ITransactionHistoryTablePrinter
{
    void Display(IEnumerable<TransactionHistory> history);
}