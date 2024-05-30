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
    public partial class EcoY : Form
    {
 
        private Random random = new Random();
        private int[] stockData = new int[300];
        public EcoY()
        {
            InitializeComponent();
            label21.Text = DateTime.Now.AddDays(-1).ToLongDateString() + "   기준";
            SetRan();
            SetChart();
            for (int i = 0; i < stockData.Length; i++)
            {

                chart1.Series["Series1"].Points.AddY(stockData[i]);

            }
            this.Text = "현대차 변동";
            chart1.ChartAreas[0].AxisX.Title = "시간";
            chart1.ChartAreas[0].AxisY.Title = "주가";

            //저가
            int minValue = FindMinValue(stockData);
            label5.Text = minValue.ToString();

            //고가
            int maxValue = FindMaxValue(stockData);
            label6.Text = maxValue.ToString();

            //하한가
            int num = int.Parse(label4.Text);
            double minPrice = num - num * 0.3;
            label2.Text = minPrice.ToString() + ")";

            //상한가
            double maxPrice = num + num * 0.3;
            label3.Text = maxPrice.ToString() + ")";
        }
        private void SetRan()
        {
            for (int i = 0; i < 25; i++)
            {
                stockData[i] = random.Next(500000, 600000);
            }
            for (int i = 25; i <= 50; i++)
            {
                stockData[i] = random.Next(600000, 700000);
            }
            for (int i = 50; i < 75; i++)
            {
                stockData[i] = random.Next(400000, 600000);
            }
            for (int i = 75; i < 100; i++)
            {
                stockData[i] = random.Next(200000, 400000);

            }
            for (int i = 100; i < 125; i++)
            {
                stockData[i] = random.Next(100001, 200000);
            }
            for (int i = 125; i < 150; i++)
            {
                stockData[i] = random.Next(100001, 300000);
            }
            for (int i = 150; i < 175; i++)
            {
                stockData[i] = random.Next(300001, 500000);
            }
            for (int i = 175; i < 200; i++)
            {
                stockData[i] = random.Next(400001, 500001);
            }
            for (int i = 200; i < 225; i++)
            {
                stockData[i] = random.Next(200001, 400001);
            }
            for (int i = 225; i < 250; i++)
            {
                stockData[i] = random.Next(100001, 200001);
            }
            for (int i = 250; i < 275; i++)
            {
                stockData[i] = random.Next(200001, 400000);
            }
            for (int i = 275; i < 300; i++)
            {
                stockData[i] = random.Next(400000, 440000);
            }
        }
        private void SetChart()
        {
            chart1.Series["Series1"].Points.Clear();
        }
        private int FindMinValue(int[] array)
        {
            int minValue = array[0]; // 배열의 첫 번째 값으로 초기화

            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] < minValue)
                {
                    minValue = array[i];
                }
            }

            return minValue;
        }

        private int FindMaxValue(int[] array)
        {
            int maxValue = array[0]; // 배열의 첫 번째 값으로 초기화

            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] > maxValue)
                {
                    maxValue = array[i];
                }
            }

            return maxValue;
        }

        private void button1_Click(object sender, EventArgs e)
        {
 
        }
    }
}
