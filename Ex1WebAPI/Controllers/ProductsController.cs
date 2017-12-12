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
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Product> GetAll()
        {
            return this._context.ProductItems.ToList();
        }

        /// <summary>
        /// GET /api/products/{id}
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetProduct")]
        public IActionResult GetById(long Id)
        {
            // Obtém o produto
            var product = this._context.ProductItems.FirstOrDefault(p => p.Id == Id);

            if (product == null)
                return NotFound();

            return new ObjectResult(product);
        }

    }
}