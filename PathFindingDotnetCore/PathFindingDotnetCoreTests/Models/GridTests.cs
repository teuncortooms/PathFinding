using Microsoft.VisualStudio.TestTools.UnitTesting;
using PathFindingDotnetCore.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace PathFindingDotnetCore.Models.Tests
{
    [TestClass()]
    public class GridTests
    {
        [TestMethod()]
        public void GridTest()
        {
            Grid g = new Grid(10, 10);
        }

        [TestMethod()]
        public void GetGraph_WithoutWalls_Test()
        {
            Grid g = new Grid(3, 3);
            
            int[,] actual = g.GetGraph();
                                          //  0,  1,  2,  3,  4,  5,  6,  7,  8
            int[,] expected = new int[,] {  { 0,  1,  0,  1,  0,  0,  0,  0,  0 },  // 0
                                            { 1,  0,  1,  0,  1,  0,  0,  0,  0 },  // 1
                                            { 0,  1,  0,  0,  0,  1,  0,  0,  0 },  // 2
                                            { 1,  0,  0,  0,  1,  0,  1,  0,  0 },  // 3
                                            { 0,  1,  0,  1,  0,  1,  0,  1,  0 },  // 4
                                            { 0,  0,  1,  0,  1,  0,  0,  0,  1 },  // 5
                                            { 0,  0,  0,  1,  0,  0,  0,  1,  0 },  // 6
                                            { 0,  0,  0,  0,  1,  0,  1,  0,  1 },  // 7
                                            { 0,  0,  0,  0,  0,  1,  0,  1,  0 } };// 8

            CollectionAssert.AreEqual(actual, expected);
        }

        [TestMethod()]
        public void GetGraph_WithWalls_Test()
        {
            Grid g = new Grid(3, 3);
            g.Nodes2D[1, 1].IsWall = true;

            int[,] actual = g.GetGraph();
                                          //  0,  1,  2,  3,  4,  5,  6,  7,  8
            int[,] expected = new int[,] {  { 0,  1,  0,  1,  0,  0,  0,  0,  0 },  // 0
                                            { 1,  0,  1,  0,  0,  0,  0,  0,  0 },  // 1
                                            { 0,  1,  0,  0,  0,  1,  0,  0,  0 },  // 2
                                            { 1,  0,  0,  0,  0,  0,  1,  0,  0 },  // 3
                                            { 0,  0,  0,  0,  0,  0,  0,  0,  0 },  // 4
                                            { 0,  0,  1,  0,  0,  0,  0,  0,  1 },  // 5
                                            { 0,  0,  0,  1,  0,  0,  0,  1,  0 },  // 6
                                            { 0,  0,  0,  0,  0,  0,  1,  0,  1 },  // 7
                                            { 0,  0,  0,  0,  0,  1,  0,  1,  0 } };// 8

            CollectionAssert.AreEqual(actual, expected);
        }


        [TestMethod()]
        public void GetSourceTest()
        {
            Grid g = new Grid(3, 3);
            g.Nodes2D[1, 1].IsStart = true;

            int actual = g.GetSourceSerial();
            int expected = 4;

            Assert.AreEqual(actual, expected);
        }

        [TestMethod()]
        public void GetDestinationTest()
        {
            Grid g = new Grid(3, 3);
            g.Nodes2D[1, 1].IsFinish = true;

            int actual = g.GetDestinationSerial();
            int expected = 4;

            Assert.AreEqual(actual, expected);
        }
    }
}