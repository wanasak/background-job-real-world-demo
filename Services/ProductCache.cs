using background_job_real_world_demo.Models;

namespace background_job_real_world_demo.Services
{
    public class ProductCache : IProductCache
    {
        private IEnumerable<Product> _products;

        public IEnumerable<Product> GetProducts() => _products;

        public void StoreProducts(IEnumerable<Product> products)
        {
            Interlocked.Exchange(ref _products, products);
        }
    }

    public interface IProductCache
    {
        IEnumerable<Product> GetProducts();
        void StoreProducts(IEnumerable<Product> products);
    }
}