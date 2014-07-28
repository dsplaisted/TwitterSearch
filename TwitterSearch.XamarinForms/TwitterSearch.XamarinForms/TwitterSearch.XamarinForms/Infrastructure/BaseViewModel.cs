using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TwitterSearch.Infrastructure
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public virtual void Navigated(string navigationParameter)
        {

        }

        private string title = string.Empty;        
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        private string subTitle = string.Empty;
        public string Subtitle
        {
            get { return subTitle; }
            set { SetProperty(ref subTitle, value); }
        }

        private string icon = null;
        public string Icon
        {
            get { return icon; }
            set { SetProperty(ref icon, value); }
        }

        private bool isBusy;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }


        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] String propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))

            storage = value;
            this.OnPropertyChanged(propertyName);
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
