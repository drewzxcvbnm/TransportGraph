using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using ConsoleApplication1.IncGraph;

namespace ConsoleApplication1
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

        private static Graph GetGraph()
        {
            List<Node> producers = new List<Node>() {Node.P1, Node.P2, Node.P3};
            List<Node> consumers = new List<Node>() {Node.C1, Node.C2, Node.C3, Node.C4};
            Graph g = new Graph();
            foreach (var producer in producers)
            {
                g.AddEdge(new Edge(Node.S, producer, GetBandwidth(), 0));
                foreach (var consumer in consumers)
                {
                    g.AddEdge(new Edge(producer, consumer, GetBandwidth(), GetFlow()));
                }
            }

            foreach (var consumer in consumers)
            {
                g.AddEdge(new Edge(consumer, Node.T, GetBandwidth(), 0));
            }

            return g;
        }

        public static void Main()
        {
            Graph g = GetGraph();
            Console.WriteLine(g);
            Algorithms.BellmanFord(g, Node.S);
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