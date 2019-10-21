using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleApplication1.IncGraph;
using static ConsoleApplication1.Node;

namespace ConsoleApplication1
{
    public class Algorithms
    {
        public static void BellmanFord(Graph graph, Node src)
        {
            int V = graph.GetNumberOfNodes(), E = graph.GetNumberOfEdges();

            Dictionary<Node, Pair<int, List<Edge>>> dist = new Dictionary<Node, Pair<int, List<Edge>>>();

            // Step 1: Initialize distances from src to all other 
            // vertices as INFINITE 
            foreach (Node value in Enum.GetValues(typeof(Node)))
            {
                dist.Add(value, new Pair<int, List<Edge>>(int.MaxValue, new List<Edge>()));
            }

            dist[src].First = 0;
            List<Edge> edges = graph.GetEdges();

            // Step 2: Relax all edges |V| - 1 times. A simple 
            // shortest path from src to any other vertex can 
            // have at-most |V| - 1 edges 
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

            // Step 3: check for negative-weight cycles. The above 
            // step guarantees shortest distances if graph doesn't 
            // contain negative weight cycle. If we get a shorter 
            // path, then there is a cycle. 
            for (int j = 0; j < E; ++j)
            {
                Node u = edges[j].From;
                Node v = edges[j].To;
                int weight = (int) edges[j].Flow;
                if (dist[u].First != int.MaxValue && dist[u].First + weight < dist[v].First)
                {
                    Console.WriteLine("Graph contains negative weight cycle");
                    return;
                }
            }

            foreach (var p in dist) p.Value.Second.Reverse();

            printArr(dist, V);
        }

        static void printArr(Dictionary<Node, Pair<int, List<Edge>>> dist, int V)
        {
            Console.WriteLine("Vertex Distance from Source");
            foreach (Node i in Enum.GetValues(typeof(Node)))
                Console.WriteLine(i + "\t\t" + dist[i].First + " [" + string.Join(",",dist[i].Second.Select(e => e.ToString()).ToArray()) +"]");
        }
    }
}