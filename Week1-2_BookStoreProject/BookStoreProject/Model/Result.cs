namespace BookStoreProject.Model
{
    public class Result
    {
        public int status { get; set; }
        public string message { get; set; }
        public List<Book> BookList { get; set; }
    }
}
