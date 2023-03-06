using System.Net.Mail;
using System.Text.Json;
using Banking.models;

namespace Banking.Operations
{
    public static class BankingOps
    {
        public static void createUser()
        {
            string name,
                email,
                phone,
                password;
            bool status;
            try
            {
                Console.WriteLine("Name: \n");
                name = Console.ReadLine()!;
                while (HelperOps.ValidateFields(name, "name") == false)
                {
                    Console.WriteLine("Name is required. Please enter a valid name");
                    name = Console.ReadLine()!;
                }

                //Ensure user enters a valid email address
                do
                {
                    Console.WriteLine("Email: \n");
                    email = Console.ReadLine()!;
                    status = HelperOps.ValidateFields(email, "email");
                    if (!status)
                    {
                        Console.WriteLine("Please enter a valid email : \n");
                    }
                } while (!status);

                Console.WriteLine("Phone: \n");
                phone = Console.ReadLine()!;

                //Get password and hash it
                Console.WriteLine("Password: \n");
                password = Console.ReadLine()!;
                string encPassword = HelperOps.HashPassword(password);

                User newUser = new User()
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Email = email,
                    Phone = phone,
                    Password = encPassword,
                    Summary = new() { },
                    Created = DateTimeOffset.Now
                };

                if (File.Exists("data.txt"))
                { //TODO Check if user already exists
                    List<User> users = getUsers();
                    bool userExists = users.Any(user => user.Email == newUser.Email);

                    if (userExists)
                    {
                        Console.WriteLine(
                            "User with email {0} already exists\nTry again",
                            newUser.Email
                        );
                        createUser();
                    }

                    users.Add(newUser);
                    File.WriteAllText("data.txt", JsonSerializer.Serialize(users.ToArray()));
                }
                else
                {
                    List<User> userObject = new() { };
                    userObject.Add(newUser);
                    File.WriteAllText("data.txt", JsonSerializer.Serialize(userObject.ToArray()));
                }
                Console.WriteLine(
                    "Account setup successful!\nNow, would you like to make a deposit?\n1. Yes\n2. No\n"
                );
                var nextAction = Console.ReadLine();
                if (nextAction == "1")
                {
                    Transfer(newUser, TransactionType.Deposit);
                    Console.WriteLine(
                        "All Done!\nWould you like to do something else?\n1. Yes\n2. No"
                    );
                    var response = Console.ReadLine()!;
                    if (response == "1")
                    {
                        Program.Run();
                    }
                    else
                    {
                        Environment.Exit(0);
                    }
                }
                else
                {
                    Console.WriteLine("Goodbye");
                    Environment.Exit(0);
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static User? signIn(string email, string password)
        {
            List<User> users = getUsers();
            User? userMatch = users.FirstOrDefault(user => user.Email == email);

            if (userMatch != null)
            {
                bool passwordMatch = HelperOps.VerifyPassword(password, userMatch.Password);
                if (!passwordMatch)
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
            return userMatch;
        }

        public static void Transfer(User user, TransactionType type)
        {
            double amount;
            Transaction transaction;
            List<User> users = getUsers();
            int userIndex;

            switch (type)
            {
                case TransactionType.Deposit:
                    try
                    {
                        Console.WriteLine("Please enter Deposit Amount");
                        amount = Convert.ToDouble(Console.ReadLine()!);

                        transaction = new Transaction()
                        {
                            Id = Guid.NewGuid(),
                            Type = TransactionType.Deposit,
                            Amount = amount,
                            Balance = user.Balance + amount,
                            Created = DateTimeOffset.Now
                        };

                        if (user.Summary == null)
                        {
                            user.Summary = new() { };
                        }
                        user.Balance += amount;
                        user.Summary.Add(transaction);

                        userIndex = users.FindIndex(match => match.Email == user.Email);
                        users[userIndex] = user;

                        File.WriteAllText("data.txt", JsonSerializer.Serialize(users));
                        return;
                    }
                    catch (System.Exception ex)
                    {
                        Console.WriteLine("Oops. Something weent wrong: {}", ex);
                        return;
                    }

                case TransactionType.Withdrawal:
                    if (user.Summary == null)
                    {
                        Console.WriteLine(
                            "No transaction history on your account. Make a deposit first"
                        );
                        return;
                    }
                    Console.WriteLine("Enter withdrawal amount");
                    amount = Convert.ToDouble(Console.ReadLine());
                    if (amount > user.Balance)
                    {
                        Console.WriteLine("You do not have up to that amount, my friend");
                        return;
                    }

                    transaction = new()
                    {
                        Id = Guid.NewGuid(),
                        Amount = amount,
                        Type = TransactionType.Withdrawal,
                        Balance = user.Balance - amount,
                        Created = DateTimeOffset.Now
                    };

                    user.Balance -= amount;
                    user.Summary.Add(transaction);
                    userIndex = users.FindIndex(match => match.Email == user.Email);
                    users[userIndex] = user;

                    File.WriteAllText("data.txt", JsonSerializer.Serialize(users));

                    return;
            }
        }

        public static void DisplaySummary(User user)
        {
            List<Transaction> transactions = user.Summary;

            if (transactions.Count < 1)
            {
                Console.WriteLine("No transaction record yet");
                return;
            }

            try
            {
                Console.WriteLine("Here's a summary of your transactions:\n");
                for (int i = 0; i < transactions.Count; i++)
                {
                    Console.WriteLine(
                        "Date:{0}     Type:{1}    Amount:{2}    Balance:{3}",
                        transactions[i].Created.ToLocalTime(),
                        transactions[i].Type.ToString(),
                        transactions[i].Amount,
                        transactions[i].Balance
                    );
                }
                return;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("SOmethig went wrong: {0}", ex);
                return;
            }
        }

        public static List<User> getUsers()
        {
            string content = File.ReadAllText("data.txt");
            List<User> usersArray = JsonSerializer.Deserialize<List<User>>(content)!;
            return usersArray;
        }
    }
}
