using CarShowRoom.Entities;
using Microsoft.Win32;
using System;
using System.Text;
using System.Windows;
using Word = Microsoft.Office.Interop.Word;

namespace CarShowRoom.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddNewOrderWindow.xaml
    /// </summary>
    public partial class AddNewOrderWindow : Window
    {
        Order _current = new Order();
        User currentUser;
        Car currentCar;

        public AddNewOrderWindow(User user, Car car)
        {
            InitializeComponent();
            currentUser = user;
            currentCar = car;

            TextBlockBuyDate.Text = DateTime.Today.ToShortDateString();

            if (!string.IsNullOrEmpty(currentUser.Client.GetFio))
            {
                TbTitle.Text = currentUser.Client.GetFio;
            }
            if (!string.IsNullOrEmpty(currentUser.Client.Phone))
            {
                TbPhone.Text = currentUser.Client.Phone;
            }

            if (!string.IsNullOrEmpty(currentUser.Client.Email))
            {
                TbEmail.Text = currentUser.Client.Email;
            }

            TextBlockCar.Text = $"{currentCar.Title} стоимость {currentCar.Price:C}";
        }

        static bool IsValidMailAddress(string mail)
        {
            try
            {
                System.Net.Mail.MailAddress mailAddress = new System.Net.Mail.MailAddress(mail);

                return true;
            }
            catch
            {
                return false;
            }
        }


        private StringBuilder CheckFields()
        {
            StringBuilder s = new StringBuilder();
            if (string.IsNullOrWhiteSpace(TbTitle.Text))
                s.AppendLine("Поле имя пустое");
            if (!TbPhone.IsMaskFull)
                s.AppendLine("Укажите телефон");
            if (string.IsNullOrWhiteSpace(TbEmail.Text))
                s.AppendLine("Укажите email");
            if (!IsValidMailAddress(TbEmail.Text))
                s.AppendLine("Укажите корректный email");
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

            Order order = new Order();
            order.Car = currentCar;
            order.Client = currentUser.Client;
            order.StatusId = 0;
            order.DateStart = DateTime.Now;
            //order.DateEnd = DateTime.Now;


            try
            {
                DataEntities.GetContext().Orders.Add(order);
                order.Car.IsEnabled = false;
                DataEntities.GetContext().SaveChanges();
                MessageBox.Show("Автомобиль забронирован");
                BtnToPDF.Visibility = Visibility.Visible;
                BtnOk.Visibility = Visibility.Collapsed;
                BtnCancel.Visibility = Visibility.Collapsed;
                TbPhone.IsEnabled = false;
                TbEmail.IsEnabled = false;
                TbTitle.IsEnabled = false;
                _current = order;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }


        }


        private void BtnSavePDF_Click(object sender, RoutedEventArgs e)
        {
            PrintInPdf(_current);
        }

        void PrintInPdf(Order order)
        {
            try
            {
                string path = null;
                // указываем файл для сохранения
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PDF (.pdf)|*.pdf"; // Filter files by extension
                                                            // если диалог завершился успешно
                if (saveFileDialog.ShowDialog() == true)
                {
                    path = saveFileDialog.FileName;
                    Word.Application application = new Word.Application();
                    Word.Document document = application.Documents.Add();
                    Word.Paragraph paragraph = document.Paragraphs.Add();
                    Word.Range range = paragraph.Range;
                    range.Font.Bold = 1;
                    range.Text = $"Номер брони: {order.Id}";
                    range.InsertParagraphAfter();



                    range = paragraph.Range;
                    range.Font.Bold = 0;
                    range.Text = $"Дата создания записи: {order.DateStart}\n" +
                        $"Уважаемый, {order.Client.GetFio}, телефон:{order.Client.Phone}\temail:{order.Client.Email}\n" +
                        $"Вы забронировали автомобиль: {order.Car.Title}";


                    range.InsertParagraphAfter();


                    document.SaveAs2($@"{path}", Word.WdExportFormat.wdExportFormatPDF);
                    MessageBox.Show("Данные записаны в PDF-файл");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void BtnToPDF_Click(object sender, RoutedEventArgs e)
        {
            PrintInPdf(_current);
        }
    }
}
