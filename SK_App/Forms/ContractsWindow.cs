using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using SK_App.Scripts;
using DataTable = System.Data.DataTable;

namespace SK_App.Forms
{
    public partial class ContractsWindow : Form
    {
        private Database db;
        private DataTable dt;
        private DataTable dt2;
        public int role_id;
        public int idUser;
        public int selectContract;
        public int selectIDClient = 0;
        public ContractsWindow(int role, int id)
        {
            InitializeComponent();
            role_id = role;
            idUser = id;
            db = new Database();
            dt = new DataTable();
            dt2 = new DataTable();
            comboBoxSelects.Items.Add("Просмотр всех актуальных договоров");
            comboBoxSelects.Items.Add("Просмотр контрактов по Клиенту");
            comboBoxSelects.Items.Add("Просмотр архивных данных");
            comboBoxSelects.Items.Add("Просмотр всех договоров");
            if (role == 1)
            {
                comboBoxSelects.Items.Add("Админский просмотр данных");
            }
            if (role <= 2)
            {
                buttonCreate.Visible = true;
                buttonRemove.Visible = true;
                buttonUpdatePayment.Visible = true;
                buttonDizEnterProject.Visible = true;
                buttonEnterProject.Visible = true;
                buttonSave.Visible = true;
            }
            dt2 = (DataTable)db.SelectProjectsIDName();
            dt = (DataTable)db.SelectClientsIDName();
            comboBoxProject.DataSource = dt2;
            comboBoxProject.DisplayMember = "name";
            comboBoxProject.ValueMember = "project_id";
            comboBoxClient.DataSource = dt;
            comboBoxClient.DisplayMember = "name";
            comboBoxClient.ValueMember = "client_id";
            comboBoxSelectIDClient.Visible = false;
            comboBoxSelectIDClient.Enabled = false;
            buttonCreate.Enabled = false;
            buttonRemove.Enabled = false;
            buttonUpdatePayment.Enabled = false;
            buttonDizEnterProject.Enabled = false;
            buttonEnterProject.Enabled = false;
            buttonCreate.Enabled = false;
        }

        private void ClientsWindow_Load(object sender, EventArgs e)
        {
            comboBoxSelectIDClient.Text = "";
            comboBoxSelects.SelectedIndex = 0;
        }

