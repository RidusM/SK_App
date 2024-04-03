using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar.Controls;
using Microsoft.Office.Interop.Excel;
using SK_App.Scripts;
using DataTable = System.Data.DataTable;
using Point = System.Drawing.Point;

namespace SK_App.Forms
{
    public partial class MaterialsWindow : Form
    {
        private Database db;
        private DataTable dt;
        public int role_id;
        public int idUser;
        public int selectIDObject = 0;
        public int selectObject = 0;
        public MaterialsWindow(int role, int id)
        {
            InitializeComponent();
            role_id = role;
            idUser = id;
            db = new Database();
            dt = new DataTable();
            comboBoxSelects.Items.Add("Просмотр всех материалов");
            comboBoxSelects.Items.Add("Просмотр материалов необходимых на объектах");
            if (role !=4)
            {
                comboBoxSelects.Items.Add("Просмотр материалов по объекту для сотрудников высшего звена");
                if (role <= 3)
                {
                    comboBoxSelects.Items.Add("Данные по материалам для бухгалтерии");
                }

                if (role == 1)
                {
                    comboBoxSelects.Items.Add("Админский просмотр данных №1");
                    comboBoxSelects.Items.Add("Админский просмотр данных №2");
                }
            }
            if (role == 4)
            {
                button1.Visible = true;
                buttonUpdate.Visible = true;
                button1.Size = new Size(745, 92);
                buttonUpdate.Size = new Size(745, 92);
                buttonUpdate.Location = new Point(6, 595);
                button1.Location = new Point(751, 595);
            }
            if (role <= 3)
            {
                button1.Visible = true;
                buttonCreate.Visible = true;
                buttonRemove.Visible = true;
                buttonUpdate.Visible = true;
                buttonSave.Visible = true;
            }
            buttonUpdate.Text = "Обновить кол-во материала";
            dt = (DataTable)db.SelectMaterialsIDName();
            comboBoxNameMaterial.DataSource = dt;
            comboBoxNameMaterial.DisplayMember = "name";
            comboBoxNameMaterial.ValueMember = "material_id";
            comboBoxSelectIDObject.Visible = false;
            comboBoxSelectIDObject.Enabled = false;
            buttonCreate.Enabled = false;
            buttonRemove.Enabled = false;
            buttonUpdate.Enabled = false;
            button1.Enabled = false;
            buttonSave.Enabled = false;
        }

        private void ClientsWindow_Load(object sender, EventArgs e)
        {
            comboBoxNameMaterial.Text = "";
        }

