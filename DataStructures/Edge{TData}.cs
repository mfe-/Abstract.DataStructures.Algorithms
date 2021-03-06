﻿using System.Runtime.Serialization;
using System.Diagnostics;

namespace DataStructures
{
    [DebuggerDisplay("U={U}->V={V},Edge ={Weighted}")]
    [DataContract(Namespace = "http://schemas.get.com/Graph/Edges")]
    public class Edge<TData> : Edge, IEdge<TData>
    {
        /// <summary>
        /// Initializes a new instance of the Edge class.
        /// </summary>
        /// <param name="u">Vertex of the Edge</param>
        /// <param name="v">Vertex of the Edge</param>
        public Edge(IVertex u, IVertex v) : base(u,v)
        {
#pragma warning disable CS8601 // Possible null reference assignment.
            Value = default;
#pragma warning restore CS8601 // Possible null reference assignment.
        }
        /// <summary>
        /// Initializes a new instance of the Edge class.
        /// </summary>
        /// <param name="u">Vertex of the Edge</param>
        /// <param name="v">Vertex of the Edge</param>
        /// <param name="weighted">Sets the Weighted of the Edge</param>
        public Edge(IVertex u, IVertex v, double weighted) : base(u, v,weighted)
        {
#pragma warning disable CS8601 // Possible null reference assignment.
            Value = default;
#pragma warning restore CS8601 // Possible null reference assignment.
        }
        public TData Value { get; set; }
    }
}
