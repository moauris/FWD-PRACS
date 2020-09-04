using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Xml;

namespace MachineInfo.Views
{
    /// <summary>
    /// Interaction logic for SelectOSInfo.xaml
    /// </summary>
    public partial class SelectOSInfo : Window
    {
        public SelectOSInfo()
        {
            InitializeComponent();
        }
        public string OSInfomation { get; set; }

        private void OnCloseButtonClicked(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OnSubmitButtonClicked(object sender, RoutedEventArgs e)
        {
            StringBuilder sbMessage = new StringBuilder();
            bool IsFulfilled = true;
            sbMessage.AppendLine("There are Items Missing:");
            if (cbxOSName.SelectedItem is null)
            {
                //MessageBox.Show("Please Select OS Name");
                sbMessage.AppendLine("Missing : OS Name");

                IsFulfilled = false;
            }
            if (cbxOSVersion.SelectedItem is null)
            {
                //MessageBox.Show("Please Select OS Version");
                sbMessage.AppendLine("Missing : OS Version");

                IsFulfilled = false;
            }
            if (cbxOSBit.SelectedItem is null)
            {
                sbMessage.AppendLine("Missing : OS Bit");

                IsFulfilled = false;
            }

            if (!IsFulfilled)
            {
                MessageBox.Show(sbMessage.ToString(), "Warning, Empty Combobox!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            // MessageBox.Show("All Items Fulfilled, syncing");
            OSInfomation = string.Format("{0} {1} {2}"
                , ConvertElementAttribute(cbxOSName.SelectedItem)
                , ConvertElementAttribute(cbxOSVersion.SelectedItem)
                , ConvertElementAttribute(cbxOSBit.SelectedItem));

            Close();


        }

        private string ConvertElementAttribute(object xml) => ((XmlElement)xml).GetAttribute("name");
    }
}
