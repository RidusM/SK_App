using System;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using SK_App.Forms;
using SK_App.Scripts;

namespace SK_App
{
    public partial class AuthWindow : Form
    {
        private Database _db;
        private DataTable _dt;

        public AuthWindow()
        {
            InitializeComponent();
            passBox.AutoSize = false;
            this.passBox.Size = new Size(this.passBox.Size.Width, 51);
        }

        private void PassBox_TextChanged(object sender, EventArgs e)
        {
            if (loginBox.TextLength > 0)
            {
                enterButton.Enabled = true;
                enterButton.BackColor = Color.FromArgb(238,238,238);
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            _dt = new DataTable();
            _db = new Database();
            _dt = (DataTable)_db.SelectAuthInfo(loginBox.Text, passBox.Text);
            if (_dt.Rows.Count > 0)
            {
                int role_ = (int)_dt.Rows[0][2];
                string name = (string)_dt.Rows[0][1];
                int id = (int)_dt.Rows[0][0];
                this.Close();
                new Thread(() => Application.Run(new MainWindow(role_, id, name))).Start();
            }
            else
            {
                MessageBox.Show("Пользователь не найден. Проверьте введенные данные",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AuthWindow_Load(object sender, EventArgs e)
        {
            enterButton.Enabled = false;
            enterButton.BackColor = Color.Gray;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
