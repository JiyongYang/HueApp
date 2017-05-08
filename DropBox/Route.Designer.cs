namespace DropBox
{
    partial class Route
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
            this.panel_analysis_route_main = new System.Windows.Forms.Panel();
            this.panel_for_fpanels = new System.Windows.Forms.Panel();
            this.panel_user = new System.Windows.Forms.Panel();
            this.pictureBox_line = new System.Windows.Forms.PictureBox();
            this.fpanel_user = new System.Windows.Forms.FlowLayoutPanel();
            this.label_user = new System.Windows.Forms.Label();
            this.panel_initial = new System.Windows.Forms.Panel();
            this.fpanel_initial = new System.Windows.Forms.FlowLayoutPanel();
            this.label_initial = new System.Windows.Forms.Label();
            this.panel_analysis_route_left = new System.Windows.Forms.Panel();
            this.cb_analysis_route_selectScenario = new System.Windows.Forms.ComboBox();
            this.label_1 = new System.Windows.Forms.Label();
            this.btn_analysis_show_route = new System.Windows.Forms.Button();
            this.cb_analysis_route_selectTest = new System.Windows.Forms.ComboBox();
            this.label_2 = new System.Windows.Forms.Label();
            this.panel_analysis_route_main.SuspendLayout();
            this.panel_for_fpanels.SuspendLayout();
            this.panel_user.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_line)).BeginInit();
            this.panel_initial.SuspendLayout();
            this.panel_analysis_route_left.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_analysis_route_main
            // 
            this.panel_analysis_route_main.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel_analysis_route_main.Controls.Add(this.panel_for_fpanels);
            this.panel_analysis_route_main.Controls.Add(this.panel_analysis_route_left);
            this.panel_analysis_route_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_analysis_route_main.Location = new System.Drawing.Point(0, 0);
            this.panel_analysis_route_main.Margin = new System.Windows.Forms.Padding(2);
            this.panel_analysis_route_main.Name = "panel_analysis_route_main";
            this.panel_analysis_route_main.Size = new System.Drawing.Size(1118, 700);
            this.panel_analysis_route_main.TabIndex = 6;
            // 
            // panel_for_fpanels
            // 
            this.panel_for_fpanels.Controls.Add(this.panel_user);
            this.panel_for_fpanels.Controls.Add(this.panel_initial);
            this.panel_for_fpanels.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_for_fpanels.Location = new System.Drawing.Point(305, 0);
            this.panel_for_fpanels.Name = "panel_for_fpanels";
            this.panel_for_fpanels.Size = new System.Drawing.Size(813, 700);
            this.panel_for_fpanels.TabIndex = 3;
            // 
            // panel_user
            // 
            this.panel_user.BackColor = System.Drawing.Color.White;
            this.panel_user.Controls.Add(this.pictureBox_line);
            this.panel_user.Controls.Add(this.fpanel_user);
            this.panel_user.Controls.Add(this.label_user);
            this.panel_user.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_user.Location = new System.Drawing.Point(0, 343);
            this.panel_user.Name = "panel_user";
            this.panel_user.Size = new System.Drawing.Size(813, 357);
            this.panel_user.TabIndex = 1;
            this.panel_user.SizeChanged += new System.EventHandler(this.panel_user_SizeChanged);
            // 
            // pictureBox_line
            // 
            this.pictureBox_line.BackColor = System.Drawing.Color.Silver;
            this.pictureBox_line.Location = new System.Drawing.Point(20, 0);
            this.pictureBox_line.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox_line.Name = "pictureBox_line";
            this.pictureBox_line.Size = new System.Drawing.Size(793, 1);
            this.pictureBox_line.TabIndex = 3;
            this.pictureBox_line.TabStop = false;
            // 
            // fpanel_user
            // 
            this.fpanel_user.AutoScroll = true;
            this.fpanel_user.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.fpanel_user.Location = new System.Drawing.Point(0, 60);
            this.fpanel_user.Name = "fpanel_user";
            this.fpanel_user.Size = new System.Drawing.Size(813, 297);
            this.fpanel_user.TabIndex = 2;
            this.fpanel_user.WrapContents = false;
            // 
            // label_user
            // 
            this.label_user.AutoSize = true;
            this.label_user.Font = new System.Drawing.Font("굴림", 18F);
            this.label_user.Location = new System.Drawing.Point(30, 20);
            this.label_user.Name = "label_user";
            this.label_user.Size = new System.Drawing.Size(126, 24);
            this.label_user.TabIndex = 1;
            this.label_user.Text = "User Route";
            // 
            // panel_initial
            // 
            this.panel_initial.BackColor = System.Drawing.Color.White;
            this.panel_initial.Controls.Add(this.fpanel_initial);
            this.panel_initial.Controls.Add(this.label_initial);
            this.panel_initial.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_initial.Location = new System.Drawing.Point(0, 0);
            this.panel_initial.Name = "panel_initial";
            this.panel_initial.Size = new System.Drawing.Size(813, 343);
            this.panel_initial.TabIndex = 0;
            this.panel_initial.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // fpanel_initial
            // 
            this.fpanel_initial.AutoScroll = true;
            this.fpanel_initial.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.fpanel_initial.Location = new System.Drawing.Point(0, 95);
            this.fpanel_initial.Margin = new System.Windows.Forms.Padding(30, 3, 3, 3);
            this.fpanel_initial.Name = "fpanel_initial";
            this.fpanel_initial.Size = new System.Drawing.Size(813, 248);
            this.fpanel_initial.TabIndex = 1;
            this.fpanel_initial.WrapContents = false;
            // 
            // label_initial
            // 
            this.label_initial.AutoSize = true;
            this.label_initial.Font = new System.Drawing.Font("굴림", 18F);
            this.label_initial.Location = new System.Drawing.Point(30, 44);
            this.label_initial.Name = "label_initial";
            this.label_initial.Size = new System.Drawing.Size(128, 24);
            this.label_initial.TabIndex = 0;
            this.label_initial.Text = "Initial Route";
            // 
            // panel_analysis_route_left
            // 
            this.panel_analysis_route_left.BackColor = System.Drawing.Color.White;
            this.panel_analysis_route_left.Controls.Add(this.cb_analysis_route_selectScenario);
            this.panel_analysis_route_left.Controls.Add(this.label_1);
            this.panel_analysis_route_left.Controls.Add(this.btn_analysis_show_route);
            this.panel_analysis_route_left.Controls.Add(this.cb_analysis_route_selectTest);
            this.panel_analysis_route_left.Controls.Add(this.label_2);
            this.panel_analysis_route_left.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_analysis_route_left.Location = new System.Drawing.Point(0, 0);
            this.panel_analysis_route_left.Margin = new System.Windows.Forms.Padding(2);
            this.panel_analysis_route_left.Name = "panel_analysis_route_left";
            this.panel_analysis_route_left.Size = new System.Drawing.Size(305, 700);
            this.panel_analysis_route_left.TabIndex = 2;
            // 
            // cb_analysis_route_selectScenario
            // 
            this.cb_analysis_route_selectScenario.Font = new System.Drawing.Font("굴림", 14F);
            this.cb_analysis_route_selectScenario.FormattingEnabled = true;
            this.cb_analysis_route_selectScenario.Location = new System.Drawing.Point(80, 95);
            this.cb_analysis_route_selectScenario.Margin = new System.Windows.Forms.Padding(2);
            this.cb_analysis_route_selectScenario.Name = "cb_analysis_route_selectScenario";
            this.cb_analysis_route_selectScenario.Size = new System.Drawing.Size(188, 27);
            this.cb_analysis_route_selectScenario.TabIndex = 8;
            this.cb_analysis_route_selectScenario.SelectedIndexChanged += new System.EventHandler(this.cb_analysis_route_selectScenario_SelectedIndexChanged);
            // 
            // label_1
            // 
            this.label_1.AutoSize = true;
            this.label_1.Font = new System.Drawing.Font("굴림", 18F);
            this.label_1.Location = new System.Drawing.Point(80, 44);
            this.label_1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_1.Name = "label_1";
            this.label_1.Size = new System.Drawing.Size(154, 24);
            this.label_1.TabIndex = 7;
            this.label_1.Text = "시나리오선택";
            // 
            // btn_analysis_show_route
            // 
            this.btn_analysis_show_route.BackColor = System.Drawing.Color.Transparent;
            this.btn_analysis_show_route.BackgroundImage = global::DropBox.Properties.Resources._8_2_show_box;
            this.btn_analysis_show_route.FlatAppearance.BorderSize = 0;
            this.btn_analysis_show_route.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_analysis_show_route.ForeColor = System.Drawing.Color.White;
            this.btn_analysis_show_route.Location = new System.Drawing.Point(80, 528);
            this.btn_analysis_show_route.Margin = new System.Windows.Forms.Padding(2);
            this.btn_analysis_show_route.Name = "btn_analysis_show_route";
            this.btn_analysis_show_route.Size = new System.Drawing.Size(188, 63);
            this.btn_analysis_show_route.TabIndex = 6;
            this.btn_analysis_show_route.Text = "S H O W";
            this.btn_analysis_show_route.UseVisualStyleBackColor = false;
            this.btn_analysis_show_route.Click += new System.EventHandler(this.btn_analysis_show_route_Click);
            // 
            // cb_analysis_route_selectTest
            // 
            this.cb_analysis_route_selectTest.Font = new System.Drawing.Font("굴림", 14F);
            this.cb_analysis_route_selectTest.FormattingEnabled = true;
            this.cb_analysis_route_selectTest.Location = new System.Drawing.Point(80, 196);
            this.cb_analysis_route_selectTest.Margin = new System.Windows.Forms.Padding(2);
            this.cb_analysis_route_selectTest.Name = "cb_analysis_route_selectTest";
            this.cb_analysis_route_selectTest.Size = new System.Drawing.Size(188, 27);
            this.cb_analysis_route_selectTest.TabIndex = 1;
            // 
            // label_2
            // 
            this.label_2.AutoSize = true;
            this.label_2.Font = new System.Drawing.Font("굴림", 18F);
            this.label_2.Location = new System.Drawing.Point(80, 148);
            this.label_2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_2.Name = "label_2";
            this.label_2.Size = new System.Drawing.Size(130, 24);
            this.label_2.TabIndex = 0;
            this.label_2.Text = "테스트선택";
            // 
            // Route
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1118, 700);
            this.Controls.Add(this.panel_analysis_route_main);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Route";
            this.Text = "Route";
            this.panel_analysis_route_main.ResumeLayout(false);
            this.panel_for_fpanels.ResumeLayout(false);
            this.panel_user.ResumeLayout(false);
            this.panel_user.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_line)).EndInit();
            this.panel_initial.ResumeLayout(false);
            this.panel_initial.PerformLayout();
            this.panel_analysis_route_left.ResumeLayout(false);
            this.panel_analysis_route_left.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_analysis_route_main;
        private System.Windows.Forms.Panel panel_analysis_route_left;
        private System.Windows.Forms.ComboBox cb_analysis_route_selectScenario;
        private System.Windows.Forms.Label label_1;
        private System.Windows.Forms.Button btn_analysis_show_route;
        public System.Windows.Forms.ComboBox cb_analysis_route_selectTest;
        private System.Windows.Forms.Label label_2;
        private System.Windows.Forms.Panel panel_for_fpanels;
        private System.Windows.Forms.Panel panel_user;
        private System.Windows.Forms.FlowLayoutPanel fpanel_user;
        private System.Windows.Forms.Label label_user;
        private System.Windows.Forms.Panel panel_initial;
        private System.Windows.Forms.FlowLayoutPanel fpanel_initial;
        private System.Windows.Forms.Label label_initial;
        private System.Windows.Forms.PictureBox pictureBox_line;
    }
}