using Banking.models;
using Banking.Operations;

namespace Banking
{
    class Program
    {
        // private public Program() { }

        public static void Main(string[] args)
        {
            Run();
        }

        public static void Operations(User user)
        {
            string response;
            Console.WriteLine(
                "Welcome, {0}\nWhat would you like to do today?\n1. Make a Deposit\n2. Make a withdrawal\n3. View account summary",
                user.Name
            );
            int choice = Convert.ToInt32(Console.ReadLine()!);
            switch (choice)
            {
                case 1:
                    BankingOps.Transfer(user, TransactionType.Deposit);
                    Console.WriteLine(
                        "All DOne!\nWould you like to do something else?\n1. Yes\n2. No"
                    );
                    response = Console.ReadLine()!;
                    if (response == "1")
                    {
                        Run();
                    }
                    else
                    {
                        Environment.Exit(0);
                    }
                    break;

                case 2:
                    BankingOps.Transfer(user, TransactionType.Withdrawal);
                    Console.WriteLine(
                        "All Done!\nWould you like to do something else?\n1. Yes\n2. No"
                    );
                    response = Console.ReadLine()!;
                    if (response == "1")
                    {
                        Run();
                    }
                    else
                    {
                        Environment.Exit(0);
                    }
                    break;

                case 3:
                    BankingOps.DisplaySummary(user);
                    Console.WriteLine(
                        "All Done!\nWould you like to do something else?\n1. Yes\n2. No"
                    );
                    response = Console.ReadLine()!;
                    if (response == "1")
                    {
                        Run();
                    }
                    else
                    {
                        Environment.Exit(0);
                    }
                    break;
            }
        }

        private static User? signIn()
        {
            string email,
                password;
            bool validityStatus;
            do
            {
                Console.WriteLine("Enter your email");
                email = Console.ReadLine()!;
                validityStatus = HelperOps.ValidateFields(email, "email");
                if (!validityStatus)
                {
                    Console.WriteLine("Please enter a valid email : \n");
                }
            } while (!validityStatus);

            Console.WriteLine("Enter your password");
            password = Console.ReadLine()!;
            while (!HelperOps.ValidateFields(password, "password"))
            {
                Console.WriteLine("Password is required. Please enter password");
                password = Console.ReadLine()!;
            }

            User user = BankingOps.signIn(email, password);
            if (user == null)
            {
                Console.WriteLine("Wrong email/password combination.\nTry again?\n1. Yes\n2. No");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        signIn();
                        break;

                    case "2":
                        Environment.Exit(0);
                        break;
                }
            }
            return user;
        }

        public static void Run()
        {
            Console.WriteLine(
                "Welcome to V Bank.\nHow would you like to proceed?\n1. Sign In\n2. Register\n"
            );
            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:

                    User user = signIn()!;
                    Operations(user);

                    break;
                case 2:
                    BankingOps.createUser();

                    break;
            }
        }
    }
}
