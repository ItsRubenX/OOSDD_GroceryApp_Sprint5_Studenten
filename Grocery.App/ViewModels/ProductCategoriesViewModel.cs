using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;
using Grocery.Core.Services;

namespace Grocery.App.ViewModels
{
    [QueryProperty(nameof(Category), nameof(Category))]
    public partial class ProductCategoriesViewModel : BaseViewModel
    {
        private readonly IProductCategoryService _productCategoryService;
        private readonly IProductService _productService;

        private string searchText = "";
        private int lastCategoryId = 0;

        public ObservableCollection<ProductCategory> ProductCategories { get; set; } = [];
        public ObservableCollection<Product> AvailableProducts { get; set; } = [];

        [ObservableProperty]
        Category category;

        public ProductCategoriesViewModel(IProductCategoryService productCategoryService, IProductService productService)
        {
            _productCategoryService = productCategoryService;
            _productService = productService;
        }

        partial void OnCategoryChanged(Category? oldValue, Category newValue) 
        {
            if (newValue != null) 
            {
                Category = newValue;
                Title = $"Producten in {newValue.Name}";
                Load(newValue.Id);
            }

        }

        private void Load(int categoryId) 
        {
            lastCategoryId = categoryId;
            ProductCategories.Clear();
            foreach (var pc in _productCategoryService.GetAllOnCategoryId(categoryId)) 
                ProductCategories.Add(pc);
            GetAvailableProducts();
        }

        private void GetAvailableProducts() 
        {
            AvailableProducts.Clear();
            foreach (ProductCategory pc in ProductCategories)
            {
                if (pc.CategoryId == lastCategoryId)
                {
                    foreach (Product p in _productService.GetAll())
                        if (pc.ProductId == p.Id && p.Stock > 0 && (searchText == "" || p.Name.ToLower().Contains(searchText.ToLower())))
                            AvailableProducts.Add(p);
                }
            }
        }

        [RelayCommand]
        public void AddProduct(Product product) 
        {
            
        }

        [RelayCommand]
        public void PerformSearch(string searchText) 
        {
            this.searchText = searchText;
            GetAvailableProducts();
        }
    }
}
