using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeakPaint.Models
{
    public class Figures 
    {
        public string? Name { get; set; }
        public Figures(string name)
        {
            Name = name;
        }
        public Figures() { }
        public string BeginPointSLineAndRectAndPoly { get; set; }
        public string EndPointSLineAndRectAndPOly { get; set; }
        public int Gaude { get; set; }
        public string PolygineAndPolyLineAndCustom { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int ColorNotAFill { get; set; }
        public int ColorFill { get; set; }
        public string Type { get; set; }
        public double Angle { get; set; }
        public double RotateX { get; set; }
        public double RotateY { get; set; }
        public double ScaleX { get; set; }
        public double ScaleY { get; set; }
        public double SkewX { get; set; }
        public double SkewY { get; set; }
    }
}
