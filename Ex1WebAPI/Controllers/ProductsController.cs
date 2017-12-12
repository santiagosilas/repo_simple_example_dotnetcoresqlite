using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Ex1WebAPI.Models;

namespace Ex1WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Products")]
    public class ProductsController : Controller
    {
        private readonly StoreContext _context;

        /// <summary>
        /// This constructor uses Dependency injection to inject the 
        /// database context (ProductContext) into the controller
        /// The Database context will be used in the CRUD methods in the controller
        /// </summary>
        /// <param name="context"></param>
        public ProductsController(StoreContext context)
        {
            this._context = context;
            if(this._context.ProductItems.Count() == 0)
            {
                this._context.ProductItems.Add(new Product { Name = "Abacate"});
                this._context.SaveChanges();
            }
        }

        /// <summary>
        /// GET /api/products
        /// [HttpGet] attribute specifies an HTTP GET method
        /// </summary>
        /// <returns>IEnumerable<Product></returns>
        [HttpGet]
        public IEnumerable<Product> GetAll()
        {
            return this._context.ProductItems.ToList();
        }

        /// <summary>
        /// GET /api/products/{id}
        /// {id}: a placeholder variable for the ID
        /// Name = "GetTodo": To create a named route (useful to enable the app to create an HTTP link)
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>IactionResult: it represents a wide range of return types</returns>
        [HttpGet("{id}", Name = "GetProduct")]
        public IActionResult GetById(long Id)
        {
            // Obtém o produto
            var product = this._context.ProductItems.FirstOrDefault(p => p.Id == Id);

            if (product == null)
                return NotFound(); // 404 error (HTTP 404 response)

            // Returns 200 with a JSON response body
            return new ObjectResult(product);
        }

        /// <summary>
        /// [HttpPost]: HTTP Post Method
        /// [FromBody]: Get the value from the body of the HTTP request
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create([FromBody] Product product)
        {
            if (product == null)
                return BadRequest();

            this._context.ProductItems.Add(product);
            this._context.SaveChanges();

            // Returns a 201 response (the standard response for an HTTP POST method that creates 
            // a new resource on the server)
            // Uses the "GetProduct" named route to create the URL. 
            return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
        }

        /// <summary>
        /// Update uses HTTP PUT
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Product product)
        {
            if(product == null || product.Id != id)
                return BadRequest();
            var p = this._context.ProductItems.FirstOrDefault(pr => pr.Id == id);
            if (p == null)
                return NotFound();
            p.IsAvailable = product.IsAvailable;
            p.Name = product.Name;

            this._context.ProductItems.Update(p);
            this._context.SaveChanges();

            // The response is 204 (No Content)
            return new NoContentResult();
            
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var p = _context.ProductItems.FirstOrDefault(pr => pr.Id == id);
            if (p == null) return NotFound();
            this._context.ProductItems.Remove(p);
            this._context.SaveChanges();

            // Delete Response is 204 (No Content)
            return new NoContentResult();
        }



    }
}