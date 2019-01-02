using GalaSoft.MvvmLight;
using OxyPlot;
using OxyPlot.Series;
using System;
using System.Linq;
using System.Collections.Generic;

namespace savaged.Grapher.Model
{
    public class Curve : ObservableObject
    {
        private double _start;
        private double _end;
        private double _increment;
        private PlotModel _plotModel;
        private Func<double, double> _selectedFunc;

        public Curve()
        {
            _start = 0;
            _end = 10;
            _increment = 0.1;

            PlotModel = new PlotModel();

            Functions = new Dictionary<string, Func<double, double>>
            {

                { nameof(Math.Abs), Math.Abs },
                { nameof(Math.Acos), Math.Acos },
                { nameof(Math.Asin), Math.Asin },
                { nameof(Math.Atan), Math.Atan },
                { nameof(Math.Ceiling), Math.Ceiling },
                { nameof(Math.Cos), Math.Cos },
                { nameof(Math.Cosh), Math.Cosh },
                { nameof(Math.Exp), Math.Exp },
                { nameof(Math.Floor), Math.Floor },
                { nameof(Math.Log), Math.Log },
                { nameof(Math.Log10), Math.Log10 },
                { nameof(Math.Sin), Math.Sin },
                { nameof(Math.Sinh), Math.Sinh },
                { nameof(Math.Sqrt), Math.Sqrt },
                { nameof(Math.Tan), Math.Tan },
                { nameof(Math.Tanh), Math.Tanh }
            };
            SelectedFunc = Functions.Values.First();

            SetFunctionSeries();
        }

        public PlotModel PlotModel
        {
            get => _plotModel;
            private set => Set(ref _plotModel, value);
        }

        public IDictionary<string, Func<double, double>> Functions { get; }

        public Func<double, double> SelectedFunc
        {
            get => _selectedFunc;
            set
            {
                Set(ref _selectedFunc, value);
                PlotModel.Title = Functions.Where(i => i.Value == value).First().Key;
                RaisePropertyChanged(nameof(PlotModel.Title));
            }
        }

        public void Refresh()
        {
            PlotModel = new PlotModel();
            SetFunctionSeries();
            PlotModel.InvalidatePlot(true);
        }

        public double Start
        {
            get => _start;
            set => Set(ref _start, value);
        }

        public double End
        {
            get => _end;
            set => Set(ref _end, value);
        }

        public double Increment
        {
            get => _increment;
            set => Set(ref _increment, value);
        }

        private void SetFunctionSeries()
        {
            PlotModel.Series.Add(new FunctionSeries(
                SelectedFunc,
                Start,
                End,
                Increment,
                $"{PlotModel.Title}(x)"));
        }
    }
}
