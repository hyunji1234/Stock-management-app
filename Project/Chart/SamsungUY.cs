using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace project
{
    public partial class SamsungUY : Form
    {
   
        private Random random = new Random();
        private int[] stockData = new int[300];
        public SamsungUY()
        {
            InitializeComponent();
            label21.Text = DateTime.Now.AddDays(-1).ToLongDateString() + "   기준";
            SetRan();
            SetChart();
            for (int i = 0; i < stockData.Length; i++)
            {

                chart1.Series["Series1"].Points.AddY(stockData[i]);

            }
            this.Text = "삼성전자우량주 변동";
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
                stockData[i] = random.Next(10000, 20000);
            }
            for (int i = 25; i <= 50; i++)
            {
                stockData[i] = random.Next(20000, 40000);
            }
            for (int i = 50; i < 75; i++)
            {
                stockData[i] = random.Next(40000, 60000);
            }
            for (int i = 75; i < 100; i++)
            {
                stockData[i] = random.Next(60000, 70000);

            }
            for (int i = 100; i < 125; i++)
            {
                stockData[i] = random.Next(40001, 50000);
            }
            for (int i = 125; i < 150; i++)
            {
                stockData[i] = random.Next(30001, 40000);
            }
            for (int i = 150; i < 175; i++)
            {
                stockData[i] = random.Next(30001, 50000);
            }
            for (int i = 175; i < 200; i++)
            {
                stockData[i] = random.Next(20001, 30001);
            }
            for (int i = 200; i < 225; i++)
            {
                stockData[i] = random.Next(30001, 40001);
            }
            for (int i = 225; i < 250; i++)
            {
                stockData[i] = random.Next(40000, 60000);
            }
            for (int i = 250; i < 275; i++)
            {
                stockData[i] = random.Next(50000, 55000);
            }
            for (int i = 275; i < 300; i++)
            {
                stockData[i] = random.Next(55000, 60000);
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
