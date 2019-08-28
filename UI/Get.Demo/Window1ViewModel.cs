﻿using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DataStructures.Demo
{
    public class Window1ViewModel : Prism.Mvvm.BindableBase
    {
        public Window1ViewModel()
        {
            Graph = new Graph() { Directed = true };
        }
        private ICommand _ClickCommand;
        public ICommand ClickCommand => _ClickCommand ?? (_ClickCommand = new DelegateCommand<IVertex<dynamic>>(OnClickCommand));
        private int _counter = 0;
        protected void OnClickCommand(IVertex<dynamic> param)
        {
            //param.Value = param.Value + 1;

        }
        private Graph _Graph;
        public Graph Graph
        {
            get { return _Graph; }
            set { SetProperty(ref _Graph, value, nameof(Graph)); }
        }


    }
}