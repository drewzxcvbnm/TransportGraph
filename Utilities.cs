using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleApplication1.Structure;

namespace ConsoleApplication1
{
    public class Utilities
    {
        public static Graph<Edge> GetFlowGraphFromIncrementGraph(Graph<IncrementEdge> inc)
        {
            Graph<Edge> g = new Graph<Edge>();
            foreach (var edge in inc.GetEdges().Select(e => new Edge(e.From, e.To, 0)))
            {
                g.AddEdge(edge);
            }

            return g;
        }

        public static Dictionary<Node, Pair<int, List<Edge>>> BellmanFord(Graph<IncrementEdge> graph, Node src)
        {
            int V = graph.GetNumberOfNodes(), E = graph.GetNumberOfEdges();

            Dictionary<Node, Pair<int, List<Edge>>> dist = new Dictionary<Node, Pair<int, List<Edge>>>();

            foreach (Node value in Enum.GetValues(typeof(Node)))
            {
                dist.Add(value, new Pair<int, List<Edge>>(int.MaxValue, new List<Edge>()));
            }

            dist[src].First = 0;
            List<IncrementEdge> edges = graph.GetEdges();

            for (int i = 1; i < V; ++i)
            {
                for (int j = 0; j < E; ++j)
                {
                    Node u = edges[j].From;
                    Node v = edges[j].To;
                    int weight = (int) edges[j].Flow;
                    if (dist[u].First != int.MaxValue && dist[u].First + weight < dist[v].First)
                    {
                        dist[v].First = dist[u].First + weight;
                        dist[v].Second.Clear();
                        dist[v].Second.Add(edges[j]);
                        dist[v].Second.AddRange(dist[u].Second);
                    }
                }
            }

            for (int j = 0; j < E; ++j)
            {
                Node u = edges[j].From;
                Node v = edges[j].To;
                int weight = (int) edges[j].Flow;
                if (dist[u].First != int.MaxValue && dist[u].First + weight < dist[v].First)
                {
                    Console.WriteLine("Graph contains negative weight cycle");
                    return null;
                }
            }

            foreach (var p in dist) p.Value.Second.Reverse();

            return dist;
        }

        public static void PrintResults(Dictionary<Node, Pair<int, List<Edge>>> dist)
        {
            Console.WriteLine("Vertex Distance from Source");
            foreach (Node i in Enum.GetValues(typeof(Node)))
                Console.WriteLine(i + "\t\t" + dist[i].First + " [" +
                                  string.Join(",", dist[i].Second.Select(e => e.ToString()).ToArray()) + "]");
        }
    }
}