namespace ConsoleApplication1.IncGraph
{
    public class Edge
    {
        public Node From { get; set; }
        public Node To { get; set; }
        public long Bandwidth { get; set; }
        public long Flow { get; set; }

        public Edge(Node from, Node to, long bandwidth, long flow)
        {
            From = from;
            To = to;
            Flow = flow;
            Bandwidth = bandwidth;
        }

        public override string ToString()
        {
            return $"[{From}->{To}, {Bandwidth}/{Flow}]";
        }
    }
}