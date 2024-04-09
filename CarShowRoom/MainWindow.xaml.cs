using CarShowRoom.Entities;
using CarShowRoom.Pages;
using CarShowRoom.Windows;
using System;
using System.Windows;

namespace CarShowRoom
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new CataloguePage());
            Manager.CurrentUser = null;
            Manager.MainFrame = MainFrame;
        }

        private void WindowClosed(object sender, EventArgs e)
        {

            App.Current.Shutdown();
        }
        //событие попытки закрытия окна,
        // если пользователь выберет Cancel, то форму не закроем
        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult x = MessageBox.Show("Вы действительно хотите выйти?",
                "Выйти", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (x == MessageBoxResult.Cancel)
                e.Cancel = true;
        }
        // Кнопка назад
        private void BtnBackClick(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.GoBack();
        }
        // Кнопка навигации

        // Событие отрисовки страницы
        // Скрываем или показываем кнопку Назад 
        // Скрываем или показываем кнопки Для перехода к остальным страницам
        private void MainFrameContentRendered(object sender, EventArgs e)
        {
            if (MainFrame.CanGoBack)
            {
                BtnBack.Visibility = Visibility.Visible;
                BtnEnter.Visibility = Visibility.Collapsed;
                BtnOrder.Visibility = Visibility.Collapsed;
                BtnCars.Visibility = Visibility.Collapsed;
                BtnMyAccount.Visibility = Visibility.Collapsed;
                BtnUsers.Visibility = Visibility.Collapsed;
                BtnMyBooking.Visibility = Visibility.Collapsed;
                TextBlockUser.Text = "";
             

            }
            else
            {
                BtnBack.Visibility = Visibility.Collapsed;
                BtnEnter.Visibility = Visibility.Visible;

                if (Manager.CurrentUser is null)
                    return;

                if (Manager.CurrentUser.RoleId == 1)
                {
                    BtnOrder.Visibility = Visibility.Visible;
                    BtnCars.Visibility = Visibility.Visible;
                    BtnMyAccount.Visibility = Visibility.Visible;
                    BtnMyBooking.Visibility = Visibility.Collapsed;
                    BtnUsers.Visibility = Visibility.Visible;
                }
                else
                {
                    BtnOrder.Visibility = Visibility.Collapsed;
                    BtnCars.Visibility = Visibility.Collapsed;
                    BtnUsers.Visibility = Visibility.Collapsed;
                    BtnMyAccount.Visibility = Visibility.Visible;
                    BtnMyBooking.Visibility = Visibility.Visible;
                }
                IconBtnKey.Kind = MaterialDesignThemes.Wpf.PackIconKind.Logout;
                BtnEnter.ToolTip = "Выйти";
                TextBlockUser.Text = $"{Manager.CurrentUser.UserName}";
            }
        }

        private void BtnEditDev_Click(object sender, RoutedEventArgs e)
        {
        }



        private void BtnEnter_Click(object sender, RoutedEventArgs e)
        {
            if (Manager.CurrentUser != null)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show($"Выйти из системы??? ", "Выход", MessageBoxButton.OKCancel,
MessageBoxImage.Question);
                if (messageBoxResult == MessageBoxResult.OK)
                {
                    IconBtnKey.Kind = MaterialDesignThemes.Wpf.PackIconKind.Login;
                    BtnEnter.ToolTip = "Войти";
                    Manager.CurrentUser = null;
                    BtnBack.Visibility = Visibility.Collapsed;
                    BtnOrder.Visibility = Visibility.Collapsed;
                    BtnCars.Visibility = Visibility.Collapsed;
                    BtnMyAccount.Visibility = Visibility.Collapsed;
                    BtnMyBooking.Visibility = Visibility.Collapsed;
                    BtnUsers.Visibility = Visibility.Collapsed;
                    TextBlockUser.Text = "";
                    MainFrame.NavigationService.Refresh();
                    return;
                }

            }

            LoginWindow window = new LoginWindow();
            if (window.ShowDialog() == true)
            {

                if (Manager.CurrentUser.RoleId == 1)
                {
                    BtnOrder.Visibility = Visibility.Visible;
                    BtnCars.Visibility = Visibility.Visible;
                    BtnMyAccount.Visibility = Visibility.Visible;
                    BtnUsers.Visibility = Visibility.Visible;
                    BtnMyBooking.Visibility = Visibility.Collapsed;
                }
                else
                {
                    BtnOrder.Visibility = Visibility.Collapsed;
                    BtnCars.Visibility = Visibility.Collapsed;
                    BtnUsers.Visibility = Visibility.Collapsed;
                    BtnMyAccount.Visibility = Visibility.Visible;
                    BtnMyBooking.Visibility = Visibility.Visible;
                }
                IconBtnKey.Kind = MaterialDesignThemes.Wpf.PackIconKind.Logout;
                BtnEnter.ToolTip = "Выйти";
                TextBlockUser.Text = $"{Manager.CurrentUser.UserName}";
            }
            MainFrame.NavigationService.Refresh();


        }



        private void BtnMyOrder_Click(object sender, RoutedEventArgs e)
        {
            //MainFrame.Navigate(new StatusPage());


        }

        private void BtnMyAccount_Click(object sender, RoutedEventArgs e)
        {
            try
            {


                MyAccountWindow window = new MyAccountWindow();

                if (window.ShowDialog() == true)
                {

                    MessageBox.Show("Запись изменена", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch
            {
                MessageBox.Show("Ошибка");
            }
        }

        private void BtnOrder_Click(object sender, RoutedEventArgs e)
        {

            


        }

        private void BtnMyOrders_Click(object sender, RoutedEventArgs e)
        {
        //    MainFrame.Navigate(new MyBookingPage());
        }

        private void BtnBuyes_Click(object sender, RoutedEventArgs e)
        {
            //  MainFrame.Navigate(new BuyPage());
        }

        private void BtnCars_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new CarsPage());
        }

        private void BtnBooking_Click(object sender, RoutedEventArgs e)
        {
            //MainFrame.Navigate(new BookingPage());
        }



        private void BtnUsers_Click(object sender, RoutedEventArgs e)
        {
          //  MainFrame.Navigate(new UsersPage());
        }

        private void BtnOrder_Click_1(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new OrderPage());
        }
    }
}
