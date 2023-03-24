using Library.Models;

namespace Library.DTO
{
    public class CreateAuthorDTO
    {
        public string Name { get; set; } = null!;

        public int? PublisherId { get; set; }
    }
}
