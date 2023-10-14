namespace MyWebApi.Minimal3
{
    public interface IAuthorRepository
    {
        public List<Author> GetAuthors();
        public Author? GetAuthor(int id);
    }
}
