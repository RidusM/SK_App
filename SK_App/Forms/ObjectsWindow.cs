using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using SK_App.Scripts;
using DataTable = System.Data.DataTable;

namespace SK_App.Forms
{
    public partial class ObjectsWindow : Form
    {
        private Database db;
        private DataTable dt;
        public int role_id;
        public int idUser;
        public int selectIDUser = 0;
        public int selectIDProj = 0;
        public int selectIDObject = 0;

        public ObjectsWindow(int role, int id)
        {
            InitializeComponent();
            role_id = role;
            idUser = id;
            db = new Database();
            dt = new DataTable();
            comboBoxSelects.Items.Add("Просмотр всех актуальных объектов");
            comboBoxSelects.Items.Add("Просмотр объектов по Сотруднику");
            comboBoxSelects.Items.Add("Просмотр объектов по Проекту");
            if (role != 4)
            {
                comboBoxSelects.Items.Add("Просмотр всех объектов");
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
                buttonUpdate.Visible = false;
            }
            dt = (DataTable)db.SelectProjectsIDName();
            comboBoxProj.DataSource = dt;
            comboBoxProj.DisplayMember = "name";
            comboBoxProj.ValueMember = "project_id";
            comboBoxSelectIDEmp.Visible = false;
            comboBoxSelectProjects.Visible = false;
            comboBoxSelectIDEmp.Enabled = false;
            comboBoxSelectProjects.Enabled = false;
            buttonSave.Enabled = false;
            buttonCreate.Enabled = false;
            buttonRemove.Enabled = false;
            buttonUpdate.Enabled = false;
        }

        private void ClientsWindow_Load(object sender, EventArgs e)
        {
            comboBoxProj.Text = "";
        }

        public void DbSelect()
        {
            db.CountObjectsPrimeCost();
            switch (Convert.ToString(comboBoxSelects.SelectedIndex))
            {
                case "0":
                    if (role_id == 4)
                    {
                        comboBoxSelects.SelectedIndex = 1;
                    }
                    else
                    {
                        comboBoxSelectIDEmp.Visible = false;
                        comboBoxSelectProjects.Visible = false;
                        comboBoxSelectIDEmp.Enabled = false;
                        comboBoxSelectProjects.Enabled = false;
                        dataGridView1.DataSource = null;
                        dataGridView1.DataSource = db.SelectActualObjects();
                    }

                    break;
                case "1":
                    comboBoxSelectProjects.Visible = false;
                    comboBoxSelectProjects.Enabled = false;
                    dataGridView1.DataSource = null;
                    if (role_id == 4)
                    {
                        dataGridView1.DataSource = db.SelectObjectByEmployee(idUser);
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
                    comboBoxSelectProjects.Visible = true;
                    comboBoxSelectIDEmp.Enabled = false;
                    comboBoxSelectProjects.Enabled = true;
                    dataGridView1.DataSource = null;
                    dt = (DataTable)db.SelectProjectsIDName();
                    comboBoxSelectProjects.DataSource = dt;
                    comboBoxSelectProjects.DisplayMember = "name";
                    comboBoxSelectProjects.ValueMember = "project_id";
                    break;
                case "3":
                    comboBoxSelectIDEmp.Visible = false;
                    comboBoxSelectProjects.Visible = false;
                    comboBoxSelectIDEmp.Enabled = false;
                    comboBoxSelectProjects.Enabled = false;
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = db.SelectAllObjects();
                    break;
                case "4":
                    comboBoxSelectIDEmp.Visible = false;
                    comboBoxSelectProjects.Visible = false;
                    comboBoxSelectIDEmp.Enabled = false;
                    comboBoxSelectProjects.Enabled = false;
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = db.SelectArchiveObjectsData();
                    break;
                case "5":
                    comboBoxSelectIDEmp.Visible = false;
                    comboBoxSelectProjects.Visible = false;
                    comboBoxSelectIDEmp.Enabled = false;
                    comboBoxSelectProjects.Enabled = false;
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = db.SelectALLforAdmin("objects");
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
                selectIDUser = (int)comboBoxSelectIDEmp.SelectedValue;
                dataGridView1.DataSource = db.SelectObjectByEmployee(selectIDUser);
            }
            else
            {
                selectIDUser = 1;
                dataGridView1.DataSource = db.SelectObjectByEmployee(selectIDUser);
            }
        }

        private void comboBoxSelectProjID_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            if (comboBoxSelectProjects.SelectedIndex > 0)
            {
                selectIDProj = (int)comboBoxSelectProjects.SelectedValue;
                if (role_id == 4)
                {
                    dataGridView1.DataSource = db.SelectObjectByEmployeeProject(selectIDProj, idUser);
                }
                else
                {
                    dataGridView1.DataSource = db.SelectObjectByProject(selectIDProj);
                }
            }
            else
            {
                selectIDProj = 1;
                if (role_id == 4)
                {
                    dataGridView1.DataSource = db.SelectObjectByEmployeeProject(selectIDProj, idUser);
                }
                else 
                {
                    dataGridView1.DataSource = db.SelectObjectByProject(selectIDProj);
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && comboBoxSelects.SelectedIndex != 5)
            {
                comboBoxProj.Text = dataGridView1.Rows[e.RowIndex].Cells["name"].Value.ToString();
                textBoxAdress.Text = dataGridView1.Rows[e.RowIndex].Cells["adress"].Value.ToString();
                textBoxObj.Text = dataGridView1.Rows[e.RowIndex].Cells["name1"].Value.ToString();
                selectIDObject = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                buttonRemove.Enabled = true;
            }
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (comboBoxSelects.SelectedIndex != 5)
            {
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Проект";
                dataGridView1.Columns[2].HeaderText = "Объект";
                dataGridView1.Columns[3].HeaderText = "Адрес";
                dataGridView1.Columns[4].HeaderText = "Стоимость руб.";
            }
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            db.CreateObject(textBoxObj.Text, textBoxAdress.Text, Convert.ToInt32(comboBoxProj.SelectedValue));
            DbSelect();

        }

        private void buttonRemove_Click_1(object sender, EventArgs e)
        {
            db.DeleteObjects(Convert.ToInt32(selectIDObject));
            DbSelect();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            db.UpdateObjects(textBoxObj.Text, textBoxAdress.Text, selectIDObject);
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

        private void textBoxAdress_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxObj_TextChanged(object sender, EventArgs e)
        {
            if (textBoxObj.Text != "" && textBoxAdress.Text != "")
            {
                buttonUpdate.Enabled = true;
                if (comboBoxProj.Text != "") buttonCreate.Enabled = true;
            }
        }

        private void comboBoxProj_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
