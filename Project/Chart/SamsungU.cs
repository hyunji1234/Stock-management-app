using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project
{
    public partial class SamsungU : Form
    {
        private SamsungUY suy;
        Graph graph = new Graph();
        private bool isVisible = true;
        public SamsungU()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            timer1.Start();

            graph.Chart = chart1;
            graph.FirstPrice = 57400;
            graph.ChangePrice = 1148;
            graph.GraphReset();

            
            this.Text = "삼성전자우량주 변동";

          
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
            suy = new SamsungUY();
            suy.Show();
        }

        private void SamsungU_FormClosed(object sender, FormClosedEventArgs e)
        {
            graph.Stop();
        }

        private void SamsungU_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            e.Cancel = true;
        }
    }
}
