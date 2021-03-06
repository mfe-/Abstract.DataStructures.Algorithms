﻿using System;

namespace DataStructures
{
    /// <summary>
    /// Can be used for linked lists
    /// </summary>
    /// <typeparam name="TNode"></typeparam>
    public interface INode<TNode> where TNode : class, INode<TNode>
    {
        /// <summary>
        /// Left
        /// </summary>
        TNode? V { get; set; }
        /// <summary>
        /// Right
        /// </summary>
        TNode? U { get; set; }
    }
}
