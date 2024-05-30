using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml.Schema;

namespace project
{
    
    internal class Graph
    {
        public static int dataPoints = 100; // 그래프에 표시할 초기 데이터 포인트 개수
        public List<double> stockPrices = new List<double>() { };
        public int currentprice;
        private int firstprice;
        private int changeprice;
        
        private double basePrice;
        private Timer timer;
        //private object sender;
        //private ElapsedEventArgs e;
        private Chart c;

        public int minValue;
        public int maxValue;

        public Graph()
        {
            timer = new Timer { Interval = 1000 };
            timer.Start();
            timer.Elapsed += OnTimerTick;
            GenerateRandomData();
        }

        public Chart Chart { get { return c; } set { c = value; } }
        public int FirstPrice { get { return firstprice; } set { firstprice = value; } }
        public int ChangePrice { get { return changeprice; } set { changeprice = value; } }

        private void OnTimerTick(object sender, ElapsedEventArgs e)
        {
            // 주식 가격 랜덤 업데이트
            GenerateRandomData();
            if (basePrice > firstprice) { c.Series[0].Color = System.Drawing.Color.Red; }
            if (basePrice < firstprice) { c.Series[0].Color = System.Drawing.Color.Blue; }

            // 차트에 새로운 데이터 추가
            if(stockPrices.Count == 0) { }
            else { c.Series["StockPrice"].Points.AddXY(stockPrices.Count, stockPrices[stockPrices.Count - 1]); }
        }

        public void Stop()
        {
            timer.Stop();
        }

        public void GraphReset()
        {
            // 초기 랜덤 주가 데이터 생성
            GenerateRandomData();

            // 차트 설정
            c.Series.Clear();
            c.Series.Add("StockPrice");
            c.Series["StockPrice"].ChartType = SeriesChartType.Line;
            c.ChartAreas[0].AxisX.Interval = 1;
            c.ChartAreas[0].AxisX.Maximum = dataPoints;
            c.ChartAreas[0].AxisX.Minimum = 0;
            c.ChartAreas[0].AxisY.Minimum = firstprice * 0.7;
            c.ChartAreas[0].AxisY.Maximum = firstprice * 1.3;

            c.ChartAreas[0].AxisX.MajorGrid.Enabled = false;

            c.Legends.Clear();
            c.ChartAreas[0].AxisX.Title = "시간 (단위: 초 후)";
            c.ChartAreas[0].AxisY.Title = "주가 (단위: 원)";

            for (int i = 0; i < stockPrices.Count; i++)
            {
                c.Series["StockPrice"].Points.AddXY(i - 1, stockPrices[i]);
            }
        }

        public void GenerateRandomData()
        {
            Random random = new Random();
            
            if (stockPrices.Count > 1) 
            { 
                basePrice = stockPrices[stockPrices.Count - 1]; 
            }
            else { basePrice = firstprice; }
            
            basePrice += (random.NextDouble() * changeprice * 2) - changeprice;

            // 주가가 최소값보다 작으면 최소값으로, 최대값보다 크면 최대값으로 제한
            if (basePrice > (firstprice + firstprice * 0.3)) { basePrice = (firstprice + firstprice * 0.3); }
            if (basePrice < (firstprice - firstprice * 0.3)) { basePrice = (firstprice - firstprice * 0.3); }

            stockPrices.Add(basePrice);
            currentprice = (Convert.ToInt32(stockPrices[stockPrices.Count - 1]));
            minValue = Convert.ToInt32(FindMinValue(stockPrices));
            maxValue = Convert.ToInt32(FindMaxValue(stockPrices));
        }

        private double FindMinValue(List<double>stockPrices)
        {
            if (stockPrices.Count == 0)
                throw new ArgumentException("The list is empty.", nameof(stockPrices));

            double minValue = stockPrices[0];

            for (int i = 1; i < stockPrices.Count; i++)
            {
                if(minValue == 0) { minValue = stockPrices[1]; }
                if (stockPrices[i] < minValue)
                {
                    minValue = stockPrices[i];
                }
            }

            return minValue;
        }

        private double FindMaxValue(List<double> stockPrices)
        {
            if (stockPrices.Count == 0)
                throw new ArgumentException("The list is empty.", nameof(stockPrices));

            double maxValue = stockPrices[0];

            for (int i = 1; i < stockPrices.Count; i++)
            {
                if (stockPrices[i] > maxValue)
                {
                    maxValue = stockPrices[i];
                }
            }

            return maxValue;
        }
    }

    
}
