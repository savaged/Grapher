using GalaSoft.MvvmLight;
using OxyPlot;
using OxyPlot.Series;
using System;
using System.Collections.Generic;

namespace savaged.Grapher.Model
{
    public class Circle : ObservableObject
    {
        private readonly double _circleCentreX;
        private readonly double _circleCentreY;
        private readonly double _radius;

        public Circle(
            double circleCentreX,
            double circleCentreY,
            double radius)
        {
            _circleCentreX = circleCentreX;
            _circleCentreY = circleCentreY;
            _radius = radius;

            var lineSeries = GetLineSeries();
            PlotModel = new PlotModel();
            PlotModel.Series.Add(lineSeries);
        }

        public PlotModel PlotModel { get; }

        private LineSeries GetLineSeries()
        {
            var points = GetPoints();
            var lineSeries = new LineSeries();
            lineSeries.Points.AddRange(points);
            return lineSeries;
        }

        private IList<DataPoint> GetPoints()
        {
            var points = new List<DataPoint>();

            double step = 2 * Math.PI / 60;
            double theta = 0;
            double h = _circleCentreX;
            double k = _circleCentreY;
            double r = _radius;

            while (theta <= 360)
            {
                var x = h + r * Math.Cos(theta);
                var y = k + r * Math.Sin(theta);
                points.Add(new DataPoint(x, y));
                theta += step;
            }
            return points;
        }
    }
}
