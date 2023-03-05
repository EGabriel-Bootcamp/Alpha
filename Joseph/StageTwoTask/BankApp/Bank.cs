using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using System.IO;

namespace BankApp
{
    public static class Bank
    {
        // File to read and write from
        static readonly string  workingDirectory = Directory.GetCurrentDirectory();
        public static string loggedInUser = "";

        public static bool Login(string username, string password)
        {
            string file = Directory.GetParent(workingDirectory).Parent.Parent.FullName + @$"\Users\{username.ToLower()}.txt";
            if (!File.Exists(file))
            {
                WriteLine("User does not exist");
                return false;
            }
            string[] usersDetails = File.ReadAllLines(file);

            foreach (string item in usersDetails)
            {
                string[] linArr = item.Split(' ');
                if (linArr[1] == $"UserName:{username}" && linArr[2] == $"Password:{password}")
                {
                    loggedInUser = username;                    
                    return true;
                }
            }            
            return false;
        }

        public static void Signup(Account newUser)
        {
            string file = Directory.GetParent(workingDirectory).Parent.Parent.FullName + @$"\Users\{newUser.UserName.ToLower()}.txt";
            if (File.Exists(file))
            {
                WriteLine("User already exists");
                return;
            }
            //String formatted user data
            var userDetails = $"Email:{newUser.Email} UserName:{newUser.UserName} Password:{newUser.Password} Phone:{newUser.Phone} Age:{newUser.Age}";
            try
            {
                File.AppendAllText(file, userDetails);
                File.Create(Directory.GetParent(workingDirectory).Parent.Parent.FullName + @$"\Current Balance\{newUser.UserName.ToLower()}_ActDetails.txt");
                File.Create(Directory.GetParent(workingDirectory).Parent.Parent.FullName + @$"\Transaction History\{newUser.UserName.ToLower()}_history.txt");
                WriteLine("User account created successfully");
            }
            catch(Exception e)
            {
                WriteLine(e);
                WriteLine("Account creation was unsuccessful");
            }
        }

        public static double Deposit(double depositAmount)
        {
            string balance = Directory.GetParent(workingDirectory).Parent.Parent.FullName + @$"\Current Balance\{loggedInUser.ToLower()}_ActDetails.txt";
            var accountSummary = Directory.GetParent(workingDirectory).Parent.Parent.FullName + @$"\Transaction History\{loggedInUser.ToLower()}_history.txt";

            // Get user current balance
            var currentBalance = File.ReadAllText(balance);
            double newBalance;

            // On first deposit
            if (String.IsNullOrEmpty(currentBalance))
            {
                newBalance = depositAmount;
            }
            else
            {
                newBalance = Double.Parse(currentBalance) + depositAmount;
            }
            File.WriteAllText(balance, newBalance.ToString());

            // Save details to user account summary
            var summaryFormatedString = $"DepositDate {DateTime.Now} === Deposit Amount: {depositAmount} === CurrentBalance in Account: {newBalance}";
            File.AppendAllText(accountSummary, summaryFormatedString + Environment.NewLine);
            return newBalance;
        }

        public static string ViewBalance()
        {
            string file = Directory.GetParent(workingDirectory).Parent.Parent.FullName + @$"\Current Balance\{loggedInUser.ToLower()}_ActDetails.txt";
            var currentBalance = File.ReadAllText(file);

            // Checks if deposit has been made after account creation
            if (String.IsNullOrEmpty(currentBalance))
            {
                return "No Deposit has been made yet to this account. Please make a deposit";
            }
            else
            {
                return $"Current balance is ===> {currentBalance}";
            }
        }
        
        public static string Withdraw(double withdrawalAmount)
        {
            if (withdrawalAmount < 0 || withdrawalAmount.GetType() != typeof(double) ) 
            {
                return "Input is invalid";
            }
           
            string file = Directory.GetParent(workingDirectory).Parent.Parent.FullName + @$"\Current Balance\{loggedInUser.ToLower()}_ActDetails.txt";
            var accountSummary = Directory.GetParent(workingDirectory).Parent.Parent.FullName + @$"\Transaction History\{loggedInUser.ToLower()}_history.txt";

            var currentBalance = File.ReadAllText(file);
            double newBalance;

            // Checks if deposit has been made after account creation
            if (String.IsNullOrEmpty(currentBalance))
            {
                return "No Deposit has been made yet to this account. Please make a deposit";
            }
            // Makes withdrawal
            else
            {
                newBalance = Double.Parse(currentBalance) - withdrawalAmount;
                // Checks new balance if witdrawal is possible after the necessary deduction
                if (newBalance > 0)
                {
                    File.WriteAllText(file, newBalance.ToString());
                    // Save details to user account summary
                    var summaryFormatedString = $"DepositDate {DateTime.Now} === Withdrawal Amount: {withdrawalAmount} === CurrentBalance in Account: {newBalance}";
                    File.AppendAllText(accountSummary, summaryFormatedString + Environment.NewLine);
                    return $"Withdrawal success\n Withdrawal Amount: {withdrawalAmount} \t New Balance: {newBalance}";
                }
                else { return "Insufficient funds";  }
            }
        }
        
        public static string ViewAcctSummary()
        {
            var accountFile = Directory.GetParent(workingDirectory).Parent.Parent.FullName + @$"\Transaction History\{loggedInUser.ToLower()}_history.txt";
            var accountSummary = File.ReadAllText(accountFile);

            if (String.IsNullOrEmpty(accountSummary))
            {
                return "No Account History for this account";
            }
            return accountSummary;
        }
    }
}
