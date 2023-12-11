
namespace AzureFunctionsCRUD.Functions
{
    public class UpdateProduct
    {
        private readonly ApplicationDbContext _context;

        public UpdateProduct(ApplicationDbContext context)
        {
            _context = context;
        }

        [FunctionName("UpdateProduct")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "PUT", Route = null)] HttpRequest req, ILogger log)
        {
            try
            {
                log.LogInformation("C# HTTP trigger function processed a request.");

                var id = int.Parse(req.Query["id"].ToString());

                var product = await _context.Products.FindAsync(id); 

                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

                Product _product = JsonConvert.DeserializeObject<Product>(requestBody);

                ProductResponse productResponse = new();

                if (product is not null)
                {
                    product.Name = _product.Name;
                    product.Price = _product.Price;

                    _context.Products.Update(product);
                    await _context.SaveChangesAsync();

                    productResponse.Message = "Product was updated.";
                    productResponse.Date = DateTime.Now;
                    productResponse.Product = product;
                }
                else
                {
                    productResponse.Message = "Product was not found.";
                    productResponse.Date = DateTime.Now;
                    productResponse.Product = null;
                }

                return new OkObjectResult(productResponse);
            }
            catch (Exception me)
            {
                return new BadRequestObjectResult(me.Message);
            }
        }
    }
}
