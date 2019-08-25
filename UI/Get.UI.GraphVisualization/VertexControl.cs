﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using System.ComponentModel;
using System.Diagnostics;

[assembly: XmlnsDefinition("http://schemas.get.com/winfx/2009/xaml/Graph", "Get.UI")]
namespace DataStructures.UI
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:Get.UI.GraphVisualization.Vertex"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:Get.UI.GraphVisualization.Vertex;assembly=Get.UI.GraphVisualization.Vertex"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///    <local:VertexVisualization>
    ///       <graph:Vertex Weighted="2"></graph:Vertex>
    ///    </local:VertexVisualization>
    ///
    /// </summary>
    [ContentProperty("Vertex")]
    [TemplatePartAttribute(Name = "PART_Border", Type = typeof(Border))]
    public class VertexControl : Control, INotifyPropertyChanged
    {
        AdornerLayer _adornerLayer;
        AdornerItem _adornerItem;

        public VertexControl()
        {

        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (_adornerLayer == null)
            {
                _adornerLayer = AdornerLayer.GetAdornerLayer(this);
                _adornerItem = new AdornerItem(this);
                _adornerItem.MouseDown += new MouseButtonEventHandler(AdornerItem_MouseDown);
            }
            if (this.Template != null)
            {
                //Border = this.Template.FindName("PART_Border", this) as Border;
                //if (Parent != null)
                //{
                //    UIElement uIElement = Parent as UIElement;
                //    uIElement.GotFocus += (sender, eargs) =>
                //    {
                //        //Set Focus to our control when DesignerItem is IsSelected so that the GotFocus Event will be raised
                //        Focus();

                //    };
                //}

            }

        }

        protected void AdornerItem_MouseDown(object sender, MouseButtonEventArgs e)
        {
            RaiseEvent(e);
        }
        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (_adornerLayer.GetAdorners(this)==null)
                _adornerLayer.Add(_adornerItem);
        }
        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            if (!IsFocused.Equals(true))
                _adornerLayer.Remove(_adornerItem);
        }
        protected override void OnLostFocus(RoutedEventArgs e)
        {
            base.OnLostFocus(e);
            if(_adornerLayer!=null)
            _adornerLayer.Remove(_adornerItem);
        }
        
        static VertexControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(VertexControl), new FrameworkPropertyMetadata(typeof(VertexControl)));

            WidthProperty.OverrideMetadata(typeof(VertexControl), new FrameworkPropertyMetadata((double)40));
            HeightProperty.OverrideMetadata(typeof(VertexControl), new FrameworkPropertyMetadata((double)40));

            BorderThicknessProperty.OverrideMetadata(typeof(VertexControl), new FrameworkPropertyMetadata(new Thickness(1)));

            BorderBrushProperty.OverrideMetadata(typeof(VertexControl), new FrameworkPropertyMetadata(Brushes.Black));
            BackgroundProperty.OverrideMetadata(typeof(VertexControl), new FrameworkPropertyMetadata(Brushes.White));
        }

        public DataStructures.IVertex Vertex
        {
            get { return (DataStructures.IVertex)GetValue(VertexProperty); }
            set { SetValue(VertexProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Vertex.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VertexProperty =
            DependencyProperty.Register("Vertex", typeof(DataStructures.IVertex), typeof(VertexControl), new UIPropertyMetadata());

        private Point _Position;
        public virtual Point Position
        {
            get { return _Position; }
            set
            {
                if (_Position != value)
                {
                    _Position = value;
                    NotifyPropertyChanged("Position");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Notify using String property name
        /// </summary>
        protected void NotifyPropertyChanged(String propertyName)
        {
            this.VerifyPropertyName(propertyName);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Warns the developer if this object does not have
        /// a public property with the specified name. This 
        /// method does not exist in a Release build.
        /// </summary>
        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public void VerifyPropertyName(string propertyName)
        {
            #if !SILVERLIGHT
            // Verify that the property name matches a real,  
            // public, instance property on this object.
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
            {
                string msg = "Invalid property name: " + propertyName;


            }
            #endif
        }

        public static IntToStringConverter IntToStringConverter = new IntToStringConverter();
    }
    public class IntToStringConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return string.Empty;
            if (!value.GetType().Equals(typeof(int))) return string.Empty;
            if (parameter == null) return value;

            return value.ToString() + string.Empty + parameter;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return 0;
            if (!value.GetType().Equals(typeof(string))) return 0;

            int result = 0;

            if (Int32.TryParse(value.ToString(), out result))
            {
                return result;
            }
            else
            {
                return 0;
            }
        }
    }
}