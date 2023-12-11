
namespace AzureFunctionsCRUD.Functions
{
    public class DeleteProduct
    {
        private readonly ApplicationDbContext _context;

        public DeleteProduct(ApplicationDbContext context)
        {
            _context = context;
        }

        [FunctionName("DeleteProduct")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "DELETE", Route = "Product/id")] HttpRequest req, ILogger log)
        {
            try
            {
                log.LogInformation("C# HTTP trigger function processed a request.");

                var id = int.Parse(req.Query["id"].ToString());

                var product = await _context.Products.FindAsync(id);

                ProductResponse responseModel = new();

                if (product is not null)
                {
                    _context.Products.Remove(product);
                    await _context.SaveChangesAsync();

                    responseModel.Message = "The product was deleted.";
                    responseModel.Date = DateTime.Now;
                    responseModel.Product = product;
                }
                else
                {
                    responseModel.Message = "The product was no found.";
                    responseModel.Date = DateTime.Now;
                    responseModel.Product = null;
                }

                return new OkObjectResult(responseModel);
            }
            catch (Exception me)
            {
                return new BadRequestObjectResult(me.Message);
            }
        }
    }
}
