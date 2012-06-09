﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Get.Common.Mathematics
{
    public static partial class Mathematics
    {
        public static int[] CreateVector(params int[] args)
        {
            int[] v = new int[args.Count()];
            //for (int i = 0; i < v.Count(); i++)
            //    v[i] = args[i];
            return v;
        }
        public static int[] CreateMatrix(params int[][] args)
        {
            //mxn matrix
            int[] v = new int[args.Count<int[]>()];
            for (int i = 0; i < args.Count(); i++)
                v[i] = args[i][0];
            return v;
        }
        public static int[] Add(this int[] a, int[] b)
        {
            if (a.Count<int>().Equals(b.Count<int>()))
            {
                int[] r = new int[a.Count()];
                for (int i = 0; i < a.Count(); i++)
                    r[i] = a[i] + b[i];

                return r;
            }
            else
            {
                throw new NotImplementedException();
            }
        }
        public static int[] Multiply(this int[] a, int b)
        {
            int[] v = new int[a.Count()];
            for (int i = 0; i < a.Count(); i++)
               v[i] = a[i] * b;
            return v;
        }
    }
    public struct Matrix
    {
        private int[][] _m;

        public Matrix(int m, int n)
        {
            _m = new int[m][];
            for (int i = 0; i < m; i++)
            {
                _m[i] = new int[n];
            }

        }
        public Matrix(params int[][] args)
        {
            _m = new int[args.Length][];
            for (int i = 0; i < args.Length; i++)
            {
                _m[i] = args[i];
            }

        }

        public int m
        {
            get
            {
                return _m.Length;
            }
        }
        public int n
        {
            get
            {
                return _m.First().Length;
            }
        }
    }
}