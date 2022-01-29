using BookStoreProject.DBOperations;
using BookStoreProject.Model;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreProject.Controllers
{

    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        Logger logger = new Logger();
        Result result = new Result();
        /* private static List<Book> BookList = new List<Book>()
         {
             new Book{Id = 1, Title = "Lean StartUp", Genre=1, PaperCount = 200, PublishDate = new DateTime(2001,06,12)},
             new Book{Id = 2, Title ="Dune", Genre = 2, PaperCount = 250, PublishDate = new DateTime(2010,05,23) },
             new Book{Id = 3, Title = "Herland", Genre = 2, PaperCount = 540, PublishDate = new DateTime(2001,12,21)}
         }; */

        private readonly BookStoreDbContext _context;

        public BookController(BookStoreDbContext context)
        {
            _context = context;
        }

        

        [HttpGet]
        public List<Book> GetBooks()
        {
            var bookList = _context.Books.OrderBy(x => x.Title).ToList<Book>();
            return bookList;
        }
       
        /// <summary>
        /// HttpGet request with id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public Book getById(int id)
        {
            var book = _context.Books.Where(book => book.Id == id).SingleOrDefault();
            return book;
        }

        /// <summary>
        /// Requestler yapılırken sadece bir request çağrımı parametresiz olarak-[HttpGet]- çağırabilir.
        /// İstenilen id değeri ile Book listeleme.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
       /* [HttpGet]
        public Book getById([FromQuery] string id)
        {
            var book = BookList.Where(book => book.Id == Convert.ToInt32(id)).SingleOrDefault();
            return book;
        } */

        //HtppPost ile veri eklenmesi
        [HttpPost]
        public IActionResult AddBook([FromBody] Book newBook)
        {
            var book = _context.Books.SingleOrDefault(x=>x.Title == newBook.Title);

            //Eğer kitap benim listemde mevcut ise listeme eklememesi için 'is not null' olarak BadRequest çağırırz.
            if(book is not null)
                return BadRequest();

            _context.Books.Add(newBook);
            _context.SaveChanges();
            return Ok();
        }

        //HttpPut ile BookList içindeki id değeri ile eşleşen nesnenin güncellenmesi
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] Book updatedBook)
        {
            var book = _context.Books.SingleOrDefault(x => x.Id == id);
            if (book is null)
                return BadRequest();

            book.Genre = updatedBook.Genre != default ? updatedBook.Genre : book.Genre;
            if (updatedBook.Title != "string") book.Title = updatedBook.Title;
            book.PaperCount = updatedBook.PaperCount != default ? updatedBook.PaperCount : book.PaperCount; 
            book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;

            _context.SaveChanges();
            return Ok();
        }

        //HttpDelete ile BookList içindeki id değeri ile eşleşen nesnenin silinmesi.
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var Book = _context.Books.SingleOrDefault(x=>x.Id == id);
            if (Book is null)
            {
                result.status = 0;
                result.message = "Kullanici silinemedi.";
                logger.createLog(id.ToString() + "ID li " + result.message);
                result.BookList = _context.Books.ToList();
                return BadRequest();
            }
            _context.Books.Remove(Book);
            _context.SaveChanges();
            result.status = 1;
            result.message = "Kullanici silindi.";
            result.BookList = _context.Books.ToList();
            logger.createLog(id.ToString() + "ID li " + result.message);
            return Ok();

         }
    }
}