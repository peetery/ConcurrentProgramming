using Logic;
using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModel
{
    public class MainWindowViewModel : ViewModel
    {
        private readonly ModelAbstractAPI _modelLayer;
        private readonly int _width;
        private readonly int _height;

        private int _ballsAmount;
        private ObservableCollection<BallLogic> _balls;

        public ICommand ClickButton { get; set; }
        public ICommand ExitButton { get; set; }

        public MainWindowViewModel(ModelAbstractAPI modelLayer)
        {
            _modelLayer = modelLayer;
            _width = _modelLayer.Width;
            _height = _modelLayer.Height;
        }

        public MainWindowViewModel() : this(ModelAbstractAPI.CreateModelAPI()) { }

        public ObservableCollection<BallLogic> ballsGroup
        {
            get => _balls;
            set
            {
                _balls = value;
                RaisePropertyChanged("ballsGroup");
            }
        }

        public int ballsAmount
        {
            get { return _ballsAmount; }
            set
            {
                _ballsAmount = value;
                RaisePropertyChanged("ballsAmount");
            }

        public int viewHeight
        {
            get { return _height; }
        }
        public int viewWidth
        {
            get { return _width; }
        }

        private void CreateHandler()
        {
            ballsGroup = _modelLayer.createBalls(_ballsAmount, 10);
            _modelLayer.StartSimulation();
        }

        private void ExitHandler()
        {
            _modelLayer.StopSimulation();
        }
    }
}
