using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using SK_App.Scripts;
using DataTable = System.Data.DataTable;
using Point = System.Drawing.Point;

namespace SK_App.Forms
{
    public partial class TasksWindow : Form
    {
        private Database db;
        private DataTable dt;
        private DataTable dt2;
        private DataTable dt3;
        public int role_id;
        public int idUser;
        public int selectIDEmployee = 0;
        public int selectIDProject = 0;
        public int selectIDObject = 0;
        public int selectTask;
        public TasksWindow(int role, int id)
        {
            InitializeComponent();
            role_id = role;
            idUser = id;
            db = new Database();
            dt = new DataTable();
            dt2 = new DataTable();
            dt3 = new DataTable();
            comboBoxSelects.Items.Add("Просмотр задач по сотруднику");
            comboBoxSelects.Items.Add("Просмотр объектов по объекту и сотруднику");
            comboBoxSelects.Items.Add("Просмотр объектов по проекту и сотруднику");
            if (role != 4)
            {
                comboBoxSelects.Items.Add("Просмотр всех актуальных задач");
                comboBoxSelects.Items.Add("Просмотр задач по объекту");
                comboBoxSelects.Items.Add("Просмотр задач по проекту");
                comboBoxSelects.Items.Add("Просмотр всех задач");
                comboBoxSelects.Items.Add("Просмотр архивных данных");
                if (role == 1)
                {
                    comboBoxSelects.Items.Add("Админский просмотр данных");
                    comboBoxSelects.Items.Add("Админиский просмотр данных #2");
                }
            }
            if (role == 4)
            {
                buttonSave.Visible = false;
                buttonCreate.Visible = false;
                buttonRemove.Visible = false;
                buttonSendObject.Visible = false;
                buttonUpdateTask.Visible = false;
                buttonStartTask.Size = new Size(490, 92);
                buttonEndTask.Size = new Size(495, 92);
                buttonCloseTask.Size = new Size(490, 92);
                buttonStartTask.Location = new Point(6, 687);
                buttonEndTask.Location = new Point(500, 687);
                buttonCloseTask.Location = new Point(1000, 687);
            }
            dt = (DataTable)db.SelectProjectsIDName();
            comboBoxProject.DataSource = dt;
            comboBoxProject.DisplayMember = "name";
            comboBoxProject.ValueMember = "project_id";
            dt2 = (DataTable)db.SelectObjectsIDName();
            comboBoxObject.DataSource = dt2;
            comboBoxObject.DisplayMember = "name";
            comboBoxObject.ValueMember = "object_id";
            dt3 = (DataTable)db.SelectEmployeesIDName();
            comboBoxEmployee.DataSource = dt3;
            comboBoxEmployee.DisplayMember = "name";
            comboBoxEmployee.ValueMember = "employee_id";
            comboBoxSelectIDEmp.Visible = false;
            comboBoxSelectIDProj.Visible = false;
            comboBoxSelectObject.Visible = false;
            comboBoxSelectIDEmp.Enabled = false;
            comboBoxSelectIDProj.Enabled = false;
            comboBoxSelectObject.Enabled = false;
            buttonSave.Enabled = false;
            buttonCreate.Enabled = false;
            buttonRemove.Enabled = false;
            buttonSendObject.Enabled = false;
            buttonUpdateTask.Enabled = false;
            buttonCloseTask.Enabled = false;
            buttonStartTask.Enabled = false;
            buttonEndTask.Enabled = false;
        }

        private void ClientsWindow_Load(object sender, EventArgs e)
        {
            comboBoxEmployee.Text = "";
        }

