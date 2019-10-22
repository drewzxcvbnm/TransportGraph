using System;
using System.Collections.Generic;
using ConsoleApplication1;
using ConsoleApplication1.Structure;
using NUnit.Framework;
using static ConsoleApplication1.Structure.GraphAssembler;

namespace GraphCalculatorTests
{
    class TestDataProvider
    {
        internal static Func<long> GetFlow = FlowGetter(new long[] {10, 11, 9, 13, 16, 18, 20, 20, 26, 30, 28, 35});
        internal static Func<long> GetBandwidth = BandwidthGetter(new long[] {67, 54, 69}, new long[] {38, 60, 45, 42});


        internal static Func<long> FlowGetter(long[] flow)
        {
            int i = 0;
            List<long> flows = new List<long>(flow);
            return () => flows[i++];
        }

        public static Func<long> BandwidthGetter(long[] producerBands, long[] consumerBands)
        {
            int i = 0;
            List<long> bands = new List<long>();
            bands.Add(producerBands[0]);
            bands.AddRange(consumerBands);
            bands.Add(producerBands[1]);
            bands.AddRange(consumerBands);
            bands.Add(producerBands[2]);
            bands.AddRange(consumerBands);
            bands.AddRange(consumerBands);
            return () => bands[i++];
        }


        internal static Graph GetIncrementGraph()
        {
            List<Node> producers = new List<Node>() {Node.P1, Node.P2, Node.P3};
            List<Node> consumers = new List<Node>() {Node.C1, Node.C2, Node.C3, Node.C4};
            Graph g = new Graph();
            foreach (var producer in producers)
            {
                g.AddEdge(new IncrementEdge(Node.S, producer, GetBandwidth(), 0, false));
                foreach (var consumer in consumers)
                {
                    g.AddEdge(new IncrementEdge(producer, consumer, GetBandwidth(), GetFlow(), false));
                }
            }

            foreach (var consumer in consumers)
            {
                g.AddEdge(new IncrementEdge(consumer, Node.T, GetBandwidth(), 0, false));
            }

            return g;
        }
    }

    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Graph graph = AssembleGraph()
                .SetBands(new List<long>() {67, 54, 69}, new List<long>() {38, 60, 45, 42})
                .SetFlows(new List<long>() {10, 11, 9, 13, 16, 18, 20, 20, 26, 30, 28, 35})
                .Create();
            Pair<Graph, long> solve = new TransportationCostsCalculator(graph).Solve();
            Assert.AreEqual(3471, solve.Second);
        }

        [Test]
        public void Test2()
        {
            Graph graph = AssembleGraph()
                .SetBands(new List<long>() {63, 58, 69}, new List<long>() {43, 44, 46, 52})
                .SetFlows(new List<long>() {10, 11, 9, 13, 16, 18, 20, 20, 26, 30, 28, 35})
                .Create();
            Pair<Graph, long> solve = new TransportationCostsCalculator(graph).Solve();
            Assert.AreEqual(3497, solve.Second);
        }


        [Test]
        public void Test3()
        {
            Graph graph = AssembleGraph()
                .SetBands(new List<long>() {63, 68, 69}, new List<long>() {8, 90, 42, 52})
                .SetFlows(new List<long>() {10, 11, 15, 13, 16, 18, 20, 20, 26, 30, 28, 35})
                .Create();
            Pair<Graph, long> solve = new TransportationCostsCalculator(graph).Solve();
            Assert.AreEqual(3735, solve.Second);
        }

        [Test]
        public void Test4()
        {
            Graph graph = AssembleGraph()
                .SetBands(new List<long>() {63, 68, 69}, new List<long>() {800, 90, 42, 52})
                .SetFlows(new List<long>() {10, 11, 15, 13, 16, 18, 20, 20, 26, 30, 28, 35})
                .Create();
            Pair<Graph, long> solve = new TransportationCostsCalculator(graph).Solve();
            Assert.AreEqual(3512, solve.Second);
        }


        [Test]
        public void Test5()
        {
            Graph graph = AssembleGraph()
                .SetBands(new List<long>() {63, 68, 69}, new List<long>() {80, 90, 42, 52})
                .SetFlows(new List<long>() {101, 111, 151, 131, 161, 181, 201, 201, 261, 301, 281, 351})
                .Create();
            Pair<Graph, long> solve = new TransportationCostsCalculator(graph).Solve();
            Assert.AreEqual(37090, solve.Second);
        }
    }
}