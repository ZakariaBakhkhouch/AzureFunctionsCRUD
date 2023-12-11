using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFunctionsCRUD.Models
{

    public class ProductsResponse
    {
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public List<Product> Products { get; set; }
    }
    
    public class ProductResponse
    {
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public Product Product { get; set; }
    }
}
