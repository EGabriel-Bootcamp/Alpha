namespace Library_Management_System.Models.Dtos
{
    /// <summary>
    /// Dto used for displaying books by authors when an author details are to be displayed
    /// </summary>
    public class BookForAuthorDIsplayDto
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string ISBNNumber { get; set; }

        public string Version { get; set; }
    }
}
