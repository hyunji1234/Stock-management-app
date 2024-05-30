using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace project
{
    public partial class main : Form
    {
        string[] arr = { "삼성전자", "LG에너지솔루션", "삼성바이오로직스", "POSCO홀딩스", "SK하이닉스", "삼성SDI", "삼성전자우", "LG화학", "현대차", "에코프로비엠" };
        string[] value1 = new string[10];       //현재가*개수
        string[] value2 = new string[10];      //구매가*개수
        login login = new login();
        private Bio bio;
        private LG lg;
        private POSCO posco;
        private Samsung samsung;
        private SK sk;
        private SDSI sdi;
        private SamsungU samsungu;
        private LGC lgc;
        private HCar hcar;
        private Eco eco;



        public string[,] stockPortfolio = new string[10, 4];            //0번:가격, 1번:차액, 2번:등락률, 3번:개수, 4번:이름
        private int portfolioSize;
        private int currentIndex = 0;
        int index0, index1, index2, index3, index4, index5, index6, index7, index8, index9;

        private void SetHeight(System.Windows.Forms.ListView LV, int height)
        {
            // listView 높이 지정
            ImageList imgList = new ImageList();
            imgList.ImageSize = new Size(1, height);
            LV.SmallImageList = imgList;
        }

        public main()
        {
            InitializeComponent();
            DisableAllControlsExceptButton1();
            lb_loginname.Visible = false;
            tabControl3.Visible = false;
            tableLayoutPanel1.Visible = false;
            btn_save.Visible = false;
            txt_search.Visible = false;

            lv_ChartItem.Items.Add(new ListViewItem(new String[] { "삼성전자", "70,000", "1,400", "91,000", "49,000" }));
            lv_ChartItem.Items.Add(new ListViewItem(new String[] { "LG에너지솔루션", "560,000", "11,200", "728,000", "392,000" }));
            lv_ChartItem.Items.Add(new ListViewItem(new String[] { "SK하이닉스", "123,400", "2,468", "160,420", "86,380" }));
            lv_ChartItem.Items.Add(new ListViewItem(new String[] { "삼성바이오로직스", "766,000", "15,320", "995,800", "536,200" }));
            lv_ChartItem.Items.Add(new ListViewItem(new String[] { "POSCO홀딩스", "642,000", "12,840", "834,600", "449,400" }));
            lv_ChartItem.Items.Add(new ListViewItem(new String[] { "삼성전자우", "57,400", "1,148", "74,620", "40,180" }));
            lv_ChartItem.Items.Add(new ListViewItem(new String[] { "LG화학", "648,000", "12,960", "842,400", "453,600" }));
            lv_ChartItem.Items.Add(new ListViewItem(new String[] { "삼성SDI", "665,000", "13,300", "864,500", "465,500" }));
            lv_ChartItem.Items.Add(new ListViewItem(new String[] { "현대차", "196,000", "3,920", "254,800", "137,200" }));
            lv_ChartItem.Items.Add(new ListViewItem(new String[] { "에코프로비엠", "420,000", "8,400", "546,000", "294,000" }));

            SetHeight(lv_ChartItem, 44);

            pictureBox2.Visible = false;
            txt_search.Visible = false;
        }

        private void DisableAllControlsExceptButton1()  // 폼의 모든 컨트롤을 가져와서 button1을 제외한 나머지 컨트롤의 Enabled 속성을 false로 설정
        {
            foreach (Control control in this.Controls)
            {
                if (control != btn_mainLogin) { control.Enabled = false; }
                control.ForeColor = Color.Black;
            }
        }

        private void AbleAllControlsExceptButton1()  // 폼의 모든 컨트롤을 가져와서 button1을 제외한 나머지 컨트롤의 Enabled 속성을 true로 설정
        {
            foreach (Control control in this.Controls)
            {
                if (control != btn_mainLogin) { control.Enabled = true; }
                control.ForeColor = Color.Black;
            }
        }

        private void main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btn_mainhome_Click(object sender, EventArgs e)
        {
            tbc_main.SelectedIndex = 0;
        }

        private void btn_mainDomestic_Click(object sender, EventArgs e)
        {
            tbc_main.SelectedIndex = 1;
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            tbc_main.SelectedIndex = 2;
        }

        private void btn_help_Click(object sender, EventArgs e)
        {
            tbc_main.SelectedIndex = 3;
        }

        private void btn_mainLogin_Click(object sender, EventArgs e)            //Log-In버튼을 클릭했을 경우
        {
            if (login.ShowDialog() == DialogResult.OK)
            {
                AbleAllControlsExceptButton1();
                btn_mainLogin.Visible = false;
                label32.Visible = false;
                tabControl3.Visible = true;
                btn_save.Visible = true;
                tableLayoutPanel1.Visible = true;
                pictureBox2.Visible = true;
                lb_loginname.Visible = true;
                txt_search.Visible = true;
                lb_loginname.Text = login.loginName.ToString() + " 님";
                lb_loginname.Font = new Font("맑은 고딕", 12, FontStyle.Bold);
                lb_loginname.ForeColor = Color.Black;
                lbName2.Text = login.loginName.ToString() + "님의 자산 구성";

                bio = new Bio();
                lg = new LG();
                posco = new POSCO();
                samsung = new Samsung();
                sk = new SK();
                sdi = new SDSI();
                samsungu = new SamsungU();
                lgc = new LGC();
                hcar = new HCar();
                eco = new Eco();



                timer1.Start();
            }
        }

        private void lv_ChartItem_MouseDoubleClick(object sender, MouseEventArgs e)     //차트의 아이템을 클릭했을 경우
        {
            if (lv_ChartItem.FocusedItem.Text == "삼성전자") { samsung.Show(); SSPrice.Text = samsung.Send().Item1; }
            if (lv_ChartItem.FocusedItem.Text == "LG에너지솔루션") { lg.Show(); }
            if (lv_ChartItem.FocusedItem.Text == "SK하이닉스") { sk.Show(); }
            if (lv_ChartItem.FocusedItem.Text == "삼성바이오로직스") { bio.Show(); }
            if (lv_ChartItem.FocusedItem.Text == "POSCO홀딩스") { posco.Show(); }
            if (lv_ChartItem.FocusedItem.Text == "삼성전자우") { samsungu.Show(); }
            if (lv_ChartItem.FocusedItem.Text == "LG화학") { lgc.Show(); }
            if (lv_ChartItem.FocusedItem.Text == "삼성SDI") { sdi.Show(); }
            if (lv_ChartItem.FocusedItem.Text == "현대차") { hcar.Show(); }
            if (lv_ChartItem.FocusedItem.Text == "에코프로비엠") { eco.Show(); }
        }

        //보유현황 창에서 매도버튼 클릭
        private void btnSell_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem sell in listViewHave.SelectedItems) 
            {
                if(sell.Text == "삼성전자") { value1[0] = "0"; value2[0] = "0"; }
                if (sell.Text == "LG에너지솔루션") { value1[1] = "0"; value2[1] = "0"; }
                if (sell.Text == "SK하이닉스") { value1[2] = "0"; value2[2] = "0"; }
                if (sell.Text == "삼성바이오로직스") { value1[3] = "0"; value2[3] = "0"; }
                if (sell.Text == "POSCO홀딩스") { value1[4] = "0"; value2[4] = "0"; }
                if (sell.Text == "삼성전자우") { value1[5] = "0"; value2[5] = "0"; }
                if (sell.Text == "LG화학") { value1[6] = "0"; value2[6] = "0"; }
                if (sell.Text == "삼성SDI") { value1[7] = "0"; value2[7] = "0"; }
                if (sell.Text == "현대차") { value1[8] = "0"; value2[8] = "0"; }
                if (sell.Text == "에코프로비엠") { value1[9] = "0"; value2[9] = "0"; }

                listViewHave.Items.Remove(sell);
            }

            lbEvalution.Text = string.Empty;
            lbRate.Text = string.Empty;
            lbRate3.Text = string.Empty;
            lbPaL.Text = string.Empty;
            lbPrice.Text = string.Empty;
            lbPresentPrice.Text = string.Empty;
            lbBuyPrice.Text = string.Empty;
            lbBalance.Text = string.Empty;
        }

        //보유현황 페이지 상세보기 버튼 클릭 이벤트
        private void btnSearch_Click_1(object sender, EventArgs e)
        {
            if (tbSearch.Text == "")
            {
                MessageBox.Show("검색 결과가 없습니다.");
            }
            else if (tbSearch.Text == ("삼성전자"))
            {
                lbEvalution.Text = "1";
                lbRate.Text = "1";
                lbRate3.Text = "1";
                lbPaL.Text = "1";
                lbPrice.Text = "1";
                lbPresentPrice.Text = "1";
                lbBuyPrice.Text = "1";
                lbBalance.Text = "1";
            }
            else if (tbSearch.Text == ("LG에너지솔루션"))
            {
                lbEvalution.Text = "2";
                lbRate.Text = "2";
                lbRate3.Text = "2";
                lbPaL.Text = "2";
                lbPrice.Text = "2";
                lbPresentPrice.Text = "2";
                lbBuyPrice.Text = "2";
                lbBalance.Text = "2";
            }
            else if (tbSearch.Text == ("SK하이닉스"))
            {
                lbEvalution.Text = "3";
                lbRate.Text = "3";
                lbRate3.Text = "3";
                lbPaL.Text = "3";
                lbPrice.Text = "3";
                lbPresentPrice.Text = "3";
                lbBuyPrice.Text = "3";
                lbBalance.Text = "3";
            }
            else if (tbSearch.Text == ("삼성바이오로직스"))
            {
                lbEvalution.Text = "4";
                lbRate.Text = "4";
                lbRate3.Text = "4";
                lbPaL.Text = "4";
                lbPrice.Text = "4";
                lbPresentPrice.Text = "4";
                lbBuyPrice.Text = "4";
                lbBalance.Text = "4";
            }
            else if (tbSearch.Text == ("POSCO홀딩스"))
            {
                lbEvalution.Text = "5";
                lbRate.Text = "5";
                lbRate3.Text = "5";
                lbPaL.Text = "5";
                lbPrice.Text = "5";
                lbPresentPrice.Text = "5";
                lbBuyPrice.Text = "5";
                lbBalance.Text = "5";
            }
            else if (tbSearch.Text == ("삼성전자우"))
            {
                lbEvalution.Text = "6";
                lbRate.Text = "6";
                lbRate3.Text = "6";
                lbPaL.Text = "6";
                lbPrice.Text = "6";
                lbPresentPrice.Text = "6";
                lbBuyPrice.Text = "6";
                lbBalance.Text = "6";
            }
            else if (tbSearch.Text == ("LG화학"))
            {
                lbEvalution.Text = "7";
                lbRate.Text = "7";
                lbRate3.Text = "7";
                lbPaL.Text = "7";
                lbPrice.Text = "7";
                lbPresentPrice.Text = "7";
                lbBuyPrice.Text = "7";
                lbBalance.Text = "7";
            }
            else if (tbSearch.Text == ("삼성SDI"))
            {
                lbEvalution.Text = "8";
                lbRate.Text = "8";
                lbRate3.Text = "8";
                lbPaL.Text = "8";
                lbPrice.Text = "8";
                lbPresentPrice.Text = "8";
                lbBuyPrice.Text = "8";
                lbBalance.Text = "8";
            }
            else if (tbSearch.Text == ("현대차"))
            {
                lbEvalution.Text = "9";
                lbRate.Text = "9";
                lbRate3.Text = "9";
                lbPaL.Text = "9";
                lbPrice.Text = "9";
                lbPresentPrice.Text = "9";
                lbBuyPrice.Text = "9";
                lbBalance.Text = "9";
            }
            else if (tbSearch.Text == ("에코프로비엠"))
            {
                lbEvalution.Text = "10";
                lbRate.Text = "10";
                lbRate3.Text = "10";
                lbPaL.Text = "10";
                lbPrice.Text = "10";
                lbPresentPrice.Text = "10";
                lbBuyPrice.Text = "10";
                lbBalance.Text = "10";
            }
        }

        private void panel2_MouseClick(object sender, MouseEventArgs e)
        {
            MessageBox.Show("삼성전자");

            samsung.Show();
        }
        //LG에너지솔루션 클릭시 이벤트
        private void panel3_MouseClick(object sender, MouseEventArgs e)
        {
            MessageBox.Show("LG에너지솔루션");

            lg.Show();
        }
        //SK하이닉스 클릭시 이벤트
        private void panel4_MouseClick(object sender, MouseEventArgs e)
        {
            MessageBox.Show("SK하이닉스");

            sk.Show();
        }

        //삼성바이오로직스 클릭시 이벤트
        private void panel5_MouseClick(object sender, MouseEventArgs e)
        {
            MessageBox.Show("삼성바이오로직스");

            bio.Show();
        }

        //POSCO홀딩스 클릭시 이벤트
        private void panel6_MouseClick(object sender, MouseEventArgs e)
        {
            MessageBox.Show("POSCO홀딩스");

            posco.Show();
        }
        //삼성전자우 클릭시 이벤트
        private void panel7_MouseClick(object sender, MouseEventArgs e)
        {
            MessageBox.Show("삼성전자우");

            samsungu.Show();
        }
        //LG화학 클릭시 이벤트
        private void panel8_MouseClick(object sender, MouseEventArgs e)
        {
            MessageBox.Show("LG화학");

            lgc.Show();
        }
        //삼성SDI 클릭시 이벤트
        private void panel9_MouseClick(object sender, MouseEventArgs e)
        {
            MessageBox.Show("삼성SDI");

            sdi.Show();
        }
        //현대차 클릭시 이벤트
        private void panel10_MouseClick(object sender, MouseEventArgs e)
        {
            MessageBox.Show("현대차");

            hcar.Show();
        }
        //에코프로비엠 클릭시 이벤트
        private void panel11_MouseClick(object sender, MouseEventArgs e)
        {
            MessageBox.Show("에코프로비엠");

            eco.Show();
        }

        private void btnindex0_Click(object sender, EventArgs e)
        {
            if (btnindex0.Text == "★") { btnindex0.Text = "☆"; lv_interest.Items.RemoveAt(index0); }
            else { btnindex0.Text = "★"; lv_interest.Items.Add(new ListViewItem(new String[] { "삼성전자", "70,000", "1,400", "91,000", "49,000" })); index0 = lv_interest.Items.Count - 1; }
        }

        private void btnindex1_Click(object sender, EventArgs e)
        {
            if (btnindex1.Text == "★") { btnindex1.Text = "☆"; lv_interest.Items.RemoveAt(index1); }
            else { btnindex1.Text = "★"; lv_interest.Items.Add(new ListViewItem(new String[] { "LG에너지솔루션", "560,000", "11,200", "728,000", "392,000" })); index1 = lv_interest.Items.Count - 1; }
        }

        private void btnindex2_Click(object sender, EventArgs e)
        {
            if (btnindex2.Text == "★") { btnindex2.Text = "☆"; lv_interest.Items.RemoveAt(index2); }
            else { btnindex2.Text = "★"; lv_interest.Items.Add(new ListViewItem(new String[] { "SK하이닉스", "123,400", "2,468", "160,420", "86,380" })); index2 = lv_interest.Items.Count - 1; }
        }

        private void btnindex3_Click(object sender, EventArgs e)
        {
            if (btnindex3.Text == "★") { btnindex3.Text = "☆"; lv_interest.Items.RemoveAt(index3); }
            else { btnindex3.Text = "★"; lv_interest.Items.Add(new ListViewItem(new String[] { "삼성바이오직스", "766,000", "15,320", "995,800", "536,200" })); index3 = lv_interest.Items.Count - 1; }
        }

        private void btnindex4_Click(object sender, EventArgs e)
        {
            if (btnindex4.Text == "★") { btnindex4.Text = "☆"; lv_interest.Items.RemoveAt(index4); }
            else { btnindex4.Text = "★"; lv_interest.Items.Add(new ListViewItem(new String[] { "POSCO홀딩스", "642,000", "12,840", "834,600", "449,400" })); index4 = lv_interest.Items.Count - 1; }
        }

        private void btnindex5_Click(object sender, EventArgs e)
        {
            if (btnindex5.Text == "★") { btnindex5.Text = "☆"; lv_interest.Items.RemoveAt(index5); }
            else { btnindex5.Text = "★"; lv_interest.Items.Add(new ListViewItem(new String[] { "삼성전자우", "57,400", "1,148", "74,620", "40,180" })); index5 = lv_interest.Items.Count - 1; }
        }

        private void btnindex6_Click(object sender, EventArgs e)
        {
            if (btnindex6.Text == "★") { btnindex6.Text = "☆"; lv_interest.Items.RemoveAt(index6); }
            else { btnindex6.Text = "★"; lv_interest.Items.Add(new ListViewItem(new String[] { "LG화학", "648,000", "12,960", "842,400", "453,600" })); index6 = lv_interest.Items.Count - 1; }
        }

        private void btnindex7_Click(object sender, EventArgs e)
        {
            if (btnindex7.Text == "★") { btnindex7.Text = "☆"; lv_interest.Items.RemoveAt(index7); }
            else { btnindex7.Text = "★"; lv_interest.Items.Add(new ListViewItem(new String[] { "삼성SDI", "665,000", "13,300", "864,500", "465,500" })); index7 = lv_interest.Items.Count - 1; }
        }

        private void btnindex8_Click(object sender, EventArgs e)
        {
            if (btnindex8.Text == "★") { btnindex8.Text = "☆"; lv_interest.Items.RemoveAt(index8); }
            else { btnindex8.Text = "★"; lv_interest.Items.Add(new ListViewItem(new String[] { "현대차", "196,000", "3,920", "254,800", "137,200" })); index8 = lv_interest.Items.Count - 1; }
        }

        private void btnindex9_Click(object sender, EventArgs e)
        {
            if (btnindex9.Text == "★") { btnindex9.Text = "☆"; lv_interest.Items.RemoveAt(index9); }
            else { btnindex9.Text = "★"; lv_interest.Items.Add(new ListViewItem(new String[] { "에코프로비엠", "420,000", "8,400", "546,000", "294,000" })); index9 = lv_interest.Items.Count - 1; }
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            string str = txt_search.Text;


            if (txt_search.Text == "")
            {
                MessageBox.Show("검색 결과가 없습니다.");
            }
            else if (str.Contains("삼성전자"))
            {
                samsung.Show();
            }
            else if (str.Contains("LG에너지솔루션"))
            {
                lg.Show();
            }
            else if (str.Contains("SK하이닉스"))
            {
                sk.Show();
            }
            else if (str.Contains("삼성바이오로직스"))
            {
                bio.Show();
            }
            else if (str.Contains("POSCO홀딩스"))
            {
                posco.Show();
            }
            else if (str.Contains("삼성전자우"))
            {
                samsungu.Show();
            }
            else if (str.Contains("LG화학"))
            {
                lgc.Show();
            }
            else if (str.Contains("삼성SDI"))
            {
                sdi.Show();
            }
            else if (str.Contains("현대차"))
            {
                hcar.Show();
            }
            else if (str.Contains("에코프로비엠"))
            {
                eco.Show();
            }

        }

        public void timer1_Tick(object sender, EventArgs e)
        {
            SSPrice.Text = samsung.Send().Item1;
            LGEPrice.Text = lg.Send().Item1;
            SSBPrice.Text = bio.Send().Item1;
            POSCOPrice.Text = posco.Send().Item1;
            SKHPrice.Text = sk.Send().Item1;
            SSSPrice.Text = sdi.Send().Item1;
            SSEPrice.Text = samsungu.Send().Item1;
            LGCPrice.Text = lgc.Send().Item1;
            HDCPrice.Text = hcar.Send().Item1;
            ECOPrice.Text = eco.Send().Item1;

            SSDPrice.Text = samsung.Send().Item2;
            LGEDPrice.Text = lg.Send().Item2;
            SSBDPrice.Text = bio.Send().Item2;
            POSCODPrice.Text = posco.Send().Item2;
            SKHDPrice.Text = sk.Send().Item2;
            SSSDPrice.Text = sdi.Send().Item2;
            SSEDPrice.Text = samsungu.Send().Item2;
            LGCDPrice.Text = lgc.Send().Item2;
            HDCDPrice.Text = hcar.Send().Item2;
            ECODPrice.Text = eco.Send().Item2;

            SSRate.Text = samsung.Send().Item3;
            LGERate.Text = lg.Send().Item3;
            SSBRate.Text = bio.Send().Item3;
            POSCORate.Text = posco.Send().Item3;
            SKHRate.Text = sk.Send().Item3;
            SSSRate.Text = sdi.Send().Item3;
            SSERate.Text = samsungu.Send().Item3;
            LGCRate.Text = lgc.Send().Item3;
            HDCRate.Text = hcar.Send().Item3;
            ECORate.Text = eco.Send().Item3;

            SetRank();
        }

        private void btn_money_Click(object sender, EventArgs e)
        {
            double sum1 = 0;
            double sum2 = 0;

            for (int i = 0; i < 10; i++)
            {
                sum1 += Convert.ToDouble(value1[i]);
                sum2 += Convert.ToDouble(value2[i]);
            }
            label7.Text = sum1.ToString() + " 원";

            label9.Text = (sum2 - sum1).ToString() + " 원";
            label11.Text = (((sum2 - sum1) / sum1) * 100).ToString("F2") + " %";
        }

        private void SetRank()
        {
            int SS = int.Parse(SSVolume.Text);
            int LGE = int.Parse(LGEVolume.Text);
            int SSB = int.Parse(SSBVolume.Text);
            int POSCO = int.Parse(POSCOVolume.Text);
            int SKH = int.Parse(SKHVolume.Text);
            int SSS = int.Parse(SSSVolume.Text);
            int SSE = int.Parse(SSEVolume.Text);
            int LGC = int.Parse(LGCVolume.Text);
            int HDC = int.Parse(HDCVolume.Text);
            int ECO = int.Parse(ECOVolume.Text);

            int[] Rank = new int[] { SS, LGE, SSB, POSCO, SKH, SSS, SSE, LGC, HDC, ECO };
            Array.Sort(Rank); // 오름차순으로 정렬
            Array.Reverse(Rank); // 배열을 뒤집어서 내림차순으로 정렬

            if (Rank[0] == SS) { lbVO1.Text = 1 + "위 " + arr[0]; }
            else if (Rank[0] == LGE) { lbVO1.Text = 1 + "위 " + arr[1]; }
            else if (Rank[0] == SSB) { lbVO1.Text = 1 + "위 " + arr[2]; }
            else if (Rank[0] == POSCO) { lbVO1.Text = 1 + "위 " + arr[3]; }
            else if (Rank[0] == SKH) { lbVO1.Text = 1 + "위 " + arr[4]; }
            else if (Rank[0] == SSS) { lbVO1.Text = 1 + "위 " + arr[5]; }
            else if (Rank[0] == SSE) { lbVO1.Text = 1 + "위 " + arr[6]; }
            else if (Rank[0] == LGC) { lbVO1.Text = 1 + "위 " + arr[7]; }
            else if (Rank[0] == HDC) { lbVO1.Text = 1 + "위 " + arr[8]; }
            else { lbVO1.Text = 1 + "위 " + arr[9]; }

            if (Rank[1] == SS) { lbVO2.Text = 2 + "위 " + arr[0]; }
            else if (Rank[1] == LGE) { lbVO2.Text = 2 + "위 " + arr[1]; }
            else if (Rank[1] == SSB) { lbVO2.Text = 2 + "위 " + arr[2]; }
            else if (Rank[1] == POSCO) { lbVO2.Text = 2 + "위 " + arr[3]; }
            else if (Rank[1] == SKH) { lbVO2.Text = 2 + "위 " + arr[4]; }
            else if (Rank[1] == SSS) { lbVO2.Text = 2 + "위 " + arr[5]; }
            else if (Rank[1] == SSE) { lbVO2.Text = 2 + "위 " + arr[6]; }
            else if (Rank[1] == LGC) { lbVO2.Text = 2 + "위 " + arr[7]; }
            else if (Rank[1] == HDC) { lbVO2.Text = 2 + "위 " + arr[8]; }
            else { lbVO2.Text = 2 + "위 " + arr[9]; }

            if (Rank[2] == SS) { lbVO3.Text = 3 + "위 " + arr[0]; }
            else if (Rank[2] == LGE) { lbVO3.Text = 3 + "위 " + arr[1]; }
            else if (Rank[2] == SSB) { lbVO3.Text = 3 + "위 " + arr[2]; }
            else if (Rank[2] == POSCO) { lbVO3.Text = 3 + "위 " + arr[3]; }
            else if (Rank[2] == SKH) { lbVO3.Text = 3 + "위 " + arr[4]; }
            else if (Rank[2] == SSS) { lbVO3.Text = 3 + "위 " + arr[5]; }
            else if (Rank[2] == SSE) { lbVO3.Text = 3 + "위 " + arr[6]; }
            else if (Rank[2] == LGC) { lbVO3.Text = 3 + "위 " + arr[7]; }
            else if (Rank[2] == HDC) { lbVO3.Text = 3 + "위 " + arr[8]; }
            else { lbVO3.Text = 3 + "위 " + arr[9]; }

            if (Rank[3] == SS) { lbVO4.Text = 4 + "위 " + arr[0]; }
            else if (Rank[3] == LGE) { lbVO4.Text = 4 + "위 " + arr[1]; }
            else if (Rank[3] == SSB) { lbVO4.Text = 4 + "위 " + arr[2]; }
            else if (Rank[3] == POSCO) { lbVO4.Text = 4 + "위 " + arr[3]; }
            else if (Rank[3] == SKH) { lbVO4.Text = 4 + "위 " + arr[4]; }
            else if (Rank[3] == SSS) { lbVO4.Text = 4 + "위 " + arr[5]; }
            else if (Rank[3] == SSE) { lbVO4.Text = 4 + "위 " + arr[6]; }
            else if (Rank[3] == LGC) { lbVO4.Text = 4 + "위 " + arr[7]; }
            else if (Rank[3] == HDC) { lbVO4.Text = 4 + "위 " + arr[8]; }
            else { lbVO4.Text = 4 + "위 " + arr[9]; }

            if (Rank[4] == SS) { lbVO5.Text = 5 + "위 " + arr[0]; }
            else if (Rank[4] == LGE) { lbVO5.Text = 5 + "위 " + arr[1]; }
            else if (Rank[4] == SSB) { lbVO5.Text = 5 + "위 " + arr[2]; }
            else if (Rank[4] == POSCO) { lbVO5.Text = 5 + "위 " + arr[3]; }
            else if (Rank[4] == SKH) { lbVO5.Text = 5 + "위 " + arr[4]; }
            else if (Rank[4] == SSS) { lbVO5.Text = 5 + "위 " + arr[5]; }
            else if (Rank[4] == SSE) { lbVO5.Text = 5 + "위 " + arr[6]; }
            else if (Rank[4] == LGC) { lbVO5.Text = 5 + "위 " + arr[7]; }
            else if (Rank[4] == HDC) { lbVO5.Text = 5 + "위 " + arr[8]; }
            else { lbVO5.Text = 5 + "위 " + arr[9]; }

            if (Rank[5] == SS) { lbVO6.Text = 6 + "위 " + arr[0]; }
            else if (Rank[5] == LGE) { lbVO6.Text = 6 + "위 " + arr[1]; }
            else if (Rank[5] == SSB) { lbVO6.Text = 6 + "위 " + arr[2]; }
            else if (Rank[5] == POSCO) { lbVO6.Text = 6 + "위 " + arr[3]; }
            else if (Rank[5] == SKH) { lbVO6.Text = 6 + "위 " + arr[4]; }
            else if (Rank[5] == SSS) { lbVO6.Text = 6 + "위 " + arr[5]; }
            else if (Rank[5] == SSE) { lbVO6.Text = 6 + "위 " + arr[6]; }
            else if (Rank[5] == LGC) { lbVO6.Text = 6 + "위 " + arr[7]; }
            else if (Rank[5] == HDC) { lbVO6.Text = 6 + "위 " + arr[8]; }
            else { lbVO6.Text = 6 + "위 " + arr[9]; }

            if (Rank[6] == SS) { lbVO7.Text = 7 + "위 " + arr[0]; }
            else if (Rank[6] == LGE) { lbVO7.Text = 7 + "위 " + arr[1]; }
            else if (Rank[6] == SSB) { lbVO7.Text = 7 + "위 " + arr[2]; }
            else if (Rank[6] == POSCO) { lbVO7.Text = 7 + "위 " + arr[3]; }
            else if (Rank[6] == SKH) { lbVO7.Text = 7 + "위 " + arr[4]; }
            else if (Rank[6] == SSS) { lbVO7.Text = 7 + "위 " + arr[5]; }
            else if (Rank[6] == SSE) { lbVO7.Text = 7 + "위 " + arr[6]; }
            else if (Rank[6] == LGC) { lbVO7.Text = 7 + "위 " + arr[7]; }
            else if (Rank[6] == HDC) { lbVO7.Text = 7 + "위 " + arr[8]; }
            else { lbVO7.Text = 7 + "위 " + arr[9]; }

            if (Rank[7] == SS) { lbVO8.Text = 8 + "위 " + arr[0]; }
            else if (Rank[7] == LGE) { lbVO8.Text = 8 + "위 " + arr[1]; }
            else if (Rank[7] == SSB) { lbVO8.Text = 8 + "위 " + arr[2]; }
            else if (Rank[7] == POSCO) { lbVO8.Text = 8 + "위 " + arr[3]; }
            else if (Rank[7] == SKH) { lbVO8.Text = 8 + "위 " + arr[4]; }
            else if (Rank[7] == SSS) { lbVO8.Text = 8 + "위 " + arr[5]; }
            else if (Rank[7] == SSE) { lbVO8.Text = 8 + "위 " + arr[6]; }
            else if (Rank[7] == LGC) { lbVO8.Text = 8 + "위 " + arr[7]; }
            else if (Rank[7] == HDC) { lbVO8.Text = 8 + "위 " + arr[8]; }
            else { lbVO8.Text = 8 + "위 " + arr[9]; }

            if (Rank[8] == SS) { lbVO9.Text = 9 + "위 " + arr[0]; }
            else if (Rank[8] == LGE) { lbVO9.Text = 9 + "위 " + arr[1]; }
            else if (Rank[8] == SSB) { lbVO9.Text = 9 + "위 " + arr[2]; }
            else if (Rank[8] == POSCO) { lbVO9.Text = 9 + "위 " + arr[3]; }
            else if (Rank[8] == SKH) { lbVO9.Text = 9 + "위 " + arr[4]; }
            else if (Rank[8] == SSS) { lbVO9.Text = 9 + "위 " + arr[5]; }
            else if (Rank[8] == SSE) { lbVO9.Text = 9 + "위 " + arr[6]; }
            else if (Rank[8] == LGC) { lbVO9.Text = 9 + "위 " + arr[7]; }
            else if (Rank[8] == HDC) { lbVO9.Text = 9 + "위 " + arr[8]; }
            else { lbVO9.Text = 9 + "위 " + arr[9]; }

            if (Rank[9] == SS) { lbVO10.Text = 10 + "위 " + arr[0]; }
            else if (Rank[9] == LGE) { lbVO10.Text = 10 + "위 " + arr[1]; }
            else if (Rank[9] == SSB) { lbVO10.Text = 10 + "위 " + arr[2]; }
            else if (Rank[9] == POSCO) { lbVO10.Text = 10 + "위 " + arr[3]; }
            else if (Rank[9] == SKH) { lbVO10.Text = 10 + "위 " + arr[4]; }
            else if (Rank[9] == SSS) { lbVO10.Text = 10 + "위 " + arr[5]; }
            else if (Rank[9] == SSE) { lbVO10.Text = 10 + "위 " + arr[6]; }
            else if (Rank[9] == LGC) { lbVO10.Text = 10 + "위 " + arr[7]; }
            else if (Rank[9] == HDC) { lbVO10.Text = 10 + "위 " + arr[8]; }
            else { lbVO10.Text = 10 + "위 " + arr[9]; }
        }

        private void WhatHave(string a)
        {
            switch (a)
            {
                case "삼성전자": {  listViewHave.Items.Add("삼성전자"); break; }
                case "LG에너지솔루션": { listViewHave.Items.Add("LG에너지솔루션"); break; }
                case "SK하이닉스": { listViewHave.Items.Add("SK하이닉스"); break; }
                case "삼성전자우": { listViewHave.Items.Add("삼성전자우"); break; }
                case "삼성바이오로직스": { listViewHave.Items.Add("삼성바이오로직스"); break; }
                case "POSCO홀딩스": { listViewHave.Items.Add("POSCO홀딩스"); break; }
                case "현대차": { listViewHave.Items.Add("현대차"); break; }
                case "LG화학": { listViewHave.Items.Add("LG화학"); break; }
                case "에코프로비엔": { listViewHave.Items.Add("에코프로비엔"); break; }
                case "삼성SDI": { listViewHave.Items.Add("삼성SDI"); break; }
            }
        }

        //인기 목록에서 버튼 눌러서 보유로 보내는 연습--------------------------------------------------------------------------------
        private void btn_1_Click(object sender, EventArgs e) 
        {
            ShareBuyForm modal = new ShareBuyForm(this, "삼성전자");
            modal.ShowDialog(); // 모달 대화상자 생성
                                // 모달 대화상자가 Close()될 때까지 홀딩 상태
            WhatHave("삼성전자");
            stockPortfolio[0, 0] = samsung.Send().Item1;
            stockPortfolio[0, 1] = samsung.Send().Item2;
            stockPortfolio[0, 2] = samsung.Send().Item3;
            modal.Dispose();
        }

        private void btn_2_Click(object sender, EventArgs e) 
        {
            ShareBuyForm modal = new ShareBuyForm(this, "LG에너지솔루션");
            modal.ShowDialog(); // 모달 대화상자 생성
                                // 모달 대화상자가 Close()될 때까지 홀딩 상태
            WhatHave("LG에너지솔루션");
            stockPortfolio[1, 0] = lg.Send().Item1;
            stockPortfolio[1, 1] = lg.Send().Item2;
            stockPortfolio[1, 2] = lg.Send().Item3;
            modal.Dispose();
        }

        private void btn_3_Click(object sender, EventArgs e) 
        {
            ShareBuyForm modal = new ShareBuyForm(this, "SK하이닉스");
            modal.ShowDialog(); // 모달 대화상자 생성
                                // 모달 대화상자가 Close()될 때까지 홀딩 상태
            WhatHave("SK하이닉스");
            stockPortfolio[2, 0] = sk.Send().Item1;
            stockPortfolio[2, 1] = sk.Send().Item2;
            stockPortfolio[2, 2] = sk.Send().Item3;
            modal.Dispose();
        }

        private void btn_4_Click(object sender, EventArgs e) 
        {
            ShareBuyForm modal = new ShareBuyForm(this, "삼성바이오로직스");
            modal.ShowDialog(); // 모달 대화상자 생성
                                // 모달 대화상자가 Close()될 때까지 홀딩 상태
            WhatHave("삼성바이오로직스");
            stockPortfolio[3, 0] = bio.Send().Item1;
            stockPortfolio[3, 1] = bio.Send().Item2;
            stockPortfolio[3, 2] = bio.Send().Item3;
            modal.Dispose();
        }

        private void btn_5_Click(object sender, EventArgs e) 
        {
            ShareBuyForm modal = new ShareBuyForm(this, "POSCO홀딩스");
            modal.ShowDialog(); // 모달 대화상자 생성
                                // 모달 대화상자가 Close()될 때까지 홀딩 상태
            WhatHave("POSCO홀딩스");
            stockPortfolio[4, 0] = posco.Send().Item1;
            stockPortfolio[4, 1] = posco.Send().Item2;
            stockPortfolio[4, 2] = posco.Send().Item3;
            modal.Dispose();
        }

        private void btn_6_Click(object sender, EventArgs e) 
        {
            ShareBuyForm modal = new ShareBuyForm(this, "삼성전자우");
            modal.ShowDialog(); // 모달 대화상자 생성
                                // 모달 대화상자가 Close()될 때까지 홀딩 상태
            WhatHave("삼성전자우");
            stockPortfolio[5, 0] = samsungu.Send().Item1;
            stockPortfolio[5, 1] = samsungu.Send().Item2;
            stockPortfolio[5, 2] = samsungu.Send().Item3;
            modal.Dispose();
        }

        private void btn_7_Click(object sender, EventArgs e) 
        {
            ShareBuyForm modal = new ShareBuyForm(this, "LG화학");
            modal.ShowDialog(); // 모달 대화상자 생성
                                // 모달 대화상자가 Close()될 때까지 홀딩 상태
            WhatHave("LG화학");
            stockPortfolio[6, 0] = lgc.Send().Item1;
            stockPortfolio[6, 1] = lgc.Send().Item2;
            stockPortfolio[6, 2] = lgc.Send().Item3;
            modal.Dispose();
        }

        private void btn_8_Click(object sender, EventArgs e) 
        {
            ShareBuyForm modal = new ShareBuyForm(this, "삼성SDI");
            modal.ShowDialog(); // 모달 대화상자 생성
                                // 모달 대화상자가 Close()될 때까지 홀딩 상태
            WhatHave("삼성SDI");
            stockPortfolio[7, 0] = sdi.Send().Item1;
            stockPortfolio[7, 1] = sdi.Send().Item2;
            stockPortfolio[7, 2] = sdi.Send().Item3;
            modal.Dispose();
        }

        private void btn_9_Click(object sender, EventArgs e) 
        {
            ShareBuyForm modal = new ShareBuyForm(this, "현대차");
            modal.ShowDialog(); // 모달 대화상자 생성
                                // 모달 대화상자가 Close()될 때까지 홀딩 상태
            WhatHave("현대차");
            stockPortfolio[8, 0] = hcar.Send().Item1;
            stockPortfolio[8, 1] = hcar.Send().Item2;
            stockPortfolio[8, 2] = hcar.Send().Item3;
            modal.Dispose();
        }

        private void btn_10_Click(object sender, EventArgs e) 
        {
            ShareBuyForm modal = new ShareBuyForm(this, "에코프로비엠");
            modal.ShowDialog(); // 모달 대화상자 생성
                                // 모달 대화상자가 Close()될 때까지 홀딩 상태
            WhatHave("에코프로비엠");
            stockPortfolio[9, 0] = eco.Send().Item1;
            stockPortfolio[9, 1] = eco.Send().Item2;
            stockPortfolio[9, 2] = eco.Send().Item3;
            modal.Dispose();
        }
        //여기까지 버튼 연습 ------------------------------------------------------------------------------------------------------------------------

        (double, double, double, double) Price(string item1, string stock0, string stock3)
        {
            double current = Convert.ToDouble(item1);
            double buy = Convert.ToDouble(stock0);
            double num = Convert.ToDouble(stock3);
            double rate = ((current - buy) / buy) * 100;
            double evolution = current * num;
            double erate = rate;
            double buyprice = buy * num;

            return (evolution, rate, erate, buyprice);   //평가금액, 손익률, 평가금액 손익률, 매입금액
        }

        //보유 자산을 더블클릭 하였을 때 
        private void listViewHave_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listViewHave.FocusedItem.Text == "삼성전자")
            {
                lbEvalution.Text = (Price(samsung.Send().Item1, stockPortfolio[0, 0], stockPortfolio[0, 3]).Item1).ToString("F0"); ;
                lbRate.Text = (Price(samsung.Send().Item1, stockPortfolio[0, 0], stockPortfolio[0, 3]).Item2).ToString("F0") + "%";
                lbRate3.Text = (Price(samsung.Send().Item1, stockPortfolio[0, 0], stockPortfolio[0, 3]).Item3).ToString("F0") + "%"; ;
                lbPaL.Text = (Convert.ToInt32(samsung.Send().Item1) - Convert.ToInt32(stockPortfolio[0, 0])).ToString();
                lbPrice.Text = (Price(samsung.Send().Item1, stockPortfolio[0, 0], stockPortfolio[0, 3]).Item4).ToString("F0");
                lbPresentPrice.Text = samsung.Send().Item1;
                lbBuyPrice.Text = stockPortfolio[0, 0];
                lbBalance.Text = stockPortfolio[0, 3];
                value1[0] = lbEvalution.Text;
                value2[0] = lbPrice.Text;
            }
            else if (listViewHave.FocusedItem.Text == "LG에너지솔루션")
            {
                lbEvalution.Text = (Price(lg.Send().Item1, stockPortfolio[1, 0], stockPortfolio[1, 3]).Item1).ToString("F0"); ;
                lbRate.Text = (Price(lg.Send().Item1, stockPortfolio[1, 0], stockPortfolio[1, 3]).Item2).ToString("F0") + "%";
                lbRate3.Text = (Price(lg.Send().Item1, stockPortfolio[1, 0], stockPortfolio[1, 3]).Item3).ToString("F0") + "%"; ;
                lbPaL.Text = (Convert.ToInt32(lg.Send().Item1) - Convert.ToInt32(stockPortfolio[1, 0])).ToString();
                lbPrice.Text = (Price(lg.Send().Item1, stockPortfolio[1, 0], stockPortfolio[1, 3]).Item4).ToString("F0");
                lbPresentPrice.Text = lg.Send().Item1;
                lbBuyPrice.Text = stockPortfolio[1, 0];
                lbBalance.Text = stockPortfolio[1, 3];
                value1[1] = lbEvalution.Text;
                value2[1] = lbPrice.Text;
            }
            else if (listViewHave.FocusedItem.Text == "SK하이닉스")
            {
                lbEvalution.Text = (Price(sk.Send().Item1, stockPortfolio[2, 0], stockPortfolio[2, 3]).Item1).ToString("F0"); ;
                lbRate.Text = (Price(sk.Send().Item1, stockPortfolio[2, 0], stockPortfolio[2, 3]).Item2).ToString("F0") + "%";
                lbRate3.Text = (Price(sk.Send().Item1, stockPortfolio[2, 0], stockPortfolio[2, 3]).Item3).ToString("F0") + "%"; ;
                lbPaL.Text = (Convert.ToInt32(sk.Send().Item1) - Convert.ToInt32(stockPortfolio[2, 0])).ToString();
                lbPrice.Text = (Price(sk.Send().Item1, stockPortfolio[2, 0], stockPortfolio[2, 3]).Item4).ToString("F0");
                lbPresentPrice.Text = sk.Send().Item1;
                lbBuyPrice.Text = stockPortfolio[2, 0];
                lbBalance.Text = stockPortfolio[2, 3];
                value1[2] = lbEvalution.Text;
                value2[2] = lbPrice.Text;
            }
            else if (listViewHave.FocusedItem.Text == "삼성바이오로직스")
            {
                lbEvalution.Text = (Price(bio.Send().Item1, stockPortfolio[3, 0], stockPortfolio[3, 3]).Item1).ToString("F0"); ;
                lbRate.Text = (Price(bio.Send().Item1, stockPortfolio[3, 0], stockPortfolio[3, 3]).Item2).ToString("F0") + "%";
                lbRate3.Text = (Price(bio.Send().Item1, stockPortfolio[3, 0], stockPortfolio[3, 3]).Item3).ToString("F0") + "%"; ;
                lbPaL.Text = (Convert.ToInt32(bio.Send().Item1) - Convert.ToInt32(stockPortfolio[3, 0])).ToString();
                lbPrice.Text = (Price(bio.Send().Item1, stockPortfolio[3, 0], stockPortfolio[3, 3]).Item4).ToString("F0");
                lbPresentPrice.Text = bio.Send().Item1;
                lbBuyPrice.Text = stockPortfolio[3, 0];
                lbBalance.Text = stockPortfolio[3, 3];
                value1[3] = lbEvalution.Text;
                value2[3] = lbPrice.Text;
            }
            else if (listViewHave.FocusedItem.Text == "POSCO홀딩스")
            {
                lbEvalution.Text = (Price(posco.Send().Item1, stockPortfolio[4, 0], stockPortfolio[4, 3]).Item1).ToString("F0"); ;
                lbRate.Text = (Price(posco.Send().Item1, stockPortfolio[4, 0], stockPortfolio[4, 3]).Item2).ToString("F0") + "%";
                lbRate3.Text = (Price(posco.Send().Item1, stockPortfolio[4, 0], stockPortfolio[4, 3]).Item3).ToString("F0") + "%"; ;
                lbPaL.Text = (Convert.ToInt32(posco.Send().Item1) - Convert.ToInt32(stockPortfolio[4, 0])).ToString();
                lbPrice.Text = (Price(posco.Send().Item1, stockPortfolio[4, 0], stockPortfolio[4, 3]).Item4).ToString("F0");
                lbPresentPrice.Text = posco.Send().Item1;
                lbBuyPrice.Text = stockPortfolio[4, 0];
                lbBalance.Text = stockPortfolio[4, 3];
                value1[4] = lbEvalution.Text;
                value2[4] = lbPrice.Text;
            }
            else if (listViewHave.FocusedItem.Text == "삼성전자우")
            {
                lbEvalution.Text = (Price(samsungu.Send().Item1, stockPortfolio[5, 0], stockPortfolio[5, 3]).Item1).ToString("F0"); ;
                lbRate.Text = (Price(samsungu.Send().Item1, stockPortfolio[5, 0], stockPortfolio[5, 3]).Item2).ToString("F0") + "%";
                lbRate3.Text = (Price(samsungu.Send().Item1, stockPortfolio[5, 0], stockPortfolio[5, 3]).Item3).ToString("F0") + "%"; ;
                lbPaL.Text = (Convert.ToInt32(samsungu.Send().Item1) - Convert.ToInt32(stockPortfolio[5, 0])).ToString();
                lbPrice.Text = (Price(samsungu.Send().Item1, stockPortfolio[5, 0], stockPortfolio[5, 3]).Item4).ToString("F0");
                lbPresentPrice.Text = samsungu.Send().Item1;
                lbBuyPrice.Text = stockPortfolio[5, 0];
                lbBalance.Text = stockPortfolio[5, 3];
                value1[5] = lbEvalution.Text;
                value2[5] = lbPrice.Text;
            }
            else if (listViewHave.FocusedItem.Text == "LG화학")
            {
                lbEvalution.Text = (Price(lgc.Send().Item1, stockPortfolio[6, 0], stockPortfolio[6, 3]).Item1).ToString("F0"); ;
                lbRate.Text = (Price(lgc.Send().Item1, stockPortfolio[6, 0], stockPortfolio[6, 3]).Item2).ToString("F0") + "%";
                lbRate3.Text = (Price(lgc.Send().Item1, stockPortfolio[6, 0], stockPortfolio[6, 3]).Item3).ToString("F0") + "%"; ;
                lbPaL.Text = (Convert.ToInt32(lgc.Send().Item1) - Convert.ToInt32(stockPortfolio[6, 0])).ToString();
                lbPrice.Text = (Price(lgc.Send().Item1, stockPortfolio[6, 0], stockPortfolio[6, 3]).Item4).ToString("F0");
                lbPresentPrice.Text = lgc.Send().Item1;
                lbBuyPrice.Text = stockPortfolio[6, 0];
                lbBalance.Text = stockPortfolio[6, 3];
                value1[6] = lbEvalution.Text;
                value2[6] = lbPrice.Text;
            }
            else if (listViewHave.FocusedItem.Text == "삼성SDI")
            {
                lbEvalution.Text = (Price(sdi.Send().Item1, stockPortfolio[7, 0], stockPortfolio[7, 3]).Item1).ToString("F0"); ;
                lbRate.Text = (Price(sdi.Send().Item1, stockPortfolio[7, 0], stockPortfolio[7, 3]).Item2).ToString("F0") + "%";
                lbRate3.Text = (Price(sdi.Send().Item1, stockPortfolio[7, 0], stockPortfolio[7, 3]).Item3).ToString("F0") + "%"; ;
                lbPaL.Text = (Convert.ToInt32(sdi.Send().Item1) - Convert.ToInt32(stockPortfolio[7, 0])).ToString();
                lbPrice.Text = (Price(sdi.Send().Item1, stockPortfolio[7, 0], stockPortfolio[7, 3]).Item4).ToString("F0");
                lbPresentPrice.Text = sdi.Send().Item1;
                lbBuyPrice.Text = stockPortfolio[7, 0];
                lbBalance.Text = stockPortfolio[7, 3];
                value1[7] = lbEvalution.Text;
                value2[7] = lbPrice.Text;
            }
            else if (listViewHave.FocusedItem.Text == "현대차")
            {
                lbEvalution.Text = (Price(hcar.Send().Item1, stockPortfolio[8, 0], stockPortfolio[8, 3]).Item1).ToString("F0"); ;
                lbRate.Text = (Price(hcar.Send().Item1, stockPortfolio[8, 0], stockPortfolio[8, 3]).Item2).ToString("F0") + "%";
                lbRate3.Text = (Price(hcar.Send().Item1, stockPortfolio[8, 0], stockPortfolio[8, 3]).Item3).ToString("F0") + "%"; ;
                lbPaL.Text = (Convert.ToInt32(hcar.Send().Item1) - Convert.ToInt32(stockPortfolio[8, 0])).ToString();
                lbPrice.Text = (Price(hcar.Send().Item1, stockPortfolio[8, 0], stockPortfolio[8, 3]).Item4).ToString("F0");
                lbPresentPrice.Text = hcar.Send().Item1;
                lbBuyPrice.Text = stockPortfolio[8, 0];
                lbBalance.Text = stockPortfolio[8, 3];
                value1[8] = lbEvalution.Text;
                value2[8] = lbPrice.Text;
            }
            else if (listViewHave.FocusedItem.Text == "에코프로비엠")
            {
                lbEvalution.Text = (Price(eco.Send().Item1, stockPortfolio[9, 0], stockPortfolio[9, 3]).Item1).ToString("F0"); ;
                lbRate.Text = (Price(eco.Send().Item1, stockPortfolio[9, 0], stockPortfolio[9, 3]).Item2).ToString("F0") + "%";
                lbRate3.Text = (Price(eco.Send().Item1, stockPortfolio[9, 0], stockPortfolio[9, 3]).Item3).ToString("F0") + "%"; ;
                lbPaL.Text = (Convert.ToInt32(eco.Send().Item1) - Convert.ToInt32(stockPortfolio[9, 0])).ToString();
                lbPrice.Text = (Price(eco.Send().Item1, stockPortfolio[9, 0], stockPortfolio[9, 3]).Item4).ToString("F0");
                lbPresentPrice.Text = eco.Send().Item1;
                lbBuyPrice.Text = stockPortfolio[9, 0];
                lbBalance.Text = stockPortfolio[9, 3];
                value1[9] = lbEvalution.Text;
                value2[9] = lbPrice.Text;
            }
        }
    }
}
