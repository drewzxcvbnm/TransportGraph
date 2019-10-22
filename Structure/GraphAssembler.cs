using System;
using System.Collections.Generic;

namespace ConsoleApplication1.Structure
{
    public interface ISetBand
    {
        ISetFlow SetBands(List<long> producerBands, List<long> consumerBands);
    }

    public interface ISetFlow
    {
        ICreate SetFlows(List<long> flows);
    }

    public interface ICreate
    {
        Graph Create();
    }

    public class GraphAssembler : ISetBand, ISetFlow, ICreate
    {
        private static Func<long> _getFlow;
        private static Func<long> _getBandwidth;

        private GraphAssembler()
        {
        }

        public static ISetBand AssembleGraph()
        {
            return new GraphAssembler();
        }

        public ISetFlow SetBands(List<long> producerBands, List<long> consumerBands)
        {
            int i = 0;
            List<long> bands = new List<long>();
            bands.Add(producerBands[0]);
            bands.AddRange(consumerBands);
            bands.Add(producerBands[1]);
            bands.AddRange(consumerBands);
            bands.Add(producerBands[2]);
            bands.AddRange(consumerBands);
            bands.AddRange(consumerBands);
            _getBandwidth = () => bands[i++];
            return this;
        }

        public ICreate SetFlows(List<long> flows)
        {
            int i = 0;
            List<long> fl = new List<long>(flows);
            _getFlow = () => fl[i++];
            return this;
        }

        public Graph Create()
        {
            List<Node> producers = new List<Node>() {Node.P1, Node.P2, Node.P3};
            List<Node> consumers = new List<Node>() {Node.C1, Node.C2, Node.C3, Node.C4};
            Graph g = new Graph();
            foreach (var producer in producers)
            {
                g.AddEdge(new IncrementEdge(Node.S, producer, _getBandwidth(), 0, false));
                foreach (var consumer in consumers)
                {
                    g.AddEdge(new IncrementEdge(producer, consumer, _getBandwidth(), _getFlow(), false));
                }
            }

            foreach (var consumer in consumers)
            {
                g.AddEdge(new IncrementEdge(consumer, Node.T, _getBandwidth(), 0, false));
            }

            return g;
        }
    }
}