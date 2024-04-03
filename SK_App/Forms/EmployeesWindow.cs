using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using SK_App.Scripts;
using DataTable = System.Data.DataTable;

namespace SK_App.Forms
{
    public partial class Сотрудники : Form
    {
        private Database db;
        private DataTable dt;
        private DataTable dt2;
        public int role_id;
        public int idUser;
        public int selectIDEmployee = 0;
        public int selectIDRole = 0;
        public Сотрудники(int role, int id)
        {
            InitializeComponent();
            role_id = role;
            idUser = id;
            db = new Database();
            dt = new DataTable();
            dt2 = new DataTable();
            comboBoxSelects.Items.Add("Просмотр всех сотрудников");
            comboBoxSelects.Items.Add("Просмотр информации по сотруднику");
            comboBoxSelects.Items.Add("Просмотр информации по роли сотрудника");
            if (role <= 2)
            {
                comboBoxSelects.Items.Add("Кадровый просмотр всех сотрудников");
                comboBoxSelects.Items.Add("Кадровый просмотр информации по сотруднику");
            }
            if (role == 1)
            {
                comboBoxSelects.Items.Add("Админский просмотр данных");
            }
            if (role <= 2)
            {
                buttonCreate.Visible = true;
                buttonRemove.Visible = true;
                buttonUpdateContact.Visible = true;
                buttonUpdateRate.Visible = true;
                buttonRole.Visible = true;
                buttonSave.Visible = true;
            }
            dt2 = (DataTable)db.SelectEmployeeRolesIDName();
            comboBoxRole.DataSource = dt2;
            comboBoxRole.DisplayMember = "name";
            comboBoxRole.ValueMember = "employee_role_id";
            dt = (DataTable)db.SelectEmployeesIDName();
            comboBoxStaff.DataSource = dt;
            comboBoxStaff.DisplayMember = "name";
            comboBoxStaff.ValueMember = "employee_id";
            comboBoxSelectIDEmp.Visible = false;
            comboBoxSelectIDEmp.Enabled = false;
            comboBoxSelectRole.Visible = false;
            comboBoxSelectIDEmp.Enabled = false;
            textBoxLogin.Visible = false;
            textBoxPassword.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            buttonCreate.Enabled = false;
            buttonRemove.Enabled = false;
            buttonUpdateContact.Enabled = false;
            buttonUpdateRate.Enabled = false;
            buttonRole.Enabled = false;
            buttonSave.Enabled = false;
        }

        private void ClientsWindow_Load(object sender, EventArgs e)
        {
            comboBoxStaff.Text = "";
            comboBoxRole.Text = "";
        }

        public void DbSelect()
        {
            switch (Convert.ToString(comboBoxSelects.SelectedIndex))
            {
                case "0":
                    comboBoxSelectIDEmp.Visible = false;
                    comboBoxSelectIDEmp.Enabled = false;
                    comboBoxSelectRole.Visible = false;
                    comboBoxSelectRole.Enabled = false;
                    label5.Visible = false;
                    label6.Visible = false;
                    textBoxLogin.Visible = false;
                    textBoxPassword.Visible = false;
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = db.SelectAllEmployees();
                    break;
                case "1":
                    dataGridView1.DataSource = null;
                    comboBoxSelectIDEmp.Visible = true;
                    comboBoxSelectIDEmp.Enabled = true;
                    comboBoxSelectRole.Visible = false;
                    comboBoxSelectRole.Enabled = false;
                    label5.Visible = false;
                    label6.Visible = false;
                    textBoxLogin.Visible = false;
                    textBoxPassword.Visible = false;
                    dt = (DataTable)db.SelectEmployeesIDName();
                    comboBoxSelectIDEmp.DataSource = dt;
                    comboBoxSelectIDEmp.DisplayMember = "name";
                    comboBoxSelectIDEmp.ValueMember = "employee_id";
                    break;
                case "2":
                    dataGridView1.DataSource = null;
                    comboBoxSelectIDEmp.Visible = false;
                    comboBoxSelectIDEmp.Enabled = false;
                    comboBoxSelectRole.Visible = true;
                    comboBoxSelectRole.Enabled = true;
                    label5.Visible = false;
                    label6.Visible = false;
                    textBoxLogin.Visible = false;
                    textBoxPassword.Visible = false;
                    dt = (DataTable)db.SelectEmployeeRolesIDName();
                    comboBoxSelectRole.DataSource = dt;
                    comboBoxSelectRole.DisplayMember = "name";
                    comboBoxSelectRole.ValueMember = "employee_role_id";
                    break;
                case "3":
                    comboBoxSelectIDEmp.Visible = false;
                    comboBoxSelectIDEmp.Enabled = false;
                    comboBoxSelectRole.Visible = false;
                    comboBoxSelectRole.Enabled = false;
                    label5.Visible = true;
                    label6.Visible = true;
                    textBoxLogin.Visible = true;
                    textBoxPassword.Visible = true;
                    dt = (DataTable)db.BkSelectEmployee();
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = dt;
                    break;
                case "4":
                    comboBoxSelectIDEmp.Visible = true;
                    comboBoxSelectIDEmp.Enabled = true;
                    comboBoxSelectRole.Visible = false;
                    comboBoxSelectRole.Enabled = false;
                    label5.Visible = true;
                    label6.Visible = true;
                    textBoxLogin.Visible = true;
                    textBoxPassword.Visible = true;
                    dt = (DataTable)db.SelectEmployeesIDName();
                    comboBoxSelectIDEmp.DataSource = dt;
                    comboBoxSelectIDEmp.DisplayMember = "name";
                    comboBoxSelectIDEmp.ValueMember = "employee_id";
                    break;
                case "5":
                    comboBoxSelectIDEmp.Visible = false;
                    comboBoxSelectIDEmp.Enabled = false;
                    comboBoxSelectRole.Visible = false;
                    comboBoxSelectRole.Enabled = false;
                    label5.Visible = true;
                    label6.Visible = true;
                    textBoxLogin.Visible = true;
                    textBoxPassword.Visible = true;
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = db.SelectALLforAdmin("employees");
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
                if (comboBoxSelects.SelectedIndex == 4)
                {
                    dataGridView1.DataSource = db.BkSelectEmployeeByID(selectIDEmployee);
                }
                else
                {
                    dataGridView1.DataSource = db.SelectEmployeeById(selectIDEmployee);
                }
            }
            else
            {
                selectIDEmployee = 1;
                if (comboBoxSelects.SelectedIndex == 4)
                {
                    dataGridView1.DataSource = db.BkSelectEmployeeByID(selectIDEmployee);
                }
                else
                {
                    dataGridView1.DataSource = db.SelectEmployeeById(selectIDEmployee);
                }
            }
        }

