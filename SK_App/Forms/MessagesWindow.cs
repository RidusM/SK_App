using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using SK_App.Scripts;
using DataTable = System.Data.DataTable;

namespace SK_App.Forms
{
    public partial class MessagesWindow : Form
    {
        private Database db;
        private DataTable dt;
        public int role_id;
        public int idUser;
        public int selectIDUser = 0;
        public int selectIDMessage = 0;
        public MessagesWindow()
        {
            InitializeComponent();
            db = new Database();
            dt = new DataTable();
            comboBoxSelects.Items.Add("Просмотр всех сообщений");
            comboBoxSelects.Items.Add("Просмотр сообщений от сотрудника");
            dt = (DataTable)db.SelectEmployeesIDName();
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "name";
            comboBox1.ValueMember = "employee_id";
            comboBoxSelectIDEmp.Visible = false;
            comboBoxSelectIDEmp.Enabled = false;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }

        private void ClientsWindow_Load(object sender, EventArgs e)
        {
            comboBox1.Text = "";
        }

        public void DbSelect()
        {
            switch (Convert.ToString(comboBoxSelects.SelectedIndex))
            {
                case "0":
                    comboBoxSelectIDEmp.Visible = false;
                    comboBoxSelectIDEmp.Enabled = false;
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = db.SelectAllMessages();
                    break;
                case "1":
                    dataGridView1.DataSource = null;
                    comboBoxSelectIDEmp.Visible = true;
                    comboBoxSelectIDEmp.Enabled = true;
                    dt = (DataTable)db.SelectEmployeesIDName();
                    comboBoxSelectIDEmp.DataSource = dt;
                    comboBoxSelectIDEmp.DisplayMember = "name";
                    comboBoxSelectIDEmp.ValueMember = "employee_id";
                    break;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DbSelect();
        }

        private void comboBoxSelectID_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            if (comboBoxSelectIDEmp.SelectedIndex > 0)
            {
                selectIDUser = (int)comboBoxSelectIDEmp.SelectedValue;
                dataGridView1.DataSource = db.SelectMessagesByEmployee(selectIDUser);
            }
            else
            {
                selectIDUser = 1;
                dataGridView1.DataSource = db.SelectMessagesByEmployee(selectIDUser);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && comboBoxSelects.SelectedIndex != 5)
            {
                selectIDMessage = (int)dataGridView1.Rows[e.RowIndex].Cells[0].Value;
                comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBoxMessage.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            }
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (comboBoxSelects.SelectedIndex != 5)
            {
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Отправитель";
                dataGridView1.Columns[2].HeaderText = "Сообщение";
            }
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            db.CreateMessage((int)comboBox1.SelectedValue, textBoxMessage.Text);
            DbSelect();
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            db.DeleteMessage(selectIDMessage);
            DbSelect();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            db.UpdateMessage(textBoxMessage.Text, selectIDMessage);
            DbSelect();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
