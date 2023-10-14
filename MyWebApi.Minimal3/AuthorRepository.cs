namespace MyWebApi.Minimal3
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly List<Author> _authors;
        public AuthorRepository()
        {
            _authors = new List<Author>()
            {
                new Author
                {
                    Id = 1,
                    FirstName = "Joydip",
                    LastName = "Kanjilal"
                },
                new Author
                {
                    Id = 2,
                    FirstName = "Steve",
                    LastName = "Smith"
                },
                new Author
                {
                    Id = 3,
                    FirstName = "Julie",
                    LastName = "Lerman"
                },
                new Author
                {
                    Id = 4,
                    FirstName = "Simon",
                    LastName = "Bisson"
                }
            };
        }

        public Author? GetAuthor(int id)
        {
            //var rtnValue = _authors.Find(x => x.Id == id);
            return _authors.Find(x => x.Id == id);
        }

        public List<Author> GetAuthors()
        {
            return _authors;
        }
    }
}
