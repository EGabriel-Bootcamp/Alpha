using Library_Management_System.Data;

namespace Library_Management_System.Services
{
    /// <summary>
    /// Provides implementation for the IPublisher interface
    /// </summary>
    public class PublisherService : IPublisher
    {
        private readonly ApplicationDbContext _context;

        public PublisherService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void CreatePublisher(PublisherForCreation publisherDto)
        {
            var publisher = new Publisher()
            {
                CopyRightLicense = publisherDto.CopyRightLicense,
                PublisherAddress = publisherDto.PublisherAddress,
                PublisherName = publisherDto.PublisherName,
            };

            _context.Publishers.Add(publisher);
            _context.SaveChanges();
        }

        public IEnumerable<PublisherForDisplay> GetAllPublishers()
        {
            var publisherDto = new List<PublisherForDisplay>();
            var publishers = _context.Publishers.Select(x=>x);
            foreach (var publisher in publishers)
            {
                publisherDto.Add(new PublisherForDisplay
                {
                    PublisherName = publisher.PublisherName,
                    PublisherAddress = publisher.PublisherAddress,
                });
            }

            return publisherDto;
        }

        public List<PublisherForDisplay> GetAPublisher(string publisherName)
        {
            var publishers = _context.Publishers
                                      .Where(x => x.PublisherName.Contains(publisherName))
                                      .Select(x => x).ToList();

            var publishersDto = new List<PublisherForDisplay>();
            //Convert to PublisherForDisplay obj
            foreach(var publisher in publishers)
            {
                publishersDto.Add(new PublisherForDisplay
                {
                    PublisherName = publisher.PublisherName,
                    PublisherAddress = publisher.PublisherAddress,
                });
            }

            return publishersDto;
        }

        public List<AuthorForDisplayDto> GetAuthorsAttachedToAPublisher(string publisherName)
        {
            var authors = _context.Authors
                                   .Where(x => x.Publisher.PublisherName.ToLower() == publisherName.ToLower())
                                   .Select(x => x).ToList();
            var authorDisplayDto = new List<AuthorForDisplayDto>();
            foreach(var author in authors)
            {
                authorDisplayDto.Add(new AuthorForDisplayDto
                {
                    AuthorDetails = author.AuthorDetails,
                    AuthorName = author.AuthorName,
                });
            }

            return authorDisplayDto;
        }
    }
}
