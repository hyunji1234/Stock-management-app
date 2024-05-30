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
using static System.Windows.Forms.AxHost;

namespace project
{
    public partial class Samsung : Form
    {
        public event EventHandler<string> Data;
        Graph graph = new Graph();
        private bool isVisible = true;
        public Samsung()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;

            graph.Chart = chart1;
            graph.FirstPrice = 70600;
            graph.ChangePrice = 1400;
            graph.GraphReset();

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
                pictureBox2.Visible = false;
                pictureBox3.Visible = true;
            }
            if (graph.currentprice < graph.FirstPrice)
            {
                label10.ForeColor = Color.Blue;
                pictureBox3.Visible = false;
                pictureBox2.Visible = true;
            }

            pictureBox1.Visible = isVisible;
            isVisible = !isVisible;
            Data?.Invoke(this, Convert.ToString(graph.currentprice));
            //main.SetAll("삼성전자", , (), ((double)(graph.currentprice - graph.FirstPrice) / graph.FirstPrice));
            
        }

        public (string, string, string) Send()
        {
            string Price = Convert.ToString(graph.currentprice);
            string DPrice = Convert.ToString(graph.currentprice - graph.FirstPrice);
            double rate = ((double)(graph.currentprice - graph.FirstPrice) / graph.FirstPrice) * 100;
            string R = $"{rate:F2}";
            return (Price, DPrice, R);
        }

        private void Samsung_FormClosed(object sender, FormClosedEventArgs e)
        {
            graph.Stop();
        }

        private void Samsung_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            e.Cancel = true;
        }
    }
}
