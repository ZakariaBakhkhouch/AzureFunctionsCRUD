
namespace AzureFunctionsCRUD.Functions
{
    public class GetProduct
    {
        private readonly ApplicationDbContext _context;

        public GetProduct(ApplicationDbContext context)
        {
            _context = context;
        }

        [FunctionName("GetProduct")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "Get", Route = null)] HttpRequest req, ILogger log)
        {
            try
            {
                log.LogInformation("C# HTTP trigger function processed a request.");

                var id = int.Parse(req.Query["id"].ToString());

                var product = await _context.Products.FindAsync(id);

                ProductResponse productResponse = new();

                if (product is not null)
                {
                    productResponse.Message = "Product was found.";
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
