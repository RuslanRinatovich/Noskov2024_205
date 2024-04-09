using CarShowRoom.Entities;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
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

namespace CarShowRoom.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddCarPage.xaml
    /// </summary>
    public partial class AddCarPage : Page
    {
        private Car _currentItem = new Car();
        private static string _currentDirectory = Directory.GetCurrentDirectory() + @"\Images\";
        // передача в AddGoodPage товара 
        public AddCarPage(Car selectedItem)
        {
            InitializeComponent();
            LoadAndInitData(selectedItem);
        
        }

        void LoadAndInitData(Car selected)
        {
            ComboBrand.ItemsSource = DataEntities.GetContext().Brands.ToList();
            ComboAir.ItemsSource = DataEntities.GetContext().Airs.ToList();
            ComboTransmission.ItemsSource = DataEntities.GetContext().Transmissions.ToList();
            ComboType.ItemsSource = DataEntities.GetContext().Types.ToList();
            ComboFuelType.ItemsSource = DataEntities.GetContext().FuelTypes.ToList();
            ComboColor.ItemsSource = DataEntities.GetContext().Colors.ToList();

            BtnLoad.Visibility = Visibility.Collapsed;
            if (selected != null)
            {
                _currentItem = selected;
                BtnLoad.Visibility = Visibility.Visible;
                if (selected.Photos.Count > 0)
                {
                    _currentItem = selected;
                    TextBoxGoodId.Visibility = Visibility.Hidden;
                    ImageCurrentPhoto.Source = new BitmapImage(new Uri(selected.Photos.ToList()[0].GetPhoto));
                }
                else
                {
                    ImageCurrentPhoto.Source = null;
                }


            }
            DataContext = _currentItem;
        }


        // проверка полей
        private StringBuilder CheckFields()
        {
            StringBuilder s = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentItem.Title))
                s.AppendLine("Поле название пустое");
            if (_currentItem.Price < 0)
                s.AppendLine("Цена не может быть отрицательной");
            if (_currentItem.Brand is null)
                s.AppendLine("Выберите марку автомобиля");
            if (_currentItem.Transmission is null)
                s.AppendLine("Выберите тип КПП");
            if (_currentItem.Color is null)
                s.AppendLine("Выберите цвет");
            if (_currentItem.Air is null)
                s.AppendLine("Выберите вид климат системы");
            if (_currentItem.FuelType is null)
                s.AppendLine("Выберите вид топлива");
            if (_currentItem.Type is null)
                s.AppendLine("Выберите вид кузова");
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
            try
            {
                // проверка полей прошла успешно
                // если товар новый, то его ID == 0

                if (_currentItem.Id == 0)
                {
                    DataEntities.GetContext().Cars.Add(_currentItem);
                }

                DataEntities.GetContext().SaveChanges();  // Сохраняем изменения в БД
                MessageBox.Show("Запись Изменена");
                BtnLoad.Visibility = Visibility.Visible;
                // Manager.MainFrame.GoBack();  // Возвращаемся на предыдущую форму
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }


        // загрузка фото 
        private void BtnLoadClick(object sender, RoutedEventArgs e)
        {
            try
            {
                //Диалог открытия файла
                OpenFileDialog op = new OpenFileDialog();
                op.Title = "Select a picture";
                op.Filter = "JPG Files (*.jpg)|*.jpg|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|GIF Files (*.gif)|*.gif";
                // диалог вернет true, если файл был открыт
                if (op.ShowDialog() == true)
                {
                    // проверка размера файла
                    // по условию файл дожен быть не более 2Мб.
                    FileInfo fileInfo = new FileInfo(op.FileName);

                    string photo = ChangePhotoName(op.SafeFileName);
                    // путь куда нужно скопировать файл
                    string dest = _currentDirectory + photo;
                    File.Copy(op.FileName, dest);
                    Photo newPhoto = new Photo();
                    newPhoto.CarId = _currentItem.Id;
                    newPhoto.Title = photo;
                    DataEntities.GetContext().Photos.Add(newPhoto);
                    DataEntities.GetContext().SaveChanges();
                    _currentItem.ReloadPhotos = 1;
                    //DataEntities.GetContext().Entry(_currentItem).Reload();
                    // ListBoxPhotos.ItemsSource = null;
                    ListBoxPhotos.Items.Refresh();



                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }
        //подбор имени файла
        string ChangePhotoName(string _photoName)
        {
            string x = _currentDirectory + _photoName;
            string photoname = _photoName;
            int i = 0;
            if (File.Exists(x))
            {
                while (File.Exists(x))
                {
                    i++;
                    x = _currentDirectory + i.ToString() + photoname;
                }
                photoname = i.ToString() + photoname;
            }
            return photoname;
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)


        {
            ListBoxItem selectedItem = (ListBoxItem)ListBoxPhotos.ItemContainerGenerator.
                              ContainerFromItem(((Button)sender).DataContext);
            selectedItem.IsSelected = true;

            Photo deletedPhoto = (sender as Button).DataContext as Photo;
            MessageBoxResult messageBoxResult = MessageBox.Show($"Удалить изображение???",
               "Удаление", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            //если пользователь нажал ОК пытаемся удалить запись
            if (messageBoxResult == MessageBoxResult.OK)
            {
                try
                {

                    // проверка, есть ли у товара в таблице о продажах связанные записи
                    // если да, то выбрасывается исключение и удаление прерывается

                    int k = ListBoxPhotos.SelectedIndex;
                    DataEntities.GetContext().Photos.Remove(deletedPhoto);
                    //сохраняем изменения
                    DataEntities.GetContext().SaveChanges();
                    MessageBox.Show("Изображение удалено");
                    _currentItem.ReloadPhotos = 1;
                    //DataEntities.GetContext().Entry(_currentItem).Reload();
                    // ListBoxPhotos.ItemsSource = null;
                    ListBoxPhotos.Items.Refresh();
                    if (k > 0) k--;

                    ListBoxPhotos.SelectedIndex = k;
                    if (k == 0)
                        ImageCurrentPhoto.Source = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Ошибка удаления", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void ListBoxPhotos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBoxPhotos.SelectedItems.Count == 0)
                return;

            ImageCurrentPhoto.Source = new BitmapImage(new Uri((ListBoxPhotos.SelectedItem as Photo).GetPhoto));

        }

    }
}
