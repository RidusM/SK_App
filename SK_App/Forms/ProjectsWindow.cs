using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using SK_App.Scripts;
using DataTable = System.Data.DataTable;

namespace SK_App.Forms
{
    public partial class ProjectsWindow : Form
    {
        private Database db;
        private DataTable dt;
        public int role_id;
        public int idUser;
        public int selectIDEmployee = 0;
        public int SelectIDProject = 0;
        public ProjectsWindow(int role, int id)
        {
            InitializeComponent();
            role_id = role;
            idUser = id;
            db = new Database();
            dt = new DataTable();
            comboBoxSelects.Items.Add("Просмотр всех актуальных проектов");
            comboBoxSelects.Items.Add("Просмотр проектов по Сотруднику");
            if (role != 4)
            {
                comboBoxSelects.Items.Add("Просмотр всех проектов");
                comboBoxSelects.Items.Add("Архивные данные");
            }
            if (role == 1)
            {
                comboBoxSelects.Items.Add("Админский просмотр данных");
            }
            if (role == 4)
            {
                buttonSave.Visible = false;
                buttonCreate.Visible = false;
                buttonRemove.Visible = false;
                buttonEndProject.Visible = false;
                buttonUpdateProject.Visible = false;
            }
            if (role <= 3)
            {
                buttonCreate.Visible = true;
                buttonSave.Visible = true;
                buttonEndProject.Visible = true;
                buttonRemove.Visible = true;
                buttonUpdateProject.Visible = true;
            }
            dt = (DataTable)db.SelectProjectsIDName();
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "name";
            comboBox1.ValueMember = "project_id";
            comboBoxSelectIDEmp.Visible = false;
            comboBoxSelectIDEmp.Enabled = false;
            buttonCreate.Enabled = false;
            buttonSave.Enabled = false;
            buttonEndProject.Enabled = false;
            buttonRemove.Enabled = false;
            buttonUpdateProject.Enabled = false;

        }

        private void ClientsWindow_Load(object sender, EventArgs e)
        {
            comboBox1.Text = "";
        }

        public void DbSelect()
        {
            db.CountProjectsPrimeCost();
            db.CountProjectWorkingHours();
            switch (Convert.ToString(comboBoxSelects.SelectedIndex))
            {
                case "0":
                    comboBoxSelectIDEmp.Visible = false;
                    comboBoxSelectIDEmp.Enabled = false;
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = db.SelectActualProjects();
                    break;
                case "1":
                    dataGridView1.DataSource = null;
                    if (role_id == 4)
                    {
                        dataGridView1.DataSource = db.SelectProjectByEmployee(idUser);
                    }
                    else
                    {
                        comboBoxSelectIDEmp.Visible = true;
                        comboBoxSelectIDEmp.Enabled = true;
                        dt = (DataTable)db.SelectEmployeesIDName();
                        comboBoxSelectIDEmp.DataSource = dt;
                        comboBoxSelectIDEmp.DisplayMember = "name";
                        comboBoxSelectIDEmp.ValueMember = "employee_id";
                    }
                    break;
                case "2":
                    comboBoxSelectIDEmp.Visible = false;
                    comboBoxSelectIDEmp.Enabled = false;
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = db.SelectAllProjects();
                    break;
                case "3":
                    comboBoxSelectIDEmp.Visible = false;
                    comboBoxSelectIDEmp.Enabled = false;
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = db.SelectArchiveProjectsData();
                    break;
                case "4":
                    comboBoxSelectIDEmp.Visible = false;
                    comboBoxSelectIDEmp.Enabled = false;
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = db.SelectALLforAdmin("projects");
                    break;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DbSelect();
            buttonSave.Enabled = true;
        }

        private void comboBoxSelectID_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            if (comboBoxSelectIDEmp.SelectedIndex > 0)
            {
                selectIDEmployee = (int)comboBoxSelectIDEmp.SelectedValue;
                dataGridView1.DataSource = db.SelectProjectByEmployee(selectIDEmployee);
            }
            else
            {
                selectIDEmployee = 1;
                dataGridView1.DataSource = db.SelectProjectByEmployee(selectIDEmployee);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && comboBoxSelects.SelectedIndex != 5)
            {
                SelectIDProject = (int)dataGridView1.Rows[e.RowIndex].Cells[0].Value;
                comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            }
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (comboBoxSelects.SelectedIndex != 4)
            {
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Название проекта";
                dataGridView1.Columns[2].HeaderText = "Дата начала проекта";
                dataGridView1.Columns[3].HeaderText = "Дата окончания проекта";
                dataGridView1.Columns[4].HeaderText = "Рабочие часы проекта";
                dataGridView1.Columns[5].HeaderText = "Стоимость проекта, руб.";
            }
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            db.CreateProject(comboBox1.Text, DateTime.Now.ToString());
            DbSelect();
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            db.DeleteProjects((int)comboBox1.SelectedValue);
            DbSelect();
        }

        private void buttonEndProject_Click(object sender, EventArgs e)
        {
            db.EndProject(DateTime.Now.ToString(), (int)comboBox1.SelectedValue);
            DbSelect();
        }

        private void buttonUpdateProject_Click(object sender, EventArgs e)
        {
            db.UpdateProject(comboBox1.Text, SelectIDProject);
            DbSelect();
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
                    XlWorkSheet.Cells[1, j + 1] = dataGridView1.Columns[j].HeaderText;
                    XlWorkSheet.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value;
                }
            }
            XlWorkSheet.Columns.EntireColumn.AutoFit();
            XlApp.Visible = true;
            XlApp.UserControl = true;
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text != "")
            {
                buttonCreate.Enabled = true;
                if (SelectIDProject != 0)
                {
                    buttonUpdateProject.Enabled = true;
                    buttonRemove.Enabled = true;
                    buttonEndProject.Enabled = true;
                }
            }
        }
    }
}
