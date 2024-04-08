using CarShowRoom.Entities;
using CarShowRoom.Windows;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
namespace CarShowRoom.Pages
{
    /// <summary>
    /// Логика взаимодействия для TransmissionPage.xaml
    /// </summary>
    public partial class TransmissionPage : Page
    {
        int _itemcount = 0;
        public TransmissionPage()
        {
            InitializeComponent();
        }


        void LoadData()
        {
            try
            {
                DataGridGood.ItemsSource = null;
                //загрузка обновленных данных
                DataEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                List<Transmission> data = DataEntities.GetContext().Transmissions.OrderBy(p => p.Title).ToList();
                DataGridGood.ItemsSource = data;
                _itemcount = data.Count;
                TextBlockCount.Text = $" Результат запроса: {data.Count} записей из {data.Count}";
            }
            catch
            {
                MessageBox.Show("Ошибка");
            }
        }


        private void PageIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //событие отображения данного Page
            // обновляем данные каждый раз когда активируется этот Page
            if (Visibility == Visibility.Visible)
            {
                LoadData();
            }
        }

        private void BtnAddClick(object sender, RoutedEventArgs e)
        {
            try
            {
                TransmissionWindow window = new TransmissionWindow(new Transmission());
                if (window.ShowDialog() == true)
                {
                    DataEntities.GetContext().Transmissions.Add(window.currentItem);
                    DataEntities.GetContext().SaveChanges();
                    LoadData();
                    MessageBox.Show("Запись добавлена", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch
            {
                MessageBox.Show("Ошибка");
            }
        }

        private void BtnDeleteClick(object sender, RoutedEventArgs e)
        {
            // удаление выбранного товара из таблицы
            //получаем все выделенные товары
            Transmission selected = (sender as Button).DataContext as Transmission;
            // вывод сообщения с вопросом Удалить запись?
            MessageBoxResult messageBoxResult = MessageBox.Show($"Удалить запись???",
                "Удаление", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            //если пользователь нажал ОК пытаемся удалить запись
            if (messageBoxResult == MessageBoxResult.OK)
            {
                try
                {

                    // проверка, есть ли у товара в таблице о продажах связанные записи
                    // если да, то выбрасывается исключение и удаление прерывается
                    if (selected.Cars.Count > 0)
                        throw new Exception("Есть зависимые записи");

                    DataEntities.GetContext().Transmissions.Remove(selected);
                    //сохраняем изменения
                    DataEntities.GetContext().SaveChanges();
                    MessageBox.Show("Записи удалены");
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Ошибка удаления", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        // отображение номеров строк в DataGrid
        private void DataGridGoodLoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }


        private void TBoxSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateData();
        }
        // Поиск товаров конкретного производителя
        private void ComboTypeSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateData();
        }
        /// <summary>
        /// Метод для фильтрации и сортировки данных
        /// </summary>
        private void UpdateData()
        {
            // получаем текущие данные из бд
            var currentDatas = DataEntities.GetContext().Transmissions.OrderBy(p => p.Title).ToList();
            // выбор только тех товаров, по определенному диапазону скидки

            currentDatas = currentDatas.Where(p => p.Title.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();

            // сортировка
            if (ComboSort.SelectedIndex >= 0)
            {
                // сортировка по возрастанию цены
                if (ComboSort.SelectedIndex == 0)
                    currentDatas = currentDatas.OrderBy(p => p.Title).ToList();
                // сортировка по убыванию цены
                if (ComboSort.SelectedIndex == 1)
                    currentDatas = currentDatas.OrderByDescending(p => p.Title).ToList();
            }
            // В качестве источника данных присваиваем список данных
            DataGridGood.ItemsSource = currentDatas;
            // отображение количества записей
            TextBlockCount.Text = $" Результат запроса: {currentDatas.Count} записей из {_itemcount}";
        }
        // сортировка товаров 
        private void ComboSortSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateData();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //  если ни одного объекта не выделено, выходим
                if (DataGridGood.SelectedItem == null) return;
                // получаем выделенный объект
                Transmission selected = DataGridGood.SelectedItem as Transmission;
                TransmissionWindow window = new TransmissionWindow(selected);
                if (window.ShowDialog() == true)
                {
                    if (window.currentItem != null)
                    {
                        DataEntities.GetContext().Entry(window.currentItem).State = EntityState.Modified;
                        DataEntities.GetContext().SaveChanges();
                        LoadData();
                        MessageBox.Show("Запись изменена", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    DataEntities.GetContext().Entry(window.currentItem).Reload();
                    LoadData();
                }
            }
            catch
            {
                MessageBox.Show("Ошибка");
            }
        }
    }
}