        public void DbSelect()
        {
            db.CountTotalPriceObjectMaterials();
            db.CountNeedMaterials();
            switch (Convert.ToString(comboBoxSelects.SelectedIndex))
            {
                case "0":
                    comboBoxSelectIDObject.Visible = false;
                    comboBoxSelectIDObject.Enabled = false;
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = db.SelectAllMaterials();
                    break;
                case "1":
                    comboBoxSelectIDObject.Visible = false;
                    comboBoxSelectIDObject.Enabled = false;
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = db.SelectAllMaterialsByEmployee(idUser);
                    break;
                case "2":
                    dataGridView1.DataSource = null;
                    comboBoxSelectIDObject.Visible = true;
                    comboBoxSelectIDObject.Enabled = true;
                    dt = (DataTable)db.SelectObjectsIDName();
                    comboBoxSelectIDObject.DataSource = dt;
                    comboBoxSelectIDObject.DisplayMember = "name";
                    comboBoxSelectIDObject.ValueMember = "object_id";
                    break;
                case "3":
                    buttonUpdate.Text = "Обновить цену за ед.";
                    comboBoxSelectIDObject.Visible = false;
                    comboBoxSelectIDObject.Enabled = false;
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = db.BkSelectAllMaterials();
                    break;
                case "4":
                    comboBoxSelectIDObject.Visible = false;
                    comboBoxSelectIDObject.Enabled = false;
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = db.SelectALLforAdmin("materials");
                    break;
                case "5":
                    comboBoxSelectIDObject.Visible = false;
                    comboBoxSelectIDObject.Enabled = false;
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = db.SelectALLforAdmin("objects_materials");
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
            if (comboBoxSelectIDObject.SelectedIndex > 0)
            {
                selectIDObject = (int)comboBoxSelectIDObject.SelectedValue;
                dataGridView1.DataSource = db.SelectAllMaterialsByObjectId(selectIDObject);
            }
            else
            {
                selectIDObject = 1;
                dataGridView1.DataSource = db.SelectAllMaterialsByObjectId(selectIDObject);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && comboBoxSelects.SelectedIndex != 4 && comboBoxSelects.SelectedIndex != 0 && comboBoxSelects.SelectedIndex != 3)
            {
                selectObject = (int)dataGridView1.Rows[e.RowIndex].Cells[0].Value;
                comboBoxNameMaterial.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBoxMaterials.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                if (comboBoxSelects.SelectedIndex == 3) textBoxUnit.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            }

            if (e.RowIndex >= 0 && comboBoxSelects.SelectedIndex == 0)
            {
                comboBoxNameMaterial.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                textBoxMaterials.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            }

            if (e.RowIndex >= 0 && comboBoxSelects.SelectedIndex == 3)
            {
                comboBoxNameMaterial.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                textBoxMaterials.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                if (comboBoxSelects.SelectedIndex == 3) textBoxUnit.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            }
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (comboBoxSelects.SelectedIndex == 0)
            {
                dataGridView1.Columns[0].HeaderText = "Название материала";
                dataGridView1.Columns[1].HeaderText = "Общее количество материалов";
            }
            else if (comboBoxSelects.SelectedIndex == 1 || comboBoxSelects.SelectedIndex == 2)
            {
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Название материала";
                dataGridView1.Columns[2].HeaderText = "Количество материалов";
                dataGridView1.Columns[3].HeaderText = "Название объекта";
                dataGridView1.Columns[4].HeaderText = "Название проекта";
                dataGridView1.Columns[5].HeaderText = "Адресс";
            }
            else if (comboBoxSelects.SelectedIndex == 3)
            {
                dataGridView1.Columns[0].HeaderText = "Название материала";
                dataGridView1.Columns[1].HeaderText = "Количество материалов";
                dataGridView1.Columns[2].HeaderText = "Цена за ед.";
                dataGridView1.Columns[3].HeaderText = "Итоговая цена";
            }
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            db.CreateMaterials(comboBoxNameMaterial.Text, Convert.ToInt32(textBoxMaterials.Text),
                Convert.ToDecimal(textBoxUnit.Text));
            DbSelect();
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (comboBoxSelects.SelectedIndex != 3) db.DeleteMaterials((int)comboBoxNameMaterial.SelectedValue);
            else db.DeleteObjectsMaterials((int)comboBoxNameMaterial.SelectedValue, selectObject);
            DbSelect();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (comboBoxSelects.SelectedIndex == 3)
            {
                db.UpdateMaterialPrice((int)comboBoxNameMaterial.SelectedValue, textBoxUnit.Text);
                DbSelect();
            }
            else if (comboBoxSelects.SelectedIndex >= 1 && comboBoxSelects.SelectedIndex < 4)
            {
                db.UpdateMaterialObjects(Convert.ToDecimal(textBoxMaterials.Text),
                    (int)comboBoxNameMaterial.SelectedValue, selectObject);
                DbSelect();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            db.CreateObjectsMaterials(selectObject, (int)comboBoxNameMaterial.SelectedValue);
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

        private void comboBoxNameMaterial_TextChanged(object sender, EventArgs e)
        {
            if (comboBoxNameMaterial.Text != "")
            {
                buttonRemove.Enabled = true;
                button1.Enabled = true;
            }
        }

        private void textBoxMaterials_TextChanged(object sender, EventArgs e)
        {
            if (textBoxMaterials.Text != "" && comboBoxNameMaterial.Text != "" && comboBoxSelects.SelectedIndex >=1) 
                buttonUpdate.Enabled = true;
        }

        private void textBoxUnit_TextChanged(object sender, EventArgs e)
        {
            if (comboBoxNameMaterial.Text != "" && textBoxUnit.Text != "") buttonCreate.Enabled = true;
        }
    }
}
