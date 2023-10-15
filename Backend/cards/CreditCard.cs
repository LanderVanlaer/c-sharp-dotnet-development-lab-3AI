using System.Collections.Immutable;
using Backend.accounts.exceptions;
using Backend.cards.exceptions;
using Backend.customers;
using Backend.Transactions;

namespace Backend.cards;

public class CreditCard : Card, ITransactionable
{
    private readonly List<Transaction> _transactions = new();

    internal CreditCard(string pinCode, Customer holder) : base(pinCode, holder)
    {
    }

    public string CVC => (Int64.Parse(Number) % 987).ToString().PadLeft(3, '0');
    public decimal Balance { get; private set; }

    public ImmutableList<Transaction> Transactions => _transactions.ToImmutableList();

    public bool CanTransact(decimal amount)
    {
        return amount < Balance;
    }

    public string Identification => Number;

    /// <seealso cref="_transactions" />
    /// <exception cref="ArgumentException">If the transaction is not From or To the CreditCard</exception>
    public void AddTransaction(Transaction transaction)
    {
        if (transaction.From == this)
            Balance -= transaction.Amount;
        else if (transaction.To == this)
            Balance += transaction.Amount;
        else
            throw new ArgumentException(
                $"Can only add a transaction that is {nameof(Transaction.From)} or {nameof(Transaction.To)} this {nameof(CreditCard)}",
                nameof(transaction));

        _transactions.Add(transaction);
    }

    /// <param name="n">The amount you wish to withdraw</param>
    /// <returns>
    ///     <see cref="CreditCard.Balance" />
    /// </returns>
    /// <exception cref="InsufficientAccountBalanceException">
    ///     If you withdraw more money than you have on your card
    /// </exception>
    public decimal WithdrawMoney(decimal n)
    {
        if (!CanTransact(n))
            throw new InsufficientCardBalanceException(Balance);

        return Balance -= n;
    }
}