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

using Excel = Microsoft.Office.Interop.Excel;

namespace CarShowRoom.Pages
{
    /// <summary>
    /// Логика взаимодействия для CarsPage.xaml
    /// </summary>
    public partial class CarsPage : Page
    {
        int _itemcount = 0;
        List<Car> cars;
        public CarsPage()
        {
            InitializeComponent();
            LoadComboBoxItems();
            LoadDataGrid();
        }

        void LoadDataGrid()
        {
            cars = DataEntities.GetContext().Cars.OrderBy(p => p.Title).ToList();
            DataGridGood.ItemsSource = cars;
            _itemcount = cars.Count;
            TextBlockCount.Text = $" Результат запроса: {cars.Count} записей из {cars.Count}";
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
        private void BtnCategories_Click(object sender, RoutedEventArgs e)
        {
            //  Manager.MainFrame.Navigate(new CategoryPage());
        }


        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
             Manager.MainFrame.Navigate(new AddCarPage(null));
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
              Manager.MainFrame.Navigate(new AddCarPage((sender as Button).DataContext as Car));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //событие отображения данного Page
            // обновляем данные каждый раз когда активируется этот Page
            if (Visibility == Visibility.Visible)
            {
                DataGridGood.ItemsSource = null;
                //загрузка обновленных данных
                DataEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                LoadDataGrid();
            }
        }


        /// <summary>
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
            cars = currentData;
            DataGridGood.ItemsSource = cars;
            // отображение количества записей
            TextBlockCount.Text = $" Результат запроса: {currentData.Count} записей из {_itemcount}";
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateData();
        }



