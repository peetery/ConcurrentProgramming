using System.ComponentModel;
using System.Runtime.CompilerServices;
using Model;

namespace ViewModel
{
    public class ViewModel : INotifyPropertyChanged
    {
        private ModelAbstractAPI _model;
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
