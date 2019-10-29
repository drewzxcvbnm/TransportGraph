using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ConsoleApplication1.Structure;
using static ConsoleApplication1.Structure.GraphAssembler;

namespace ConsoleApplication1
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<long> prods = new List<long>();
            prods.Add(Convert.ToInt64(this.P1.Text));
            prods.Add(Convert.ToInt64(this.P2.Text));
            prods.Add(Convert.ToInt64(this.P3.Text));
            List<long> cons = new List<long>();
            cons.Add(Convert.ToInt64(this.C1.Text));
            cons.Add(Convert.ToInt64(this.C2.Text));
            cons.Add(Convert.ToInt64(this.C3.Text));
            cons.Add(Convert.ToInt64(this.C4.Text));
            List<long> flows = new List<long>();
            flows.Add(Convert.ToInt64(this.P1C1.Text));
            flows.Add(Convert.ToInt64(this.P1C2.Text));
            flows.Add(Convert.ToInt64(this.P1C3.Text));
            flows.Add(Convert.ToInt64(this.P1C4.Text));
            flows.Add(Convert.ToInt64(this.P2C1.Text));
            flows.Add(Convert.ToInt64(this.P2C2.Text));
            flows.Add(Convert.ToInt64(this.P2C3.Text));
            flows.Add(Convert.ToInt64(this.P2C4.Text));
            flows.Add(Convert.ToInt64(this.P3C1.Text));
            flows.Add(Convert.ToInt64(this.P3C2.Text));
            flows.Add(Convert.ToInt64(this.P3C3.Text));
            flows.Add(Convert.ToInt64(this.P3C4.Text));
            Graph<IncrementEdge> graph = AssembleGraph()
                .SetBands(prods, cons)
                .SetFlows(flows)
                .Create();
            Pair<Graph<Edge>, long> solve = new TransportationCostsCalculator(graph).Solve();
            this.@out.Text = $"Price is {solve.Second}\nFlow Graph:\n{solve.First}";
        }

    }
}