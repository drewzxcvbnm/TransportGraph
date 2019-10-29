using System;
using System.Collections.Generic;
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
            return string.Format("[{0}->{1}, {2}]", From, To, Flow);
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
            return string.Format("[{0}->{1}, {2}/{3}]", From, To, Bandwidth, Flow);
        }
    }

    public class Graph<T> where T : Edge
    {
        private Dictionary<Node, List<T>> graph = new Dictionary<Node, List<T>>();

        public void AddEdge(T edge)
        {
            Node from = edge.From;
            Node to = edge.To;
            if (!graph.ContainsKey(from)) graph.Add(from, new List<T>());
            graph[from].Add(edge);
            if (!graph.ContainsKey(to)) graph.Add(to, new List<T>());
        }

        public int GetNumberOfNodes()
        {
            return graph.Keys.Count;
        }

        public int GetNumberOfEdges()
        {
            return graph.Values.Select(list => list.Count).Sum();
        }

        public List<T> GetEdges()
        {
            return new List<T>(graph.Values.SelectMany(list => list).ToArray());
        }

        public T FindEdge(T edge)
        {
            Node from = edge.From;
            T single = graph[from].Single(e => e.From == edge.From && e.To == edge.To);
            return single;
        }

        public T FindReverseEdge(T edge)
        {
            Node from = edge.To;
            T single = graph[from].Single(e => e.To == edge.From && e.From == edge.To);
            return single;
        }

        public void RemoveEdge(T edge)
        {
            Node from = edge.From;
            T single = graph[from].Single(e => e.From == edge.From && e.To == edge.To);
            graph[from].Remove(single);
        }

        public List<T> GetEdgesForNode(Node node)
        {
            return new List<T>(graph[node]);
        }

        public bool HasReversed(T edge)
        {
            List<T> edges = graph[edge.To];
            return edges.Any(e => e.To == edge.From && e.From == edge.To);
        }

        public override string ToString()
        {
            return "{\n\t" + string.Join("\t",
                       graph.Select(kv => kv.Key + "=" + "{" + string.Join<T>(",", kv.Value) + "}\n").ToArray()) +
                   "}";
        }
    }
}