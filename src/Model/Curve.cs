using GalaSoft.MvvmLight;
using OxyPlot;
using OxyPlot.Series;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows;
using OxyPlot.Axes;

namespace savaged.Grapher.Model
{
    public class Curve : ObservableObject
    {
        private double _start;
        private double _end;
        private double _increment;
        private PlotModel _plotModel;
        private Func<double, double> _selectedFunc;
        private string _title;

        private Func<double, double> Add => (x) => x + Increment;
        private Func<double, double> Div => (x) => x / (Increment == 0 ? Increment : ulong.MinValue);
        private Func<double, double> Mod => (x) => x % Increment;
        private Func<double, double> Mul => (x) => x * Increment;
        private Func<double, double> Pow => (x) => Math.Pow(x, Increment);
        private Func<double, double> Sub => (x) => x - Increment;

        public Curve()
        {
            _start = -10;
            _end = 10;
            _increment = 0.1;

            PlotModel = new PlotModel();

            Functions = new Dictionary<string, Func<double, double>>
            {
                { nameof(Math.Abs), Math.Abs },
                { nameof(Math.Acos), Math.Acos },
                { nameof(Add), Add },
                { nameof(Math.Asin), Math.Asin },
                { nameof(Math.Atan), Math.Atan },
                { nameof(Math.Ceiling), Math.Ceiling },
                { nameof(Math.Cos), Math.Cos },
                { nameof(Math.Cosh), Math.Cosh },
                { nameof(Div), Div },
                { nameof(Math.Exp), Math.Exp },
                { nameof(Math.Floor), Math.Floor },
                { nameof(Math.Log), Math.Log },
                { nameof(Math.Log10), Math.Log10 },
                { nameof(Mod), Mod },
                { nameof(Mul), Mul },
                { nameof(Pow), Pow },
                { nameof(Math.Sin), Math.Sin },
                { nameof(Math.Sinh), Math.Sinh },
                { nameof(Math.Sqrt), Math.Sqrt },
                { nameof(Sub), Sub },
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
                _title = Functions.Where(i => i.Value == value).First().Key;
                PlotModel.Title = _title;
                RaisePropertyChanged(nameof(PlotModel.Title));
            }
        }

        public void Refresh()
        {
            PlotModel = new PlotModel { Title = _title };
            SetupGraph();
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

        private void SetupGraph()
        {
            SetAxes();
            SetFunctionSeries();
        }

        private void SetAxes()
        {
            // TODO make min & max available as properties on the UI
            var yAxis = new LinearAxis
            {
                Position = AxisPosition.Left,
                Minimum = -10,
                Maximum = 10
            };
            var xAxis = new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Minimum = -10,
                Maximum = 10
            };
            _plotModel.Axes.Add(yAxis);
            _plotModel.Axes.Add(xAxis);
        }

        private void SetFunctionSeries()
        {
            try
            {
                PlotModel.Series.Add(new FunctionSeries(
                    SelectedFunc,
                    Start,
                    End,
                    Increment,
                    $"{PlotModel.Title}(x)"));
            }
            catch (OutOfMemoryException)
            {
                MessageBox.Show(
                    "Oops! That's too much for me to handle!");
            }
        }
    }
}
