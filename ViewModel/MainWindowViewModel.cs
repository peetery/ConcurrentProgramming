using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModel
{
    public class MainWindowViewModel : ViewModel
    {
        private readonly ModelAbstractAPI _modelLayer;
        private int _ballsAmount;
        private IList _balls;
        private readonly int _width;
        private readonly int _height;

        public MainWindowViewModel(ModelAbstractAPI modelLayer)
        {
            _modelLayer = modelLayer;
            _width = _modelLayer.Width;
            _height = _modelLayer.Height;
        }

        public MainWindowViewModel () : this(ModelAbstractAPI.CreateModelAPI()) { }


    }
}
