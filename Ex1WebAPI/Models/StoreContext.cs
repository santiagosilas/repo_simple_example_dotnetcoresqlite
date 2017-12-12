using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore; // Entity Framework Core 
using System.Linq;
using System.Threading.Tasks;

namespace Ex1WebAPI.Models
{
    /// <summary>
    /// Classe que coordena as funcionalidades do EF para um dado modelo de dados (data model)
    /// Herda de Microsoft.EntityFrameworkCore.DbContext
    /// </summary>
    public class StoreContext: DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options): 
            base(options)
        {

        }
        public DbSet<Product> TodoItems { get; set; }
    }
}
