using Backend.Transactions.exceptions;
using Newtonsoft.Json;

namespace Backend.Transactions;

public readonly struct Transaction
{
    [JsonIgnore] public readonly ITransactionable From;
    [JsonIgnore] public readonly ITransactionable To;
    public readonly decimal Amount;
    public readonly DateTime Date;

    internal Transaction(ITransactionable from, ITransactionable to, decimal amount)
    {
        if (!from.CanTransact(amount)) throw new InsufficientBalanceException();

        From = from;
        To = to;
        Amount = amount;

        Date = DateTime.Now;

        from.AddTransaction(this);
        to.AddTransaction(this);
    }

    public override string ToString()
    {
        return
            $"Transaction ${Amount} from {From.GetType().Name}({From.Identification}) to {To.GetType().Name}({To.Identification}) at {Date}";
    }
}