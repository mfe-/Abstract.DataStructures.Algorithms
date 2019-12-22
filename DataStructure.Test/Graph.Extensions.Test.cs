﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Algorithms.Graph;
using Xunit;

namespace DataStructures.Test
{
    public class GraphExensionsTest
    {
        private readonly Graph _g;

        public GraphExensionsTest()
        {
            //constructor will be called for each test
            XElement xmlElement = XElement.Parse(EmbeddedResourceLoader.GetFileContents("dijkstra.xml"));
            xmlElement = xmlElement.Elements().FirstOrDefault(a => a.Name.LocalName.Equals("Graph", StringComparison.Ordinal));
            _g = GraphExtensions.Load(xmlElement);
        }

        [Fact]
        public void DepthFirstSearch_on_undirected_graph_should_find_one_path_from_v1_to_v6()
        {
            var v1 = _g.Start;
            var v6 = v1.Edges.First().V.Edges.Last().V;
            //1-6
            var resultv1 = v1.DepthFirstSearch(v6, false);
            //was v6 found?
            Assert.Equal(v6, resultv1.Last().V);
            //some possible solutions
            //1->3->6, 1->3->5->6,1->2->3->5->6,1->4->6
            //1->3->5->5->2->1->4->3->2->3->6
        }
        [Fact]
        public void DepthFirstSearch_on_undirected_graph_should_find_one_path_from_v6_to_v1()
        {
            var v1 = _g.Start;
            var v6 = v1.Edges.First().V.Edges.Last().V;
            //look for 1
            //6->3->1
            var resultv6 = v6.DepthFirstSearch(v1, false);
            Assert.Equal(resultv6.Last().V, v1);
        }
        [Fact]
        public void DepthFirstSearch_on_undirected_graph_should_find_one_path_from_v2_to_v2()
        {
            var v1 = _g.Start;
            var v2 = v1.Edges.Last().V;
            //loop 2->1 -> 2 (weil undirected)
            var resultv6 = v2.DepthFirstSearch(v2, false);
            Assert.Equal(resultv6.Last().V, v2);
        }
        [Fact]
        public void DepthFirstSearch_on_undirected_graph_should_find_all_edges()
        {
            var v1 = _g.Start;
            //providing no goal should visit all vertices in the graph
            var resultv = v1.DepthFirstSearch(null, false);
            //the graph contains 7 vertices
            Assert.Equal(7, resultv.ToVertexList().Distinct().Count());
        }
        [Fact]
        public void DepthFirstSearch_on_directed_graph_from_v1_to_v2_and_v1_to_v2()
        {
            //circule detection
            IVertex v1 = new Vertex<object>(1);
            IVertex v2 = new Vertex<object>(2);

            //2 undirected connected Vertices
            //v1->v2
            v1.AddEdge(v2);
            //v2->v1
            v2.AddEdge(v1);

            var resultva = v2.DepthFirstSearch(v2, true);
            Assert.Equal(resultva.First().U, resultva.Last().V);
        }
        [Fact]
        public void DepthFirstSearch_on_directed_graph_from_v2_to_v2_should_find_v2()
        {
            var v1 = _g.Start;
            var v2 = v1.Edges.Last().V;

            var result = v2.DepthFirstSearch(v2);
            Assert.True(result.Any());
            Assert.Equal(v2, result.Last().V);

        }
        [Fact]
        public void DepthFirstSearch_on_undirected_graph_from_v1_to_v3_should_find_no_goal()
        {
            //v1->v2->v3
            IVertex v1 = new Vertex<string>(1);
            IVertex v2 = new Vertex<string>(2);
            IVertex v3 = new Vertex<string>(3);

            v1.AddEdge(v2, 0, false);
            v2.AddEdge(v3, 0, false);

            var result = v1.DepthFirstSearch(v1, false);

            //the result should not contain v1 as there is only a way from
            //v1 -> v2 -> v3 but no other
            Assert.NotEqual(v1, result.Last().V);
            Assert.Equal(2, result.Count());
            Assert.Equal(v3, result.Last().V);
        }
        [Fact]
        public void DepthFirstSearch_on_directed_graph_from_v1_to_v3_should_not_find_v1_as_a_goal()
        {
            //v1->v2->v3
            IVertex v1 = new Vertex<string>(1);
            IVertex v2 = new Vertex<string>(2);
            IVertex v3 = new Vertex<string>(3);

            //v1->v2
            v1.AddEdge(v2, 0);
            //v2->v3
            v2.AddEdge(v3, 0);

            var result = v1.DepthFirstSearch(v1);

            //the result should not contain v1 as there is only a way from
            //v1 -> v2 -> v3 but no other
            Assert.NotEqual(v1, result.Last().V);
            Assert.Equal(2, result.Count());
            Assert.Equal(v3, result.Last().V);
        }
        [Fact]
        public void DepthFirstSearch_on_undirected_graph_from_v1_to_v3_but_with_directed_search_option_should_find_v1_as_a_goal()
        {
            //v1->v2->v3
            IVertex v1 = new Vertex<string>(1);
            IVertex v2 = new Vertex<string>(2);
            IVertex v3 = new Vertex<string>(3);

            //v1->v2
            //v2->v1
            v1.AddEdge(v2, 0, false);
            //v2->v3
            //v3->v2
            v2.AddEdge(v3, 0);

            var result = v1.DepthFirstSearch(v1);

            //the result should not contain v1 as there is only a way from
            //v1 -> v2 -> v3 but no other
            Assert.Equal(v1, result.Last().V);
            Assert.Equal(2, result.Count());
        }
        [Fact]
        public void DepthFirstSearch_on_undirected_graph_with_a_goal_which_does_not_exist_should_return_all_edges()
        {
            var edgeResultList = _g.Start.DepthFirstSearch(new Vertex<object>(), false);
            Assert.Equal(12, edgeResultList.Count());
            //should contain seven unique vertices
            Assert.Equal(7, edgeResultList.ToVertexList().Count());

        }
        [Fact]
        public void DepthFirstSearch_on_undirected_graph_with_tail_should_find_goal()
        {
            //example which occours in kruskal
            var v1 = new Vertex<object>(1);
            var v2 = new Vertex<object>(2);
            var v3 = new Vertex<object>(3);
            var v4 = new Vertex<object>(4);
            var v5 = new Vertex<object>(5);
            var v6 = new Vertex<object>(6);
            var v7 = new Vertex<object>(7);

            v3.AddEdge(v6, 0, false);
            v3.AddEdge(v4, 0, false);
            v1.AddEdge(v2, 0, false);
            v5.AddEdge(v6, 0, false);
            v5.AddEdge(v7, 0, false);
            v1.AddEdge(v4, 0, false);
            v4.AddEdge(v6, 0, false);

            var circule634 = v6.DepthFirstSearch(v6, false);
            Assert.Equal(circule634.Last().V, v6);

        }
        [Fact]
        public void DephFirstSearch_find_all_vertices_from_graph()
        {
            var dfsVerticesResultList = _g.Start.DephFirstSearch();
            var dfsEdgesResultList = _g.Start.DepthFirstSearch(new Vertex<object>(), false);

            dfsEdgesResultList.ToVertexList();

            Assert.Equal(7, dfsVerticesResultList.Count());


        }

        [Fact]
        public void AdjacencyList_should_return_expected_result()
        {
            int[][] result = _g.AdjacencyList();

            int[][] expected = { new int[]{0,2,5,3,0,0,0},
                              new int[]{2,0,4,0,6,0,0},
                              new int[]{5,4,0,1,4,1,0},
                              new int[]{3,0,1,0,0,3,0},
                              new int[]{0,6,4,0,0,2,2},
                              new int[]{0,0,1,3,2,0,5},
                              new int[]{0,0,0,0,2,5,0}
                             };

            for (int i = 0; i < result.Length; i++)
                for (int j = 0; j < result[i].Length; j++)
                    Assert.Equal(result[i][j], expected[i][j]);

        }


    }
}
