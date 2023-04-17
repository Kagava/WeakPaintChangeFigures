using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using Avalonia.Utilities;
using DynamicData;
using ReactiveUI;
using WeakPaint.Models;
using WeakPaint.ViewModels.Pages;
using static System.Convert;

namespace WeakPaint.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public int IndexColorFill = 0;
        public int IndexColorNotAFill = 0;

        public Figures IDKFigures;
        
        public bool Flag_Clear_Settigs = false;
        public string outputing = string.Empty;
        public Figures nameTochange;
        private object content;
        public ObservableCollection<ViewModelBase> vmbaseCollection;
        public ObservableCollection<Shape> shapes = new ObservableCollection<Shape>();
        public ObservableCollection<Figures> collectionsOfNames = new ObservableCollection<Figures>();
        public ObservableCollection<int> numbers = new ObservableCollection<int>();

        private MyColors mCConture; 
        private MyColors mCInside;

        public double RotateTransformAngle = 0;
        public string PointTurn = string.Empty;
        public string ScaleParameters = string.Empty;
        public string SkewParameters = string.Empty;

        public string name = string.Empty;

        private string BeginPointSLines = string.Empty;
        private string EndPointSLines = string.Empty;
        private int GaugeSLines = 0;

        private string PointsBrokenLine = string.Empty;
        private int GaugeBrokenLine = 0;

        private string PathCompositeFigures = string.Empty;
        private int GaugeCompositeFigure = 0;

        private string BeginPointEllipse = string.Empty;
        private int WidthEllipse = 0;
        private int HeightEllipse = 0;
        private int GaugeEllipse = 0;

        private string BeginPointRectangle = string.Empty;
        private int WidthRectangle = 0;
        private int HeightRectangle = 0;
        private int GaugeRectangle = 0;

        private string PointsPolygon = string.Empty;
        private int GaugePolygon = 0;

        public int Index = 0;

        public int GetSetIndexColorNotAFill { get => IndexColorNotAFill; set { this.RaiseAndSetIfChanged(ref IndexColorNotAFill, value); } }
        public int GetSetIndexColorFill { get => IndexColorFill; set { this.RaiseAndSetIfChanged(ref IndexColorFill, value); } }

        public string GetSetBeginPointSLines { get => BeginPointSLines; set { this.RaiseAndSetIfChanged(ref BeginPointSLines, value); } }
        public string GetSetEndPointSLines { get => EndPointSLines; set { this.RaiseAndSetIfChanged(ref EndPointSLines, value); } }
        public int GetSetGaugeSLines { get => GaugeSLines; set { this.RaiseAndSetIfChanged(ref GaugeSLines, value); } }

        public string GetSetPointsBrokenLine { get => PointsBrokenLine; set { this.RaiseAndSetIfChanged(ref PointsBrokenLine, value); } }
        public int GetSetGaugeBrokenLine { get => GaugeBrokenLine; set { this.RaiseAndSetIfChanged(ref GaugeBrokenLine, value); } }

        public string GetSetPathCompositeFigures { get => PathCompositeFigures; set { this.RaiseAndSetIfChanged(ref PathCompositeFigures, value); } }
        public int GetSetGaugeCompositeFigure { get => GaugeCompositeFigure; set { this.RaiseAndSetIfChanged(ref GaugeCompositeFigure, value); } }

        public string GetSetBeginPointEllipse { get => BeginPointEllipse; set { this.RaiseAndSetIfChanged(ref BeginPointEllipse, value); } }
        public int GetSetWidthEllipse { get => WidthEllipse; set { this.RaiseAndSetIfChanged(ref WidthEllipse, value); } }
        public int GetSetHeightEllipse { get => HeightEllipse; set { this.RaiseAndSetIfChanged(ref HeightEllipse, value); } }
        public int GetSetGaugeEllipse { get => GaugeEllipse; set { this.RaiseAndSetIfChanged(ref GaugeEllipse, value); } }

        public string GetSetBeginPointRectangle { get => BeginPointRectangle; set { this.RaiseAndSetIfChanged(ref BeginPointRectangle, value); } }
        public int GetSetWidthRectangle { get => WidthRectangle; set { this.RaiseAndSetIfChanged(ref WidthRectangle, value); } }
        public int GetSetHeightRectangle { get => HeightRectangle; set { this.RaiseAndSetIfChanged(ref HeightRectangle, value); } }
        public int GetSetGaugeRectangle { get => GaugeRectangle; set { this.RaiseAndSetIfChanged(ref GaugeRectangle, value); } }

        public string GetSetPointsPolygon { get => PointsPolygon; set { this.RaiseAndSetIfChanged(ref PointsPolygon, value); } }
        public int GetSetGaugePolygon { get => GaugePolygon; set { this.RaiseAndSetIfChanged(ref GaugePolygon, value); } }
        
        public int GetSetIndex { get => Index; set { this.RaiseAndSetIfChanged(ref Index, value); } }
        public double GetSetRotateTransformAngle { get => RotateTransformAngle; set { this.RaiseAndSetIfChanged(ref RotateTransformAngle, value); } }
        public string GetSetPointTurn { get => PointTurn; set { this.RaiseAndSetIfChanged(ref PointTurn, value); } }
        public string GetSetScaleParameters { get => ScaleParameters; set { this.RaiseAndSetIfChanged(ref ScaleParameters, value); } }
        public string GetSetSkewParameters { get => SkewParameters; set { this.RaiseAndSetIfChanged(ref SkewParameters, value); } }
        

        public ObservableCollection<MyColors> solors = new ObservableCollection<MyColors>();
        public MainWindowViewModel()
        {
            vmbaseCollection = new ObservableCollection<ViewModelBase>();
            vmbaseCollection.Add(new SLineViewModel());
            vmbaseCollection.Add(new BrokenLineViewModel());
            vmbaseCollection.Add(new MyPolygonViewModel());
            vmbaseCollection.Add(new MyRectangleViewModel());
            vmbaseCollection.Add(new MyEl1ipseViewModel());
            vmbaseCollection.Add(new CompositeFiguresViewModel());
            Content = VmbaseCollection[0];
           
            PropertyInfo[] colorProps = typeof(Colors).GetProperties(BindingFlags.Public | BindingFlags.Static);
            foreach (PropertyInfo colorProp in colorProps)
            {
                if (colorProp.PropertyType == typeof(Color))
                {
                    Color color = (Color)colorProp.GetValue(null, null);
                    string colorName = colorProp.Name;
                    SolidColorBrush brush = new SolidColorBrush(color);
                    MyColors item = new MyColors() { Brush = brush, Color = colorName };
                    solors.Add(item);
                }
            }
            Flag_Clear_Settigs = true;
            mCConture = solors[2];
            mCInside = solors[2];
            AddFigure = ReactiveCommand.Create(() =>
            {
                bool Flag_Block_Input = false;
                for (int i = 0; i <  CollectionsOfNames.Count; i++) 
                {
                    if (CollectionsOfNames[i].Name == name)
                    {
                        Flag_Block_Input = true;
                        break;
                    }
                }
                if (!Flag_Block_Input)
                {
                    if (Content == vmbaseCollection[0])
                    {
                        GetIndexsColor();
                        Line line = new Line();
                        Figures fig = new Figures(Name);
                        fig.Type = "line";
                        fig.BeginPointSLineAndRectAndPoly = BeginPointSLines;
                        fig.EndPointSLineAndRectAndPOly = EndPointSLines;
                        fig.Gaude = GaugeSLines;
                        fig.ColorNotAFill = IndexColorNotAFill;
                        line.StrokeThickness = GaugeSLines;
                        line.Stroke = mCConture.Brush;
                        line.StartPoint = Avalonia.Point.Parse(BeginPointSLines);
                        line.EndPoint = Avalonia.Point.Parse(EndPointSLines);
                        TransformGroup transformGroup1 = new TransformGroup();
                        if (GetSetPointTurn != string.Empty)
                        {
                            RotateTransform rotateTransform1 = new RotateTransform(GetSetRotateTransformAngle);
                            string ValueToRotateX = GetSetPointTurn.Substring(0, GetSetPointTurn.IndexOf(' '));
                            string ValueToRotateY = GetSetPointTurn.Substring(ValueToRotateX.Length + 1);
                            fig.RotateX = ToDouble(ValueToRotateX);
                            fig.RotateY = ToDouble(ValueToRotateY);
                            fig.Angle = GetSetRotateTransformAngle;
                            rotateTransform1.CenterX = ToDouble(ValueToRotateX);
                            rotateTransform1.CenterY = ToDouble(ValueToRotateY);
                            transformGroup1.Children.Add(rotateTransform1);
                        }
                        if (GetSetScaleParameters != string.Empty)
                        {
                            ScaleTransform scaleTransform = new ScaleTransform();
                            string ValueToScaleX = GetSetScaleParameters.Substring(0, GetSetScaleParameters.IndexOf(' '));
                            string ValueToScaleY = GetSetScaleParameters.Substring(ValueToScaleX.Length + 1);
                            scaleTransform.ScaleX = ToDouble(ValueToScaleX);
                            scaleTransform.ScaleY = ToDouble(ValueToScaleY);
                            fig.ScaleX = ToDouble(ValueToScaleX);
                            fig.ScaleY = ToDouble(ValueToScaleX);
                            transformGroup1.Children.Add(scaleTransform);
                        }
                        if (GetSetSkewParameters != string.Empty)
                        {
                            SkewTransform skewTransform = new SkewTransform();
                            string ValueToSkewX = GetSetSkewParameters.Substring(0, GetSetSkewParameters.IndexOf(' '));
                            string ValueToSkewY = GetSetSkewParameters.Substring(ValueToSkewX.Length + 1);
                            skewTransform.AngleX = ToDouble(ValueToSkewX);
                            skewTransform.AngleY = ToDouble(ValueToSkewY);
                            fig.SkewX = ToDouble(ValueToSkewX);
                            fig.SkewY = ToDouble(ValueToSkewY);
                            transformGroup1.Children.Add(skewTransform);
                        }
                        line.RenderTransform = transformGroup1;
                        Shapes.Add(line);
                        CollectionsOfNames.Add(fig);
                        Numbers.Add(0);
                        Clean();
                    }
                    if (Content == vmbaseCollection[1]) // BrockenLine
                    {
                        GetIndexsColor();
                        List<Avalonia.Point> listOfPoints = new List<Avalonia.Point>();
                        string[] words = GetSetPointsBrokenLine.Split(' ');
                        foreach (string word in words)
                        {
                            //0,0 65,0 78,26, 91,39
                            listOfPoints.Add(Avalonia.Point.Parse(word));
                        }
                        Polyline BLine = new Polyline();
                        BLine.StrokeThickness = GetSetGaugeBrokenLine;
                        BLine.Stroke = mCConture.Brush;
                        BLine.Points = listOfPoints;
                        Figures fig = new Figures(Name);
                        fig.Type = "BrokenLine";
                        fig.PolygineAndPolyLineAndCustom = GetSetPointsBrokenLine;
                        fig.Gaude = GetSetGaugeBrokenLine;
                        fig.ColorNotAFill = IndexColorNotAFill;
                        TransformGroup transformGroup1 = new TransformGroup();
                        if (GetSetPointTurn != string.Empty)
                        {
                            RotateTransform rotateTransform1 = new RotateTransform(GetSetRotateTransformAngle);
                            
                            string ValueToRotateX = GetSetPointTurn.Substring(0, GetSetPointTurn.IndexOf(' '));
                            string ValueToRotateY = GetSetPointTurn.Substring(ValueToRotateX.Length + 1);
                            rotateTransform1.CenterX = ToDouble(ValueToRotateX);
                            rotateTransform1.CenterY = ToDouble(ValueToRotateY);
                            transformGroup1.Children.Add(rotateTransform1);
                            fig.RotateX = ToDouble(ValueToRotateX);
                            fig.RotateY = ToDouble(ValueToRotateY);
                            fig.Angle = GetSetRotateTransformAngle;
                        }
                        if (GetSetScaleParameters != string.Empty)
                        {
                            ScaleTransform scaleTransform = new ScaleTransform();
                            string ValueToScaleX = GetSetScaleParameters.Substring(0, GetSetScaleParameters.IndexOf(' '));
                            string ValueToScaleY = GetSetScaleParameters.Substring(ValueToScaleX.Length + 1);
                            scaleTransform.ScaleX = ToDouble(ValueToScaleX);
                            scaleTransform.ScaleY = ToDouble(ValueToScaleY);
                            transformGroup1.Children.Add(scaleTransform);
                            fig.ScaleX = ToDouble(ValueToScaleX);
                            fig.ScaleY = ToDouble(ValueToScaleX);

                        }
                        if (GetSetSkewParameters != string.Empty)
                        {
                            SkewTransform skewTransform = new SkewTransform();
                            string ValueToSkewX = GetSetSkewParameters.Substring(0, GetSetSkewParameters.IndexOf(' '));
                            string ValueToSkewY = GetSetSkewParameters.Substring(ValueToSkewX.Length + 1);
                            skewTransform.AngleX = ToDouble(ValueToSkewX);
                            skewTransform.AngleY = ToDouble(ValueToSkewY);
                            transformGroup1.Children.Add(skewTransform);
                            fig.SkewX = ToDouble(ValueToSkewX);
                            fig.SkewY = ToDouble(ValueToSkewY);
                        }
                        BLine.RenderTransform = transformGroup1;
                        Shapes.Add(BLine);
                        CollectionsOfNames.Add(fig);
                        Numbers.Add(1);
                        Clean();
                    }
                    if (Content == vmbaseCollection[2]) // Polygone
                    {
                        GetIndexsColor();
                        List<Avalonia.Point> listOfPoints = new List<Avalonia.Point>();
                        string[] words = GetSetPointsPolygon.Split(' ');
                        foreach (string word in words)
                        {
                            listOfPoints.Add(Avalonia.Point.Parse(word));
                        }
                        Polygon poly = new Polygon();
                        poly.StrokeThickness = GetSetGaugePolygon;
                        poly.Stroke = mCConture.Brush;
                        poly.Points = listOfPoints;
                        poly.Fill = mCInside.Brush;
                        Figures fig = new Figures(Name);
                        fig.Type = "Polygone";
                        fig.Gaude = GetSetGaugePolygon;
                        fig.ColorNotAFill = IndexColorNotAFill;
                        fig.ColorFill = IndexColorFill;
                        fig.PolygineAndPolyLineAndCustom = GetSetPointsPolygon;
                        TransformGroup transformGroup1 = new TransformGroup();
                        if (GetSetPointTurn != string.Empty)
                        {
                            RotateTransform rotateTransform1 = new RotateTransform(GetSetRotateTransformAngle);
                            string ValueToRotateX = GetSetPointTurn.Substring(0, GetSetPointTurn.IndexOf(' '));
                            string ValueToRotateY = GetSetPointTurn.Substring(ValueToRotateX.Length + 1);
                            rotateTransform1.CenterX = ToDouble(ValueToRotateX);
                            rotateTransform1.CenterY = ToDouble(ValueToRotateY);
                            transformGroup1.Children.Add(rotateTransform1);
                            fig.RotateX = ToDouble(ValueToRotateX);
                            fig.RotateY = ToDouble(ValueToRotateY);
                            fig.Angle = GetSetRotateTransformAngle;
                        }
                        if (GetSetScaleParameters != string.Empty)
                        {
                            ScaleTransform scaleTransform = new ScaleTransform();
                            string ValueToScaleX = GetSetScaleParameters.Substring(0, GetSetScaleParameters.IndexOf(' '));
                            string ValueToScaleY = GetSetScaleParameters.Substring(ValueToScaleX.Length + 1);
                            scaleTransform.ScaleX = ToDouble(ValueToScaleX);
                            scaleTransform.ScaleY = ToDouble(ValueToScaleY);
                            transformGroup1.Children.Add(scaleTransform);
                            fig.ScaleX = ToDouble(ValueToScaleX);
                            fig.ScaleY = ToDouble(ValueToScaleX);
                        }
                        if (GetSetSkewParameters != string.Empty)
                        {
                            SkewTransform skewTransform = new SkewTransform();
                            string ValueToSkewX = GetSetSkewParameters.Substring(0, GetSetSkewParameters.IndexOf(' '));
                            string ValueToSkewY = GetSetSkewParameters.Substring(ValueToSkewX.Length + 1);
                            skewTransform.AngleX = ToDouble(ValueToSkewX);
                            skewTransform.AngleY = ToDouble(ValueToSkewY);
                            transformGroup1.Children.Add(skewTransform);
                            fig.SkewX = ToDouble(ValueToSkewX);
                            fig.SkewY = ToDouble(ValueToSkewY);
                        }
                        poly.RenderTransform = transformGroup1;
                        Shapes.Add(poly);
                        CollectionsOfNames.Add(fig);
                        Numbers.Add(2);
                        Clean();
                    }
                    if (Content == vmbaseCollection[3]) // Rectangle
                    {
                        GetIndexsColor();
                        Rectangle rect = new Rectangle();
                        rect.Width = GetSetWidthRectangle;
                        rect.Height = GetSetHeightRectangle;
                        rect.Stroke = mCConture.Brush;
                        rect.StrokeThickness = GetSetGaugeRectangle;
                        rect.Margin = Avalonia.Thickness.Parse(GetSetBeginPointRectangle);
                        rect.Fill = mCInside.Brush;
                        Figures fig = new Figures(Name);
                        fig.Type = "Rectangle";
                        fig.Gaude = GetSetGaugeRectangle;
                        fig.ColorNotAFill = IndexColorNotAFill;
                        fig.ColorFill = IndexColorFill;
                        fig.Width = GetSetWidthRectangle;
                        fig.Height = GetSetHeightRectangle;
                        fig.BeginPointSLineAndRectAndPoly = GetSetBeginPointRectangle;
                        TransformGroup transformGroup1 = new TransformGroup();
                        if (GetSetPointTurn != string.Empty)
                        {
                            RotateTransform rotateTransform1 = new RotateTransform(GetSetRotateTransformAngle);
                            string ValueToRotateX = GetSetPointTurn.Substring(0, GetSetPointTurn.IndexOf(' '));
                            string ValueToRotateY = GetSetPointTurn.Substring(ValueToRotateX.Length + 1);
                            rotateTransform1.CenterX = ToDouble(ValueToRotateX);
                            rotateTransform1.CenterY = ToDouble(ValueToRotateY);
                            transformGroup1.Children.Add(rotateTransform1);
                            fig.RotateX = ToDouble(ValueToRotateX);
                            fig.RotateY = ToDouble(ValueToRotateY);
                            fig.Angle = GetSetRotateTransformAngle;
                        }
                        if (GetSetScaleParameters != string.Empty)
                        {
                            ScaleTransform scaleTransform = new ScaleTransform();
                            string ValueToScaleX = GetSetScaleParameters.Substring(0, GetSetScaleParameters.IndexOf(' '));
                            string ValueToScaleY = GetSetScaleParameters.Substring(ValueToScaleX.Length + 1);
                            scaleTransform.ScaleX = ToDouble(ValueToScaleX);
                            scaleTransform.ScaleY = ToDouble(ValueToScaleY);
                            transformGroup1.Children.Add(scaleTransform);
                            fig.ScaleX = ToDouble(ValueToScaleX);
                            fig.ScaleY = ToDouble(ValueToScaleX);
                        }
                        if (GetSetSkewParameters != string.Empty)
                        {
                            SkewTransform skewTransform = new SkewTransform();
                            string ValueToSkewX = GetSetSkewParameters.Substring(0, GetSetSkewParameters.IndexOf(' '));
                            string ValueToSkewY = GetSetSkewParameters.Substring(ValueToSkewX.Length + 1);
                            skewTransform.AngleX = ToDouble(ValueToSkewX);
                            skewTransform.AngleY = ToDouble(ValueToSkewY);
                            transformGroup1.Children.Add(skewTransform);
                            fig.SkewX = ToDouble(ValueToSkewX);
                            fig.SkewY = ToDouble(ValueToSkewY);
                        }
                        rect.RenderTransform = transformGroup1;
                        Shapes.Add(rect);
                        CollectionsOfNames.Add(fig);
                        Numbers.Add(3);
                        Clean();
                    }
                    if (Content == vmbaseCollection[4]) // Ellipse
                    {
                        GetIndexsColor();
                        Ellipse elip = new Ellipse();
                        elip.Width = GetSetWidthEllipse;
                        elip.Height = GetSetHeightEllipse;
                        elip.Stroke = mCConture.Brush;
                        elip.StrokeThickness = GetSetGaugeEllipse;
                        elip.Margin = Avalonia.Thickness.Parse(GetSetBeginPointEllipse);
                        elip.Fill = mCInside.Brush;
                        TransformGroup transformGroup1 = new TransformGroup();
                        Figures fig = new Figures(Name);
                        fig.Type = "Elipse";
                        fig.Gaude = GetSetGaugeEllipse;
                        fig.ColorNotAFill = IndexColorNotAFill;
                        fig.ColorFill = IndexColorFill;
                        fig.Width = GetSetWidthEllipse;
                        fig.Height = GetSetHeightEllipse;
                        fig.PolygineAndPolyLineAndCustom = GetSetPointsPolygon;
                        if (GetSetPointTurn != string.Empty)
                        {
                            RotateTransform rotateTransform1 = new RotateTransform(GetSetRotateTransformAngle);
                            string ValueToRotateX = GetSetPointTurn.Substring(0, GetSetPointTurn.IndexOf(' '));
                            string ValueToRotateY = GetSetPointTurn.Substring(ValueToRotateX.Length + 1);
                            rotateTransform1.CenterX = ToDouble(ValueToRotateX);
                            rotateTransform1.CenterY = ToDouble(ValueToRotateY);
                            transformGroup1.Children.Add(rotateTransform1);
                            fig.RotateX = ToDouble(ValueToRotateX);
                            fig.RotateY = ToDouble(ValueToRotateY);
                            fig.Angle = GetSetRotateTransformAngle;
                        }
                        if (GetSetScaleParameters != string.Empty)
                        {
                            ScaleTransform scaleTransform = new ScaleTransform();
                            string ValueToScaleX = GetSetScaleParameters.Substring(0, GetSetScaleParameters.IndexOf(' '));
                            string ValueToScaleY = GetSetScaleParameters.Substring(ValueToScaleX.Length + 1);
                            scaleTransform.ScaleX = ToDouble(ValueToScaleX);
                            scaleTransform.ScaleY = ToDouble(ValueToScaleY);
                            transformGroup1.Children.Add(scaleTransform);
                            fig.ScaleX = ToDouble(ValueToScaleX);
                            fig.ScaleY = ToDouble(ValueToScaleX);
                        }
                        if (GetSetSkewParameters != string.Empty)
                        {
                            SkewTransform skewTransform = new SkewTransform();
                            string ValueToSkewX = GetSetSkewParameters.Substring(0, GetSetSkewParameters.IndexOf(' '));
                            string ValueToSkewY = GetSetSkewParameters.Substring(ValueToSkewX.Length + 1);
                            skewTransform.AngleX = ToDouble(ValueToSkewX);
                            skewTransform.AngleY = ToDouble(ValueToSkewY);
                            transformGroup1.Children.Add(skewTransform);
                            fig.SkewX = ToDouble(ValueToSkewX);
                            fig.SkewY = ToDouble(ValueToSkewY);
                        }
                        elip.RenderTransform = transformGroup1;
                        Shapes.Add(elip);
                        CollectionsOfNames.Add(fig);
                        Numbers.Add(4);
                        Clean();
                    }
                    if (Content == vmbaseCollection[5]) // CompositeFigure
                    {
                        GetIndexsColor();
                        //M 0,0 c 0,0 50,0 50,-50 c 0,0 50,0 50,50 h -50 v 50 l -50,50 Z
                        Path path = new Path();
                        path.Data = Geometry.Parse(GetSetPathCompositeFigures);
                        path.Stroke = mCConture.Brush;
                        path.StrokeThickness = GetSetGaugeCompositeFigure;
                        path.Fill = mCInside.Brush;
                        Figures fig = new Figures(Name);
                        fig.Type = "PathF";
                        fig.ColorFill = IndexColorFill;
                        fig.ColorNotAFill = IndexColorNotAFill;
                        fig.Gaude = GetSetGaugeCompositeFigure;
                        fig.PolygineAndPolyLineAndCustom = GetSetPathCompositeFigures;
                        TransformGroup transformGroup1 = new TransformGroup();
                        if (GetSetPointTurn != string.Empty)
                        {
                            RotateTransform rotateTransform1 = new RotateTransform(GetSetRotateTransformAngle);
                            string ValueToRotateX = GetSetPointTurn.Substring(0, GetSetPointTurn.IndexOf(' '));
                            string ValueToRotateY = GetSetPointTurn.Substring(ValueToRotateX.Length + 1);
                            rotateTransform1.CenterX = ToDouble(ValueToRotateX);
                            rotateTransform1.CenterY = ToDouble(ValueToRotateY);
                            transformGroup1.Children.Add(rotateTransform1);
                            fig.RotateX = ToDouble(ValueToRotateX);
                            fig.RotateY = ToDouble(ValueToRotateY);
                            fig.Angle = GetSetRotateTransformAngle;
                        }
                        if (GetSetScaleParameters != string.Empty)
                        {
                            ScaleTransform scaleTransform = new ScaleTransform();
                            string ValueToScaleX = GetSetScaleParameters.Substring(0, GetSetScaleParameters.IndexOf(' '));
                            string ValueToScaleY = GetSetScaleParameters.Substring(ValueToScaleX.Length + 1);
                            scaleTransform.ScaleX = ToDouble(ValueToScaleX);
                            scaleTransform.ScaleY = ToDouble(ValueToScaleY);
                            transformGroup1.Children.Add(scaleTransform);
                            fig.ScaleX = ToDouble(ValueToScaleX);
                            fig.ScaleY = ToDouble(ValueToScaleX);
                        }
                        if (GetSetSkewParameters != string.Empty)
                        {
                            SkewTransform skewTransform = new SkewTransform();
                            string ValueToSkewX = GetSetSkewParameters.Substring(0, GetSetSkewParameters.IndexOf(' '));
                            string ValueToSkewY = GetSetSkewParameters.Substring(ValueToSkewX.Length + 1);
                            skewTransform.AngleX = ToDouble(ValueToSkewX);
                            skewTransform.AngleY = ToDouble(ValueToSkewY);
                            transformGroup1.Children.Add(skewTransform);
                            fig.SkewX = ToDouble(ValueToSkewX);
                            fig.SkewY = ToDouble(ValueToSkewY);
                        }
                        path.RenderTransform = transformGroup1;
                        Shapes.Add(path);
                        CollectionsOfNames.Add(fig);
                        Numbers.Add(5);
                        Clean();
                    }
                }
                else
                {
                    Shapes.RemoveAt(GetSetIndex);
                    CollectionsOfNames.RemoveAt(GetSetIndex);
                    if (Content == vmbaseCollection[0])
                    {
                        GetIndexsColor();
                        Line line = new Line();
                        Figures fig = new Figures(Name);
                        fig.Type = "line";
                        fig.BeginPointSLineAndRectAndPoly = BeginPointSLines;
                        fig.EndPointSLineAndRectAndPOly = EndPointSLines;
                        fig.Gaude = GaugeSLines;
                        fig.ColorNotAFill = IndexColorNotAFill;
                        line.StrokeThickness = GaugeSLines;
                        line.Stroke = mCConture.Brush;
                        line.StartPoint = Avalonia.Point.Parse(BeginPointSLines);
                        line.EndPoint = Avalonia.Point.Parse(EndPointSLines);
                        TransformGroup transformGroup1 = new TransformGroup();
                        if (GetSetPointTurn != string.Empty)
                        {
                            RotateTransform rotateTransform1 = new RotateTransform(GetSetRotateTransformAngle);
                            string ValueToRotateX = GetSetPointTurn.Substring(0, GetSetPointTurn.IndexOf(' '));
                            string ValueToRotateY = GetSetPointTurn.Substring(ValueToRotateX.Length + 1);
                            fig.RotateX = ToDouble(ValueToRotateX);
                            fig.RotateY = ToDouble(ValueToRotateY);
                            fig.Angle = GetSetRotateTransformAngle;
                            rotateTransform1.CenterX = ToDouble(ValueToRotateX);
                            rotateTransform1.CenterY = ToDouble(ValueToRotateY);
                            transformGroup1.Children.Add(rotateTransform1);
                        }
                        if (GetSetScaleParameters != string.Empty)
                        {
                            ScaleTransform scaleTransform = new ScaleTransform();
                            string ValueToScaleX = GetSetScaleParameters.Substring(0, GetSetScaleParameters.IndexOf(' '));
                            string ValueToScaleY = GetSetScaleParameters.Substring(ValueToScaleX.Length + 1);
                            scaleTransform.ScaleX = ToDouble(ValueToScaleX);
                            scaleTransform.ScaleY = ToDouble(ValueToScaleY);
                            fig.ScaleX = ToDouble(ValueToScaleX);
                            fig.ScaleY = ToDouble(ValueToScaleX);
                            transformGroup1.Children.Add(scaleTransform);
                        }
                        if (GetSetSkewParameters != string.Empty)
                        {
                            SkewTransform skewTransform = new SkewTransform();
                            string ValueToSkewX = GetSetSkewParameters.Substring(0, GetSetSkewParameters.IndexOf(' '));
                            string ValueToSkewY = GetSetSkewParameters.Substring(ValueToSkewX.Length + 1);
                            skewTransform.AngleX = ToDouble(ValueToSkewX);
                            skewTransform.AngleY = ToDouble(ValueToSkewY);
                            fig.SkewX = ToDouble(ValueToSkewX);
                            fig.SkewY = ToDouble(ValueToSkewY);
                            transformGroup1.Children.Add(skewTransform);
                        }
                        line.RenderTransform = transformGroup1;
                        Shapes.Add(line);
                        CollectionsOfNames.Add(fig);
                        Numbers.Add(0);
                        Clean();
                    }
                    if (Content == vmbaseCollection[1]) // BrockenLine
                    {
                        GetIndexsColor();
                        List<Avalonia.Point> listOfPoints = new List<Avalonia.Point>();
                        string[] words = GetSetPointsBrokenLine.Split(' ');
                        foreach (string word in words)
                        {
                            //0,0 65,0 78,26, 91,39
                            listOfPoints.Add(Avalonia.Point.Parse(word));
                        }
                        Polyline BLine = new Polyline();
                        BLine.StrokeThickness = GetSetGaugeBrokenLine;
                        BLine.Stroke = mCConture.Brush;
                        BLine.Points = listOfPoints;
                        Figures fig = new Figures(Name);
                        fig.Type = "BrokenLine";
                        fig.PolygineAndPolyLineAndCustom = GetSetPointsBrokenLine;
                        fig.Gaude = GetSetGaugeBrokenLine;
                        fig.ColorNotAFill = IndexColorNotAFill;
                        TransformGroup transformGroup1 = new TransformGroup();
                        if (GetSetPointTurn != string.Empty)
                        {
                            RotateTransform rotateTransform1 = new RotateTransform(GetSetRotateTransformAngle);

                            string ValueToRotateX = GetSetPointTurn.Substring(0, GetSetPointTurn.IndexOf(' '));
                            string ValueToRotateY = GetSetPointTurn.Substring(ValueToRotateX.Length + 1);
                            rotateTransform1.CenterX = ToDouble(ValueToRotateX);
                            rotateTransform1.CenterY = ToDouble(ValueToRotateY);
                            transformGroup1.Children.Add(rotateTransform1);
                            fig.RotateX = ToDouble(ValueToRotateX);
                            fig.RotateY = ToDouble(ValueToRotateY);
                            fig.Angle = GetSetRotateTransformAngle;
                        }
                        if (GetSetScaleParameters != string.Empty)
                        {
                            ScaleTransform scaleTransform = new ScaleTransform();
                            string ValueToScaleX = GetSetScaleParameters.Substring(0, GetSetScaleParameters.IndexOf(' '));
                            string ValueToScaleY = GetSetScaleParameters.Substring(ValueToScaleX.Length + 1);
                            scaleTransform.ScaleX = ToDouble(ValueToScaleX);
                            scaleTransform.ScaleY = ToDouble(ValueToScaleY);
                            transformGroup1.Children.Add(scaleTransform);
                            fig.ScaleX = ToDouble(ValueToScaleX);
                            fig.ScaleY = ToDouble(ValueToScaleX);

                        }
                        if (GetSetSkewParameters != string.Empty)
                        {
                            SkewTransform skewTransform = new SkewTransform();
                            string ValueToSkewX = GetSetSkewParameters.Substring(0, GetSetSkewParameters.IndexOf(' '));
                            string ValueToSkewY = GetSetSkewParameters.Substring(ValueToSkewX.Length + 1);
                            skewTransform.AngleX = ToDouble(ValueToSkewX);
                            skewTransform.AngleY = ToDouble(ValueToSkewY);
                            transformGroup1.Children.Add(skewTransform);
                            fig.SkewX = ToDouble(ValueToSkewX);
                            fig.SkewY = ToDouble(ValueToSkewY);
                        }
                        BLine.RenderTransform = transformGroup1;
                        Shapes.Add(BLine);
                        CollectionsOfNames.Add(fig);
                        Numbers.Add(1);
                        Clean();
                    }
                    if (Content == vmbaseCollection[2]) // Polygone
                    {
                        GetIndexsColor();
                        List<Avalonia.Point> listOfPoints = new List<Avalonia.Point>();
                        string[] words = GetSetPointsPolygon.Split(' ');
                        foreach (string word in words)
                        {
                            listOfPoints.Add(Avalonia.Point.Parse(word));
                        }
                        Polygon poly = new Polygon();
                        poly.StrokeThickness = GetSetGaugePolygon;
                        poly.Stroke = mCConture.Brush;
                        poly.Points = listOfPoints;
                        poly.Fill = mCInside.Brush;
                        Figures fig = new Figures(Name);
                        fig.Type = "Polygone";
                        fig.Gaude = GetSetGaugePolygon;
                        fig.ColorNotAFill = IndexColorNotAFill;
                        fig.ColorFill = IndexColorFill;
                        fig.BeginPointSLineAndRectAndPoly = GetSetPointsPolygon;
                        TransformGroup transformGroup1 = new TransformGroup();
                        if (GetSetPointTurn != string.Empty)
                        {
                            RotateTransform rotateTransform1 = new RotateTransform(GetSetRotateTransformAngle);
                            string ValueToRotateX = GetSetPointTurn.Substring(0, GetSetPointTurn.IndexOf(' '));
                            string ValueToRotateY = GetSetPointTurn.Substring(ValueToRotateX.Length + 1);
                            rotateTransform1.CenterX = ToDouble(ValueToRotateX);
                            rotateTransform1.CenterY = ToDouble(ValueToRotateY);
                            transformGroup1.Children.Add(rotateTransform1);
                            fig.RotateX = ToDouble(ValueToRotateX);
                            fig.RotateY = ToDouble(ValueToRotateY);
                            fig.Angle = GetSetRotateTransformAngle;
                        }
                        if (GetSetScaleParameters != string.Empty)
                        {
                            ScaleTransform scaleTransform = new ScaleTransform();
                            string ValueToScaleX = GetSetScaleParameters.Substring(0, GetSetScaleParameters.IndexOf(' '));
                            string ValueToScaleY = GetSetScaleParameters.Substring(ValueToScaleX.Length + 1);
                            scaleTransform.ScaleX = ToDouble(ValueToScaleX);
                            scaleTransform.ScaleY = ToDouble(ValueToScaleY);
                            transformGroup1.Children.Add(scaleTransform);
                            fig.ScaleX = ToDouble(ValueToScaleX);
                            fig.ScaleY = ToDouble(ValueToScaleX);
                        }
                        if (GetSetSkewParameters != string.Empty)
                        {
                            SkewTransform skewTransform = new SkewTransform();
                            string ValueToSkewX = GetSetSkewParameters.Substring(0, GetSetSkewParameters.IndexOf(' '));
                            string ValueToSkewY = GetSetSkewParameters.Substring(ValueToSkewX.Length + 1);
                            skewTransform.AngleX = ToDouble(ValueToSkewX);
                            skewTransform.AngleY = ToDouble(ValueToSkewY);
                            transformGroup1.Children.Add(skewTransform);
                            fig.SkewX = ToDouble(ValueToSkewX);
                            fig.SkewY = ToDouble(ValueToSkewY);
                        }
                        poly.RenderTransform = transformGroup1;
                        Shapes.Add(poly);
                        CollectionsOfNames.Add(fig);
                        Numbers.Add(2);
                        Clean();
                    }
                    if (Content == vmbaseCollection[3]) // Rectangle
                    {
                        GetIndexsColor();
                        Rectangle rect = new Rectangle();
                        rect.Width = GetSetWidthRectangle;
                        rect.Height = GetSetHeightRectangle;
                        rect.Stroke = mCConture.Brush;
                        rect.StrokeThickness = GetSetGaugeRectangle;
                        rect.Margin = Avalonia.Thickness.Parse(GetSetBeginPointRectangle);
                        rect.Fill = mCInside.Brush;
                        Figures fig = new Figures(Name);
                        fig.Type = "Rectangle";
                        fig.Gaude = GetSetGaugeRectangle;
                        fig.ColorNotAFill = IndexColorNotAFill;
                        fig.ColorFill = IndexColorFill;
                        fig.Width = GetSetWidthRectangle;
                        fig.Height = GetSetHeightRectangle;
                        fig.BeginPointSLineAndRectAndPoly = GetSetBeginPointRectangle;
                        TransformGroup transformGroup1 = new TransformGroup();
                        if (GetSetPointTurn != string.Empty)
                        {
                            RotateTransform rotateTransform1 = new RotateTransform(GetSetRotateTransformAngle);
                            string ValueToRotateX = GetSetPointTurn.Substring(0, GetSetPointTurn.IndexOf(' '));
                            string ValueToRotateY = GetSetPointTurn.Substring(ValueToRotateX.Length + 1);
                            rotateTransform1.CenterX = ToDouble(ValueToRotateX);
                            rotateTransform1.CenterY = ToDouble(ValueToRotateY);
                            transformGroup1.Children.Add(rotateTransform1);
                            fig.RotateX = ToDouble(ValueToRotateX);
                            fig.RotateY = ToDouble(ValueToRotateY);
                            fig.Angle = GetSetRotateTransformAngle;
                        }
                        if (GetSetScaleParameters != string.Empty)
                        {
                            ScaleTransform scaleTransform = new ScaleTransform();
                            string ValueToScaleX = GetSetScaleParameters.Substring(0, GetSetScaleParameters.IndexOf(' '));
                            string ValueToScaleY = GetSetScaleParameters.Substring(ValueToScaleX.Length + 1);
                            scaleTransform.ScaleX = ToDouble(ValueToScaleX);
                            scaleTransform.ScaleY = ToDouble(ValueToScaleY);
                            transformGroup1.Children.Add(scaleTransform);
                            fig.ScaleX = ToDouble(ValueToScaleX);
                            fig.ScaleY = ToDouble(ValueToScaleX);
                        }
                        if (GetSetSkewParameters != string.Empty)
                        {
                            SkewTransform skewTransform = new SkewTransform();
                            string ValueToSkewX = GetSetSkewParameters.Substring(0, GetSetSkewParameters.IndexOf(' '));
                            string ValueToSkewY = GetSetSkewParameters.Substring(ValueToSkewX.Length + 1);
                            skewTransform.AngleX = ToDouble(ValueToSkewX);
                            skewTransform.AngleY = ToDouble(ValueToSkewY);
                            transformGroup1.Children.Add(skewTransform);
                            fig.SkewX = ToDouble(ValueToSkewX);
                            fig.SkewY = ToDouble(ValueToSkewY);
                        }
                        rect.RenderTransform = transformGroup1;
                        Shapes.Add(rect);
                        CollectionsOfNames.Add(fig);
                        Numbers.Add(3);
                        Clean();
                    }
                    if (Content == vmbaseCollection[4]) // Ellipse
                    {
                        GetIndexsColor();
                        Ellipse elip = new Ellipse();
                        elip.Width = GetSetWidthEllipse;
                        elip.Height = GetSetHeightEllipse;
                        elip.Stroke = mCConture.Brush;
                        elip.StrokeThickness = GetSetGaugeEllipse;
                        elip.Margin = Avalonia.Thickness.Parse(GetSetBeginPointEllipse);
                        elip.Fill = mCInside.Brush;
                        TransformGroup transformGroup1 = new TransformGroup();
                        Figures fig = new Figures(Name);
                        fig.Type = "Elipse";
                        fig.Gaude = GetSetGaugeEllipse;
                        fig.ColorNotAFill = IndexColorNotAFill;
                        fig.ColorFill = IndexColorFill;
                        fig.Width = GetSetWidthEllipse;
                        fig.Height = GetSetHeightEllipse;
                        fig.BeginPointSLineAndRectAndPoly = GetSetBeginPointEllipse;
                        if (GetSetPointTurn != string.Empty)
                        {
                            RotateTransform rotateTransform1 = new RotateTransform(GetSetRotateTransformAngle);
                            string ValueToRotateX = GetSetPointTurn.Substring(0, GetSetPointTurn.IndexOf(' '));
                            string ValueToRotateY = GetSetPointTurn.Substring(ValueToRotateX.Length + 1);
                            rotateTransform1.CenterX = ToDouble(ValueToRotateX);
                            rotateTransform1.CenterY = ToDouble(ValueToRotateY);
                            transformGroup1.Children.Add(rotateTransform1);
                            fig.RotateX = ToDouble(ValueToRotateX);
                            fig.RotateY = ToDouble(ValueToRotateY);
                            fig.Angle = GetSetRotateTransformAngle;
                        }
                        if (GetSetScaleParameters != string.Empty)
                        {
                            ScaleTransform scaleTransform = new ScaleTransform();
                            string ValueToScaleX = GetSetScaleParameters.Substring(0, GetSetScaleParameters.IndexOf(' '));
                            string ValueToScaleY = GetSetScaleParameters.Substring(ValueToScaleX.Length + 1);
                            scaleTransform.ScaleX = ToDouble(ValueToScaleX);
                            scaleTransform.ScaleY = ToDouble(ValueToScaleY);
                            transformGroup1.Children.Add(scaleTransform);
                            fig.ScaleX = ToDouble(ValueToScaleX);
                            fig.ScaleY = ToDouble(ValueToScaleX);
                        }
                        if (GetSetSkewParameters != string.Empty)
                        {
                            SkewTransform skewTransform = new SkewTransform();
                            string ValueToSkewX = GetSetSkewParameters.Substring(0, GetSetSkewParameters.IndexOf(' '));
                            string ValueToSkewY = GetSetSkewParameters.Substring(ValueToSkewX.Length + 1);
                            skewTransform.AngleX = ToDouble(ValueToSkewX);
                            skewTransform.AngleY = ToDouble(ValueToSkewY);
                            transformGroup1.Children.Add(skewTransform);
                            fig.SkewX = ToDouble(ValueToSkewX);
                            fig.SkewY = ToDouble(ValueToSkewY);
                        }
                        elip.RenderTransform = transformGroup1;
                        Shapes.Add(elip);
                        CollectionsOfNames.Add(fig);
                        Numbers.Add(4);
                        Clean();
                    }
                    if (Content == vmbaseCollection[5]) // CompositeFigure
                    {
                        GetIndexsColor();
                        //M 0,0 c 0,0 50,0 50,-50 c 0,0 50,0 50,50 h - 50 v 50 l - 50,50 Z
                        Path path = new Path();
                        path.Data = Geometry.Parse(GetSetPathCompositeFigures);
                        path.Stroke = mCConture.Brush;
                        path.StrokeThickness = GetSetGaugeCompositeFigure;
                        path.Fill = mCInside.Brush;
                        Figures fig = new Figures(Name);
                        fig.Type = "PathF";
                        fig.ColorFill = IndexColorFill;
                        fig.ColorNotAFill = IndexColorNotAFill;
                        fig.Gaude = GetSetGaugeCompositeFigure;
                        fig.PolygineAndPolyLineAndCustom = GetSetPathCompositeFigures;
                        TransformGroup transformGroup1 = new TransformGroup();
                        if (GetSetPointTurn != string.Empty)
                        {
                            RotateTransform rotateTransform1 = new RotateTransform(GetSetRotateTransformAngle);
                            string ValueToRotateX = GetSetPointTurn.Substring(0, GetSetPointTurn.IndexOf(' '));
                            string ValueToRotateY = GetSetPointTurn.Substring(ValueToRotateX.Length + 1);
                            rotateTransform1.CenterX = ToDouble(ValueToRotateX);
                            rotateTransform1.CenterY = ToDouble(ValueToRotateY);
                            transformGroup1.Children.Add(rotateTransform1);
                            fig.RotateX = ToDouble(ValueToRotateX);
                            fig.RotateY = ToDouble(ValueToRotateY);
                            fig.Angle = GetSetRotateTransformAngle;
                        }
                        if (GetSetScaleParameters != string.Empty)
                        {
                            ScaleTransform scaleTransform = new ScaleTransform();
                            string ValueToScaleX = GetSetScaleParameters.Substring(0, GetSetScaleParameters.IndexOf(' '));
                            string ValueToScaleY = GetSetScaleParameters.Substring(ValueToScaleX.Length + 1);
                            scaleTransform.ScaleX = ToDouble(ValueToScaleX);
                            scaleTransform.ScaleY = ToDouble(ValueToScaleY);
                            transformGroup1.Children.Add(scaleTransform);
                            fig.ScaleX = ToDouble(ValueToScaleX);
                            fig.ScaleY = ToDouble(ValueToScaleX);
                        }
                        if (GetSetSkewParameters != string.Empty)
                        {
                            SkewTransform skewTransform = new SkewTransform();
                            string ValueToSkewX = GetSetSkewParameters.Substring(0, GetSetSkewParameters.IndexOf(' '));
                            string ValueToSkewY = GetSetSkewParameters.Substring(ValueToSkewX.Length + 1);
                            skewTransform.AngleX = ToDouble(ValueToSkewX);
                            skewTransform.AngleY = ToDouble(ValueToSkewY);
                            transformGroup1.Children.Add(skewTransform);
                            fig.SkewX = ToDouble(ValueToSkewX);
                            fig.SkewY = ToDouble(ValueToSkewY);
                        }
                        path.RenderTransform = transformGroup1;
                        Shapes.Add(path);
                        CollectionsOfNames.Add(fig);
                        Numbers.Add(5);
                        Clean();
                    }
                }
            });
            ClearSetting = ReactiveCommand.Create(() =>
            {
                Clean();
            });
            DeleteCommand = ReactiveCommand.Create(() =>
            {
                Shapes.RemoveAt(GetSetIndex);
                CollectionsOfNames.RemoveAt(GetSetIndex);
            });
        }

        public object Content
        {
            get { return content; }
            set 
            {
                Clean();
                this.RaiseAndSetIfChanged(ref content, value);
            }
        }
        public MyColors MCConture
        {
            get { return mCConture; }
            set
            {
                this.RaiseAndSetIfChanged(ref mCConture, value);
            }
        
        }
        public MyColors MCInside
        {
            get { return mCInside; }
            set
            {
                this.RaiseAndSetIfChanged(ref mCInside, value);
            }

        }
        public ObservableCollection<ViewModelBase> VmbaseCollection
        {
            get { return vmbaseCollection; }
            set
            {
                this.RaiseAndSetIfChanged(ref vmbaseCollection, value);
            }
        }
        public ObservableCollection<MyColors> Solors 
        { 
            get { return solors; } 
            set 
            {
                this.RaiseAndSetIfChanged(ref solors, value);
            }
        }
        public ObservableCollection<Shape> Shapes
        {
            get { return shapes; }
            set
            {
                this.RaiseAndSetIfChanged(ref shapes, value);
            }
        }
        public ObservableCollection<Figures> CollectionsOfNames
        {
            get => collectionsOfNames;
            set
            {
                this.RaiseAndSetIfChanged(ref collectionsOfNames, value);
            }
        }
        public Figures TakeNameFromLB
        {
            get { return nameTochange; }
            set
            {
                this.RaiseAndSetIfChanged(ref nameTochange, value);
            }
        }
        public ObservableCollection<int> Numbers
        {
            get { return numbers; }
            set
            {
                this.RaiseAndSetIfChanged(ref numbers, value);
            }
        }
        public string Outputing
        {
            get { return outputing; }
            set { outputing = value; }
        }
        public ReactiveCommand<Unit, Unit> AddFigure { get; }
        public ReactiveCommand<Unit, Unit> ClearSetting { get; }
        public ReactiveCommand<Unit, Unit> DeleteCommand { get; }
       
        public string Name { get => name; set { this.RaiseAndSetIfChanged(ref name, value); } }
        private void Clean()
        {
            if (Flag_Clear_Settigs)
            {
                
                GetSetBeginPointSLines = string.Empty;
                GetSetEndPointSLines = string.Empty;
                GetSetGaugeSLines = 1;
                GetSetPointsBrokenLine = string.Empty;
                GetSetGaugeBrokenLine = 1;
                GetSetBeginPointEllipse = string.Empty;
                GaugeCompositeFigure = 0;
                GetSetBeginPointEllipse = string.Empty;
                GetSetWidthEllipse = 0;
                GetSetHeightEllipse = 0;
                GetSetGaugeEllipse = 1;
                GetSetBeginPointRectangle = string.Empty;
                GetSetWidthRectangle = 0;
                GetSetHeightRectangle = 0;
                GetSetGaugeRectangle = 1;
                GetSetPointsPolygon = string.Empty;
                GetSetGaugePolygon = 1;
                GetSetPathCompositeFigures = string.Empty;
                GetSetGaugeCompositeFigure = 1;
                MCConture = solors[2];
                MCInside = solors[2];
                Name = string.Empty;
                GetSetRotateTransformAngle = 0;
                GetSetPointTurn = string.Empty;
                GetSetScaleParameters = string.Empty;
                GetSetSkewParameters = string.Empty;
            }
            
        }
        public void GetIndexsColor()
        {
            int count = 0;
            foreach(MyColors color in solors)
            {
                if (color == mCConture)
                {
                    GetSetIndexColorNotAFill = count;
                }
                if (color == mCInside)
                {
                    GetSetIndexColorFill = count;
                }
                count++;
            }
        }
    }
   
}
