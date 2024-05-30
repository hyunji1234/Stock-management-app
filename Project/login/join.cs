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
    public partial class join : Form
    {
        login login = new login();
        public List<string> joininfo = new List<string>() { };

        public join(login login)
        {
            InitializeComponent();
            this.login = login;
            txt_joinname.ForeColor = Color.Gray;
            txt_joinid.ForeColor = Color.Gray;
            txt_joinpassword.ForeColor = Color.Gray;
        }

        private void btnjoin_Click(object sender, EventArgs e)
        {
            if ((txt_joinname.Text != string.Empty) && (txt_joinid.Text != string.Empty) && (txt_joinpassword.Text != string.Empty))
            {
                joininfo.Add(txt_joinname.Text);
                joininfo.Add(txt_joinid.Text);
                joininfo.Add(txt_joinpassword.Text);
                //login.info.Add(joininfo);
                DialogResult = DialogResult.OK;
                MessageBox.Show($"{joininfo[0]}님\n회원가입되었습니다.");
            }
            else { MessageBox.Show("다시 입력해주세요"); txt_joinname.Text = string.Empty; txt_joinid.Text = string.Empty; txt_joinpassword.Text = string.Empty; return; }
        }

        private void txt_joinname_TextChanged(object sender, EventArgs e)
        {
            txt_joinname.ForeColor = Color.Black;
        }

        private void txt_joinid_TextChanged(object sender, EventArgs e)
        {
            txt_joinid.ForeColor = Color.Black;
        }

        private void txt_joinpassword_TextChanged(object sender, EventArgs e)
        {
            txt_joinpassword.PasswordChar = '*';
            txt_joinpassword.ForeColor = Color.Black;
        }
    }
}
