using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Trainings;

namespace Timetable
{
    /// <summary>
    /// Логика взаимодействия для TaskWindow.xaml
    /// </summary>
    public partial class TaskWindow : Window
    {
        public delegate void OkButtonClicked(Training training);
        public event OkButtonClicked onOkClicked;

        public TaskWindow(OkButtonClicked handler)
        {
            InitializeComponent();
            onOkClicked += handler;
        }

        public TaskWindow(OkButtonClicked handler, Training training)
        {
            InitializeComponent();
            onOkClicked += handler;

            clientInputBox.Text = training.ClientName;
            datePicker.Text = training.TimeFrom.ToString("d");
            hoursFromBox.Text = training.TimeFrom.Hour.ToString();
            minutesFromBox.Text = training.TimeFrom.Minute.ToString();
            hoursToBox.Text = training.TimeTo.Hour.ToString();
            minutesToBox.Text = training.TimeTo.Minute.ToString();
            priceBox.Text = training.Price.ToString();
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                isClientNameValid(clientInputBox.Text);
            } catch (FormatException exception)
            {
                MessageBox.Show(exception.Message);
                return;
            }

            try
            {
                isTimeValid(hoursFromBox.Text, minutesFromBox.Text);
                isTimeValid(hoursToBox.Text, minutesToBox.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Время должно быть целым числом и указвается в 24-часовом формате!");
                return;
            }

            try
            {
                isPriceValid(priceBox.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Стоимость должна быть целым неотрицательным числом!");
                return;
            }

            try
            {
                isDateValid(datePicker.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Выберите дату!");
                return;
            }

            try
            {
                Training training = new Training();
                training.ClientName = clientInputBox.Text;
                training.TimeFrom = DateTime.Parse(datePicker.Text + " " + hoursFromBox.Text + ":" + minutesFromBox.Text);
                training.TimeTo = DateTime.Parse(datePicker.Text + " " + hoursToBox.Text + ":" + minutesToBox.Text);
                training.Price = int.Parse(priceBox.Text);
                training.PayMethod = (bool)cashButton.IsChecked ? PayMethod.Cash : PayMethod.Card;
                onOkClicked(training);
                Close();
            }
            catch (FormatException exception)
            {
                MessageBox.Show(exception.Message);
            }

        }

        private void isClientNameValid(string clientName)
        {
            if  ( clientName.Length == 0 )
                throw new FormatException("Введите имя клиента!");
        }

        private void isDateValid(string date)
        {
            DateTime.Parse(date);
        }

        private void isTimeValid(string hours, string minutes)
        {
            int mins = int.Parse(minutes);
            int hrs = int.Parse(hours);
            if ((mins < 0) || (mins > 59) || (hrs < 0) || (hrs > 23))
                throw new FormatException();
        }

        private void isPriceValid(string price)
        {
            int prc = int.Parse(price);
            if (prc < 0)
                throw new FormatException();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }

}
