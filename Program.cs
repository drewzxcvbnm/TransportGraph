using System;
using System.Collections.Generic;
using static ConsoleApplication1.Structure.GraphAssembler;
using System.Linq;
using ConsoleApplication1.Structure;

namespace ConsoleApplication1
{
    class MainClass
    {
        /**
         * Inputs:
         * 63 68 69 80 90 42 52 101 111 151 131 161 181 201 201 261 301 281 351 //price: 37090
         * 67 54 69 38 60 45 42 10 11 9 13 16 18 20 20 26 30 28 35 //price: 3471 (my var)
         * P1 P2 P3 C1 C2 C3 C4 P1->C1 P1->C2 P1->C3 P1->C4 P2->C1 P2->C2 P2->C3 P2->C4 P3->C1 P3->C2 P3->C3 P3->C4 
         */

        public static void Main()
        {
            Console.Write("Enter all required graph information>");
            List<long> input = Console.ReadLine().Split().Select(s => long.Parse(s)).ToList();
            Graph<IncrementEdge> graph = AssembleGraph()
                .SetBands(input.GetRange(0, 3), input.GetRange(3, 4))
                .SetFlows(input.GetRange(7, 12))
                .Create();
            Pair<Graph<Edge>, long> solve = new TransportationCostsCalculator(graph).Solve();
            Console.WriteLine("Price is " + solve.Second);
            Console.WriteLine("Flow Graph:");
            Console.WriteLine(solve.First);
            Console.ReadKey();
        }
    }
}