        public void DbSelect()
        {
            db.CountHoursTask();
            db.CountTaskEmployeeCost();
            db.CountTaskPrimeCost();
            db.CountHoursEmployee();
            db.CountTaskTimeByEmployee();
            switch (Convert.ToString(comboBoxSelects.SelectedIndex))
            {
                case "0":
                    comboBoxSelectObject.Visible = false;
                    comboBoxSelectIDEmp.Visible = false;
                    comboBoxSelectIDProj.Visible = false;
                    comboBoxSelectObject.Enabled = false;
                    comboBoxSelectIDEmp.Enabled = false;
                    comboBoxSelectIDProj.Enabled = false;
                    dataGridView1.DataSource = null;
                    if (role_id == 4)
                    {
                        dataGridView1.DataSource = db.SelectTasksByEmployeeId(idUser);
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
                case "1":
                    if (role_id == 4)
                    {
                        comboBoxSelectIDEmp.Visible = false;
                        comboBoxSelectIDEmp.Enabled = false;
                    }
                    else
                    {
                        comboBoxSelectIDEmp.Visible = true;
                        comboBoxSelectIDEmp.Enabled = true;
                    }
                    comboBoxSelectObject.Visible = true;
                    comboBoxSelectIDProj.Visible = false;
                    comboBoxSelectObject.Enabled = true;
                    comboBoxSelectIDProj.Enabled = false;
                    dataGridView1.DataSource = null;
                    dt2 = (DataTable)db.SelectEmployeesIDName();
                    comboBoxSelectIDEmp.DataSource = dt2;
                    comboBoxSelectIDEmp.DisplayMember = "name";
                    comboBoxSelectIDEmp.ValueMember = "employee_id";
                    comboBoxSelectIDEmp.Location = new Point(18, 94);
                    dt = (DataTable)db.SelectObjectsIDName();
                    comboBoxSelectObject.DataSource = dt;
                    comboBoxSelectObject.DisplayMember = "name";
                    comboBoxSelectObject.ValueMember = "object_id";
                    break;
                case "2":
                    if (role_id == 4)
                    {
                        comboBoxSelectIDEmp.Visible = false;
                        comboBoxSelectIDEmp.Enabled = false;
                    }
                    else
                    {
                        comboBoxSelectIDEmp.Visible = true;
                        comboBoxSelectIDEmp.Enabled = true;
                    }
                    comboBoxSelectObject.Visible = false;
                    comboBoxSelectIDProj.Visible = true;
                    comboBoxSelectObject.Enabled = false;
                    comboBoxSelectIDProj.Enabled = true;
                    dataGridView1.DataSource = null;
                    dt2 = (DataTable)db.SelectEmployeesIDName();
                    comboBoxSelectIDEmp.DataSource = dt2;
                    comboBoxSelectIDEmp.DisplayMember = "name";
                    comboBoxSelectIDEmp.ValueMember = "employee_id";
                    comboBoxSelectIDEmp.Location = new Point(18, 94);
                    dt = (DataTable)db.SelectProjectsIDName();
                    comboBoxSelectIDProj.DataSource = dt;
                    comboBoxSelectIDProj.DisplayMember = "name";
                    comboBoxSelectIDProj.ValueMember = "project_id";
                    break;
                case "3":
                    comboBoxSelectObject.Visible = false;
                    comboBoxSelectIDEmp.Visible = false;
                    comboBoxSelectIDProj.Visible = false;
                    comboBoxSelectObject.Enabled = false;
                    comboBoxSelectIDEmp.Enabled = false;
                    comboBoxSelectIDProj.Enabled = false;
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = db.SelectActualTasks();
                    break;
                case "4":
                    comboBoxSelectObject.Visible = true;
                    comboBoxSelectIDEmp.Visible = false;
                    comboBoxSelectIDProj.Visible = false;
                    comboBoxSelectObject.Enabled = true;
                    comboBoxSelectIDEmp.Enabled = false;
                    comboBoxSelectIDProj.Enabled = false;
                    dataGridView1.DataSource = null;
                    dt = (DataTable)db.SelectObjectsIDName();
                    comboBoxSelectObject.DataSource = dt;
                    comboBoxSelectObject.DisplayMember = "name";
                    comboBoxSelectObject.ValueMember = "object_id";
                    break;
                case "5":
                    comboBoxSelectObject.Visible = false;
                    comboBoxSelectIDEmp.Visible = false;
                    comboBoxSelectIDProj.Visible = true;
                    comboBoxSelectObject.Enabled = false;
                    comboBoxSelectIDEmp.Enabled = false;
                    comboBoxSelectIDProj.Enabled = true;
                    dt2 = (DataTable)db.SelectProjectsIDName();
                    comboBoxSelectIDProj.DataSource = dt2;
                    comboBoxSelectIDProj.DisplayMember = "name";
                    comboBoxSelectIDProj.ValueMember = "project_id";
                    dataGridView1.DataSource = null;
                    break;
                case "6":
                    comboBoxSelectObject.Visible = false;
                    comboBoxSelectIDEmp.Visible = false;
                    comboBoxSelectIDProj.Visible = false;
                    comboBoxSelectObject.Enabled = false;
                    comboBoxSelectIDEmp.Enabled = false;
                    comboBoxSelectIDProj.Enabled = false;
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = db.SelectAllTasks();
                    break;
                case "7":
                    comboBoxSelectObject.Visible = false;
                    comboBoxSelectIDEmp.Visible = false;
                    comboBoxSelectIDProj.Visible = false;
                    comboBoxSelectObject.Enabled = false;
                    comboBoxSelectIDEmp.Enabled = false;
                    comboBoxSelectIDProj.Enabled = false;
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = db.SelectArchiveTasksData();
                    break;
                case "8":
                    comboBoxSelectObject.Visible = false;
                    comboBoxSelectIDEmp.Visible = false;
                    comboBoxSelectIDProj.Visible = false;
                    comboBoxSelectObject.Enabled = false;
                    comboBoxSelectIDEmp.Enabled = false;
                    comboBoxSelectIDProj.Enabled = false;
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = db.SelectALLforAdmin("tasks");
                    break;
                case "9":
                    comboBoxSelectObject.Visible = false;
                    comboBoxSelectIDEmp.Visible = false;
                    comboBoxSelectIDProj.Visible = false;
                    comboBoxSelectObject.Enabled = false;
                    comboBoxSelectIDEmp.Enabled = false;
                    comboBoxSelectIDProj.Enabled = false;
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = db.SelectALLforAdmin("tasks");
                    break;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DbSelect();
            buttonSave.Enabled = true;
        }

        private void CheckSelectIndexChanged()
        {
            dataGridView1.DataSource = null;
            if (comboBoxSelectIDEmp.SelectedIndex > 0)
            {
                selectIDEmployee = (int)comboBoxSelectIDEmp.SelectedValue;
                if (comboBoxSelectObject.Enabled == true)
                {
                    if (comboBoxSelectObject.SelectedIndex > 0)
                        selectIDObject = (int)comboBoxSelectObject.SelectedValue;
                    else selectIDObject = 1;
                    dataGridView1.DataSource = db.SelectEmployeeTasksByObjectId(selectIDEmployee, selectIDObject);
                }
                else if (comboBoxSelectIDProj.Enabled == true)
                {
                    if (comboBoxSelectIDProj.SelectedIndex > 0)
                        selectIDProject = (int)comboBoxSelectIDProj.SelectedValue;
                    else selectIDProject = 1;
                    selectIDProject = (int)comboBoxSelectIDProj.SelectedValue;
                    dataGridView1.DataSource = db.SelectEmployeeTasksByProjectId(selectIDEmployee, selectIDProject);
                }
                else dataGridView1.DataSource = db.SelectTasksByEmployeeId(selectIDEmployee);
            }
            else if (comboBoxSelectIDEmp.SelectedIndex == 0)
            {
                selectIDEmployee = 1;
                if (comboBoxSelectObject.Enabled == true)
                {
                    if (comboBoxSelectObject.SelectedIndex > 0)
                        selectIDObject = (int)comboBoxSelectObject.SelectedValue;
                    else selectIDObject = 1;
                    dataGridView1.DataSource = db.SelectEmployeeTasksByObjectId(selectIDEmployee, selectIDObject);
                }
                else if (comboBoxSelectIDProj.Enabled == true)
                {
                    if (comboBoxSelectIDProj.SelectedIndex > 0)
                        selectIDProject = (int)comboBoxSelectIDProj.SelectedValue;
                    else selectIDProject = 1;
                    dataGridView1.DataSource = db.SelectEmployeeTasksByProjectId(selectIDEmployee, selectIDProject);
                }
                else dataGridView1.DataSource = db.SelectTasksByEmployeeId(selectIDEmployee);
            }
            else if (comboBoxSelectObject.SelectedIndex > 0)
            {
                selectIDObject = (int)comboBoxSelectObject.SelectedValue;
                if (comboBoxSelectIDEmp.Enabled == true)
                {
                    if (comboBoxSelectIDEmp.SelectedIndex > 0)
                        selectIDEmployee = (int)comboBoxSelectIDEmp.SelectedValue;
                    else selectIDEmployee = 1;
                    dataGridView1.DataSource = db.SelectEmployeeTasksByObjectId(selectIDEmployee, selectIDObject);
                }
                else dataGridView1.DataSource = db.SelectTasksByObjectId(selectIDObject);
            }
            else if (comboBoxSelectObject.SelectedIndex == 0)
            {
                selectIDObject = 1;
                if (comboBoxSelectIDEmp.Enabled == true)
                {
                    if (comboBoxSelectIDEmp.SelectedIndex > 0)
                        selectIDEmployee = (int)comboBoxSelectIDEmp.SelectedValue;
                    else selectIDEmployee = 1;
                    dataGridView1.DataSource = db.SelectEmployeeTasksByObjectId(selectIDEmployee, selectIDObject);
                }
                else dataGridView1.DataSource = db.SelectTasksByObjectId(selectIDObject);
            }
            else if (comboBoxSelectIDProj.SelectedIndex > 0)
            {
                selectIDProject = (int)comboBoxSelectIDProj.SelectedValue;
                if (comboBoxSelectIDEmp.Enabled == true)
                {
                    if (comboBoxSelectIDEmp.SelectedIndex > 0)
                        selectIDEmployee = (int)comboBoxSelectIDEmp.SelectedValue;
                    else selectIDEmployee = 1;
                    dataGridView1.DataSource = db.SelectEmployeeTasksByProjectId(selectIDEmployee, selectIDProject);
                }
                else dataGridView1.DataSource = db.SelectTasksByProjectId(selectIDObject);
            }
            else if (comboBoxSelectIDProj.SelectedIndex == 0)
            {
                selectIDProject = 1;
                if(comboBoxSelectIDEmp.Enabled == true)
                {
                    if (comboBoxSelectIDEmp.SelectedIndex > 0)
                        selectIDEmployee = (int)comboBoxSelectIDEmp.SelectedValue;
                    else selectIDEmployee = 1;
                    dataGridView1.DataSource = db.SelectEmployeeTasksByProjectId(selectIDEmployee, selectIDProject);
                }
                else dataGridView1.DataSource = db.SelectTasksByProjectId(selectIDObject);
            }
        }

        private void comboBoxSelectProjID_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckSelectIndexChanged();
        }

        private void comboBoxSelectObject_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckSelectIndexChanged();
        }

