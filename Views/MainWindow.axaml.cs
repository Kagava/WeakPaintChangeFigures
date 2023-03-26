using System.Reactive;
using System.Xml.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using JetBrains.Annotations;
using ReactiveUI;
using WeakPaint.ViewModels;

namespace WeakPaint.Views
{
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
        }
        public void SI(object sender, RoutedEventArgs e)
        {
            if (DataContext is MainWindowViewModel mainWindowViewModel)
            {
                int i = 0;
                bool Flag_Block_Input = false;
                for (i = 0; i < mainWindowViewModel.CollectionsOfNames.Count; i++)
                {
                    if (mainWindowViewModel.CollectionsOfNames[i].Name == mainWindowViewModel.TakeNameFromLB.Name)
                    {
                        break;
                    }
                }
                mainWindowViewModel.Content = mainWindowViewModel.VmbaseCollection[mainWindowViewModel.Numbers[i]];
            }
        }
        
    }
}
