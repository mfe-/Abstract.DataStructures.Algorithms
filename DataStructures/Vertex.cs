﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.Serialization;
using System;
using System.Diagnostics;
using System.Linq;

namespace DataStructures
{
    [DebuggerDisplay("Vertex={Weighted},Value={Value},GUID={_Guid}")]
    [DataContract(Namespace = "http://schemas.get.com/Graph/Vertex")]
    public class Vertex<TData> : IVertex<TData>
    {
        private ObservableCollection<IEdge> _Edges;
        [DataMember(Name = "Guid", Order = 3, IsRequired = true)]
        private readonly Guid _Guid;
        private int _weighted;
        private TData _Data;

        /// <summary>
        /// Initializes a new instance of the Vertex class.
        /// </summary>
        public Vertex()
        {
            _Guid = Guid.NewGuid();
            _Edges = new ObservableCollection<IEdge>();
            _Data = default;
        }

        /// <summary>
        /// Initializes a new instance of the Vertex class that contains the specified weighted.
        /// </summary>
        /// <param name="weighted"></param>
        public Vertex(int weighted)
            : this()
        {
            _weighted = weighted;
        }
        [DataMember(Name = "Value", Order = 0, IsRequired = false)]
        public TData Value
        {
            get { return _Data; }
            set { _Data = value; NotifyPropertyChanged(nameof(Value)); }
        }
        /// <summary>
        /// Gets or sets the Weighted of the vertex
        /// </summary>
        [DataMember(Name = "Weighted", Order = 1, IsRequired = true)]
        public int Weighted { get { return _weighted; } set { _weighted = value; NotifyPropertyChanged(nameof(Weighted)); } }

        /// <summary>
        /// Gets or sets the list of edges which connects the vertex neighbours
        /// </summary>
        [DataMember(Name = "Edges", Order = 2, IsRequired = true)]
        public ObservableCollection<IEdge> Edges
        {
            get { return _Edges; }
            protected set { _Edges = value; NotifyPropertyChanged(nameof(Edges)); }
        }

        /// <summary>
        /// Amount of neighbours
        /// </summary>
        public int Size
        {
            get
            {
                return Edges.Count; //Knotengrad
            }
        }


        /// <summary>
        /// Creates a (un)directed edge to the overgiven Vertex
        /// </summary>
        /// <param name="u">Vertex to connect</param>
        /// <param name="weighted">Weighted of the Edge</param>
        /// <param name="directed">False if the edge should be undirected (2 edges); othwise directed (1 edge)</param>
        public virtual IEdge AddEdge(IVertex u, int weighted = 0, bool directed = true)
        {
            IEdge<TData> e1 = new Edge<TData>((IVertex)this, u, weighted);
            _Edges.Add(e1);
            if (!directed)
            {
                u?.AddEdge((IVertex)this, weighted, true);
            }
            return _Edges.Last();
        }

        public virtual void RemoveEdge(IVertex u)
        {
            RemoveEdge(u, true);
        }
        public virtual void RemoveEdge(IVertex u, bool directed)
        {
            IEdge edge = this.Edges.FirstOrDefault(a => a.U.Equals(this) && a.V.Equals(u));
            if (edge != null)
            {
                if (directed.Equals(false))
                {
                    IEdge edged = edge.V.Edges.FirstOrDefault(a => a.U.Equals(edge.V) && a.V.Equals(this) && a.Weighted.Equals(edge.Weighted));

                    edge.V.Edges.Remove(edged);
                }
                this.Edges.Remove(edge);
            }
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return base.ToString() + string.Empty + Weighted;
        }
        /// <summary>
        /// Determines with the guid whether the specified Object is equal to the current Object.
        /// http://msdn.microsoft.com/en-us/library/bsc2ak47.aspx
        /// </summary>
        /// <param name="obj">The object to compare with the current object. </param>
        /// <returns>true if the specified Object is equal to the current Object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (obj != null && !obj.GetType().Equals(typeof(Vertex<TData>))) return false;

            return this._Guid.Equals((obj as Vertex<TData>)?._Guid);
        }
        /// <summary>
        /// Returns the Hashvalue for this typ based on the internal used guid
        /// </summary>
        /// <returns>A hash code for the current Object.</returns>
        public override int GetHashCode()
        {
            return _Guid.GetHashCode();
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Notify using String property name
        /// </summary>
        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

    }
}