        private void comboBoxSelectIDEmp_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckSelectIndexChanged();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && comboBoxSelects.SelectedIndex != 5)
            {
                selectTask = (int)dataGridView1.Rows[e.RowIndex].Cells[0].Value;
                textBoxTask.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                comboBoxEmployee.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                comboBoxProject.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                comboBoxObject.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            }

            buttonStartTask.Enabled = true;
            buttonEndTask.Enabled = true;
            buttonCloseTask.Enabled = true;
            buttonRemove.Enabled = true;
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (comboBoxSelects.SelectedIndex < 8)
            {
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Название задачи";
                dataGridView1.Columns[2].HeaderText = "Исполнитель задачи";
                dataGridView1.Columns[3].HeaderText = "Проект";
                dataGridView1.Columns[4].HeaderText = "Объект";
                dataGridView1.Columns[5].HeaderText = "Дата начала";
                dataGridView1.Columns[6].HeaderText = "Дата окончания";
                dataGridView1.Columns[7].HeaderText = "Рабочие часы";
                dataGridView1.Columns[8].HeaderText = "Себестоимость";
            }
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            db.CreateTask(textBoxTask.Text, DateTime.Now.ToString(), Convert.ToInt32(comboBoxProject.ValueMember));
            DbSelect();
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            db.DeleteTasks(selectTask);
            DbSelect();
        }

        private void buttonUpdateTask_Click(object sender, EventArgs e)
        {
            db.UpdateTask(textBoxTask.Text, selectTask);
            DbSelect();
        }

        private void buttonCloseTask_Click(object sender, EventArgs e)
        {
            db.EndTask(DateTime.Now.ToString(), selectTask);
            DbSelect();
        }

        private void buttonStartTask_Click(object sender, EventArgs e)
        {
            db.StartTaskByEmployeeID(idUser, selectTask, DateTime.Now.ToString());
            DbSelect();
        }

        private void buttonEndTask_Click(object sender, EventArgs e)
        {
            db.StopTaskByEmployeeID(idUser, selectTask, DateTime.Now.ToString());
            DbSelect();
        }

        private void buttonSendObject_Click(object sender, EventArgs e)
        {
            db.UpdateTaskObject((int)comboBoxObject.SelectedValue, selectTask);
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

        private void textBoxTask_TextChanged(object sender, EventArgs e)
        {
            if (textBoxTask.Text != "" && comboBoxProject.Text != "")
            {
                buttonCreate.Enabled = true;
                buttonUpdateTask.Enabled = true;
            }
        }

        private void comboBoxProject_TextChanged(object sender, EventArgs e)
        {
            if (textBoxTask.Text != "" && comboBoxProject.Text != "")
            {
                buttonCreate.Enabled = true;
                buttonUpdateTask.Enabled = true;
            }
        }

        private void comboBoxObject_TextChanged(object sender, EventArgs e)
        {
            if (selectTask != 0 && comboBoxObject.Text != "")
            {
                buttonSendObject.Enabled = true;
                buttonUpdateTask.Enabled = true;
            }
        }
    }
}
