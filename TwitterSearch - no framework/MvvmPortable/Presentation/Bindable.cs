using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace MvvmPortable.Presentation
{
    [DataContract]
    public abstract class Bindable : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] String propertyName = null)
        {
            Requires.NotNullOrEmpty(propertyName, "propertyName");

            if (object.Equals(storage, value)) return false;

            storage = value;
            this.OnPropertyChanged(propertyName);
            return true;
        }

        protected bool SetProperty<T>(ref T storage, T value, Expression<Func<T>> propertyExpression)
        {
            if (object.Equals(storage, value)) return false;

            var body = (MemberExpression)propertyExpression.Body;

            storage = value;
            this.OnPropertyChanged(body.Member.Name);
            return true;
        }

        protected void OnPropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            var body = (MemberExpression)propertyExpression.Body;
            OnPropertyChanged(body.Member.Name);
        }

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
