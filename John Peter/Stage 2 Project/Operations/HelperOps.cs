using System.Net.Mail;
using BCrypt.Net;

namespace Banking.Operations
{
    public class HelperOps
    {
        public static string HashPassword(string password)
        {
            string hash = BCrypt.Net.BCrypt.HashPassword(password);
            return hash;
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            bool isMatch = BCrypt.Net.BCrypt.Verify(password, hashedPassword);
            return isMatch;
        }

        public static bool ValidateFields(string variable, string field)
        {
            bool status = true;
            switch (field)
            {
                case "email":
                    return IsValid(variable);
                case "name":

                    return string.IsNullOrEmpty(variable) || variable.Any(char.IsDigit)
                        ? false
                        : true;

                case "password":
                    return string.IsNullOrEmpty(variable) ? false : true;
            }
            return status;
        }

        private static bool IsValid(string email)
        {
            var valid = true;

            try
            {
                var emailAddress = new MailAddress(email);
            }
            catch
            {
                valid = false;
            }

            return valid;
        }
    }
}
