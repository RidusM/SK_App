using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace SK_App.Forms
{
    public partial class MainWindow : Form
    {
        public int userRole_id;
        public string name_;
        public int id_User;
        public MainWindow(int role, int id, string name)
        {
            InitializeComponent();
            userRole_id = role;
            name_ = name;
            id_User = id;
        }

        protected void MainWindow_Closed(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ButtonMaterials_Click(object sender, EventArgs e)
        {
            MaterialsWindow mw = new MaterialsWindow(userRole_id, id_User);
            mw.Show();
        }

        private void ButtonObjects_Click(object sender, EventArgs e)
        {
            ObjectsWindow ow = new ObjectsWindow(userRole_id, id_User);
            ow.Show();
        }

        private void ButtonProjects_Click(object sender, EventArgs e)
        {
            ProjectsWindow pw = new ProjectsWindow(userRole_id, id_User);
            pw.Show();

        }

        private void ButtonEmployees_Click(object sender, EventArgs e)
        {
            Сотрудники ew = new Сотрудники(userRole_id, id_User);
            ew.Show();
        }

        private void ButtonClients_Click(object sender, EventArgs e)
        {
            ClientsWindow cw = new ClientsWindow(userRole_id, id_User);
            cw.Show();
        }

        private void ButtonContracts_Click(object sender, EventArgs e)
        {
            ContractsWindow cw = new ContractsWindow(userRole_id, id_User);
            cw.Show();
        }

        private void ButtonMessages_Click(object sender, EventArgs e)
        {
            MessagesWindow mw = new MessagesWindow();
            mw.Show();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            labelName.Text = name_;
            string sw_userRole = Convert.ToString(userRole_id);
            switch (sw_userRole)
            {
                case "4":
                    buttonClients.Visible = false;
                    buttonContracts.Visible = false;
                    buttonEmployees.Visible = false;
                    buttonMaterials.Width = 420;
                    buttonMessages.Width = 420;
                    buttonTasks.Width = 420;
                    buttonMaterials.Location = new Point(5, 135);
                    buttonTasks.Location = new Point(5, 258);
                    buttonObjects.Text = "Мои объекты";
                    buttonProjects.Text = "Мои проекты";
                    buttonTasks.Text = "Мои задачи";
                    labelRole.Text = "Сотрудник низ.звена";
                    break;
                case "3":
                    labelRole.Text = "Сотрудник выс.звена";
                    break;
                case "2":
                    labelRole.Text = "Бухгалтер";
                    break;
                case "1":
                    labelRole.Text = "Администратор";
                    break;
            }   

        }

        private void ButtonProfile_Click(object sender, EventArgs e)
        {
            ProfileWindow pw = new ProfileWindow(id_User);
            pw.Show();
        }

        private void buttonTasks_Click(object sender, EventArgs e)
        {
            TasksWindow tw = new TasksWindow(userRole_id, id_User);
            tw.Show();
        }

        private void panel3_Click(object sender, EventArgs e)
        {
            Application.Exit();
            new Thread(() => Application.Run(new AuthWindow())).Start();
        }
    }
}
