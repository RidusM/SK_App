using SK_App.Scripts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SK_App.Forms
{
    public partial class ProfileWindow : Form
    {
        public int id_User; 
        private Database db;
        private DataTable dt;
        public ProfileWindow(int id)
        {
            id_User = id;
            InitializeComponent();
        }

        private void MessagesWindow_Load(object sender, EventArgs e)
        {
            dt = new DataTable();
            db = new Database();
            dt = (DataTable)db.SelectEmployeeInfo(id_User);
            if (dt.Rows.Count > 0)
            {
                labelName.Text = dt.Rows[0][0].ToString();
                labelRole.Text = dt.Rows[0][3].ToString();
                labelPhoneNumber.Text = dt.Rows[0][1].ToString();
                labelHourlyRate.Text = dt.Rows[0][2].ToString();
                labelMonthHours.Text = dt.Rows[0][4].ToString();
            }
            else
            {
                MessageBox.Show("Пользователь не найден", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
