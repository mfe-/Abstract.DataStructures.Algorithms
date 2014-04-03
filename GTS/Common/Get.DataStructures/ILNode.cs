﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Get.DataStructure;

namespace Get.DataStructure
{
    public interface ITreeNode<T, D> : INode<T, D>
        where T : INode<T,D>
    {
        T Parent { get; set; }
    }
}