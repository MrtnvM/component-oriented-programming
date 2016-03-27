using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using iTextSharp.text;
using Models;
using iTextSharp.text.pdf;

namespace ReportBuilder
{
    public partial class ReportBuilder : Component
    {
        public readonly int InstanceId;
        private static int _nextInstanceId;
        private static long _classInstanceCount;

        private readonly string _path;
        private readonly List<Product> _products;

        public long IstanceCount => _classInstanceCount;

        public ReportBuilder(IEnumerable<Product> products, string path)
        {
            InitializeComponent();

            InstanceId = _nextInstanceId++;
            _classInstanceCount++;

            _path = path;
            _products = products.ToList();
        }

        public void Generate()
        {
            var products = _products.OrderByDescending(p => p.Price);
            var doc = new PdfDocument();
            PdfWriter.GetInstance(doc, new FileStream(_path, FileMode.Create));
            doc.Open();

            WriteHeader(doc);
            WriteReport(doc, products);

            doc.Close();
        }

        private void WriteHeader(PdfDocument doc)
        {
            var header = new Paragraph(new Phrase("Отчет по остатку товара"))
            {
                Alignment = Element.ALIGN_CENTER,
                Font = {Size = 18}
            };
            doc.Add(header);
        }

        private void WriteReport(PdfDocument doc, IEnumerable<Product> products)
        {
            foreach (var product in products)
            {
                var p = new Paragraph(new Phrase(product.Name))
                {
                    Font = {Size = 16},
                    Alignment = Element.ALIGN_LEFT
                };
                p.Font.SetStyle("bold");
                doc.Add(p);

                p = new Paragraph(new Phrase(product.Description))
                {
                    Alignment = Element.ALIGN_JUSTIFIED,
                    Font = {Size = 14}
                };
                doc.Add(p);

                p = new Paragraph(new Phrase(product.Price))
                {
                    Font = {Size = 16},
                    Alignment = Element.ALIGN_LEFT
                };
                p.Font.SetStyle("bold");
                doc.Add(p);

                doc.Add(new Paragraph(""));
            }
        }

        ~ReportBuilder()
        {
            _classInstanceCount--;
        }
    }
}
