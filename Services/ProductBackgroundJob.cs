
namespace background_job_real_world_demo.Services;

public class ProductBackgroundJob : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IProductCache _productCache;

    public ProductBackgroundJob(IServiceProvider serviceProvider, IProductCache productCache)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        _productCache = productCache ?? throw new ArgumentNullException(nameof(productCache));
    }

    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        await base.StartAsync(cancellationToken);
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await base.StopAsync(cancellationToken);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await UpdateProductsCache();
            await Task.Delay(1000, stoppingToken);
        }
    }


    private async Task UpdateProductsCache()
    {
        Console.WriteLine("ProductBackgroundJob started.");

        using var scope = _serviceProvider.CreateScope();
        var productService = scope.ServiceProvider.GetRequiredService<IProductService>();
        var products = await productService.GetProducts();
        _productCache.StoreProducts(products);
    }
}