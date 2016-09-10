using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ShoppingCartService.Controllers
{
    [Route("api/[controller]")]
    public class ShoppingCartController : Controller
    {
        private static IList<Cart> _carts = new List<Cart>();
        // private static IList<Books> _reservedBooks = new List<Books> (){
        //     new Books()
        //     {
        //         BookId = 1,
        //         Name = "Api for dummies",
        //         Author = "For Dummies Series"
        //     },
        //     new Books()
        //     {
        //         BookId = 2,
        //         Name = "French for dummies",
        //         Author = "Renan"
        //     }
        //     ,
        //     new Books()
        //     {
        //         BookId = 3,
        //         Name = "French",
        //         Author = "For Dummies Series"
        //     }
        // };

        // GET api/ShoppingCart
        [HttpGet]
        public IEnumerable<Cart> Get()
        {
            return _carts;
        }

        // GET api/ShoppingCart/5
        [HttpGet("{id}")]
        public Cart Get(int id)
        {
            return _carts.FirstOrDefault(c => c.CartId == id);
        }

        // POST api/ShoppingCart example of content to post [{"bookId":2,"name":"French for dummies","author":"Renan"},{"bookId":3,"name":"French","author":"For Dummies Series"}]
        [HttpPost]
        public void Post([FromBody]IEnumerable<Books> books)
        {
            var cart = new Cart().Create();

            foreach (var book in books)
            {
                cart.ReservedBooks.Add(book);
            }

            if (!_carts.Any(c => c.CartId == cart.CartId))
            {  
                _carts.Add(cart);
            }

        }

        [HttpPost("checkout/{id}")]
        public void Post(int id)
        {
            bool canCheckout = true;
            //TODO call the CheckoutService
            //delete the cart
            if(canCheckout)
            {
                RemoveCart(id);
            }
            
        }

        // DELETE api/ShoppingCart/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            RemoveCart(id);
        }

        private static void RemoveCart(int id)
        {
            if (_carts.Any(c => c.CartId == id))
            {
                _carts.Remove(_carts.First(c => c.CartId == id));
            }

        
        }

    }

    public class Cart
    {
        public int CartId { get; set; }
        public IList<Books> ReservedBooks { get; set; }

        public Cart Create ()
        {
          return new Cart 
                    {
                        CartId = new Random().Next(1000,3000),
                        ReservedBooks = new List<Books>()
                    };
        }

    }

    public class Books
    {
        public int BookId { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
    }

}
