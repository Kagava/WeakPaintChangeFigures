using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeakPaint.Models
{
    public class Figures : ITransformShape
    {
        public string? Name { get; set; }
        public Figures(string name)
        {
            Name = name;
        }
        public double RotateTransformDeg { get; set; }
        public double RotateTransformX { get; set; }
        public double RotateTransformY { get; set; }
        public double ScaleTransformX { get; set; }
        public double ScaleTransformY { get; set; }
        public double SkewTransformAngelX { get; set; }
        public double SkewTransformAngelY { get; set; }
    }
}
