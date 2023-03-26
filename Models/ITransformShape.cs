using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeakPaint.Models
{
    public interface ITransformShape
    {
        // RotateTransform
        double RotateTransformDeg { get; set; }
        double RotateTransformX { get; set; }
        double RotateTransformY { get; set; }

        // ScaleTransform
        double ScaleTransformX { get; set; }
        double ScaleTransformY { get; set; }

        // SkewTransform
        double SkewTransformAngelX { get; set; }
        double SkewTransformAngelY { get; set; }
    }
}
