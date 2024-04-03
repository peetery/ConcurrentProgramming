using System;
using System.Collections.ObjectModel;

namespace Data
{
    public class Table
    {
        private int _Width;
        private int _Height;
        private ObservableCollection<Ball> _Balls;

        public Table()
        {
            _Balls = new ObservableCollection<Ball>();
            _Width = 500;
            _Height = 500;
        }
    }
}