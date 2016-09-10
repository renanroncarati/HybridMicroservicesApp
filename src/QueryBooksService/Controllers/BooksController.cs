using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace QueryBooksService.Controllers
{
    [Route("api/[controller]")]
    public class BooksController : Controller
    {
        private static IList<Books> _books = new List<Books>(){
            new Books()
            {
                BookId = 1,
                Name = "Api for dummies",
                Author = "For Dummies Series"
            },
            new Books()
            {
                BookId = 2,
                Name = "French for dummies",
                Author = "Renan"
            }
            ,
            new Books()
            {
                BookId = 3,
                Name = "French",
                Author = "For Dummies Series"
            }
        };

        // GET api/values
        [HttpGet]
        public IEnumerable<Books> Get()
        {
            return _books;
        }

        // GET api/values/queryName
        [HttpGet("name/{queryName}")]
        public IEnumerable<Books> Get(string queryName)
        {
            return _books.Where(b => b.Name.ToLower().Contains(queryName.ToLower()));
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Books Get(int id)
        {
            return _books?.FirstOrDefault(b => b.BookId == id);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]Books book)
        {
            if (!_books.Any(b => b.BookId == book.BookId))
            {
                _books.Add(book);
            }

        }

        //example of content to post [{"bookId":2,"name":"French for dummies","author":"Renan"},{"bookId":3,"name":"French","author":"For Dummies Series"}]
        [HttpPost("addCart")]
        public void Post([FromBody]IEnumerable<Books> books)
        {
            //call ShoppingCartService
            QueryBookService.Clients.HttpClientProvider.PostContent("/shoppingcart", books.ToList());

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            //varifica se o livro existe
            if (_books.Any(b => b.BookId == id))
            {
                _books.Remove(_books.FirstOrDefault(b => b.BookId == id));
            }
        }
    }

    public class Books
    {
        public int BookId { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
    }
}
