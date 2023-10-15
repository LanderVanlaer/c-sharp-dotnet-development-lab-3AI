using System.Collections.Immutable;

namespace Backend.Transactions;

public interface ITransactionable
{
    ImmutableList<Transaction> Transactions { get; }
    string Identification { get; }
    void AddTransaction(Transaction transaction);
    bool CanTransact(decimal amount);
}