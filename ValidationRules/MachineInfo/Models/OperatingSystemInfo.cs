using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineInfo.Models
{
    public class OperatingSystemInfo : INotifyPropertyChanged
    {
        private string osname;

        public string OSName
        {
            get { return osname; }
            set 
            { 
                osname = value;
                OnPropertyChanged("OSName");
            }
        }

        private string osversion;

        public string OSVersion 
        {
            get { return osversion; }
            set
            {
                osversion = value;
                OnPropertyChanged("OSVersion");
            }
        }

        private string osarch;

        public string OSArch
        {
            get { return osarch; }
            set
            {
                osarch = value;
                OnPropertyChanged("OSArch");
            }
        }

        protected virtual void OnPropertyChanged(string PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
