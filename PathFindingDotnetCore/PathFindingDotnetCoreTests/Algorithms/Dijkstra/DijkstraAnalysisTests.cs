using Microsoft.VisualStudio.TestTools.UnitTesting;
using PathFindingDotnetCore.Algorithms.Dijkstra;
using System;
using System.Collections.Generic;
using System.Text;

namespace PathFindingDotnetCore.Algorithms.Dijkstra.Tests
{
    [TestClass()]
    public class DijkstraAnalysisTests
    {
        private int[,] graph;

        [TestInitialize()]
        public void Init()
        {
            this.graph = new int[,] { { 0,  4,  0,  0,  0,  0,  0,  8,  0 },
                                        { 4,  0,  8,  0,  0,  0,  0,  11, 0 },
                                        { 0,  8,  0,  7,  0,  4,  0,  0,  2 },
                                        { 0,  0,  7,  0,  9,  14, 0,  0,  0 },
                                        { 0,  0,  0,  9,  0,  10, 0,  0,  0 },
                                        { 0,  0,  4,  14, 10, 0,  2,  0,  0 },
                                        { 0,  0,  0,  0,  0,  2,  0,  1,  6 },
                                        { 8,  11, 0,  0,  0,  0,  1,  0,  7 },
                                        { 0,  0,  2,  0,  0,  0,  6,  7,  0 } };
        }

        [TestMethod()]
        public void Prop_VisitedInOrder_has_vertices_in_order_when_initialised()
        {
            DijkstraAnalysis d = new DijkstraAnalysis(graph, 0, 7);

            int[] actual = d.VisitedInOrder;
            int[] expected = new int[] { 0, 1, 7, 6, 5, 2, 8, 3, 0 };

            CollectionAssert.AreEqual(actual, expected);
        }

        [TestMethod()]
        public void Prop_Parents_has_parent_of_every_vertex_when_initialised()
        {
            DijkstraAnalysis d = new DijkstraAnalysis(graph, 0, 7);

            int[] actual = d.Parents;
            int[] expected = new int[] { -1, 0, 1, 2, 5, 6, 7, 0, 2 };

            CollectionAssert.AreEqual(actual, expected);
        }

        [TestMethod()]
        public void Prop_ShortestPathToDest_has_shortest_path_from_src_to_dest_when_initialised()
        {
            DijkstraAnalysis d = new DijkstraAnalysis(graph, 0, 7);

            int[] actual = d.ShortestPathToDest;
            int[] expected = new int[] { 0, 7 };

            CollectionAssert.AreEqual(actual, expected);
        }
    }
}