using background_job_real_world_demo.Models;

namespace background_job_real_world_demo.Services;

public class ProductService : IProductService
{
    private readonly HttpClient _httpClient;

    public ProductService(HttpClient httpClient)
    {
        this._httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<IEnumerable<Product>> GetProducts()
    {
        await Task.Delay(5000);
        var products = await _httpClient.GetFromJsonAsync<IEnumerable<Product>>("https://api.restful-api.dev/objects");
        return products ?? Enumerable.Empty<Product>();
    }
}

public interface IProductService
{
    Task<IEnumerable<Product>> GetProducts();
}