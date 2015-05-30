using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CooperationWithOther
{
    public class NotifyingObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        int m_valueWithRandomAdded;
        public int ValueWithRandomAdded
        {
            get { return m_valueWithRandomAdded; }
            set
            {
                m_valueWithRandomAdded = value;
                m_valueWithRandomAdded += new Random((int)DateTime.Now.Ticks).Next();
                OnPropertyChanged();
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var handler = PropertyChanged;
            if (handler == null)
                return;

            handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
