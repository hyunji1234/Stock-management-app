using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project
{
    public partial class Eco : Form
    {
        private EcoY ecoy;
        Graph graph = new Graph();
        private bool isVisible = true;
        public Eco()
        {
            InitializeComponent();
        
            CheckForIllegalCrossThreadCalls = false;
            graph.Chart = chart1;
            graph.FirstPrice = 420000;
            graph.ChangePrice = 8400;
            graph.GraphReset();

            this.Text = "에코프로비엠 실시간 변동";

            timer1.Start();

            label11.Text = (graph.FirstPrice * 1.3).ToString() + ")";
            label12.Text = (graph.FirstPrice * 0.7).ToString() + ")";

            label13.Text += graph.FirstPrice.ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            label10.Text = graph.currentprice.ToString();
            label15.Text = graph.minValue.ToString();
            label14.Text = graph.maxValue.ToString();

            if (graph.currentprice > graph.FirstPrice)
            {
                label10.ForeColor = Color.Red;
            }
            if (graph.currentprice < graph.FirstPrice)
            {
                label10.ForeColor = Color.Blue;
            }

            pictureBox1.Visible = isVisible;
            isVisible = !isVisible;
        }

        public (string, string, string) Send()
        {
            string Price = Convert.ToString(graph.currentprice);
            string DPrice = Convert.ToString(graph.currentprice - graph.FirstPrice);
            double rate = ((double)(graph.currentprice - graph.FirstPrice) / graph.FirstPrice) * 100;
            string R = $"{rate:F2}";
            return (Price, DPrice, R);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ecoy = new EcoY();
            ecoy.ShowDialog();
        }

        private void Eco_FormClosed(object sender, FormClosedEventArgs e)
        {
            graph.Stop();
        }

        private void Eco_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            e.Cancel = true;
        }
    }
}
