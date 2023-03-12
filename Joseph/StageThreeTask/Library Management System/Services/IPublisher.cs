namespace Library_Management_System.Services
{
    public interface IPublisher
    {
        public void CreatePublisher(PublisherForCreation publisherDto);
        public List<PublisherForDisplay> GetAPublisher(string publisher);
        public IEnumerable<PublisherForDisplay> GetAllPublishers();
        public List<AuthorForDisplayDto> GetAuthorsAttachedToAPublisher(string publisherName);
    }
}
