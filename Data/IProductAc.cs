using MwTesting.Model;

namespace MwTesting.Data
{
    public interface IProductAc
    {
        bool SaveChanges();
        IEnumerable<Product> GetAllProducts();
        Product GetProductById(int id);
        void CreateProduct(Product product);
        void DeleteProduct(Product product);
    }
}