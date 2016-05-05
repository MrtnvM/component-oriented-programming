using System.Windows.Controls;

namespace ProductList
{
    // ReSharper disable once RedundantExtendsListEntry
    public partial class ProductItem : UserControl
    {
        public ProductItem(Product product, string currency = "руб.")
        {
            InitializeComponent();

            Title.Text = product.Name;
            Price.Text = $"{product.Price} {currency}";
        }
    }
}
