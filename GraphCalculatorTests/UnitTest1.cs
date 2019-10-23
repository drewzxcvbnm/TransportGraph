using System;
using System.Collections.Generic;
using ConsoleApplication1;
using ConsoleApplication1.Structure;
using NUnit.Framework;
using static ConsoleApplication1.Structure.GraphAssembler;

namespace GraphCalculatorTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Graph<IncrementEdge> graph = AssembleGraph()
                .SetBands(new List<long>() {67, 54, 69}, new List<long>() {38, 60, 45, 42})
                .SetFlows(new List<long>() {10, 11, 9, 13, 16, 18, 20, 20, 26, 30, 28, 35})
                .Create();
            Pair<Graph<Edge>, long> solve = new TransportationCostsCalculator(graph).Solve();
            Assert.AreEqual(3471, solve.Second);
        }

        [Test]
        public void Test2()
        {
            Graph<IncrementEdge> graph = AssembleGraph()
                .SetBands(new List<long>() {63, 58, 69}, new List<long>() {43, 44, 46, 52})
                .SetFlows(new List<long>() {10, 11, 9, 13, 16, 18, 20, 20, 26, 30, 28, 35})
                .Create();
            Pair<Graph<Edge>, long> solve = new TransportationCostsCalculator(graph).Solve();
            Assert.AreEqual(3497, solve.Second);
        }


        [Test]
        public void Test3()
        {
            Graph<IncrementEdge> graph = AssembleGraph()
                .SetBands(new List<long>() {63, 68, 69}, new List<long>() {8, 90, 42, 52})
                .SetFlows(new List<long>() {10, 11, 15, 13, 16, 18, 20, 20, 26, 30, 28, 35})
                .Create();
            Pair<Graph<Edge>, long> solve = new TransportationCostsCalculator(graph).Solve();
            Assert.AreEqual(3735, solve.Second);
        }

        [Test]
        public void Test4()
        {
            Graph<IncrementEdge> graph = AssembleGraph()
                .SetBands(new List<long>() {63, 68, 69}, new List<long>() {800, 90, 42, 52})
                .SetFlows(new List<long>() {10, 11, 15, 13, 16, 18, 20, 20, 26, 30, 28, 35})
                .Create();
            Pair<Graph<Edge>, long> solve = new TransportationCostsCalculator(graph).Solve();
            Assert.AreEqual(3512, solve.Second);
        }


        [Test]
        public void Test5()
        {
            Graph<IncrementEdge> graph = AssembleGraph()
                .SetBands(new List<long>() {63, 68, 69}, new List<long>() {80, 90, 42, 52})
                .SetFlows(new List<long>() {101, 111, 151, 131, 161, 181, 201, 201, 261, 301, 281, 351})
                .Create();
            Pair<Graph<Edge>, long> solve = new TransportationCostsCalculator(graph).Solve();
            Assert.AreEqual(37090, solve.Second);
        }
    }
}