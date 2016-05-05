using System.Linq;
using System.Windows;
using Microsoft.Win32;
using ProductList;
using ReportBuilder;

namespace Computer_Accounting
{
    // ReSharper disable once RedundantExtendsListEntry
    public partial class MainWindow : Window
    {
        private readonly ComputerContext _db = new ComputerContext();

        public MainWindow()
        {
            InitializeComponent();
            AddDataToDb();

            var products = _db.Computers.ToArray();
            ProductContainer.SetData(products, 
                new ProductList.ProductDataConverter(p =>
                {
                    var computer = (Computer)p;
                    return new ProductList.Product
                    {
                        Name = computer.Name,
                        Description = computer.Description,
                        Price = computer.Price
                    };
                }
            ));
        }

        private void AddDataToDb()
        {
            var computers = GenerateTestData();

            var temp = _db.Computers.ToList();
            _db.Computers.RemoveRange(temp);
            _db.SaveChanges();

            _db.Computers.AddRange(computers);
            _db.SaveChanges();
        }

        Computer[] GenerateTestData()
        {
            return new []
            {
                new Computer { Name = "Mac Mini", Price = 40990, Description = "Несмотря на невысокую цену, в квадратном корпусе Mac mini шириной всего 19,7 см спрятаны полноценные возможности компьютера Mac. Просто подключите к нему монитор, клавиатуру и мышь — и вы готовы к великим свершениям."}, 
                new Computer { Name = "MacBook Air", Price = 77990, Description = "С процессорами Intel Core i5 и i7 5-го поколения и графическими процессорами Intel HD Graphics 6000 вы сможете решить любые задачи с невероятной скоростью: от редактирования фотографий до поиска в Сети. Все эти возможности помещаются в невероятно тонком корпусе unibody: его толщина всего 1,7 см, а вес — 1,08 кг. "},
                new Computer { Name = "MacBook", Price = 106990, Description = "С процессорами Intel Core i5 и i7 5-го поколения и графическими процессорами Intel HD Graphics 6000 вы сможете решить любые задачи с невероятной скоростью: от редактирования фотографий до поиска в Сети. Все эти возможности помещаются в невероятно тонком корпусе unibody: его толщина всего 1,7 см, а вес — 1,08 кг. "},
                new Computer { Name = "iMac", Price = 89990, Description = "Каждая модель iMac создавалась, чтобы стать потрясающей настольной компьютерной системой. Самый лучший экран, высокопроизводительные процессоры, графическая система и накопители — всё в невероятно тонком бесшовном корпусе. Новый 21,5‑дюймовый iMac с дисплеем Retina 4K продолжает эту традицию. Как и передовая 27-дюймовая модель с дисплеем 5K, он обеспечивает потрясающее качество изображения. Добавьте невероятное погружение в контент — и iMac снова на шаг впереди." }         
            };
        }

        private void btnMakeReport_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog
            {
                Title = "Выберете место сохранения отчета"
            };

            string path;

            dialog.FileOk += (obj, args) =>
            {
                path = ((SaveFileDialog) obj).FileName;
                var computers = _db.Computers.ToList();
                ProductReportBuilder reportBuilder = new ProductReportBuilder(
                    new ReportBuilder.ProductDataConverter(p =>
                    {
                        var computer = (Computer)p;
                        return new ReportBuilder.Product
                        {
                            Name = computer.Name,
                            Description = computer.Description,
                            Price = computer.Price
                        };
                    }), computers, path);
                reportBuilder.Generate();
            };
            dialog.ShowDialog();
        }
    }
}
