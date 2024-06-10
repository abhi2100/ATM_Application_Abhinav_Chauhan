using System;
using System.Collections.Generic;

public class Account
{
    public int AccountNumber { get; set; }
    public double Balance { get; set; }
    public double InterestRate { get; set; }
    public string AccountHolderName { get; set; }
    public List<string> Transactions { get; set; }

    public Account(int accountNumber, double initialBalance, double interestRate, string accountHolderName)
    {
        AccountNumber = accountNumber;
        Balance = initialBalance;
        InterestRate = interestRate;
        AccountHolderName = accountHolderName;
        Transactions = new List<string>();
    }

    public void Deposit(double amount)
    {
        Balance += amount;
        Transactions.Add("Amount Deposited: " + amount);
    }

    public bool Withdraw(double amount)
    {
        if (Balance >= amount)
        {
            Balance -= amount;
            Transactions.Add("Amount Withdrawn: " + amount);
            return true;
        }
        else
        {
            return false;
        }
    }

    public double CheckBalance()
    {
        return Balance;
    }

    public void DisplayTransactions()
    {
        Console.WriteLine("Name of account holder: " + AccountHolderName);
        Console.WriteLine("Account Number: " + AccountNumber);
        Console.WriteLine("Account Interest Rate: " + InterestRate);
        Console.WriteLine("Balance Left: " + Balance);  // Change here
        foreach (string transaction in Transactions)
        {
            Console.WriteLine(transaction);
        }
        Console.WriteLine();  // Added a line break here
    }
}

public class Bank
{
    private List<Account> accounts;

    public Bank()
    {
        accounts = new List<Account>();
        for (int i = 100; i < 110; i++)
        {
            accounts.Add(new Account(i, 100, 0.03, "Default Account"));
        }
    }

    public Account RetrieveAccount(int accountNumber)
    {
        foreach (Account account in accounts)
        {
            if (account.AccountNumber == accountNumber)
            {
                return account;
            }
        }
        return null;
    }

    public void AddAccount(Account account)
    {
        accounts.Add(account);
    }
}

public class AtmApplication
{
    private Bank bank;

    public AtmApplication()
    {
        bank = new Bank();
    }

    public void Run()
    {
        Console.WriteLine("Welcome to the ATM Application\n");  // Added a line break here
        while (true)
        {
            Console.WriteLine("Choose the following options by the number associated with the option");
            Console.WriteLine("1. Create Account\n2. Select Account\n3. Exit\n");  // Added a line break here
            int choice = Convert.ToInt32(Console.ReadLine());

            if (choice == 1)
            {
                Console.Write("Enter account holder name: ");
                string accountHolderName = Console.ReadLine();
                Console.Write("Enter account number: ");
                int accountNumber = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter initial balance: ");
                double initialBalance = Convert.ToDouble(Console.ReadLine());
                Console.Write("Enter interest rate: ");
                double interestRate = Convert.ToDouble(Console.ReadLine());

                Account newAccount = new Account(accountNumber, initialBalance, interestRate, accountHolderName);
                bank.AddAccount(newAccount);
                Console.WriteLine("Account created successfully.\n");  // Added a line break here
            }
            else if (choice == 2)
            {
                Console.Write("Enter account number: ");
                int accountNumber = Convert.ToInt32(Console.ReadLine());
                Account account = bank.RetrieveAccount(accountNumber);

                if (account != null)
                {
                    Console.WriteLine("Welcome to ABC bank Mr. " + account.AccountHolderName);
                    while (true)
                    {
                        Console.WriteLine("\nWhat do you want to do next?");  // Added a line break here
                        Console.WriteLine("1. Check Balance\n2. Deposit\n3. Withdraw\n4. Display Transactions\n5. Exit Account\n");  // Added a line break here
                        int accountChoice = Convert.ToInt32(Console.ReadLine());

                        if (accountChoice == 1)
                        {
                            Console.WriteLine("Balance Left: " + account.CheckBalance() + "\n");  // Added a line break here
                        }
                        else if (accountChoice == 2)
                        {
                            Console.Write("Enter deposit amount: ");
                            double depositAmount = Convert.ToDouble(Console.ReadLine());
                            account.Deposit(depositAmount);
                        }
                        else if (accountChoice == 3)
                        {
                            Console.Write("Enter withdrawal amount: ");
                            double withdrawalAmount = Convert.ToDouble(Console.ReadLine());
                            bool success = account.Withdraw(withdrawalAmount);

                            if (success)
                            {
                                Console.WriteLine("Withdrawal successful.\n");  // Added a line break here
                            }
                            else
                            {
                                Console.WriteLine("Insufficient balance.\n");  // Added a line break here
                            }
                        }
                        else if (accountChoice == 4)
                        {
                            account.DisplayTransactions();
                        }
                        else if (accountChoice == 5)
                        {
                            break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Account not found.\n");  // Added a line break here
                }
            }
            else if (choice == 3)
            {
                Console.WriteLine("Do you want to exit? [y/n]");
                string exitChoice = Console.ReadLine();
                if (exitChoice.ToLower() == "y")
                {
                    Console.WriteLine("Thank you for using the Application.");
                    break;
                }
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        AtmApplication atmApplication = new AtmApplication();
        atmApplication.Run();
    }
}
