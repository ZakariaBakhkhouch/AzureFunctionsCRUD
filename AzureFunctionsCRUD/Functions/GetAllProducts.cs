
namespace AzureFunctionsCRUD.Functions
{
    public class GetAllProducts
    {
        private readonly ApplicationDbContext _context;

        public GetAllProducts(ApplicationDbContext context)
        {
            _context = context;
        }

        [FunctionName("GetAllProducts")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "GET", Route = "Products")] HttpRequest req, ILogger log)
        {
            try
            {
                log.LogInformation("C# HTTP trigger function processed a request.");

                var products = await _context.Products.ToListAsync();

                ProductsResponse responseModel = new();

                if (products.Count > 0)
                {
                    responseModel.Message = "List of products.";
                    responseModel.Date = DateTime.Now;
                    responseModel.Products = products;
                }
                else
                {
                    responseModel.Message = "No products was found.";
                    responseModel.Date = DateTime.Now;
                    responseModel.Products = null;
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
