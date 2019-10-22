using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using ConsoleApplication1.Graph;

namespace ConsoleApplication1
{
    class MyClass
    {
        static Func<long> GetFlow = FlowGetter();
        private static Func<long> GetBandwidth = BandwidthGetter();

        public static Func<long> FlowGetter()
        {
            int i = 0;
            List<long> flows = new List<long>() {10, 11, 9, 13, 16, 18, 20, 20, 26, 30, 28, 35};
            return () => flows[i++];
        }

        public static Func<long> BandwidthGetter()
        {
            int i = 0;
            List<long> bands = new List<long>();
            bands.Add(67);
            bands.AddRange(new List<long>() {38, 60, 45, 42});
            bands.Add(54);
            bands.AddRange(new List<long>() {38, 60, 45, 42});
            bands.Add(69);
            bands.AddRange(new List<long>() {38, 60, 45, 42});
            bands.AddRange(new List<long>() {38, 60, 45, 42});
            return () => bands[i++];
        }

        public static Graph.Graph GetFlowGraphFromIncrementGraph(Graph.Graph inc)
        {
            Graph.Graph g = new Graph.Graph();
            foreach (var edge in inc.GetEdges().Select(e => new Edge(e.From, e.To, 0)))
            {
                g.AddEdge(edge);
            }

            return g;
        }

        private static Graph.Graph GetIncrementGraph()
        {
            List<Node> producers = new List<Node>() {Node.P1, Node.P2, Node.P3};
            List<Node> consumers = new List<Node>() {Node.C1, Node.C2, Node.C3, Node.C4};
            Graph.Graph g = new Graph.Graph();
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

        public static void Main()
        {
//            NewMethod();
            Graph.Graph g = GetIncrementGraph();
            Pair<Graph.Graph, long> solve = new TransportationCostsCalculator(g).Solve();
            Console.WriteLine("Price: " + solve.Second);
            Console.WriteLine("Flow Graph:");
            Console.WriteLine(solve.First);
//            var bellmanFord = Algorithms.BellmanFord(g, Node.S);
//            Algorithms.PrintResults(bellmanFord);
//            Form form = new Form();
//            form.Paint += new PaintEventHandler(pictureBox1_Paint);
//            form.ShowDialog();
        }

        private static void NewMethod()
        {
            Graph.Graph g = new Graph.Graph();
            g.AddEdge(new Edge(Node.S, Node.P2, 0));
            g.AddEdge(new Edge(Node.S, Node.P3, 0));

            g.AddEdge(new Edge(Node.P1, Node.S, 0));
            g.AddEdge(new Edge(Node.P1, Node.C2, 11));
            g.AddEdge(new Edge(Node.P1, Node.C4, 13));

            g.AddEdge(new Edge(Node.P2, Node.C1, 16));
            g.AddEdge(new Edge(Node.P2, Node.C2, 18));
            g.AddEdge(new Edge(Node.P2, Node.C3, 20));
            g.AddEdge(new Edge(Node.P2, Node.C4, 20));
            g.AddEdge(new Edge(Node.P2, Node.S, 0));

            g.AddEdge(new Edge(Node.P3, Node.C1, 26));
            g.AddEdge(new Edge(Node.P3, Node.C2, 30));
            g.AddEdge(new Edge(Node.P3, Node.C3, 28));
            g.AddEdge(new Edge(Node.P3, Node.C4, 35));

            g.AddEdge(new Edge(Node.C1, Node.P1, -10));

            g.AddEdge(new Edge(Node.C2, Node.T, 0));

            g.AddEdge(new Edge(Node.C3, Node.P1, -9));

            g.AddEdge(new Edge(Node.C4, Node.T, 0));

            List<Edge> edges = Algorithms.BellmanFord(g, Node.S)[Node.T].Second;
            Console.WriteLine(String.Join(",", edges));
        }

//        static void pictureBox1_Paint(object sender, PaintEventArgs e)
//        {
//            Pen s = new Pen(Color.Chartreuse);
//            e.Graphics.DrawRectangle(s,new Rectangle(1,1,100,100));
//        }
    }
}
