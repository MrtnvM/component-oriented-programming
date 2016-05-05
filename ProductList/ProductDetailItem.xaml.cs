using System.Windows.Controls;

namespace ProductList
{
    // ReSharper disable once RedundantExtendsListEntry
    public partial class ProductDetailItem : UserControl
    {
        public ProductDetailItem(Product product, string currency = "руб.")
        {
            InitializeComponent();

            Title.Text = product.Name;
            Price.Text = $"{product.Price} {currency}";
            Description.Text = product.Description;
        }
    }
}
