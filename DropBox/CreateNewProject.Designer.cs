namespace DropBox
{
    partial class CreateNewProject
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
            this.textBox_projectname = new System.Windows.Forms.TextBox();
            this.comboBox_size = new System.Windows.Forms.ComboBox();
            this.label_projectname = new System.Windows.Forms.Label();
            this.label_size = new System.Windows.Forms.Label();
            this.button_create_cancel = new System.Windows.Forms.Button();
            this.btCreate = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label_newProject_title = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox_projectname
            // 
            this.textBox_projectname.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_projectname.Location = new System.Drawing.Point(272, 35);
            this.textBox_projectname.Name = "textBox_projectname";
            this.textBox_projectname.Size = new System.Drawing.Size(221, 29);
            this.textBox_projectname.TabIndex = 0;
            this.textBox_projectname.Enter += new System.EventHandler(this.textBoxProjectName_Enter);
            this.textBox_projectname.Leave += new System.EventHandler(this.textBoxProjectName_Leave);
            // 
            // comboBox_size
            // 
            this.comboBox_size.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox_size.FormattingEnabled = true;
            this.comboBox_size.IntegralHeight = false;
            this.comboBox_size.Items.AddRange(new object[] {
            "iPhone5 (640 x 1136)",
            "iPhone6 (750 x 1334)",
            "iPhone6S (750 x 1334)",
            "iPhone6+ (1080 x 1920)",
            "iPhone6S+ (1080 x 1920)",
            "GalaxyS2_HD (720 x 1280)",
            "GalaxyS3 (720 x 1280)",
            "GalaxyS4 (1080 x 1920)",
            "GalaxyS5 (1080 x 1920)",
            "GalaxyS6 (1440 x 2560)",
            "GalaxyNote2 (720 x 1280)",
            "GalaxyNote3 (1080 x 1920)",
            "GalaxyNote4 (1440 x 2560)",
            "GalaxyNote5 (1440 x 2560)",
            "OptimusG_Pro (1080 x 1920)",
            "G2 (1080 x 1920)",
            "G3 (1440 x 2560)",
            "G4 (1440 x 2560)"});
            this.comboBox_size.Location = new System.Drawing.Point(272, 79);
            this.comboBox_size.Name = "comboBox_size";
            this.comboBox_size.Size = new System.Drawing.Size(221, 32);
            this.comboBox_size.TabIndex = 2;
            // 
            // label_projectname
            // 
            this.label_projectname.AutoSize = true;
            this.label_projectname.Font = new System.Drawing.Font("굴림", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_projectname.ForeColor = System.Drawing.Color.Black;
            this.label_projectname.Location = new System.Drawing.Point(97, 37);
            this.label_projectname.Name = "label_projectname";
            this.label_projectname.Size = new System.Drawing.Size(156, 24);
            this.label_projectname.TabIndex = 6;
            this.label_projectname.Text = "Project name:";
            // 
            // label_size
            // 
            this.label_size.AutoSize = true;
            this.label_size.Font = new System.Drawing.Font("굴림", 18F);
            this.label_size.ForeColor = System.Drawing.Color.Black;
            this.label_size.Location = new System.Drawing.Point(191, 83);
            this.label_size.Name = "label_size";
            this.label_size.Size = new System.Drawing.Size(62, 24);
            this.label_size.TabIndex = 7;
            this.label_size.Text = "Size:";
            // 
            // button_create_cancel
            // 
            this.button_create_cancel.BackColor = System.Drawing.Color.Transparent;
            this.button_create_cancel.BackgroundImage = global::DropBox.Properties.Resources._2_cancel;
            this.button_create_cancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_create_cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_create_cancel.ForeColor = System.Drawing.Color.Black;
            this.button_create_cancel.Location = new System.Drawing.Point(105, 141);
            this.button_create_cancel.Name = "button_create_cancel";
            this.button_create_cancel.Size = new System.Drawing.Size(140, 47);
            this.button_create_cancel.TabIndex = 5;
            this.button_create_cancel.UseVisualStyleBackColor = false;
            this.button_create_cancel.Click += new System.EventHandler(this.button_create_cancel_Click);
            // 
            // btCreate
            // 
            this.btCreate.BackColor = System.Drawing.Color.Transparent;
            this.btCreate.BackgroundImage = global::DropBox.Properties.Resources._3_ok;
            this.btCreate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btCreate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btCreate.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btCreate.ForeColor = System.Drawing.Color.Black;
            this.btCreate.Location = new System.Drawing.Point(353, 141);
            this.btCreate.Name = "btCreate";
            this.btCreate.Size = new System.Drawing.Size(140, 47);
            this.btCreate.TabIndex = 3;
            this.btCreate.UseVisualStyleBackColor = false;
            this.btCreate.Click += new System.EventHandler(this.btCreate_Click);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label_projectname);
            this.panel2.Controls.Add(this.button_create_cancel);
            this.panel2.Controls.Add(this.btCreate);
            this.panel2.Controls.Add(this.label_size);
            this.panel2.Controls.Add(this.textBox_projectname);
            this.panel2.Controls.Add(this.comboBox_size);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.ForeColor = System.Drawing.Color.Maroon;
            this.panel2.Location = new System.Drawing.Point(0, 50);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(598, 228);
            this.panel2.TabIndex = 8;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImage = global::DropBox.Properties.Resources._2_bg_50_50;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.label_newProject_title);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(598, 50);
            this.panel1.TabIndex = 4;
            // 
            // label_newProject_title
            // 
            this.label_newProject_title.AutoSize = true;
            this.label_newProject_title.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_newProject_title.ForeColor = System.Drawing.Color.White;
            this.label_newProject_title.Location = new System.Drawing.Point(260, 16);
            this.label_newProject_title.Name = "label_newProject_title";
            this.label_newProject_title.Size = new System.Drawing.Size(91, 16);
            this.label_newProject_title.TabIndex = 1;
            this.label_newProject_title.Text = "New Project";
            // 
            // CreateNewProject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(598, 278);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CreateNewProject";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NewProject";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }



        #endregion

        private System.Windows.Forms.TextBox textBox_projectname;
        private System.Windows.Forms.Label label_newProject_title;
        private System.Windows.Forms.ComboBox comboBox_size;
        private System.Windows.Forms.Button btCreate;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button_create_cancel;
        private System.Windows.Forms.Label label_projectname;
        private System.Windows.Forms.Label label_size;
        private System.Windows.Forms.Panel panel2;
    }
}