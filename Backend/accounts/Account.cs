using System.Collections.Immutable;
using Backend.customers;
using Backend.Transactions;
using Backend.Utils;

namespace Backend.accounts;

public abstract class Account : Base, ITransactionable, IComparable
{
    // ReSharper disable once MemberCanBePrivate.Global
    public const int NumberLength = 10;
    private readonly HashSet<Customer> _accessList = new();
    private readonly string _number;

    private readonly List<Transaction> _transactions = new();

    protected Account()
    {
        _number = GenerateAccountNumber();
    }

    /// <exception cref="ArgumentException">If you try to set with an invalid length</exception>
    protected Account(string number)
    {
        if (!IsValidAccountNumber(number))
            throw new ArgumentException($"{nameof(Account)} {nameof(Number)} invalid");

        _number = number;
    }

    public ImmutableHashSet<Customer> AccessList => _accessList.ToImmutableHashSet();

    public decimal Balance { get; protected set; }

    /// <exception cref="ArgumentException">If you try to set with an invalid length</exception>
    public virtual string Number
    {
        get => _number;
        init
        {
            if (!IsValidAccountNumber(value))
                throw new ArgumentException($"{nameof(Account)} {nameof(Number)} invalid");

            _number = value;
        }
    }

    /// <param name="id">
    ///     <see cref="Customer.Id">Customers Id</see>
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     If <see cref="Customer" /> is not connected to the <see cref="Account" />
    /// </exception>
    public Customer this[int id]
    {
        get
        {
            try
            {
                return _accessList.First(customer => customer.Id == id);
            }
            catch (InvalidOperationException)
            {
                throw new ArgumentOutOfRangeException($"No {nameof(Customer)} found with the given id {id}");
            }
        }
    }

    public string Identification => Number;

    public ImmutableList<Transaction> Transactions => _transactions.ToImmutableList();

    public bool CanTransact(decimal amount)
    {
        return amount < Balance;
    }

    /// <seealso cref="_transactions" />
    /// <exception cref="ArgumentException">If the transaction is not From or To the Account</exception>
    public void AddTransaction(Transaction transaction)
    {
        if (transaction.From == this)
            Balance -= transaction.Amount;
        else if (transaction.To == this)
            Balance += transaction.Amount;
        else
            throw new ArgumentException(
                $"Can only add a transaction that is {nameof(Transaction.From)} or {nameof(Transaction.To)} this {nameof(Account)}",
                nameof(transaction));

        _transactions.Add(transaction);
    }

    /// <seealso cref="NumberLength" />
    protected static string GenerateAccountNumber()
    {
        Random random = new();
        char[] number = new char[NumberLength];

        for (int i = 0; i < NumberLength; i++)
            number[i] = (char)((int)Math.Floor(random.NextDouble() * 10d) + 48);

        return new string(number);
    }

    /// <seealso cref="NumberLength" />
    /// <seealso cref="char.IsDigit(char)" />
    public static bool IsValidAccountNumber(string number)
    {
        return number.Length == NumberLength && number.All(Char.IsDigit);
    }

    /// <exception cref="ArgumentException">
    ///     When the <see cref="Customer" /> is already connected to the <see cref="Account" />
    /// </exception>
    public virtual void AddCustomer(Customer c)
    {
        if (!_accessList.Add(c))
            throw new ArgumentException($"The given {nameof(Customer)} already has access to this {nameof(Account)}");
    }

    /// <exception cref="ArgumentException">
    ///     When the <see cref="Customer" /> is not connected to the <see cref="Account" />
    /// </exception>
    public virtual void RemoveCustomer(Customer c)
    {
        if (!_accessList.Remove(c))
            throw new ArgumentException($"The given {nameof(Customer)} is not connected to this {nameof(Account)}");
    }

    public int CompareTo(object? obj)
    {
        if (obj is not Account account)
            throw new ArgumentException("Obj has to be of type " + nameof(Account), nameof(obj));

        return String.Compare(Number, account.Number, StringComparison.Ordinal);
    }
}