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
            int i = 0;
            while (talliedFlow < requiredFlow)
            {
                Console.WriteLine("Iter " + (++i));
                List<IncrementEdge> bestPath = Algorithms.BellmanFord(incGraph, Node.S)[Node.T].Second
                    .Select(e => (IncrementEdge) e).ToList();
                var bands = bestPath.Select(e => e.Bandwidth).ToList();
                bands.Add(requiredFlow - talliedFlow);
                long delta = bands.Min();
                talliedFlow += delta;
                price += delta * bestPath.Select(e => e.Flow).Sum(); //1289 -> 1559 -> 1919
                Console.WriteLine("delta: " + delta + " min(" + string.Join(",", bands) + ")");
                Console.WriteLine("Dmin: " + bestPath.Select(e => e.Flow).Sum());
                Console.WriteLine("New Price: " + price);
                Console.WriteLine("Path: " + string.Join(",", bestPath));
                Console.WriteLine("");
                UpdateFlowGraph(bestPath, delta);
                UpdateIncrementGraph(delta, bestPath);
            }
        }

        private void UpdateIncrementGraph(long delta, List<IncrementEdge> bestPath)
        {
            bestPath.ForEach(e => UpdateIncrementEdge(delta, e));
        }

        private void UpdateIncrementEdge(long delta, IncrementEdge incrementEdge)
        {
            IncrementEdge edge = (IncrementEdge) incGraph.FindEdge(incrementEdge);
            edge.Bandwidth -= delta;
            if (edge.Bandwidth == 0) incGraph.RemoveEdge(edge);
            if (!incGraph.HasReversed(edge))
                incGraph.AddEdge(new IncrementEdge(edge.To, edge.From, 0, edge.Flow * -1, true));
            Edge query = new Edge(edge.To, edge.From, edge.Flow);
            ((IncrementEdge) incGraph.FindEdge(query)).Bandwidth += delta;
        }

        private void UpdateFlowGraph(List<IncrementEdge> bestPath, long delta)
        {
            bestPath.ForEach(e => UpdateFlowEdge(delta, e));
        }

        private void UpdateFlowEdge(long delta, IncrementEdge edge)
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