using System;
using System.Collections.Generic;
using System.Linq;
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

//        static void pictureBox1_Paint(object sender, PaintEventArgs e)
//        {
//            Pen s = new Pen(Color.Chartreuse);
//            e.Graphics.DrawRectangle(s,new Rectangle(1,1,100,100));
//        }
    }
}