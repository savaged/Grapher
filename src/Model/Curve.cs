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
        private double _increment;
        private double _yAxisStart;
        private double _yAxisEnd;
        private double _xAxisStart;
        private double _xAxisEnd;
        private double _start;
        private double _end;
        private double _radius;
        private PlotModel _plotModel;
        private Func<double, double> _selectedFunc;
        private string _title;

        private Func<double, double> Add => (x) => x + Increment;
        private Func<double, double> Div => (x) => x / (Increment == 0 ? Increment : ulong.MinValue);
        private Func<double, double> Mod => (x) => x % Increment;
        private Func<double, double> Mul => (x) => x * Increment;
        private Func<double, double> Pow => (x) => Math.Pow(x, Increment);
        private Func<double, double> Sub => (x) => x - Increment;
        private Func<double, double> Rad => (x) => (Math.PI * x) / 180;
        private Func<double, double> Cir => Circle;

        public Curve()
        {
            YAxisStart = XAxisStart = 0;
            YAxisEnd = XAxisEnd = 5;
            Start = 0;
            End = 5;
            Increment = 0.1;
            Radius = (XAxisEnd - XAxisStart) / 2;

            PlotModel = new PlotModel();

            Functions = new Dictionary<string, Func<double, double>>
            {
                { nameof(Math.Abs), Math.Abs },
                { nameof(Math.Acos), Math.Acos },
                { nameof(Add), Add },
                { nameof(Math.Asin), Math.Asin },
                { nameof(Math.Atan), Math.Atan },
                { nameof(Math.Ceiling), Math.Ceiling },
                { nameof(Cir), Cir },
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
                { nameof(Rad), Rad },
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

        public double YAxisStart
        {
            get => _yAxisStart;
            set => Set(ref _yAxisStart, value);
        }
        public double YAxisEnd
        {
            get => _yAxisEnd;
            set => Set(ref _yAxisEnd, value);
        }

        public double XAxisStart
        {
            get => _xAxisStart;
            set => Set(ref _xAxisStart, value);
        }
        public double XAxisEnd
        {
            get => _xAxisEnd;
            set => Set(ref _xAxisEnd, value);
        }

        public double Radius
        {
            get => _radius;
            set => Set(ref _radius, value);
        }

        private void SetupGraph()
        {
            SetAxes();
            SetFunctionSeries();
        }

        private void SetAxes()
        {
            var yAxis = new LinearAxis
            {
                Position = AxisPosition.Left,
                Minimum = YAxisStart,
                Maximum = YAxisEnd
            };
            var xAxis = new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Minimum = XAxisStart,
                Maximum = XAxisEnd
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

        private double Circle(double degree)
        {
            // TODO Lots to do here. Anyone interested in coding this for us?
            var x = Radius * Math.Cos(degree);
            return x;
        }
    }
}