        private void comboBoxSelectRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            if (comboBoxSelectRole.SelectedIndex > 0)
            {
                selectIDRole = (int)comboBoxSelectRole.SelectedValue;
                dataGridView1.DataSource = db.SelectEmployeeByRoleID(selectIDRole);
            }
            else
            {
                selectIDRole = 1;
                dataGridView1.DataSource = db.SelectEmployeeByRoleID(selectIDRole);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && comboBoxSelects.SelectedIndex != 5)
            {
                comboBoxStaff.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                comboBoxRole.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBoxPhoneNumber.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                textBoxRate.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                if (comboBoxSelects.SelectedIndex == 3 || comboBoxSelects.SelectedIndex == 4)
                {
                    textBoxLogin.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                    textBoxPassword.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                }
            }
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (comboBoxSelects.SelectedIndex != 4)
            {
                dataGridView1.Columns[0].HeaderText = "Сотрудник";
                dataGridView1.Columns[1].HeaderText = "Роль";
                dataGridView1.Columns[2].HeaderText = "Номер телефона";
                dataGridView1.Columns[3].HeaderText = "Ставка руб./ч";
                dataGridView1.Columns[4].HeaderText = "Рабочие часы в месяце";
                dataGridView1.Columns[5].HeaderText = "Зарплата/месяц";

            }

            if (comboBoxSelects.SelectedIndex == 3 || comboBoxSelects.SelectedIndex == 4)
            {
                dataGridView1.Columns[0].HeaderText = "Сотрудник";
                dataGridView1.Columns[1].HeaderText = "Роль";
                dataGridView1.Columns[2].HeaderText = "Номер телефона";
                dataGridView1.Columns[3].HeaderText = "Ставка руб./ч";
                dataGridView1.Columns[4].HeaderText = "Рабочие часы в месяце";
                dataGridView1.Columns[5].HeaderText = "Зарплата/месяц";
                dataGridView1.Columns[6].HeaderText = "Логин";
                dataGridView1.Columns[7].HeaderText = "Пароль";
            }
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            db.CreateEmployee(comboBoxStaff.Text, textBoxPhoneNumber.Text, Convert.ToDecimal(textBoxRate.Text),
                (int)comboBoxRole.SelectedValue, textBoxLogin.Text, textBoxPassword.Text);
            DbSelect();
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            db.DeleteEmployee((int)comboBoxStaff.SelectedValue);
            DbSelect();
        }

        private void buttonUpdateContact_Click(object sender, EventArgs e)
        {
            db.UpdateEmployeeContactInfo(comboBoxStaff.Text, textBoxPhoneNumber.Text, (int)comboBoxStaff.SelectedValue);
            DbSelect();
        }

