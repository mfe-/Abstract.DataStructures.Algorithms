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
using Get.Model.Graph;

namespace Get.Demo
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(Window1_Loaded);
        }

        void Window1_Loaded(object sender, RoutedEventArgs e)
        {
            Graph graph = new Graph();

            Vertex v1 = new Vertex(1);
            Vertex v2 = new Vertex(2);

            Vertex v3 = new Vertex(3);
            Vertex v4 = new Vertex(4);

            Vertex v5 = new Vertex(5);
            Vertex v6 = new Vertex(6);

            v1.addEge(v2);
            v2.addEge(v3);
            v3.addEge(v4);
            v4.addEge(v1);
            v1.addEge(v3);
            

            graph.addVertec(v1);
            //Get.Common.XML.WriteXmlSerializer(typeof(Graph), Environment.CurrentDirectory +"\\graph.xml", graph);

            _GraphVisualization.Graph = graph;
        }
    }
}
