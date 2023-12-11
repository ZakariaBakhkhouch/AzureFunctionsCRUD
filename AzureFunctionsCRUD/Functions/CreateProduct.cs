
namespace AzureFunctionsCRUD.Functions
{
    public class CreateProduct
    {
        private readonly ApplicationDbContext _context;

        public CreateProduct(ApplicationDbContext context)
        {
            _context = context;
        }

        [FunctionName("CreateProduct")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "POST", Route = "Product")] HttpRequest req, ILogger log)
        {
            try
            {
                log.LogInformation("C# HTTP trigger function processed a request.");

                string name = req.Query["name"];

                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

                Product product = JsonConvert.DeserializeObject<Product>(requestBody);

                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();

                ProductResponse responseModel = new();

                responseModel.Message = "The product was created.";
                responseModel.Date = DateTime.Now;
                responseModel.Product = product;

                return new OkObjectResult(responseModel);
            }
            catch(Exception me)
            {
                return new BadRequestObjectResult(me.Message);
            }
        }
    }
}
