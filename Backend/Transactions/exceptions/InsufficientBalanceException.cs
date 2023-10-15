namespace Backend.Transactions.exceptions;

public class InsufficientBalanceException : InvalidOperationException
{
    public InsufficientBalanceException() : base("Insufficient Balance")
    {
    }
}