﻿using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication1.Structure
{
    public enum Node
    {
        S,
        T,
        P1,
        P2,
        P3,
        C1,
        C2,
        C3,
        C4
    }


    public class Edge
    {
        public Node From { get; set; }
        public Node To { get; set; }
        public long Flow { get; set; }

        public Edge(Node from, Node to, long flow)
        {
            From = from;
            To = to;
            Flow = flow;
        }

        public override string ToString()
        {
            return $"[{From}->{To}, {Flow}]";
        }
    }

    public class IncrementEdge : Edge
    {
        public long Bandwidth { get; set; }
        public bool IsReversed { get; set; }

        public IncrementEdge(Node from, Node to, long bandwidth, long flow, bool isReversed) : base(from, to, flow)
        {
            Bandwidth = bandwidth;
            IsReversed = isReversed;
        }

        public override string ToString()
        {
            return $"[{From}->{To}, {Bandwidth}/{Flow}]";
        }
    }

    public class Graph
    {
        private Dictionary<Node, List<Edge>> graph = new Dictionary<Node, List<Edge>>();

        public void AddEdge(Edge edge)
        {
            Node from = edge.From;
            Node to = edge.To;
            if (!graph.ContainsKey(from)) graph.Add(from, new List<Edge>());
            graph[from].Add(edge);
            if (!graph.ContainsKey(to)) graph.Add(to, new List<Edge>());
        }

        public int GetNumberOfNodes() => graph.Keys.Count;

        public int GetNumberOfEdges() => graph.Values.Select(list => list.Count).Sum();

        public List<Edge> GetEdges() => new List<Edge>(graph.Values.SelectMany(list => list).ToArray());

        public Edge FindEdge(Edge edge)
        {
            Node from = edge.From;
            Edge single = graph[from].Single(e => e.From == edge.From && e.To == edge.To);
            return single;
        }

        public void RemoveEdge(Edge edge)
        {
            Node from = edge.From;
            Edge single = graph[from].Single(e => e.From == edge.From && e.To == edge.To);
            graph[from].Remove(single);
        }

        public List<Edge> GetEdgesForNode(Node node) => new List<Edge>(graph[node]);

        public bool HasReversed(Edge edge)
        {
            List<Edge> edges = graph[edge.To];
            return edges.Any(e => e.To == edge.From && e.From == edge.To);
        }

        public override string ToString()
        {
            return "{\n\t" + string.Join("\t",
                       graph.Select(kv => kv.Key + "=" + "{" + string.Join<Edge>(",", kv.Value) + "}\n").ToArray()) +
                   "}";
        }
    }
}