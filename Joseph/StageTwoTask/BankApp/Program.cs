// See https://aka.ms/new-console-template for more information

using static System.Console;
using BankApp;
using System.Numerics;

string workingDirectory = Directory.GetCurrentDirectory();
string currentDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
Directory.CreateDirectory(currentDirectory + @"\Users");
Directory.CreateDirectory(currentDirectory + @"\Current Balance");
Directory.CreateDirectory(currentDirectory + @"\Transaction History");


while (true)
{
    if (Bank.loggedInUser != "")
    {
        Console.WriteLine("Please select an opion to proceed");
        Console.WriteLine("1. Deposit \n2. View Balance \n3. Withdraw \n4.View Account Summary \n5.Logout");


        var userSelect = Int32.TryParse(ReadLine(), out var userSelection);
        //Validates user selection
        if (userSelect)
        {
            switch (userSelection)
        {
            //Deposit
            case 1:
                while (true)
                {
                    Write("Enter amount to deposit");
                    Write("Press q to exit");

                    // Validate user input
                    var userInput = ReadLine();
                    var validateInput = Double.TryParse(userInput, out var value);
                    // Checks is user wants to quit deposit
                    if (!validateInput && Convert.ToChar(userInput).ToString().ToLower() == "q")
                    {
                        break;
                    }
                    // Checks if value is an integer
                    else if (!validateInput)
                    {
                        WriteLine("Invalid deposit amount");
                    }
                    if (value >= 0 && value.GetType() == typeof(double))
                    {
                        var balance = Bank.Deposit(value);
                        WriteLine("Deposit successful");
                        WriteLine($"Your current balance is {balance}");
                    }
                    else { WriteLine("depositAmount invalid"); }
                }
                break;
            //ViewBalance
            case 2:
                var acctBalance = Bank.ViewBalance();
                WriteLine(acctBalance);
                break;
            //Withdraw
            case 3:
                Write("Enter amount to withdraw");
                
                // Validates user input
                var validateWithdrawal = Double.TryParse(ReadLine(), out var withdrawalAmount);
                if (!validateWithdrawal)
                {
                    WriteLine("Invalid deposit amount");
                }
                else
                {
                    var withdrawalStatus = Bank.Withdraw(withdrawalAmount);
                    WriteLine(withdrawalStatus);
                }
                break;
            //Account Summary
            case 4:
                var accountSummary = Bank.ViewAcctSummary();
                WriteLine(accountSummary);
                break;
             // Logout
            case 5:
                Bank.loggedInUser = "";
                break;
            default:
                WriteLine("Invalid input");
                break;
        }
        }
        else
        {
            WriteLine("Invalid selction");
        }
        
    }
    else
    {
        Console.WriteLine("Please select an opion to proceed");
        Console.WriteLine("1. Login to your account \n2. Signup for an account");


        var userSelect = Int32.TryParse(ReadLine(), out var userSelection);
        //Validate user input
        if (userSelect)
        {
            switch (userSelection)
        {   
            case 1:
                var count = 1;

                // Limit login trial to 3
                do
                {
                    Write("Username:  ");
                    var userName =  ReadLine();
                    Write("Password:  ");
                    var password = ReadLine();

                    var loginStatus = Bank.Login(userName, password);
                    if (loginStatus)
                    {
                        WriteLine("Login Successful");
                        break;
                    }
                    WriteLine("Login Unsucccesful");
                    count++;
                    if (count == 4) { WriteLine("Max number of trials exceeded"); }
                    WriteLine();
                } while (count <= 3);
                break; 
            case 2:
                // User instructions
                Write("Email: ");
                var email = ReadLine();
                Write("Username: ");
                var username = ReadLine();
                Write("Age: ");
                var age = Int32.Parse(ReadLine());
                Write("Phone: ");
                var phone = ReadLine();
                Write("Password: ");
                var pass = ReadLine();

                var userDetails = new Account {
                    Email= email,
                    Age = age,
                    UserName = username,
                    Password = pass,
                    Phone = phone,
                };
                Bank.Signup(userDetails);
                break;
            default:
                WriteLine("Invalid selection");
                return 0;
        }
        }
        else
        {
            WriteLine("Invalid input");
        }
    }
}