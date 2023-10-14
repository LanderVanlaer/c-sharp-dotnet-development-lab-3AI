using Backend.customers;

namespace Backend.accounts;

public class SavingAccount : Account
{
    internal SavingAccount() : base(GenerateAccountNumber())
    {
    }

    public override string Number
    {
        get => base.Number;
        init
        {
            if (!value.StartsWith("123"))
                throw new ArgumentException(
                    // ReSharper disable once StringLiteralTypo
                    $"The {nameof(Number)} of a {nameof(SavingAccount)} needs to follow the format 123xxxxxxx");
            base.Number = value;
        }
    }

    // ReSharper disable once MemberCanBePrivate.Global
    public new static string GenerateAccountNumber()
    {
        string number = Account.GenerateAccountNumber();
        return "123" + number[3..];
    }

    public override void AddCustomer(Customer c)
    {
        base.AddCustomer(c);

        if (!c.FirstSavingsAccount) return;
        Balance += 50;
        c.FirstSavingsAccount = false;
    }
}