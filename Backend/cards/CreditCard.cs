using Backend.accounts.exceptions;
using Backend.cards.exceptions;
using Backend.customers;

namespace Backend.cards;

public class CreditCard : Card
{
    internal CreditCard(string pinCode, Customer holder) : base(pinCode, holder)
    {
    }

    public string CVC => (Int64.Parse(Number) % 987).ToString().PadLeft(3, '0');
    public decimal Balance { get; private set; }

    /// <param name="n">The amount you wish to withdraw</param>
    /// <returns>
    ///     <see cref="CreditCard.Balance" />
    /// </returns>
    /// <exception cref="InsufficientAccountBalanceException">
    ///     If you withdraw more money than you have on your card
    /// </exception>
    public decimal WithdrawMoney(decimal n)
    {
        if (Balance < n)
            throw new InsufficientCardBalanceException(Balance);

        return Balance -= n;
    }
}