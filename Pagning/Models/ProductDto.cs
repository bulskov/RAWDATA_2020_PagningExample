using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pagning.Models
{
    public class ProductDto
    {
        public string Url { get; set; }
        public string Name { get; set; }
        public string CategoryUrl { get; set; }
        public string QuantityPerUnit { get; set; }
        public int UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
    }
}
