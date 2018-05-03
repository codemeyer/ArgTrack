using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace ArgTrack.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        protected bool SetProperty<T>(ref T fieldReference, T newValue, Expression<Func<T>> property)
        {
            if (Equals(fieldReference, newValue)) return false;

            fieldReference = newValue;

            var memberExpression = property.Body as MemberExpression;
            OnPropertyChanged(memberExpression.Member.Name);

            return true;
        }
    }
}
