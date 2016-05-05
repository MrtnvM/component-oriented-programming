using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ReportBuilder
{
    public class ProductDataConverter
    {
        private readonly Func<object, Product> _convertor;

        public ProductDataConverter(Func<object, Product> convertor)
        {
            _convertor = convertor;
        }

        public Product ConvertToProduct(object yourModel)
        {
            return _convertor(yourModel);
        }

        public IEnumerable<Product> ConvertToProducts(IEnumerable yourModels)
        {
            var models = yourModels as object[] ?? yourModels.Cast<object>().ToArray();

            var products = new List<Product>(models.Length);
            products.AddRange(models.Select(ConvertToProduct));

            return products;
        }
    }
}
