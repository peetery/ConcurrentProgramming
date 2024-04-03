using System;
using System.Collections.ObjectModel;

namespace Data
{
    public class Table
    {
        public int Width { get; set; }
        public int Height { get; set; }

        private ObservableCollection<Ball> _Balls;

        public Table(int width, int height)
        {
            _Balls = new ObservableCollection<Ball>();
            Width = width;
            Height = height;
        }
    }
}