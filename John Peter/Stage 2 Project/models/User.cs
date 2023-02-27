using System.ComponentModel.DataAnnotations;

namespace Banking.models
{
    public record User
    {
        // public User(
        //     Guid id,
        //     string name,
        //     string email,
        //     string phone,
        //     string password,
        //     DateTimeOffset created
        // ) =>
        //     (Id, Name, Email, Phone, Password, Created) = (
        //         id,
        //         name,
        //         email,
        //         phone,
        //         password,
        //         created
        //     );

        public Guid Id { get; init; }

        public string Name { get; set; }

        //
        public string Email { get; set; }

        public string Phone { get; set; }

        public string Password { get; set; }
        public double Balance { get; set; }

        public List<Transaction> Summary { get; set; }

        public DateTimeOffset Created { get; set; }
    }
}
