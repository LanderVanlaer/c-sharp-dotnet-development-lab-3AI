using System.Collections.Immutable;
using Backend.accounts;
using Backend.customers;
using Backend.Utils;

namespace Backend;

public class Bank : Base
{
    public readonly AccountsHandler Accounts;
    public readonly CustomersHandler Customers;

    public Bank()
    {
        Accounts = new AccountsHandler(this);
        Customers = new CustomersHandler(this);
    }

    public readonly struct CustomersHandler : IHandler<Customer, int>
    {
        private readonly Bank _bank;
        private readonly HashSet<Customer> _customers = new();

        internal CustomersHandler(Bank bank)
        {
            _bank = bank;
        }

        public ImmutableHashSet<Customer> All => _customers.ToImmutableHashSet();

        /// <param name="customerId">
        ///     <see cref="Customer.Id">Customer.Id</see>
        /// </param>
        /// <exception cref="InvalidOperationException">
        ///     see
        ///     <see cref="Enumerable.First{TSource}(System.Collections.Generic.IEnumerable{TSource})">Enumerable.First</see>
        /// </exception>
        public Customer this[int customerId]
        {
            get { return _customers.First(a => a.Id == customerId); }
        }

        /// <summary>Removes the customer from the Bank and all the connected <see cref="Bank.Accounts" /></summary>
        /// <param name="customer">The customer to remove</param>
        /// <exception cref="ArgumentException">When the customer was not recognized in the system</exception>
        public void Remove(Customer customer)
        {
            if (!_customers.Remove(customer))
                throw new ArgumentException($"the given {nameof(Customer)} is not recognized in the system");

            foreach (Account account in _bank.Accounts.All.Where(a =>
                         a.AccessList.Contains(customer)
                     )
                    )
                try
                {
                    account.RemoveCustomer(customer);
                }
                catch (ArgumentException)
                {
                }
        }

        public void Remove(int customerId)
        {
            Remove(this[customerId]);
        }

        public Customer Create()
        {
            Customer customer = new();
            _customers.Add(customer);
            return customer;
        }

        public Customer Create(string name, string contactPhoneNumber, DateTime birthDay)
        {
            Customer customer = new(name, contactPhoneNumber, birthDay);
            _customers.Add(customer);
            return customer;
        }
    }

    public readonly struct AccountsHandler : IHandler<Account, string>
    {
        private readonly HashSet<Account> _accounts = new();
        private readonly Bank _bank;

        internal AccountsHandler(Bank bank)
        {
            _bank = bank;
        }

        public ImmutableHashSet<Account> All => _accounts.ToImmutableHashSet();

        /// <param name="accountNumber">
        ///     <see cref="Account.Number">Account.Number</see>
        /// </param>
        /// <exception cref="InvalidOperationException">
        ///     see
        ///     <see cref="Enumerable.First{TSource}(System.Collections.Generic.IEnumerable{TSource})">Enumerable.First</see>
        /// </exception>
        public Account this[string accountNumber]
        {
            get { return _accounts.First(a => a.Number == accountNumber); }
        }

        /// <exception cref="ArgumentException">When the account was not recognized in the system</exception>
        public void Remove(Account account)
        {
            if (account.Balance > 0)
                throw new InvalidOperationException(
                    $"{nameof(Account)} balance too high, could not delete account with balance higher than 0");

            if (!_accounts.Remove(account))
                throw new ArgumentException($"The given {nameof(Account)} is not recognized in the system");
        }

        public void Remove(string accountNumber)
        {
            Remove(this[accountNumber]);
        }

        public CurrentAccount CreateCurrent()
        {
            CurrentAccount currentAccount = new();
            _accounts.Add(currentAccount);
            return currentAccount;
        }

        public SavingAccount CreateSaving()
        {
            SavingAccount savingAccount = new();
            _accounts.Add(savingAccount);
            return savingAccount;
        }
    }
}