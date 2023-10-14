using System.Collections.Immutable;
using Backend.accounts.exceptions;
using Backend.cards;
using Backend.customers;
using Backend.Utils;

namespace Backend.accounts;

public class CurrentAccount : Account
{
    public readonly CardsHandler Cards;

    internal CurrentAccount()
    {
        Cards = new CardsHandler(this);
    }

    /// <param name="n">The amount you wish to withdraw</param>
    /// <returns>
    ///     <see cref="Account.Balance" />
    /// </returns>
    /// <exception cref="InsufficientAccountBalanceException">
    ///     If you withdraw more money than you have in your account
    /// </exception>
    public decimal WithdrawMoney(decimal n)
    {
        if (Balance < n)
            throw new InsufficientAccountBalanceException(Balance);

        return Balance -= n;
    }

    /// <param name="n">The amount you wish to deposit</param>
    /// <returns>
    ///     <see cref="Account.Balance" />
    /// </returns>
    public decimal DepositMoney(decimal n)
    {
        return Balance += n;
    }

    public override void RemoveCustomer(Customer c)
    {
        base.RemoveCustomer(c);

        foreach (Card card in Cards.All.Where(card => card.Holder == c))
            Cards.Remove(card);
    }

    public readonly struct CardsHandler : IHandler<Card, string>
    {
        private readonly HashSet<Card> _cards = new();
        private readonly Account _account;

        internal CardsHandler(Account account)
        {
            _account = account;
        }

        public ImmutableHashSet<Card> All => _cards.ToImmutableHashSet();

        /// <param name="cardNumber">
        ///     <see cref="Card.Number">Card.Number</see>
        /// </param>
        /// <exception cref="InvalidOperationException">
        ///     see
        ///     <see cref="Enumerable.First{TSource}(System.Collections.Generic.IEnumerable{TSource})">Enumerable.First</see>
        /// </exception>
        public Card this[string cardNumber]
        {
            get { return _cards.First(a => a.Number == cardNumber); }
        }

        /// <exception cref="ArgumentException">When the Card was not recognized in the system</exception>
        public void Remove(Card card)
        {
            if (!_cards.Remove(card))
                throw new ArgumentException(
                    $"the given {nameof(Card)} is not connected to this {nameof(CurrentAccount)}");
        }

        /// <seealso cref="Remove(Card)" />
        /// <exception cref="ArgumentException">When cards balance is too high</exception>
        /// <exception cref="ArgumentException">see <see cref="Remove(Card)" /></exception>
        public void Remove(CreditCard card)
        {
            if (card.Balance != 0)
                throw new ArgumentException(
                    $"{nameof(Card)}s balance is too high, when removing a card from an {nameof(Account)}, the {nameof(Card)} has to have a balance of $0");

            Remove(card as Card);
        }

        public void Remove(string cardNumber)
        {
            Remove(this[cardNumber]);
        }

        public CreditCard CreateCreditCard(string pinCode, Customer holder)
        {
            if (!_account.AccessList.Contains(holder))
                throw new InvalidOperationException(
                    $"The given holder is not connected to the {nameof(CurrentAccount)}"
                );

            CreditCard createCredit = new(pinCode, holder);
            _cards.Add(createCredit);
            return createCredit;
        }

        public BankCard CreateBankCard(string pinCode, Customer holder)
        {
            if (!_account.AccessList.Contains(holder))
                throw new InvalidOperationException(
                    $"The given holder is not connected to the {nameof(CurrentAccount)}"
                );

            BankCard bankCard = new(pinCode, holder);
            _cards.Add(bankCard);
            return bankCard;
        }
    }
}