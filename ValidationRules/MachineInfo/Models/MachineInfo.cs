using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace MachineInfo.Models
{
    public class MachineInfoData : INotifyPropertyChanged, IEditableObject
    {
        private DataItem _copyItem;
        private DataItem _currentItem;

        public string HostName
        {
            get { return _currentItem.HostName; }
            set 
            { 
                _currentItem.HostName = value;
                OnPropertyChanged("HostName");
            }
        }
        public string Domain
        {
            get { return _currentItem.Domain; }
            set 
            { 
                _currentItem.Domain = value;
                OnPropertyChanged("Domain");
            }
        }

        public string IPv4
        {
            get { return _currentItem.IPv4; }
            set 
            { 
                _currentItem.IPv4 = value;
                OnPropertyChanged("IPv4");
            }
        }

        public string IPv6
        {
            get { return _currentItem.IPv6; }
            set 
            { 
                _currentItem.IPv6 = value;
                OnPropertyChanged("IPv6");
            }
        }

        public string OSInformation
        {
            get { return _currentItem.OSInformation; }
            set 
            { 
                _currentItem.OSInformation = value;
                OnPropertyChanged("OSInformation");
            }
        }

        private struct DataItem
        {
            internal string HostName;
            internal string Domain;
            internal string IPv4;
            internal string IPv6;
            internal string OSInformation;
            internal static DataItem NewItem()
            {
                return new DataItem
                {
                    HostName = "",
                    Domain = "",
                    IPv4 = "",
                    IPv6 = "",
                    OSInformation = ""
                };
            }
        }
        #region INotifyPropertyChanged Implementations
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
        #region IEditableObject Implementations
        public void BeginEdit()
        {
            _copyItem = _currentItem;
        }

        public void CancelEdit()
        {
            _currentItem = _copyItem;
            OnPropertyChanged("");
        }

        public void EndEdit()
        {
            _copyItem = DataItem.NewItem();
        }
        #endregion

    }
}
