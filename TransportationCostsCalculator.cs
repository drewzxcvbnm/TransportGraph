using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleApplication1.Structure;
using static ConsoleApplication1.Utilities;

namespace ConsoleApplication1
{
    public class TransportationCostsCalculator
    {
        private long requiredFlow;
        private long talliedFlow;
        private Graph<Edge> flowGraph;
        private Graph<IncrementEdge> incGraph;
        private long price;

        public TransportationCostsCalculator(Graph<IncrementEdge> incGraph)
        {
            this.incGraph = incGraph;
            this.flowGraph = GetFlowGraphFromIncrementGraph(incGraph);
            requiredFlow = incGraph.GetEdgesForNode(Node.P1).Select(e => ((IncrementEdge) e).Bandwidth).Sum();
            talliedFlow = 0;
        }

        public Pair<Graph<Edge>, long> Solve()
        {
            Calculate();
            return new Pair<Graph<Edge>, long>(flowGraph, price);
        }

        private void Calculate()
        {
            while (talliedFlow < requiredFlow)
            {
                List<IncrementEdge> bestPath = Utilities.BellmanFord(incGraph, Node.S)[Node.T].Second
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
            bestPath.ForEach(e => UpdateIncrementEdge(delta, e));
        }

        private void UpdateIncrementEdge(long delta, IncrementEdge incrementEdge)
        {
            IncrementEdge edge = incGraph.FindEdge(incrementEdge);
            edge.Bandwidth -= delta;
            if (edge.Bandwidth == 0) incGraph.RemoveEdge(edge);
            if (!incGraph.HasReversed(edge))
                incGraph.AddEdge(new IncrementEdge(edge.To, edge.From, 0, edge.Flow * -1, true));
            incGraph.FindReverseEdge(edge).Bandwidth += delta;
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
                flowGraph.FindReverseEdge(edge).Flow -= addition;
                return;
            }

            flowGraph.FindEdge(query).Flow += addition;
        }
    }
}