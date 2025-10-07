using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;

namespace Grocery.Core.Services
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IProductCategoryService _productCategoryService;
        public ProductCategoryService(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }
        public List<ProductCategory> GetAll()
        {
            return _productCategoryService.GetAll();
        }
        public ProductCategory Add(ProductCategory item)
        {
            return _productCategoryService.Add(item);
        }
        public List<ProductCategory> GetAllOnCategoryId()
        {
            return _productCategoryService.GetAllOnCategoryId();
        }
    }
}
