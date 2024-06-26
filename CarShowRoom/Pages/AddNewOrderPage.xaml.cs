﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
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
using CarShowRoom.Entities;
using Type = CarShowRoom.Entities.Type;
using Excel = Microsoft.Office.Interop.Excel;
using CarShowRoom.Windows;

namespace CarShowRoom.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddNewOrderPage.xaml
    /// </summary>
    public partial class AddNewOrderPage : Page
    {
        private Order _currentItem = new Order();
        private Client _currentClient;
        private Car _currentCar;
        int _priceListId = -1;
        // путь к файлу
        double _price = 0;
        public AddNewOrderPage(Order selectedItem)
        {
            InitializeComponent();
            // если передано null, то мы добавляем новый товар

            if (selectedItem != null)
            {
                _currentItem = selectedItem;
                TextBoxId.Visibility = Visibility.Hidden;
                _currentClient = _currentItem.Client;
                _currentCar = _currentItem.Car;
                tbClient.Text = $" {_currentItem.Client.LastName}  {_currentItem.Client.FirstName}\nтелефон: {_currentItem.Client.Phone}\nemail:{_currentItem.Client.Email}";
            }
            else
            {
                tbClient.IsReadOnly = true;
                btnLoadClient.Visibility = Visibility.Hidden;
                BtnExcel.Visibility = Visibility.Hidden;
                _currentItem.DateStart = DateTime.Now;
                _currentItem.DateEnd = DateTime.Now;
            }
            lbClient.ItemsSource = DataEntities.GetContext().Clients.ToList();
            DataContext = _currentItem;
            TextBoxPrice.Text = _currentItem.GetTotalPrice.ToString("c");
            DtData.ItemsSource = DataEntities.GetContext().OrderContents.Where(o => o.OrderId == _currentItem.Id).ToList();
            ComboStatus.ItemsSource = DataEntities.GetContext().Status.ToList();
            ComboCar.ItemsSource = DataEntities.GetContext().Cars.ToList();
            ComboStatus.ItemsSource = DataEntities.GetContext().Status.ToList();
        }
        // проверка полей
        private StringBuilder CheckFields()
        {
            StringBuilder s = new StringBuilder();
            if (_currentItem.Car is null)
                s.AppendLine("Выберите авто");
            if (_currentItem.Client is null)
                s.AppendLine("Выберите клиента");
            return s;
        }
        // сохранение 
        private void BtnSaveClick(object sender, RoutedEventArgs e)
        {
            StringBuilder _error = CheckFields();
            // если ошибки есть, то выводим ошибки в MessageBox
            // и прерываем выполнение 
            if (_error.Length > 0)
            {
                MessageBox.Show(_error.ToString());
                return;
            }
            // проверка полей прошла успешно
            if (_currentItem.Id == 0)
            {
                DataEntities.GetContext().Orders.Add(_currentItem);
            }
            MessageBox.Show(_currentCar.Id.ToString() + " " + _currentItem.Car.Id.ToString());
            
            DataEntities.GetContext().ChangeTracker.Entries()
            .Where(x => x.State == EntityState.Modified &&
                        !typeof(Order).IsAssignableFrom(x.Entity.GetType()))
            .ToList()
            .ForEach(entry => {
                entry.CurrentValues.SetValues(entry.OriginalValues);
            });
            if (_currentCar.Id != _currentItem.Car.Id)
            {
                _currentCar.IsEnabled = true;

            }
            _currentItem.Car.IsEnabled = false;
            DataEntities.GetContext().SaveChanges();
            MessageBox.Show("Запись Изменена");
            // Возвращаемся на предыдущую форму
            Manager.MainFrame.GoBack();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message.ToString());
            //}
        }

        private void PrintExcel()
        {
            string fileName = AppDomain.CurrentDomain.BaseDirectory + "\\" + "Check" + ".xltx";
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
                xlSheet.Name = "Чек";
                int row = 9;
                int i = 0;

                xlSheet.Cells[4, 3] = _currentItem.Id;



                xlSheet.Cells[5, 3] = $"{_currentItem.Client.LastName} {_currentItem.Client.FirstName} ";
                xlSheet.Cells[6, 4] = _currentItem.Client.PassportSeries;
                xlSheet.Cells[6, 7] = _currentItem.Client.PassportNum;
                xlSheet.Cells[7, 3] = _currentItem.Client.Phone;

                string s = "";
                if (_currentItem.DateEnd != null)
                {
                    s = _currentItem.DateEnd.ToString();
                }
                xlSheet.Cells[8, 4] = s;
                xlSheet.Cells[9, 5] = _currentItem.Car.Title;
                xlSheet.Cells[10, 5] = _currentItem.GetTotalPrice;
                int j = 13;
                foreach (OrderContent orderContent in _currentItem.OrderContents)
                {
                    xlSheet.Cells[j, 1] = (j - 12).ToString();
                    xlSheet.Cells[j, 2] = $"{orderContent.Option.Title}";
                    xlSheet.Cells[j, 6] = $"{orderContent.Option.Price}";
                    xlSheet.Cells[j, 7] = "1";
                    xlSheet.Cells[j, 8] = $"{orderContent.Option.Price}";
                    j++;
                    Excel.Range r = xlSheet.get_Range("A" + j.ToString(), "H" + j.ToString());
                    r.Insert(Excel.XlInsertShiftDirection.xlShiftDown);
                    xlSheet.Range[xlSheet.Cells[j, 2], xlSheet.Cells[j, 5]].Merge();
                }
                xlSheet.Range[xlSheet.Cells[13, 1], xlSheet.Cells[j - 1, 8]].Borders.LineStyle = true;
                xlSheet.Cells[j+2, 3] = $"{Manager.CurrentUser.Client.LastName} {Manager.CurrentUser.Client.FirstName} ";

                row++;
                xlSheet.Cells[j+3, 3] = DateTime.Today.ToShortDateString();
                //выбираем всю область данных*/
                xlSheetRange = xlSheet.UsedRange;
                //выравниваем строки и колонки по их содержимому
                //xlSheetRange.Columns.AutoFit();
                //xlSheetRange.Rows.AutoFit();
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

        private void DtDataLoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }
        private void btnLoadClient_Click(object sender, RoutedEventArgs e)
        {
            hostLoadClient.IsOpen = true;
        }
        private void btnClientOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lbClient.SelectedItems.Count > 0)
                {
                    for (int i = 0; i < lbClient.SelectedItems.Count; i++)
                    {
                        Client xair = lbClient.SelectedItems[i] as Client;
                        if (xair != null)
                        {
                            _currentItem.Client = xair;
                            _currentClient = xair;
                            tbClient.Text = $" {xair.LastName}  {xair.FirstName}\nтелефон: {xair.Phone}\nemail:{xair.Email}";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            hostLoadClient.IsOpen = false;
        }

        private void btnClientCancel_Click(object sender, RoutedEventArgs e)
        {
            hostLoadClient.IsOpen = false;
        }

      

        private void ComboCar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _currentItem.Car = ComboCar.SelectedItem as Car;            
        }

        private void BtnExcel_Click(object sender, RoutedEventArgs e)
        {
            if (_currentItem.Id == 0)
                return;
            PrintExcel();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //    если мы только добавляем нового агента, то продаж у него еще нет
                if (_currentItem.Id == 0) return;

                // Открываем окно добавления новой продажи
                AddOptionWindow window = new AddOptionWindow(_currentItem);
                // если в окне добавления продажи нажата кнопка ОК
                if (window.ShowDialog() == true)
                {
                    // добавляем новую продажу
                    OrderContent orderContent = new OrderContent();
                    orderContent.Option = window.currentItem;
                    orderContent.Order = _currentItem;
                    _currentItem.OrderContents.Add(orderContent);
                    DataEntities.GetContext().SaveChanges();
                    TextBoxPrice.Text = _currentItem.GetTotalPrice.ToString("c");
                    DtData.ItemsSource = DataEntities.GetContext().OrderContents.Where(o => o.OrderId == _currentItem.Id).ToList();
                }
            }
            catch
            {
                MessageBox.Show("Ошибка");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                // если ни одного объекта не выделено, выходим
                if (DtData.SelectedItem == null) return;
                // получаем выделенный объект
                MessageBoxResult messageBoxResult = MessageBox.Show($"Удалить запись? ", "Удаление", MessageBoxButton.OKCancel,
MessageBoxImage.Question);
                if (messageBoxResult == MessageBoxResult.OK)
                {
                    OrderContent deletedItem = DtData.SelectedItem as OrderContent;


                    DataEntities.GetContext().OrderContents.Remove(deletedItem);
                    DataEntities.GetContext().SaveChanges();
                    // после удаления продажи
                    // подгружаем измененные данные
                    TextBoxPrice.Text = _currentItem.GetTotalPrice.ToString("c");
                    DtData.ItemsSource = DataEntities.GetContext().OrderContents.Where(o => o.OrderId == _currentItem.Id).ToList();
                    MessageBox.Show("Запись удалена", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
