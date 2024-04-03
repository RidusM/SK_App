namespace SK_App.Forms
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.labelRole = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.buttonClients = new System.Windows.Forms.Button();
            this.buttonContracts = new System.Windows.Forms.Button();
            this.buttonEmployees = new System.Windows.Forms.Button();
            this.buttonProjects = new System.Windows.Forms.Button();
            this.buttonMaterials = new System.Windows.Forms.Button();
            this.buttonObjects = new System.Windows.Forms.Button();
            this.buttonProfile = new System.Windows.Forms.Button();
            this.buttonTasks = new System.Windows.Forms.Button();
            this.buttonMessages = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(40)))), ((int)(((byte)(49)))));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.labelRole);
            this.panel1.Controls.Add(this.labelName);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(431, 12);
            this.panel1.MinimumSize = new System.Drawing.Size(397, 472);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(402, 617);
            this.panel1.TabIndex = 14;
            // 
            // panel3
            // 
            this.panel3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel3.BackgroundImage")));
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel3.Location = new System.Drawing.Point(332, 14);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(58, 56);
            this.panel3.TabIndex = 1;
            this.panel3.Click += new System.EventHandler(this.panel3_Click);
            // 
            // labelRole
            // 
            this.labelRole.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelRole.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.labelRole.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelRole.Font = new System.Drawing.Font("Rostov", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelRole.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(40)))), ((int)(((byte)(49)))));
            this.labelRole.Location = new System.Drawing.Point(50, 542);
            this.labelRole.Name = "labelRole";
            this.labelRole.Size = new System.Drawing.Size(303, 40);
            this.labelRole.TabIndex = 5;
            this.labelRole.Text = "Test";
            this.labelRole.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelName
            // 
            this.labelName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.labelName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelName.Font = new System.Drawing.Font("Rostov", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(40)))), ((int)(((byte)(49)))));
            this.labelName.Location = new System.Drawing.Point(50, 396);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(303, 40);
            this.labelName.TabIndex = 4;
            this.labelName.Text = "Test";
            this.labelName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(62)))), ((int)(((byte)(70)))));
            this.label3.Font = new System.Drawing.Font("Rostov", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(211)))), ((int)(((byte)(105)))));
            this.label3.Location = new System.Drawing.Point(3, 471);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(387, 40);
            this.label3.TabIndex = 3;
            this.label3.Text = "Роль пользователя";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(62)))), ((int)(((byte)(70)))));
            this.label1.Font = new System.Drawing.Font("Rostov", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(211)))), ((int)(((byte)(105)))));
            this.label1.Location = new System.Drawing.Point(3, 327);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(387, 40);
            this.label1.TabIndex = 1;
            this.label1.Text = "Имя пользователя";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel2.Location = new System.Drawing.Point(43, 14);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(303, 297);
            this.panel2.TabIndex = 0;
            // 
            // buttonClients
            // 
            this.buttonClients.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonClients.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonClients.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(40)))), ((int)(((byte)(49)))));
            this.buttonClients.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonClients.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.buttonClients.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.buttonClients.FlatAppearance.BorderSize = 0;
            this.buttonClients.Font = new System.Drawing.Font("Rostov", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonClients.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(211)))), ((int)(((byte)(105)))));
            this.buttonClients.Location = new System.Drawing.Point(217, 135);
            this.buttonClients.MinimumSize = new System.Drawing.Size(195, 113);
            this.buttonClients.Name = "buttonClients";
            this.buttonClients.Size = new System.Drawing.Size(206, 117);
            this.buttonClients.TabIndex = 11;
            this.buttonClients.Text = "Клиенты";
            this.buttonClients.UseVisualStyleBackColor = false;
            this.buttonClients.Click += new System.EventHandler(this.ButtonClients_Click);
            // 
            // buttonContracts
            // 
            this.buttonContracts.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonContracts.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonContracts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(40)))), ((int)(((byte)(49)))));
            this.buttonContracts.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonContracts.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.buttonContracts.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.buttonContracts.FlatAppearance.BorderSize = 0;
            this.buttonContracts.Font = new System.Drawing.Font("Rostov", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonContracts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(211)))), ((int)(((byte)(105)))));
            this.buttonContracts.Location = new System.Drawing.Point(5, 258);
            this.buttonContracts.MinimumSize = new System.Drawing.Size(195, 113);
            this.buttonContracts.Name = "buttonContracts";
            this.buttonContracts.Size = new System.Drawing.Size(206, 117);
            this.buttonContracts.TabIndex = 12;
            this.buttonContracts.Text = "Договоры";
            this.buttonContracts.UseVisualStyleBackColor = false;
            this.buttonContracts.Click += new System.EventHandler(this.ButtonContracts_Click);
            // 
            // buttonEmployees
            // 
            this.buttonEmployees.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonEmployees.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonEmployees.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(40)))), ((int)(((byte)(49)))));
            this.buttonEmployees.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonEmployees.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.buttonEmployees.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.buttonEmployees.FlatAppearance.BorderSize = 0;
            this.buttonEmployees.Font = new System.Drawing.Font("Rostov", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonEmployees.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(211)))), ((int)(((byte)(105)))));
            this.buttonEmployees.Location = new System.Drawing.Point(5, 135);
            this.buttonEmployees.MinimumSize = new System.Drawing.Size(195, 113);
            this.buttonEmployees.Name = "buttonEmployees";
            this.buttonEmployees.Size = new System.Drawing.Size(206, 117);
            this.buttonEmployees.TabIndex = 10;
            this.buttonEmployees.Text = "Сотрудники";
            this.buttonEmployees.UseVisualStyleBackColor = false;
            this.buttonEmployees.Click += new System.EventHandler(this.ButtonEmployees_Click);
            this.buttonEmployees.Resize += new System.EventHandler(this.MainWindow_Load);
            // 
            // buttonProjects
            // 
            this.buttonProjects.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonProjects.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonProjects.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(40)))), ((int)(((byte)(49)))));
            this.buttonProjects.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonProjects.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.buttonProjects.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.buttonProjects.FlatAppearance.BorderSize = 0;
            this.buttonProjects.Font = new System.Drawing.Font("Rostov", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonProjects.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(211)))), ((int)(((byte)(105)))));
            this.buttonProjects.Location = new System.Drawing.Point(217, 12);
            this.buttonProjects.MinimumSize = new System.Drawing.Size(195, 113);
            this.buttonProjects.Name = "buttonProjects";
            this.buttonProjects.Size = new System.Drawing.Size(206, 117);
            this.buttonProjects.TabIndex = 8;
            this.buttonProjects.Text = "Проекты";
            this.buttonProjects.UseVisualStyleBackColor = false;
            this.buttonProjects.Click += new System.EventHandler(this.ButtonProjects_Click);
            // 
            // buttonMaterials
            // 
            this.buttonMaterials.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonMaterials.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonMaterials.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(40)))), ((int)(((byte)(49)))));
            this.buttonMaterials.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonMaterials.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.buttonMaterials.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.buttonMaterials.FlatAppearance.BorderSize = 0;
            this.buttonMaterials.Font = new System.Drawing.Font("Rostov", 20.25F);
            this.buttonMaterials.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(211)))), ((int)(((byte)(105)))));
            this.buttonMaterials.Location = new System.Drawing.Point(217, 258);
            this.buttonMaterials.MinimumSize = new System.Drawing.Size(195, 113);
            this.buttonMaterials.Name = "buttonMaterials";
            this.buttonMaterials.Size = new System.Drawing.Size(206, 117);
            this.buttonMaterials.TabIndex = 9;
            this.buttonMaterials.Text = "Строй Материалы";
            this.buttonMaterials.UseVisualStyleBackColor = false;
            this.buttonMaterials.Click += new System.EventHandler(this.ButtonMaterials_Click);
            // 
            // buttonObjects
            // 
            this.buttonObjects.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonObjects.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonObjects.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(40)))), ((int)(((byte)(49)))));
            this.buttonObjects.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonObjects.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.buttonObjects.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.buttonObjects.FlatAppearance.BorderSize = 0;
            this.buttonObjects.Font = new System.Drawing.Font("Rostov", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonObjects.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(211)))), ((int)(((byte)(105)))));
            this.buttonObjects.Location = new System.Drawing.Point(5, 12);
            this.buttonObjects.MinimumSize = new System.Drawing.Size(195, 113);
            this.buttonObjects.Name = "buttonObjects";
            this.buttonObjects.Size = new System.Drawing.Size(206, 117);
            this.buttonObjects.TabIndex = 3;
            this.buttonObjects.Text = "Объекты";
            this.buttonObjects.UseVisualStyleBackColor = false;
            this.buttonObjects.Click += new System.EventHandler(this.ButtonObjects_Click);
            // 
            // buttonProfile
            // 
            this.buttonProfile.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonProfile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(40)))), ((int)(((byte)(49)))));
            this.buttonProfile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonProfile.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.buttonProfile.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.buttonProfile.FlatAppearance.BorderSize = 10;
            this.buttonProfile.Font = new System.Drawing.Font("Rostov", 20.25F);
            this.buttonProfile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(211)))), ((int)(((byte)(105)))));
            this.buttonProfile.Location = new System.Drawing.Point(5, 504);
            this.buttonProfile.MinimumSize = new System.Drawing.Size(206, 117);
            this.buttonProfile.Name = "buttonProfile";
            this.buttonProfile.Size = new System.Drawing.Size(420, 125);
            this.buttonProfile.TabIndex = 17;
            this.buttonProfile.Text = "Мой профиль";
            this.buttonProfile.UseVisualStyleBackColor = false;
            this.buttonProfile.Click += new System.EventHandler(this.ButtonProfile_Click);
            // 
            // buttonTasks
            // 
            this.buttonTasks.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonTasks.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(40)))), ((int)(((byte)(49)))));
            this.buttonTasks.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonTasks.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.buttonTasks.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.buttonTasks.FlatAppearance.BorderSize = 0;
            this.buttonTasks.Font = new System.Drawing.Font("Rostov", 20.25F);
            this.buttonTasks.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(211)))), ((int)(((byte)(105)))));
            this.buttonTasks.Location = new System.Drawing.Point(217, 381);
            this.buttonTasks.Name = "buttonTasks";
            this.buttonTasks.Size = new System.Drawing.Size(206, 117);
            this.buttonTasks.TabIndex = 18;
            this.buttonTasks.Text = "Задачи";
            this.buttonTasks.UseVisualStyleBackColor = false;
            this.buttonTasks.Click += new System.EventHandler(this.buttonTasks_Click);
            // 
            // buttonMessages
            // 
            this.buttonMessages.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonMessages.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(40)))), ((int)(((byte)(49)))));
            this.buttonMessages.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonMessages.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.buttonMessages.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.buttonMessages.FlatAppearance.BorderSize = 0;
            this.buttonMessages.Font = new System.Drawing.Font("Rostov", 20.25F);
            this.buttonMessages.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(211)))), ((int)(((byte)(105)))));
            this.buttonMessages.Location = new System.Drawing.Point(5, 381);
            this.buttonMessages.MinimumSize = new System.Drawing.Size(206, 117);
            this.buttonMessages.Name = "buttonMessages";
            this.buttonMessages.Size = new System.Drawing.Size(206, 117);
            this.buttonMessages.TabIndex = 13;
            this.buttonMessages.Text = "Сообщения";
            this.buttonMessages.UseVisualStyleBackColor = false;
            this.buttonMessages.Click += new System.EventHandler(this.ButtonMessages_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(40)))), ((int)(((byte)(49)))));
            this.ClientSize = new System.Drawing.Size(840, 641);
            this.Controls.Add(this.buttonTasks);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.buttonProfile);
            this.Controls.Add(this.buttonMessages);
            this.Controls.Add(this.buttonObjects);
            this.Controls.Add(this.buttonMaterials);
            this.Controls.Add(this.buttonContracts);
            this.Controls.Add(this.buttonEmployees);
            this.Controls.Add(this.buttonProjects);
            this.Controls.Add(this.buttonClients);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(843, 539);
            this.Name = "MainWindow";
            this.Text = "Главное окно";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainWindow_Closed);
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelRole;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Button buttonClients;
        private System.Windows.Forms.Button buttonContracts;
        private System.Windows.Forms.Button buttonEmployees;
        private System.Windows.Forms.Button buttonProjects;
        private System.Windows.Forms.Button buttonMaterials;
        private System.Windows.Forms.Button buttonObjects;
        private System.Windows.Forms.Button buttonProfile;
        private System.Windows.Forms.Button buttonTasks;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button buttonMessages;
    }
}