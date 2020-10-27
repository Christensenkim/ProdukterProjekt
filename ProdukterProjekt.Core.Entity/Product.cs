using System;
using System.Collections.Generic;
using System.Text;

namespace ProdukterProjekt.Core.Entity
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Color { get; set; }
        public string Ptype { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
