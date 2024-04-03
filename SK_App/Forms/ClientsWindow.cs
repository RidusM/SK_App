using System;
using System.Windows.Forms;
using SK_App.Scripts;
using Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;

namespace SK_App.Forms
{
    public partial class ClientsWindow : Form
    {
        private Database db;
        private DataTable dt;
        private DataTable dt2;
        public int role_id;
        public int idUser;
        public int selectIDProj;
        public ClientsWindow(int role, int id)
        {
            InitializeComponent();
            role_id = role;
            idUser = id;
            db = new Database();
            dt = new DataTable();
            comboBoxSelects.Items.Add("Просмотр всех актуальных заказчиков");
            comboBoxSelects.Items.Add("Просмотр заказчиков по Проекту");
            if (role != 4)
            {
                comboBoxSelects.Items.Add("Просмотр всех заказчиков");
                comboBoxSelects.Items.Add("Архивные данные");
            }
            if (role == 1)
            {
                comboBoxSelects.Items.Add("Админский просмотр данных");
            }
            if (role < 2)
            {
                buttonCreate.Visible = true;
                buttonRemove.Visible = true;
                buttonUpdate.Visible = true;
                buttonSave.Visible = true;
            }
            dt2 = (DataTable)db.SelectClientsIDName();
            dt = (DataTable)db.SelectProjectsIDName();
            comboBoxSelectProjects.DataSource = dt;
            comboBoxSelectProjects.DisplayMember = "name";
            comboBoxSelectProjects.ValueMember = "project_id";
            comboBoxFIO.DataSource = dt2;
            comboBoxFIO.DisplayMember = "name";
            comboBoxFIO.ValueMember = "client_id";
            dataGridView1.ColumnHeadersVisible = false;
            comboBoxSelectProjects.Visible = false;
            comboBoxSelectProjects.Enabled = false;
            buttonCreate.Enabled = false;
            buttonRemove.Enabled = false;
            buttonUpdate.Enabled = false;
            buttonSave.Enabled = false;
        }

        private void ClientsWindow_Load(object sender, EventArgs e)
        {
            comboBoxFIO.Text = "";
        }

        public void DbSelect()
        {
            dataGridView1.ColumnHeadersVisible = true;
            switch (Convert.ToString(comboBoxSelects.SelectedIndex))
            {
                case "0":
                    comboBoxSelectProjects.Visible = false;
                    comboBoxSelectProjects.Enabled = false;
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = db.SelectActualClients();
                    break;
                case "1":
                    comboBoxSelectProjects.Visible = true;
                    comboBoxSelectProjects.Enabled = true;
                    comboBoxSelectProjects.DataSource = dt;
                    dt = (DataTable)db.SelectProjectsIDName();
                    comboBoxSelectProjects.DisplayMember = "name";
                    comboBoxSelectProjects.ValueMember = "project_id";
                    break;
                case "2":
                    comboBoxSelectProjects.Visible = false;
                    comboBoxSelectProjects.Enabled = false;
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = db.SelectAllClients();
                    break;
                case "3":
                    comboBoxSelectProjects.Visible = false;
                    comboBoxSelectProjects.Enabled = false;
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = db.SelectArchiveClientsData();
                    break;
                case "4":
                    comboBoxSelectProjects.Visible = false;
                    comboBoxSelectProjects.Enabled = false;
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = db.SelectALLforAdmin("clients");
                    break;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DbSelect();
        }

        private void comboBoxSelectProjID_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            if (comboBoxSelectProjects.SelectedIndex > 0)
            {
                selectIDProj = (int)comboBoxSelectProjects.SelectedValue;
                dataGridView1.DataSource = db.SelectClientsByProjectId(selectIDProj);
            }
            else
            {
                dataGridView1.DataSource = db.SelectClientsByProjectId(selectIDProj);
            }
           
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && comboBoxSelects.SelectedIndex != 5)
            {
                comboBoxFIO.Text = dataGridView1.Rows[e.RowIndex].Cells["name"].Value.ToString();
                textBoxPhone.Text = dataGridView1.Rows[e.RowIndex].Cells["phone_number"].Value.ToString();
            }
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            db.CreateClient(comboBoxFIO.Text, textBoxPhone.Text);
            DbSelect();
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            db.DeleteClients((int)comboBoxFIO.SelectedValue);
            DbSelect();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            db.UpdateClient(comboBoxFIO.Text, textBoxPhone.Text, (int)comboBoxFIO.SelectedValue);
            DbSelect();
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (comboBoxSelects.SelectedIndex != 4)
            {
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "ФИО";
                dataGridView1.Columns[2].HeaderText = "Номер телефона";
                dataGridView1.Columns[3].HeaderText = "Проект";
            }

            buttonSave.Enabled = true;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application XlApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook XlWorkBook = XlApp.Workbooks.Add(@"C:\Users\storm\OneDrive\Документы\SK_APP\ExcelFile.xlsx"); //создать новый файл: XlApp.Workbooks.Add();
            Microsoft.Office.Interop.Excel.Worksheet XlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)XlWorkBook.Worksheets.get_Item(1); //1-й лист по порядку
            Range cells = XlWorkBook.Worksheets[1].Cells;
            cells.NumberFormat = "@";
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                for (int j = 1; j < dataGridView1.ColumnCount; j++)
                {
                    XlWorkSheet.Cells[1, j+1] = dataGridView1.Columns[j].HeaderText;
                    XlWorkSheet.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value;
                }
            }
            XlWorkSheet.Columns.EntireColumn.AutoFit();
            XlApp.Visible = true;
            XlApp.UserControl = true;
        }

        private void comboBoxFIO_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void textBoxPhone_TextChanged(object sender, EventArgs e)
        {
            if (comboBoxFIO.Text != "" && textBoxPhone.Text != "")
            {
                buttonCreate.Enabled = true;
                buttonUpdate.Enabled = true;
            }
        }

        private void comboBoxFIO_TextChanged(object sender, EventArgs e)
        {
            if (comboBoxFIO.Text != "") buttonRemove.Enabled = true;
        }
    }
}