        public void DbSelect()
        {
            db.ContractFinalAmount();
            switch (Convert.ToString(comboBoxSelects.SelectedIndex))
            {
                case "0":
                    comboBoxSelectIDClient.Visible = false;
                    comboBoxSelectIDClient.Enabled = false;
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = db.SelectActualContracts();
                    break;
                case "1":
                    dataGridView1.DataSource = null;
                    comboBoxSelectIDClient.Visible = true;
                    comboBoxSelectIDClient.Enabled = true;
                    dt = (DataTable)db.SelectClientsIDName();
                    comboBoxSelectIDClient.DataSource = dt;
                    comboBoxSelectIDClient.DisplayMember = "name";
                    comboBoxSelectIDClient.ValueMember = "client_id";
                    break;
                case "2":
                    comboBoxSelectIDClient.Visible = false;
                    comboBoxSelectIDClient.Enabled = false;
                    dataGridView1.DataSource = null;
                    dt = (DataTable)db.SelectArchiveContractsData();
                    break;
                case "3":
                    comboBoxSelectIDClient.Visible = false;
                    comboBoxSelectIDClient.Enabled = false;
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = db.SelectAllContracts();
                    break;
                case "4":
                    comboBoxSelectIDClient.Visible = false;
                    comboBoxSelectIDClient.Enabled = false;
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = db.SelectALLforAdmin("contracts");
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
            if (comboBoxSelectIDClient.SelectedIndex > 0)
            {
                selectIDClient = (int)comboBoxSelectIDClient.SelectedValue;
                dataGridView1.DataSource = db.SelectContractsByClientId(selectIDClient);
            }
            else
            {
                selectIDClient = 1;
                dataGridView1.DataSource = db.SelectContractsByClientId(selectIDClient);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && comboBoxSelects.SelectedIndex != 5)
            {
                comboBoxClient.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                selectContract = (int)dataGridView1.Rows[e.RowIndex].Cells[0].Value;
                textBoxPayment.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                comboBoxProject.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                buttonRemove.Enabled = true;
            }
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (comboBoxSelects.SelectedIndex != 4)
            {
                dataGridView1.Columns[0].HeaderText = "Договор";
                dataGridView1.Columns[1].HeaderText = "ФИО Клиента";
                dataGridView1.Columns[2].HeaderText = "Название проекта";
                dataGridView1.Columns[3].HeaderText = "Дата заключения";
                dataGridView1.Columns[4].HeaderText = "Внесенный платеж, руб.";
                dataGridView1.Columns[5].HeaderText = "Финальная стоимость проекта, руб.";
            }
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            try
            {
                db.CreateContract((int)comboBoxClient.SelectedValue, Convert.ToDecimal(textBoxPayment.Text), DateTime.Now.ToString());
                DbSelect();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Вы не выбрали указали сотрудника", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (selectContract == 0) MessageBox.Show("Вы не выбрали ячейку для удаления, пожалуйста, нажмите на строку для удаления",
                "Ошибка",MessageBoxButtons.OK, MessageBoxIcon.Error);
                db.DeleteContracts(selectContract);
            DbSelect();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            db.UpdateContractPayment(Convert.ToDecimal(textBoxPayment.Text), selectContract);
            DbSelect();
        }

        private void buttonEnterProject_Click(object sender, EventArgs e)
        {
            db.UpdateContractEnterProject(selectContract, Convert.ToInt32(comboBoxProject.SelectedValue));
            DbSelect();
        }

        private void buttonDizEnterProject_Click(object sender, EventArgs e)
        {
            db.UpdateContractDissEnterProject(selectContract, (int)comboBoxProject.SelectedValue);
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application XlApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook XlWorkBook = XlApp.Workbooks.Add(@"C:\Users\storm\OneDrive\Документы\SK_APP\ExcelFile.xlsx"); //создать новый файл: XlApp.Workbooks.Add();
            Microsoft.Office.Interop.Excel.Worksheet XlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)XlWorkBook.Worksheets.get_Item(1); //1-й лист по порядку
            Microsoft.Office.Interop.Excel.Range formatRangeText;
            Microsoft.Office.Interop.Excel.Range formatRangeRuble;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                for (int j = 1; j < dataGridView1.ColumnCount; j++)
                {
                    XlWorkSheet.Cells[1, j + 1] = dataGridView1.Columns[j].HeaderText;
                    XlWorkSheet.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value;
                }
            }
            formatRangeText = XlWorkSheet.get_Range("B2", "C1000");
            formatRangeText.NumberFormat = "@";
            formatRangeRuble = XlWorkSheet.get_Range("E2", "F1000");
            formatRangeRuble.NumberFormat = "# ##0,000 ₽";
            XlWorkSheet.Columns.EntireColumn.AutoFit();
            XlApp.Visible = true;
            XlApp.UserControl = true;
        }

        private void comboBoxClient_TextChanged(object sender, EventArgs e)
        {
            if (comboBoxClient.Text != "" && textBoxPayment.Text != "") buttonCreate.Enabled = true;
        }

        private void textBoxPayment_TextChanged(object sender, EventArgs e)
        {
            if (buttonUpdatePayment.Text != "" && selectContract != 0) buttonUpdatePayment.Enabled = true;
        }

        private void comboBoxProject_TextChanged(object sender, EventArgs e)
        {
            if (comboBoxProject.Text != "" && selectContract != 0)
            {
                buttonEnterProject.Enabled = true;
                buttonDizEnterProject.Enabled = true;
            }
        }
    }
}
