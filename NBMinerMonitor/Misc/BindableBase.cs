using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace NBMinerMonitor.Misc
{
    /// <summary>
    /// База для MVVM
    /// </summary>
    public class BindableBase : INotifyPropertyChanged
    {
        public void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        //INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