        private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateData();
        }


        private void ComboSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateData();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Car car = (sender as Button).DataContext as Car;
            car.SetPrevPhoto = 1;
            //MessageBox.Show(car.GetCurrentPhoto);
            Image image = (sender as Button).Tag as Image;
            image.Source = new BitmapImage(new Uri(car.GetCurrentPhoto));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Car car = (sender as Button).DataContext as Car;
            car.SetNextPhoto = 1;
            // MessageBox.Show(car.GetCurrentPhoto);
            Image image = (sender as Button).Tag as Image;
            image.Source = new BitmapImage(new Uri(car.GetCurrentPhoto));
        }


        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            // удаление выбранного товара из таблицы
            //получаем все выделенные товары
            Car selected = (sender as Button).DataContext as Car;
            // вывод сообщения с вопросом Удалить запись?
            MessageBoxResult messageBoxResult = MessageBox.Show($"Удалить информацию о зале, фотографии и ???",
                "Удаление", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            //если пользователь нажал ОК пытаемся удалить запись
            if (messageBoxResult == MessageBoxResult.OK)
            {
                try
                {

                    //// проверка, есть ли у товара в таблице о продажах связанные записи
                    //// если да, то выбрасывается исключение и удаление прерывается
                    if (selected.Orders.Count > 0)
                        throw new Exception("Есть зависимые записи о залах");

                    DataEntities.GetContext().Photos.RemoveRange(selected.Photos);
                    DataEntities.GetContext().Cars.Remove(selected);
                    //сохраняем изменения
                    DataEntities.GetContext().SaveChanges();
                    MessageBox.Show("Записи удалены");
                    LoadDataGrid();
                    UpdateData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Ошибка удаления", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void DataGridGoodLoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }


        private void PrintExcel()
        {
            string fileName = AppDomain.CurrentDomain.BaseDirectory + "\\" + "Cars" + ".xltx";
            Excel.Application xlApp = new Excel.Application();
            Excel.Worksheet xlSheet = new Excel.Worksheet();
            try
            {
                //добавляем книгу
                xlApp.Workbooks.Open(fileName, System.Type.Missing, System.Type.Missing, System.Type.Missing, System.Type.Missing,
                                          System.Type.Missing, System.Type.Missing, System.Type.Missing, System.Type.Missing,
                                          System.Type.Missing, System.Type.Missing, System.Type.Missing, System.Type.Missing,
                                          System.Type.Missing, System.Type.Missing);
                //делаем временно неактивным документ
                xlApp.Interactive = false;
                xlApp.EnableEvents = false;
                Excel.Range xlSheetRange;
                //выбираем лист на котором будем работать (Лист 1)
                xlSheet = (Excel.Worksheet)xlApp.Sheets[1];
                //Название листа
                xlSheet.Name = "Список товаров";
                int row = 2;
                int i = 0;


                foreach (Car car in cars)
                {
                    xlSheet.Cells[row, 1] = (i + 1).ToString();
                    string s;
                    // DateTime y = Convert.ToDateTime(dtOrders.Rows[i].Cells[1].Value);
                    xlSheet.Cells[row, 2] = car.Id.ToString();
                    xlSheet.Cells[row, 3] = car.Title.ToString();
                    xlSheet.Cells[row, 4] = car.Type.Title.ToString();
                    xlSheet.Cells[row, 5] = car.Brand.Title.ToString();
                    s = "";
                    if (car.EngineCapacity != null) s = car.EngineCapacity.ToString();
                    xlSheet.Cells[row, 6] = s;
                    s = "";
                    if (car.HorsePower != null) s = car.HorsePower.ToString();
                    xlSheet.Cells[row, 7] = s;
                    xlSheet.Cells[row, 8] = car.Transmission.Title.ToString();
                    xlSheet.Cells[row, 9] = car.Color.Title.ToString();
                    s = "";
                    if (car.FuelRate != null) s = car.FuelRate.ToString();
                    xlSheet.Cells[row, 10] = s;
                    s = "";
                    if (car.TrunkVolume != null) s = car.TrunkVolume.ToString();
                    xlSheet.Cells[row, 11] = s;
                    s = "";
                    if (car.DoorCount != null) s = car.DoorCount.ToString();
                    xlSheet.Cells[row, 12] = s;
                    xlSheet.Cells[row, 13] = car.Air.Title.ToString();
                    xlSheet.Cells[row, 14] = car.FuelType.Title.ToString();
                    s = "";
                    if (car.Year != null) s = car.Year.ToString();
                    xlSheet.Cells[row, 15] = s;
                    s = "";
                    if (car.Price != null) s = car.Price.ToString();
                    xlSheet.Cells[row, 16] = s;
                    row++;
                    Excel.Range r = xlSheet.get_Range("A" + row.ToString(), "P" + row.ToString());
                    r.Insert(Excel.XlInsertShiftDirection.xlShiftDown);
                    i++;
                }




                row--;
                xlSheetRange = xlSheet.get_Range("A2:P" + (row + 1).ToString(), System.Type.Missing);
                xlSheetRange.Borders.LineStyle = true;
                //xlSheet.Cells[row + 1, 9] = "=SUM(I2:I" + row.ToString() + ")";

                //xlSheet.Cells[row + 1, 8] = "ИТОГО:";
                row++;

                //выбираем всю область данных*/
                xlSheetRange = xlSheet.UsedRange;
                //выравниваем строки и колонки по их содержимому
                xlSheetRange.Columns.AutoFit();
                xlSheetRange.Rows.AutoFit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                //Показываем ексель
                xlApp.Visible = true;
                xlApp.Interactive = true;
                xlApp.ScreenUpdating = true;
                xlApp.UserControl = true;
            }
        }

        private void BtnExcel_Click(object sender, RoutedEventArgs e)
        {
            PrintExcel();
        }

        private void BtnTypes_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new TypePage());
        }

        private void BtnBrands_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new BrandPage());
        }

        private void BtnTransmission_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new TransmissionPage());
        }

        private void BtnAir_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AirPage());
        }

          private void BtnColors_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new ColorsPage());
        }

        private void BtnFuelTypes_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new FuelTypePage());
        }
    }
}
