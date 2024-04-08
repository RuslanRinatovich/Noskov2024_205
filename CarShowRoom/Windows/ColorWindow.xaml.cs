using CarShowRoom.Entities;
using System.Text;
using System.Windows;
namespace CarShowRoom.Windows
{
    /// <summary>
    /// Логика взаимодействия для ColorWindow.xaml
    /// </summary>
    public partial class ColorWindow : Window
    {
        public Color currentItem { get; private set; }


        public ColorWindow(Color p)
        {
            InitializeComponent();
            currentItem = p;
            DataContext = currentItem;
        }


        private StringBuilder CheckFields()
        {
            StringBuilder s = new StringBuilder();
            if (TbName.Text == "")
                s.AppendLine("Укажите название");

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
            currentItem.Title = TbName.Text;
            this.DialogResult = true;
        }

    }
}