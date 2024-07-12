using MwTesting.Model;

namespace MwTesting.Data
{
    public class SqlProductAc(UserContext productContext) : IProductAc
    {
        public void CreateProduct(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            productContext.Products.Add(product);
        }

        public void DeleteProduct(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            productContext.Products.Remove(product);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return productContext.Products.ToList();
        }

        public Product GetProductById(int id)
        {
            return productContext.Products.FirstOrDefault(p => p.Id == id);

        }

        public bool SaveChanges()
        {
            return productContext.SaveChanges() >= 0;
        }
    }
}