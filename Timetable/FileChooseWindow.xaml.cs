using Microsoft.Win32;
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

namespace Timetable
{
    /// <summary>
    /// Interaction logic for FileChooseWindow.xaml
    /// </summary>
    public partial class FileChooseWindow : Window
    {
        public FileChooseWindow()
        {
            InitializeComponent();
        }

        private void chooseFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Multiselect = false;
            openFileDialog.ShowDialog();
            fileFiled.Text = openFileDialog.FileName;
        }

        private void withoutFileButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();

            Close();

            window.Show();
        }

        private void withFileButton_Click(object sender, RoutedEventArgs e)
        {
            if (fileFiled.Text == "")
                MessageBox.Show("Файл не выбран!");
            else
            {
                MainWindow window = new MainWindow(fileFiled.Text);

                Close();

                window.Show();
            }
        }
    }
}
