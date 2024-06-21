using System;

public class BankAccount
{
    public int AccountNumber { get; private set; }
    public decimal Balance { get; private set; }

    public BankAccount(int accountNumber, decimal initialBalance)
    {
        AccountNumber = accountNumber;
        Balance = initialBalance;
        Logger.Instance.Log($"Account {AccountNumber} created with initial balance {Balance:C}");
    }

    public void Deposit(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Deposit amount must be positive");
        }

        Balance += amount;
        Logger.Instance.Log($"Deposited {amount:C} to account {AccountNumber}. New balance: {Balance:C}");
    }

    public void Withdraw(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Withdraw amount must be positive");
        }

        if (amount > Balance)
        {
            Logger.Instance.Log($"Attempt to withdraw {amount:C} from account {AccountNumber} failed due to insufficient funds");
            throw new InvalidOperationException("Insufficient funds");
        }

        Balance -= amount;
        Logger.Instance.Log($"Withdrew {amount:C} from account {AccountNumber}. New balance: {Balance:C}");
    }
}

public sealed class Logger
{
    private static readonly Lazy<Logger> lazyInstance = new Lazy<Logger>(() => new Logger());

    public static Logger Instance => lazyInstance.Value;

    private Logger()
    {
    }

    public void Log(string message)
    {
        Console.WriteLine($"[{DateTime.Now}] {message}");
    }
}


public class Program
{
    public static void Main()
    {
        try
        {
            BankAccount account = new BankAccount(12345, 1000.00m);
            account.Deposit(200.00m);
            account.Withdraw(50.00m);
            account.Withdraw(1500.00m); 
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
