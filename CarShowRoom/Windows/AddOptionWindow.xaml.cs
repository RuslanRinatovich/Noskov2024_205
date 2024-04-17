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
using System.Windows.Shapes;

namespace CarShowRoom.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddOptionWindow.xaml
    /// </summary>
    public partial class AddOptionWindow : Window
    {
        public Option currentItem { get; private set; }


        public AddOptionWindow(Order p)
        {
            InitializeComponent();
            currentItem = new Option();
            DataContext = currentItem;
            ComboBoxOption.ItemsSource = DataEntities.GetContext().Options.ToList();
        }


        private StringBuilder CheckFields()
        {
            StringBuilder s = new StringBuilder();
            if (ComboBoxOption.SelectedIndex == -1)
                s.AppendLine("Выберите опцию");

            return s;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder _error = CheckFields();
            // если ошибки есть, то выводим ошибки в MessageBox
            // и прерываем выполнение 
            if (_error.Length > 0)
            {
                MessageBox.Show(_error.ToString());
                return;
            }
            this.DialogResult = true;
        }

        private void ComboBoxOption_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            currentItem = ComboBoxOption.SelectedItem as Option;
            TextBoxPrice.Text = currentItem.Price.ToString("C");
        }
    }
}
