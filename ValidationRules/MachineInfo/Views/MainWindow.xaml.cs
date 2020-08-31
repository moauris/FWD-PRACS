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
using MachineInfo.Models;

namespace MachineInfo.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnWindowMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void OnFormAreaLoaded(object sender, RoutedEventArgs e)
        {
            sPanelFormArea.DataContext = new MachineInfoData();
        }

        private void OnFormAreaError(object sender, ValidationErrorEventArgs e)
        {

        }

        private void OnCloseButtonClicked(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
