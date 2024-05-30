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
    public partial class pwchange : Form
    {
        login login = new login();
        int index = 0;
        public pwchange(login login)
        {
            InitializeComponent();
            this.login = login;
            txt_changename.ForeColor = Color.Gray;
            txt_changeid.ForeColor = Color.Gray;
        }

        private void btncheck_Click(object sender, EventArgs e)
        {
            if((txt_changename.Text != string.Empty) && (txt_changeid.Text != string.Empty))
            {
                for (int i = 0; i < login.info.Count; i++)
                {
                    if (login.info[i][0].Contains(txt_changename.Text)) { index = i; }
                }
                if (login.info[index][1] == txt_changeid.Text) { pn_pwchange.Visible = false; }
                else { MessageBox.Show("일치하는 정보가 없습니다."); txt_changename.Text = string.Empty; txt_changeid.Text = string.Empty; }
            }
            else { MessageBox.Show("일치하는 정보가 없습니다."); txt_changename.Text = string.Empty; txt_changeid.Text = string.Empty; }
        }

        private void btn_pwchange_Click(object sender, EventArgs e)
        {
            if ((txt_newpw.Text == string.Empty) && (txt_pwcheck.Text == string.Empty)) { MessageBox.Show("다시 입력하세요"); }
            if (txt_newpw.Text != txt_pwcheck.Text) { MessageBox.Show("다시 입력하세요"); }
            if (txt_newpw.Text == txt_pwcheck.Text) { login.info[index][2] = txt_newpw.Text; MessageBox.Show("변경되었습니다."); }
            DialogResult = DialogResult.OK;
        }

        private void txt_changename_TextChanged(object sender, EventArgs e)
        {
            txt_changename.ForeColor = Color.Black;
        }

        private void txt_changeid_TextChanged(object sender, EventArgs e)
        {
            txt_changeid.ForeColor = Color.Black;
        }

        private void txt_newpw_TextChanged(object sender, EventArgs e)
        {
            txt_newpw.ForeColor = Color.Black;
            txt_newpw.PasswordChar = '*';
        }

        private void txt_pwcheck_TextChanged(object sender, EventArgs e)
        {
            txt_pwcheck.ForeColor = Color.Black;
            txt_pwcheck.PasswordChar = '*';
        }
    }
}
