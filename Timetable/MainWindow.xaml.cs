using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Trainings;

namespace Timetable
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public IController controller;
        public MainWindow()
        {
            controller = new TimetableController();

            try
            {
                controller.loadData();
            }catch (Exception)
            {

            }

            InitializeComponent();
        }

        public MainWindow(string path)
        {
            controller = new TimetableController();

            InitializeComponent();

            try
            {
                controller.loadData(path);
            } 
            catch (FileNotFoundException)
            {
                MessageBox.Show("Файл не найден!");
            }
            catch (FormatException)
            {
                MessageBox.Show("Неверный формат данных в файле!");
            }
            catch (IOException)
            {
                MessageBox.Show("Ошибка при чтении данных из файла!");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TrainingList.ItemsSource = controller.Trainings;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Training selected = (Training)TrainingList.SelectedItem;
            if (selected == null)
                MessageBox.Show("Запись не выбрана!");
            else
                controller.Trainings.Remove(selected);
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Training selected = (Training) TrainingList.SelectedItem;
            if (selected == null)
                MessageBox.Show("Запись не выбрана!");
            else
            {
                TaskWindow window = new TaskWindow((Training training) => controller.edit(selected, training), selected);

                window.Owner = this;

                window.ShowDialog();
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            TaskWindow window = new TaskWindow( (Training training) => controller.addUniqueTraining(training) );

            window.Owner = this;

            window.ShowDialog();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            controller.saveData();
        }

        private void CostButton_Click(object sender, RoutedEventArgs e)
        {
            CostWindow window = new CostWindow(controller.Clients, (String clientName) => controller[clientName] );

            window.Owner = this;

            window.ShowDialog();
        }
    }
}
