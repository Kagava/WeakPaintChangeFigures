using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeakPaint.ViewModels.Pages
{
    public class MyRectangleViewModel : ViewModelBase
    {
        private string Head = "Прямоугольник";
        private string Header
        {
            get => Head;
        }
    }
}
