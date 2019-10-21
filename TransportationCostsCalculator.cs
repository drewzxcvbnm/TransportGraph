using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ConsoleApplication1.Graph;
using static ConsoleApplication1.MyClass;

namespace ConsoleApplication1
{
    public class TransportationCostsCalculator
    {
        private long requiredFlow;
        private long talliedFlow;
        private Graph.Graph flowGraph;
        private Graph.Graph incGraph;
        private long price;

        public TransportationCostsCalculator(Graph.Graph incGraph)
        {
            this.incGraph = incGraph;
            this.flowGraph = GetFlowGraphFromIncrementGraph(incGraph);
            requiredFlow = incGraph.GetEdgesForNode(Node.P1).Select(e => ((IncrementEdge) e).Bandwidth).Sum();
            talliedFlow = 0;
        }

        public Pair<Graph.Graph, long> Solve()
        {
            Calculate();
            return new Pair<Graph.Graph, long>(flowGraph, price);
        }

        private void Calculate()
        {
            while (talliedFlow < requiredFlow)
            {
                List<IncrementEdge> bestPath = Algorithms.BellmanFord(incGraph, Node.S)[Node.T].Second
                    .Select(e => (IncrementEdge) e).ToList();
                var bands = bestPath.Select(e => e.Bandwidth).ToList();
                bands.Add(requiredFlow - talliedFlow);
                long delta = bands.Min();
                talliedFlow += delta;
                price += delta * bestPath.Select(e => e.Flow).Sum();
                UpdateFlowGraph(bestPath, delta);
                UpdateIncrementGraph(delta, bestPath);
            }
        }

        private void UpdateIncrementGraph(long delta, List<IncrementEdge> bestPath)
        {
            foreach (var incrementEdge in bestPath)
            {
                IncrementEdge edge = (IncrementEdge) incGraph.FindEdge(incrementEdge);
                edge.Bandwidth -= delta;
                if (edge.Bandwidth == 0) incGraph.RemoveEdge(edge);
                if (!incGraph.HasReversed(edge))
                    incGraph.AddEdge(new IncrementEdge(edge.To, edge.From, delta, edge.Flow * -1, true));
            }
        }

        private void UpdateFlowGraph(List<IncrementEdge> bestPath, long delta)
        {
            foreach (var edge in bestPath)
            {
                Edge query = edge;
                long addition = delta;
                if (edge.IsReversed)
                {
                    addition *= -1;
                    query = new Edge(edge.To, edge.From, edge.Flow);
                }

                flowGraph.FindEdge(query).Flow += addition;
            }
        }
    }
}