        private void buttonUpdateRate_Click(object sender, EventArgs e)
        {
            db.UpdateEmployeeRate(Convert.ToDecimal(textBoxRate.Text), (int)comboBoxStaff.SelectedValue);
            DbSelect();
        }

        private void buttonRole_Click(object sender, EventArgs e)
        {
            db.UpdateEmployeeRole((int)comboBoxRole.SelectedValue, (int)comboBoxStaff.SelectedValue);
            DbSelect();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application XlApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook XlWorkBook = XlApp.Workbooks.Add(@"C:\Users\storm\OneDrive\Документы\SK_APP\ExcelFile.xlsx"); //создать новый файл: XlApp.Workbooks.Add();
            Microsoft.Office.Interop.Excel.Worksheet XlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)XlWorkBook.Worksheets.get_Item(1); //1-й лист по порядку
            Microsoft.Office.Interop.Excel.Range formatRangeText;
            Microsoft.Office.Interop.Excel.Range formatRangeRuble1;
            Microsoft.Office.Interop.Excel.Range formatRangeRuble2;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    XlWorkSheet.Cells[1, j + 1] = dataGridView1.Columns[j].HeaderText;
                    XlWorkSheet.Cells[i + 1, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                }
            }
            formatRangeText = XlWorkSheet.get_Range("A2", "C1000");
            formatRangeText.NumberFormat = "@";
            formatRangeRuble1 = XlWorkSheet.get_Range("D2", "D1000");
            formatRangeRuble1.NumberFormat = "# ##0,000 ₽";
            formatRangeRuble2 = XlWorkSheet.get_Range("F2", "F1000");
            formatRangeRuble2.NumberFormat = "# ##0,000 ₽";
            XlWorkSheet.Columns.EntireColumn.AutoFit();
            XlApp.Visible = true;
            XlApp.UserControl = true;
        }

        private void comboBoxStaff_TextChanged(object sender, EventArgs e)
        {
            if (comboBoxStaff.Text != "" && textBoxPhoneNumber.Text != ""
                                         && textBoxRate.Text != "" && textBoxLogin.Text != "" &&
                                         textBoxPassword.Text != "") buttonCreate.Enabled = true;
            if (comboBoxStaff.SelectedIndex != 0) buttonRemove.Enabled = true;
            if (comboBoxRole.SelectedIndex != 0 && comboBoxStaff.SelectedIndex != 0) buttonRole.Enabled = true;
            if (comboBoxStaff.Text != "" && textBoxPhoneNumber.Text != "" && textBoxPhoneNumber.Text != "")
                buttonUpdateContact.Enabled = true;

        }

        private void textBoxRate_TextChanged(object sender, EventArgs e)
        {
            if (comboBoxStaff.Text != "" && textBoxPhoneNumber.Text != ""
                                         && textBoxRate.Text != "" && textBoxLogin.Text != "" &&
                                         textBoxPassword.Text != "" && selectIDRole != 0) buttonCreate.Enabled = true;
            if (comboBoxStaff.SelectedIndex != 0) buttonUpdateRate.Enabled = true;
        }

        private void textBoxPhoneNumber_TextChanged(object sender, EventArgs e)
        {
            if (comboBoxStaff.Text != "" && textBoxPhoneNumber.Text != ""
                                         && textBoxRate.Text != "" && textBoxLogin.Text != "" &&
                                         textBoxPassword.Text != "" && selectIDRole != 0) buttonCreate.Enabled = true;
            if (comboBoxStaff.Text != "" && textBoxPhoneNumber.Text != "" && textBoxPhoneNumber.Text != "")
                buttonUpdateContact.Enabled = true;
        }

        private void comboBoxRole_TextChanged(object sender, EventArgs e)
        {
            if (comboBoxStaff.Text != "" && textBoxPhoneNumber.Text != ""
                                         && textBoxRate.Text != "" && textBoxLogin.Text != "" &&
                                         textBoxPassword.Text != "" && selectIDRole != 0) buttonCreate.Enabled = true;
            if (comboBoxRole.SelectedIndex != 0 && comboBoxStaff.SelectedIndex != 0) buttonRole.Enabled = true;
        }

        private void textBoxLogin_TextChanged(object sender, EventArgs e)
        {
            if (comboBoxStaff.Text != "" && textBoxPhoneNumber.Text != ""
                                         && textBoxRate.Text != "" && textBoxLogin.Text != "" &&
                                         textBoxPassword.Text != "" && selectIDRole != 0) buttonCreate.Enabled = true;
        }

        private void textBoxPassword_TextChanged(object sender, EventArgs e)
        {
            if (comboBoxStaff.Text != "" && textBoxPhoneNumber.Text != ""
                                         && textBoxRate.Text != "" && textBoxLogin.Text != "" &&
                                         textBoxPassword.Text != "" && selectIDRole != 0) buttonCreate.Enabled = true;
        }
    }
}
