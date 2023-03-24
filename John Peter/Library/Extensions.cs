using Library.DTO;
using Library.Models;

namespace Library
{
    public static class Extensions
    {
        public static PublisherDTO AsIs(this Publisher publisher)
        {
            return new PublisherDTO
            {
                Id = publisher.Id,
                Name = publisher.Name,
                // Authors = publisher.Authors
            };
        }

        public static AuthorDTO AuthorList(this Author author)
        {
            if (author is null)
            {
                throw new ArgumentNullException(nameof(author));
            }

            return new AuthorDTO
            {
                Id = author.Id,
                Name = author.Name,
                PublisherName = author.Publisher.Name,
                Books = author.Books.ToList()
            };
        }

        public static BookDTO BookList(this Book book)
        {
            return new BookDTO
            {
                Id = book.Id,
                Title = book.Title,
                Authors = book.Authors
            };
        }
    }
}
