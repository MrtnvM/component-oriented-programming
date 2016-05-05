using System.Collections;
using System.Windows.Controls;

namespace ProductList
{
    // ReSharper disable once RedundantExtendsListEntry
    public partial class ProductList : UserControl
    {
        public ProductList()
        {
            InitializeComponent();
        }

        public void SetData(IEnumerable models, ProductDataConverter convertor)
        {
            var products = convertor.ConvertToProducts(models);
            foreach (var product in products)
            {
                var productItem = new ProductItem(product);
                ProductContainer.Children.Add(productItem);
            }
        }
    }
}
