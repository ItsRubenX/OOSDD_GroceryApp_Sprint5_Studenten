using Grocery.Core.Interfaces.Repositories;
using Grocery.Core.Models;

namespace Grocery.Core.Data.Repositories
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private readonly List<ProductCategory> productCategories;

        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductCategoryRepository(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;

            productCategories = [
                new ProductCategory(1, 1, 3),
                new ProductCategory(2, 2, 3),
                new ProductCategory(3, 3, 2),
                new ProductCategory(4, 4, 5),
            ];

            foreach (ProductCategory pc in productCategories)
            {
                pc.Product = _productRepository.Get(pc.ProductId);
                pc.Category = _categoryRepository.Get(pc.CategoryId);
            }
        }
        public ProductCategory Add(ProductCategory item)
        {
            productCategories.Add(item);
            return item;
        }
        public List<ProductCategory> GetAll()
        {
            return productCategories;
        }
        public List<ProductCategory> GetAllOnCategoryId(int id)
        {
            return productCategories.Where(pc => pc.CategoryId == id).ToList();
        }
    }
}
