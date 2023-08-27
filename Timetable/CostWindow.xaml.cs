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
    /// Interaction logic for CostWindow.xaml
    /// </summary>
    public partial class CostWindow : Window
    {
        List<string> items;
        public delegate int SelectionChanged(string clientName);
        public event SelectionChanged onSelected;

        public CostWindow(List<string> items, SelectionChanged onSelected)
        {
            this.items = items;

            this.onSelected = onSelected;

            InitializeComponent();
        }

        private void clientNameComboBox_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void clientNameComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int cost = onSelected((string)clientNameComboBox.SelectedItem);
            if (cost >= 0)
                costBox.Text = onSelected( (string) clientNameComboBox.SelectedItem ).ToString();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            clientNameComboBox.ItemsSource = items;
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
