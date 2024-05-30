using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace project
{
    public partial class ShareBuyForm : Form
    {
        main main = new main();
        string name;

        public ShareBuyForm(main main, string a)
        {
            InitializeComponent();
            this.main = main;
            name = a;
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            tbMoney.Text += "1";
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            tbMoney.Text += "2";        }

        private void btn3_Click(object sender, EventArgs e)
        {
            tbMoney.Text += "3";
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            tbMoney.Text += "4";
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            tbMoney.Text += "5";
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            tbMoney.Text += "6";
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            tbMoney.Text += "7";
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            tbMoney.Text += "8";
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            tbMoney.Text += "9";
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            tbMoney.Text = "";
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            tbMoney.Text += "0";
        }

        private void btnDot_Click(object sender, EventArgs e)
        {
            tbMoney.Text += ".";
        }

        private void btnBuy_Click(object sender, EventArgs e) 
        {
            MessageBox.Show($"주식을 {tbMoney.Text}주 만큼 구매하셨습니다.");
            
            if (name == "삼성전자") { main.stockPortfolio[0, 3] = tbMoney.Text; }
            if (name == "LG에너지솔루션") { main.stockPortfolio[1, 3] = tbMoney.Text; }
            if (name == "SK하이닉스") { main.stockPortfolio[2, 3] = tbMoney.Text; }
            if (name == "삼성바이오로직스") { main.stockPortfolio[3, 3] = tbMoney.Text; }
            if (name == "POSCO홀딩스") { main.stockPortfolio[4, 3] = tbMoney.Text; }
            if (name == "삼성전자우") { main.stockPortfolio[5, 3] = tbMoney.Text; }
            if (name == "LG화학") { main.stockPortfolio[6, 3] = tbMoney.Text; }
            if (name == "삼성SDI") { main.stockPortfolio[7, 3] = tbMoney.Text; }
            if (name == "현대차") { main.stockPortfolio[8, 3] = tbMoney.Text; }
            if (name == "에코프로비엠") { main.stockPortfolio[9, 3] = tbMoney.Text; }
            this.Close();
        }

        
    }
}
