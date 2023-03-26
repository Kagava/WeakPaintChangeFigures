using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Media;

namespace WeakPaint.Models
{
    public class TransformRectangle : ITransformShape
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public Brush? Color { get; set; }
        public double RotateTransformDeg { get; set; }
        public double RotateTransformX { get; set; }
        public double RotateTransformY { get; set; }
        public double ScaleTransformX { get; set; }
        public double ScaleTransformY { get; set; }
        public double SkewTransformAngelX { get; set; }
        public double SkewTransformAngelY { get; set; }
    }
}
