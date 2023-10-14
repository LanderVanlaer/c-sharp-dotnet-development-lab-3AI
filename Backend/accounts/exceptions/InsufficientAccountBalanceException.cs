namespace Backend.accounts.exceptions;

public class InsufficientAccountBalanceException : Exception
{
    public InsufficientAccountBalanceException(decimal balance) : base(
        $"Insufficient Balance, you only have ${balance} left on your account")
    {
    }
}