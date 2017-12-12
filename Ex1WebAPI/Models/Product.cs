using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ex1WebAPI.Models
{
    /// <summary>
    /// Product (Id, Name, IsAvailable)
    /// </summary>
    public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsAvailable { get; set; }
    }
}
