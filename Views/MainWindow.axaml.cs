using System.IO;
using System.Linq;
using System.Reactive;
using System.Text.Json;
using System.Text;
using System.Xml.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using Avalonia.VisualTree;
using JetBrains.Annotations;
using ReactiveUI;
using WeakPaint.ViewModels;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Collections.ObjectModel;
using System.Xml;
using Avalonia.Controls.Shapes;
using WeakPaint.Models;
using System.Collections.Generic;
using Avalonia.Media;

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
        public async void SaveCnavasAsPNG(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExtension = ".PNG";
            string? result = await saveFileDialog.ShowAsync(this);
            var canvas = this.GetVisualDescendants().OfType<Canvas>().Where(canvas => canvas.Name.Equals("canvas")).FirstOrDefault();
            if (DataContext is MainWindowViewModel mainWindowViewModel)
            {
                if (result != null)
                {
                    var pxsize = new PixelSize((int)canvas.Bounds.Width, (int)canvas.Bounds.Height);
                    var size = new Size(canvas.Bounds.Width, canvas.Bounds.Height);
                    using (RenderTargetBitmap bitmap = new RenderTargetBitmap(pxsize, new Avalonia.Vector(96, 96)))
                    {
                        canvas.Measure(size);
                        canvas.Arrange(new Rect(size));
                        bitmap.Render(canvas);
                        bitmap.Save(result);
                    }
                }
            }
        }
        public async void SaveCnavasAsXML(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExtension = ".XML";
            string? result = await saveFileDialog.ShowAsync(this);
            if (DataContext is MainWindowViewModel mainWindowViewModel)
            {
                if (result != null)
                {
                    XmlSerializer xs = new XmlSerializer(typeof(ObservableCollection<Figures>));
                    using (XmlWriter writer = XmlWriter.Create(result))
                    {
                        xs.Serialize(writer, mainWindowViewModel.CollectionsOfNames);
                    }
                }
            }
        }
        public async void SaveCnavasAsJSON(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExtension = ".JSON";
            string? result = await saveFileDialog.ShowAsync(this);
            if (DataContext is MainWindowViewModel mainWindowViewModel)
            {
                if (result != null)
                {
                    using (FileStream fs = new FileStream(result, FileMode.OpenOrCreate))
                    {
                        string jsons = JsonSerializer.Serialize(mainWindowViewModel.CollectionsOfNames);
                        byte[] buffer = Encoding.Default.GetBytes(jsons);
                        fs.Write(buffer, 0, buffer.Length);
                    }
                }
            }
        }
        public async void OpenJSON(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            string[]? result = await openFileDialog.ShowAsync(this);
            if (DataContext is MainWindowViewModel mainWindowViewModel)
            {
                if (result != null)
                {
                    using (FileStream fs = new FileStream(result[0], FileMode.Open))
                    {
                        byte[] buffer = new byte[fs.Length];
                        fs.Read(buffer, 0, buffer.Length);
                        string jsonText = Encoding.Default.GetString(buffer);
                        mainWindowViewModel.Shapes.Clear();
                        mainWindowViewModel.CollectionsOfNames.Clear();
                        List<Figures> newList;
                        newList = JsonSerializer.Deserialize<List<Figures>>(jsonText)!;
                        foreach (Figures item in newList)
                        {
                            mainWindowViewModel.GetSetIndex++;
                            mainWindowViewModel.CollectionsOfNames.Add(item);
                            TransformGroup newGroup = new TransformGroup();
                            RotateTransform newRotate = new RotateTransform();
                            ScaleTransform newScale = new ScaleTransform();
                            SkewTransform newSkew = new SkewTransform();
                            newSkew.AngleX = item.SkewX;
                            newSkew.AngleY = item.SkewY;
                            newScale.ScaleX = item.ScaleX;
                            newScale.ScaleY = item.ScaleY;
                            newRotate.CenterX = item.RotateX;
                            newRotate.CenterY = item.RotateY;
                            newRotate.Angle = item.Angle;
                            newGroup.Children.Add(newRotate);
                            newGroup.Children.Add(newScale);
                            newGroup.Children.Add(newSkew);
                            if (item.Type == "line")
                            {
                                Line newShape = new Line
                                {
                                    RenderTransform = newGroup,
                                    StartPoint = Avalonia.Point.Parse(item.BeginPointSLineAndRectAndPoly),
                                    EndPoint = Avalonia.Point.Parse(item.EndPointSLineAndRectAndPOly),
                                    Stroke = mainWindowViewModel.Solors[item.ColorNotAFill].Brush,
                                    StrokeThickness = item.Gaude,

                                };
                                mainWindowViewModel.Shapes.Add(newShape);
                            }
                            if (item.Type == "Rectangle")
                            {
                                Rectangle newShape = new Rectangle
                                {
                                    RenderTransform = newGroup,
                                    Margin = Avalonia.Thickness.Parse(item.BeginPointSLineAndRectAndPoly),
                                    Width = item.Width,
                                    Height = item.Height,
                                    Fill = mainWindowViewModel.Solors[item.ColorFill].Brush,
                                    Stroke = mainWindowViewModel.Solors[item.ColorNotAFill].Brush,
                                    StrokeThickness = item.Gaude
                                };
                                mainWindowViewModel.Shapes.Add(newShape);
                            }
                            if (item.Type == "Elipse")
                            {
                                Ellipse newShape = new Ellipse
                                {
                                    RenderTransform = newGroup,
                                    Margin = Avalonia.Thickness.Parse(item.BeginPointSLineAndRectAndPoly),
                                    Width = item.Width,
                                    Height = item.Height,
                                    Fill = mainWindowViewModel.Solors[item.ColorFill].Brush,
                                    Stroke = mainWindowViewModel.Solors[item.ColorNotAFill].Brush,
                                    StrokeThickness = item.Gaude
                                };
                                mainWindowViewModel.Shapes.Add(newShape);
                            }
                            if (item.Type == "PathF")
                            {
                                Avalonia.Controls.Shapes.Path newShape = new Avalonia.Controls.Shapes.Path
                                {
                                    RenderTransform = newGroup,
                                    Data = Geometry.Parse(item.PolygineAndPolyLineAndCustom),
                                    Fill = mainWindowViewModel.Solors[item.ColorFill].Brush,
                                    Stroke = mainWindowViewModel.Solors[item.ColorNotAFill].Brush,
                                    StrokeThickness = item.Gaude
                                };
                                mainWindowViewModel.Shapes.Add(newShape);
                            }
                            if (item.Type == "BrokenLine")
                            {
                                List<Avalonia.Point> listOfPolyLinePoints = new List<Avalonia.Point>();
                                string[] words = item.PolygineAndPolyLineAndCustom.Split(' ');
                                foreach (string word in words)
                                {
                                    listOfPolyLinePoints.Add(Avalonia.Point.Parse(word));
                                }
                                Polyline newShape = new Polyline
                                {
                                    RenderTransform = newGroup,
                                    Points = listOfPolyLinePoints,
                                    Stroke = mainWindowViewModel.Solors[item.ColorNotAFill].Brush,
                                    StrokeThickness = item.Gaude
                                };
                                mainWindowViewModel.Shapes.Add(newShape);
                            }
                            if (item.Type == "Polygone")
                            {
                                List<Avalonia.Point> listOfPolyLinePoints = new List<Avalonia.Point>();
                                string[] words = item.PolygineAndPolyLineAndCustom.Split(' ');
                                foreach (string word in words)
                                {
                                    listOfPolyLinePoints.Add(Avalonia.Point.Parse(word));
                                }
                                Polyline newShape = new Polyline
                                {
                                    RenderTransform = newGroup,
                                    Points = listOfPolyLinePoints,
                                    Stroke = mainWindowViewModel.Solors[item.ColorNotAFill].Brush,
                                    Fill = mainWindowViewModel.Solors[item.ColorFill].Brush,
                                    StrokeThickness = item.Gaude
                                };
                                mainWindowViewModel.Shapes.Add(newShape);
                            }
                        }
                    }
                }
            }
        }
        public async void OpenXML(object sender, RoutedEventArgs args)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            string[]? result = await openFileDialog.ShowAsync(this);
            if (DataContext is MainWindowViewModel mainWindowViewModel)
            {
                if (result != null)
                {
                    mainWindowViewModel.CollectionsOfNames.Clear();
                    mainWindowViewModel.Shapes.Clear();
                    List<Figures> newList;
                    XmlSerializer xs = new XmlSerializer(typeof(List<Figures>));
                    using (XmlReader reader = XmlReader.Create(result[0]))
                    {
                        newList = (List<Figures>)xs.Deserialize(reader)!;
                        foreach (Figures item in newList)
                        {
                            mainWindowViewModel.GetSetIndex++;
                            mainWindowViewModel.CollectionsOfNames.Add(item);
                            TransformGroup newGroup = new TransformGroup();
                            RotateTransform newRotate = new RotateTransform();
                            ScaleTransform newScale = new ScaleTransform();
                            SkewTransform newSkew = new SkewTransform();
                            newSkew.AngleX = item.SkewX;
                            newSkew.AngleY = item.SkewY;
                            newScale.ScaleX = item.ScaleX;
                            newScale.ScaleY = item.ScaleY;
                            newRotate.CenterX = item.RotateX;
                            newRotate.CenterY = item.RotateY;
                            newRotate.Angle = item.Angle;
                            newGroup.Children.Add(newRotate);
                            newGroup.Children.Add(newScale);
                            newGroup.Children.Add(newSkew);
                            if (item.Type == "line")
                            {
                                Line newShape = new Line
                                {
                                    RenderTransform = newGroup,
                                    StartPoint = Avalonia.Point.Parse(item.BeginPointSLineAndRectAndPoly),
                                    EndPoint = Avalonia.Point.Parse(item.EndPointSLineAndRectAndPOly),
                                    Stroke = mainWindowViewModel.Solors[item.ColorNotAFill].Brush,
                                    StrokeThickness = item.Gaude,

                                };
                                mainWindowViewModel.Shapes.Add(newShape);
                            }
                            if (item.Type == "Rectangle")
                            {
                                Rectangle newShape = new Rectangle
                                {
                                    RenderTransform = newGroup,
                                    Margin = Avalonia.Thickness.Parse(item.BeginPointSLineAndRectAndPoly),
                                    Width = item.Width,
                                    Height = item.Height,
                                    Fill = mainWindowViewModel.Solors[item.ColorFill].Brush,
                                    Stroke = mainWindowViewModel.Solors[item.ColorNotAFill].Brush,
                                    StrokeThickness = item.Gaude
                                };
                                mainWindowViewModel.Shapes.Add(newShape);
                            }
                            if (item.Type == "Elipse")
                            {
                                Ellipse newShape = new Ellipse
                                {
                                    RenderTransform = newGroup,
                                    Margin = Avalonia.Thickness.Parse(item.BeginPointSLineAndRectAndPoly),
                                    Width = item.Width,
                                    Height = item.Height,
                                    Fill = mainWindowViewModel.Solors[item.ColorFill].Brush,
                                    Stroke = mainWindowViewModel.Solors[item.ColorNotAFill].Brush,
                                    StrokeThickness = item.Gaude
                                };
                                mainWindowViewModel.Shapes.Add(newShape);
                            }
                            if (item.Type == "PathF")
                            {
                                Avalonia.Controls.Shapes.Path newShape = new Avalonia.Controls.Shapes.Path
                                {
                                    RenderTransform = newGroup,
                                    Data = Geometry.Parse(item.PolygineAndPolyLineAndCustom),
                                    Fill = mainWindowViewModel.Solors[item.ColorFill].Brush,
                                    Stroke = mainWindowViewModel.Solors[item.ColorNotAFill].Brush,
                                    StrokeThickness = item.Gaude
                                };
                                mainWindowViewModel.Shapes.Add(newShape);
                            }
                            if (item.Type == "BrokenLine")
                            {
                                List<Avalonia.Point> listOfPolyLinePoints = new List<Avalonia.Point>();
                                string[] words = item.PolygineAndPolyLineAndCustom.Split(' ');
                                foreach (string word in words)
                                {
                                    listOfPolyLinePoints.Add(Avalonia.Point.Parse(word));
                                }
                                Polyline newShape = new Polyline
                                {
                                    RenderTransform = newGroup,
                                    Points = listOfPolyLinePoints,
                                    Stroke = mainWindowViewModel.Solors[item.ColorNotAFill].Brush,
                                    StrokeThickness = item.Gaude
                                };
                                mainWindowViewModel.Shapes.Add(newShape);
                            }
                            if (item.Type == "Polygone")
                            {
                                List<Avalonia.Point> listOfPolyLinePoints = new List<Avalonia.Point>();
                                string[] words = item.PolygineAndPolyLineAndCustom.Split(' ');
                                foreach (string word in words)
                                {
                                    listOfPolyLinePoints.Add(Avalonia.Point.Parse(word));
                                }
                                Polyline newShape = new Polyline
                                {
                                    RenderTransform = newGroup,
                                    Points = listOfPolyLinePoints,
                                    Stroke = mainWindowViewModel.Solors[item.ColorNotAFill].Brush,
                                    Fill = mainWindowViewModel.Solors[item.ColorFill].Brush,
                                    StrokeThickness = item.Gaude
                                };
                                mainWindowViewModel.Shapes.Add(newShape);
                            }
                        }
                    }
                }
            }
        }
    }
}
