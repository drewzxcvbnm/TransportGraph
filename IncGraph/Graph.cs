using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication1.IncGraph
{
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
        

        public override string ToString()
        {
            return "{\n\t" + string.Join("\t",
                       graph.Select(kv => kv.Key + "=" + "{" + string.Join<Edge>(",", kv.Value) + "}\n").ToArray()) + "}";
        }
    }
}