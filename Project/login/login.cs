using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project
{
    public partial class login : Form
    {
        public List<List<string>> info = new List<List<string>>() { new List<string> { "OOO", "1234", "1234" } };

        public bool Success;
        public bool IsLoginSuccessful { get { return Success; } private set { Success = value; } }

        public string loginName { get; set; }

        public login()
        {
            InitializeComponent();
            txt_id.ForeColor = Color.Gray;
            txt_password.ForeColor = Color.Gray;
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            bool isLoginSuccessful = CheckLoginCredentials();
            IsLoginSuccessful = isLoginSuccessful;

            if (isLoginSuccessful) { this.DialogResult = DialogResult.OK; this.Close(); }
            else { MessageBox.Show("아이디 또는 비밀번호가 일치하지 않습니다."); txt_id.Text = string.Empty; txt_password.Text = string.Empty; IsLoginSuccessful = false; }
        }

        private bool CheckLoginCredentials()
        {
            for (int i = 0; i < info.Count; i++)
            {
                if ((info[i][1] == txt_id.Text) && (info[i][2] == txt_password.Text))
                {
                    loginName = info[i][0];
                    return true;
                }
            }
            return false;
        }

        private void btn_join_Click(object sender, EventArgs e)
        {
            join join = new join(this);
            DialogResult result = join.ShowDialog();
            if (result == DialogResult.OK) { info.Add(join.joininfo); }
            join.Dispose();
        }

        private void btn_pwchange_Click(object sender, EventArgs e)
        {
            pwchange change = new pwchange(this);
            DialogResult result = change.ShowDialog();
            if (result == DialogResult.OK) { change.Dispose(); }
        }

        private void login_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms.Count == 0) { Application.Exit(); }
        }

        private void txt_password_TextChanged(object sender, EventArgs e)
        {
            txt_password.PasswordChar = '*';
            txt_password.ForeColor = Color.Black;
        }

        private void txt_id_TextChanged(object sender, EventArgs e)
        {
            txt_id.ForeColor = Color.Black;
        }
    }
}
