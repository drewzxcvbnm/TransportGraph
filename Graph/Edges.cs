namespace ConsoleApplication1.Graph
{
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
}