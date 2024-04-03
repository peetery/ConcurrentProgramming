using Data;
using System;
using System.Collections.ObjectModel;

namespace Logic
{
    public class LogicAPI
    {
        private readonly DataAPI _dataAPI;
        public Table table { get }
        public ObservableCollection<Ball> balls { get; } = new ObservableCollection<Ball>();
    }
}
