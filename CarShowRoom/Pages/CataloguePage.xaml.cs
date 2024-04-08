using CarShowRoom.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CarShowRoom.Pages
{
    /// <summary>
    /// Логика взаимодействия для CataloguePage.xaml
    /// </summary>
    public partial class CataloguePage : Page
    {
        int _itemcount = 0;
        public CataloguePage()
        {
            InitializeComponent();
            LoadComboBoxItems();
            LoadDataGrid();
        }

        void LoadDataGrid()
        {
            //загрузка обновленных данных
            DataEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
            List<Car> goods = DataEntities.GetContext().Cars.OrderBy(p => p.Title).ToList();
            LViewGoods.ItemsSource = goods;
            _itemcount = goods.Count;
            TextBlockCount.Text = $" Результат запроса: {goods.Count} записей из {goods.Count}";
        }

        void LoadComboBoxItems()
        {
            var types = DataEntities.GetContext().Types.OrderBy(p => p.Title).ToList();
            types.Insert(0, new Entities.Type
            {
                Title = "Все виды"
            }
            );
            ComboType.ItemsSource = types;
            ComboType.SelectedIndex = 0;


        }


        /// Метод для фильтрации и сортировки данных
        /// </summary>
        private void UpdateData()
        {
            // получаем текущие данные из бд
            //var currentGoods = DataBDEntities.GetContext().Abonements.OrderBy(p => p.CategoryTrainer.Trainer.LastName).ToList();

            var currentData = DataEntities.GetContext().Cars.OrderBy(p => p.Title).ToList();
            // выбор только тех товаров, которые принадлежат данному производителю


            if (ComboType.SelectedIndex > 0)
                currentData = currentData.Where(p => p.TypeId == (ComboType.SelectedItem as Entities.Type).Id).ToList();



            // выбор тех товаров, в названии которых есть поисковая строка
            currentData = currentData.Where(p => p.Title.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();


            if (ComboSort.SelectedIndex >= 0)
            {
                // сортировка по возрастанию цены
                if (ComboSort.SelectedIndex == 0)
                    currentData = currentData.OrderBy(p => p.Title).ToList();
                if (ComboSort.SelectedIndex == 1)
                    currentData = currentData.OrderByDescending(p => p.Title).ToList();
                if (ComboSort.SelectedIndex == 2)
                    currentData = currentData.OrderBy(p => p.Price).ToList();
                if (ComboSort.SelectedIndex == 3)
                    currentData = currentData.OrderByDescending(p => p.Price).ToList();
            }
            // В качестве источника данных присваиваем список данных
            LViewGoods.ItemsSource = currentData;
            // отображение количества записей
            TextBlockCount.Text = $" Результат запроса: {currentData.Count} записей из {_itemcount}";
        }

        private void TBoxSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateData();
        }

        private void ComboSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateData();
        }

        private void ComboTypeSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateData();
        }


        private void ComboSortSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateData();
        }

     
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Room room = (sender as Button).DataContext as Room;
            //BookingWindow bookingWindow = new BookingWindow(room);
            //bookingWindow.ShowDialog();
        }

       
        private void PageIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                LoadDataGrid();
            }
        }

      

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Car car = (sender as Button).DataContext as Car;
            car.SetPrevPhoto = 1;
            //MessageBox.Show(room.GetCurrentPhoto);
            Image image = (sender as Button).Tag as Image;
            image.Source = new BitmapImage(new Uri(car.GetCurrentPhoto));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Car car = (sender as Button).DataContext as Car;
            car.SetNextPhoto = 1;
            // MessageBox.Show(room.GetCurrentPhoto);
            Image image = (sender as Button).Tag as Image;
            image.Source = new BitmapImage(new Uri(car.GetCurrentPhoto));
        }
    }
}
