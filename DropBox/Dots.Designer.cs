namespace DropBox
{
    partial class Dots
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
            this.panel_analysis_dot_main = new System.Windows.Forms.Panel();
            this.panel_analysis_dots_right = new System.Windows.Forms.Panel();
            this.panel_detail_info_label = new System.Windows.Forms.Panel();
            this.label_detail_info_title = new System.Windows.Forms.Label();
            this.panel_detail_info = new System.Windows.Forms.Panel();
            this.label_testdate = new System.Windows.Forms.Label();
            this.label_longest = new System.Windows.Forms.Label();
            this.label_shortest = new System.Windows.Forms.Label();
            this.label_visit = new System.Windows.Forms.Label();
            this.label_time = new System.Windows.Forms.Label();
            this.label_click = new System.Windows.Forms.Label();
            this.panel_analysis_dots_left = new System.Windows.Forms.Panel();
            this.cb_analysis_dots_selectScenario = new System.Windows.Forms.ComboBox();
            this.label_1 = new System.Windows.Forms.Label();
            this.btn_analysis_show_dot = new System.Windows.Forms.Button();
            this.cb_analysis_dots_selectImage = new System.Windows.Forms.ComboBox();
            this.label_3 = new System.Windows.Forms.Label();
            this.cb_analysis_dots_selectTest = new System.Windows.Forms.ComboBox();
            this.label_2 = new System.Windows.Forms.Label();
            this.panel_analysis_picture = new System.Windows.Forms.Panel();
            this.panel_analysis_picture_dot = new System.Windows.Forms.Panel();
            this.panel_analysis_dot_main.SuspendLayout();
            this.panel_analysis_dots_right.SuspendLayout();
            this.panel_detail_info_label.SuspendLayout();
            this.panel_detail_info.SuspendLayout();
            this.panel_analysis_dots_left.SuspendLayout();
            this.panel_analysis_picture.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_analysis_dot_main
            // 
            this.panel_analysis_dot_main.BackColor = System.Drawing.Color.White;
            this.panel_analysis_dot_main.Controls.Add(this.panel_analysis_dots_right);
            this.panel_analysis_dot_main.Controls.Add(this.panel_analysis_dots_left);
            this.panel_analysis_dot_main.Controls.Add(this.panel_analysis_picture);
            this.panel_analysis_dot_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_analysis_dot_main.Location = new System.Drawing.Point(0, 0);
            this.panel_analysis_dot_main.Margin = new System.Windows.Forms.Padding(2);
            this.panel_analysis_dot_main.Name = "panel_analysis_dot_main";
            this.panel_analysis_dot_main.Size = new System.Drawing.Size(1100, 739);
            this.panel_analysis_dot_main.TabIndex = 5;
            // 
            // panel_analysis_dots_right
            // 
            this.panel_analysis_dots_right.BackColor = System.Drawing.Color.White;
            this.panel_analysis_dots_right.Controls.Add(this.panel_detail_info_label);
            this.panel_analysis_dots_right.Controls.Add(this.panel_detail_info);
            this.panel_analysis_dots_right.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel_analysis_dots_right.Location = new System.Drawing.Point(634, 0);
            this.panel_analysis_dots_right.Margin = new System.Windows.Forms.Padding(2);
            this.panel_analysis_dots_right.Name = "panel_analysis_dots_right";
            this.panel_analysis_dots_right.Size = new System.Drawing.Size(466, 739);
            this.panel_analysis_dots_right.TabIndex = 2;
            // 
            // panel_detail_info_label
            // 
            this.panel_detail_info_label.BackgroundImage = global::DropBox.Properties.Resources._8_4_line;
            this.panel_detail_info_label.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel_detail_info_label.Controls.Add(this.label_detail_info_title);
            this.panel_detail_info_label.Location = new System.Drawing.Point(4, 44);
            this.panel_detail_info_label.Name = "panel_detail_info_label";
            this.panel_detail_info_label.Size = new System.Drawing.Size(120, 40);
            this.panel_detail_info_label.TabIndex = 2;
            // 
            // label_detail_info_title
            // 
            this.label_detail_info_title.ForeColor = System.Drawing.Color.Black;
            this.label_detail_info_title.Location = new System.Drawing.Point(11, 4);
            this.label_detail_info_title.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_detail_info_title.Name = "label_detail_info_title";
            this.label_detail_info_title.Size = new System.Drawing.Size(107, 23);
            this.label_detail_info_title.TabIndex = 0;
            this.label_detail_info_title.Text = "세부정보";
            this.label_detail_info_title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel_detail_info
            // 
            this.panel_detail_info.BackgroundImage = global::DropBox.Properties.Resources.test2;
            this.panel_detail_info.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel_detail_info.Controls.Add(this.label_testdate);
            this.panel_detail_info.Controls.Add(this.label_longest);
            this.panel_detail_info.Controls.Add(this.label_shortest);
            this.panel_detail_info.Controls.Add(this.label_visit);
            this.panel_detail_info.Controls.Add(this.label_time);
            this.panel_detail_info.Controls.Add(this.label_click);
            this.panel_detail_info.Location = new System.Drawing.Point(4, 100);
            this.panel_detail_info.Name = "panel_detail_info";
            this.panel_detail_info.Size = new System.Drawing.Size(420, 400);
            this.panel_detail_info.TabIndex = 1;
            // 
            // label_testdate
            // 
            this.label_testdate.AutoSize = true;
            this.label_testdate.BackColor = System.Drawing.Color.Transparent;
            this.label_testdate.ForeColor = System.Drawing.Color.Black;
            this.label_testdate.Location = new System.Drawing.Point(18, 229);
            this.label_testdate.Name = "label_testdate";
            this.label_testdate.Size = new System.Drawing.Size(38, 12);
            this.label_testdate.TabIndex = 5;
            this.label_testdate.Text = "label7";
            // 
            // label_longest
            // 
            this.label_longest.AutoSize = true;
            this.label_longest.BackColor = System.Drawing.Color.Transparent;
            this.label_longest.ForeColor = System.Drawing.Color.Black;
            this.label_longest.Location = new System.Drawing.Point(18, 190);
            this.label_longest.Name = "label_longest";
            this.label_longest.Size = new System.Drawing.Size(38, 12);
            this.label_longest.TabIndex = 4;
            this.label_longest.Text = "label6";
            // 
            // label_shortest
            // 
            this.label_shortest.AutoSize = true;
            this.label_shortest.BackColor = System.Drawing.Color.Transparent;
            this.label_shortest.ForeColor = System.Drawing.Color.Black;
            this.label_shortest.Location = new System.Drawing.Point(18, 149);
            this.label_shortest.Name = "label_shortest";
            this.label_shortest.Size = new System.Drawing.Size(38, 12);
            this.label_shortest.TabIndex = 3;
            this.label_shortest.Text = "label5";
            // 
            // label_visit
            // 
            this.label_visit.AutoSize = true;
            this.label_visit.BackColor = System.Drawing.Color.Transparent;
            this.label_visit.ForeColor = System.Drawing.Color.Black;
            this.label_visit.Location = new System.Drawing.Point(18, 109);
            this.label_visit.Name = "label_visit";
            this.label_visit.Size = new System.Drawing.Size(38, 12);
            this.label_visit.TabIndex = 2;
            this.label_visit.Text = "label4";
            // 
            // label_time
            // 
            this.label_time.AutoSize = true;
            this.label_time.BackColor = System.Drawing.Color.Transparent;
            this.label_time.ForeColor = System.Drawing.Color.Black;
            this.label_time.Location = new System.Drawing.Point(18, 69);
            this.label_time.Name = "label_time";
            this.label_time.Size = new System.Drawing.Size(38, 12);
            this.label_time.TabIndex = 1;
            this.label_time.Text = "label2";
            // 
            // label_click
            // 
            this.label_click.AutoSize = true;
            this.label_click.BackColor = System.Drawing.Color.Transparent;
            this.label_click.ForeColor = System.Drawing.Color.Black;
            this.label_click.Location = new System.Drawing.Point(18, 27);
            this.label_click.Name = "label_click";
            this.label_click.Size = new System.Drawing.Size(38, 12);
            this.label_click.TabIndex = 0;
            this.label_click.Text = "label1";
            // 
            // panel_analysis_dots_left
            // 
            this.panel_analysis_dots_left.BackColor = System.Drawing.Color.White;
            this.panel_analysis_dots_left.Controls.Add(this.cb_analysis_dots_selectScenario);
            this.panel_analysis_dots_left.Controls.Add(this.label_1);
            this.panel_analysis_dots_left.Controls.Add(this.btn_analysis_show_dot);
            this.panel_analysis_dots_left.Controls.Add(this.cb_analysis_dots_selectImage);
            this.panel_analysis_dots_left.Controls.Add(this.label_3);
            this.panel_analysis_dots_left.Controls.Add(this.cb_analysis_dots_selectTest);
            this.panel_analysis_dots_left.Controls.Add(this.label_2);
            this.panel_analysis_dots_left.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_analysis_dots_left.Location = new System.Drawing.Point(0, 0);
            this.panel_analysis_dots_left.Margin = new System.Windows.Forms.Padding(2);
            this.panel_analysis_dots_left.Name = "panel_analysis_dots_left";
            this.panel_analysis_dots_left.Size = new System.Drawing.Size(305, 739);
            this.panel_analysis_dots_left.TabIndex = 1;
            // 
            // cb_analysis_dots_selectScenario
            // 
            this.cb_analysis_dots_selectScenario.Font = new System.Drawing.Font("굴림", 14F);
            this.cb_analysis_dots_selectScenario.ForeColor = System.Drawing.Color.Black;
            this.cb_analysis_dots_selectScenario.FormattingEnabled = true;
            this.cb_analysis_dots_selectScenario.Location = new System.Drawing.Point(80, 77);
            this.cb_analysis_dots_selectScenario.Margin = new System.Windows.Forms.Padding(2);
            this.cb_analysis_dots_selectScenario.Name = "cb_analysis_dots_selectScenario";
            this.cb_analysis_dots_selectScenario.Size = new System.Drawing.Size(188, 27);
            this.cb_analysis_dots_selectScenario.TabIndex = 8;
            this.cb_analysis_dots_selectScenario.SelectedIndexChanged += new System.EventHandler(this.cb_analysis_dots_selectScenario_SelectedIndexChanged);
            // 
            // label_1
            // 
            this.label_1.AutoSize = true;
            this.label_1.Font = new System.Drawing.Font("굴림", 18F);
            this.label_1.ForeColor = System.Drawing.Color.Black;
            this.label_1.Location = new System.Drawing.Point(80, 44);
            this.label_1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_1.Name = "label_1";
            this.label_1.Size = new System.Drawing.Size(154, 24);
            this.label_1.TabIndex = 7;
            this.label_1.Text = "시나리오선택";
            // 
            // btn_analysis_show_dot
            // 
            this.btn_analysis_show_dot.BackColor = System.Drawing.Color.Transparent;
            this.btn_analysis_show_dot.BackgroundImage = global::DropBox.Properties.Resources._8_2_show_box;
            this.btn_analysis_show_dot.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_analysis_show_dot.FlatAppearance.BorderSize = 0;
            this.btn_analysis_show_dot.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_analysis_show_dot.ForeColor = System.Drawing.Color.White;
            this.btn_analysis_show_dot.Location = new System.Drawing.Point(80, 516);
            this.btn_analysis_show_dot.Margin = new System.Windows.Forms.Padding(2);
            this.btn_analysis_show_dot.Name = "btn_analysis_show_dot";
            this.btn_analysis_show_dot.Size = new System.Drawing.Size(188, 63);
            this.btn_analysis_show_dot.TabIndex = 6;
            this.btn_analysis_show_dot.Text = "S H O W";
            this.btn_analysis_show_dot.UseVisualStyleBackColor = false;
            this.btn_analysis_show_dot.Click += new System.EventHandler(this.btn_analysis_show_dot_Click_1);
            // 
            // cb_analysis_dots_selectImage
            // 
            this.cb_analysis_dots_selectImage.Font = new System.Drawing.Font("굴림", 14F);
            this.cb_analysis_dots_selectImage.ForeColor = System.Drawing.Color.Black;
            this.cb_analysis_dots_selectImage.FormattingEnabled = true;
            this.cb_analysis_dots_selectImage.Location = new System.Drawing.Point(80, 256);
            this.cb_analysis_dots_selectImage.Margin = new System.Windows.Forms.Padding(2);
            this.cb_analysis_dots_selectImage.Name = "cb_analysis_dots_selectImage";
            this.cb_analysis_dots_selectImage.Size = new System.Drawing.Size(188, 27);
            this.cb_analysis_dots_selectImage.TabIndex = 3;
            // 
            // label_3
            // 
            this.label_3.AutoSize = true;
            this.label_3.Font = new System.Drawing.Font("굴림", 18F);
            this.label_3.ForeColor = System.Drawing.Color.Black;
            this.label_3.Location = new System.Drawing.Point(80, 205);
            this.label_3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_3.Name = "label_3";
            this.label_3.Size = new System.Drawing.Size(130, 24);
            this.label_3.TabIndex = 2;
            this.label_3.Text = "이미지선택";
            // 
            // cb_analysis_dots_selectTest
            // 
            this.cb_analysis_dots_selectTest.Font = new System.Drawing.Font("굴림", 14F);
            this.cb_analysis_dots_selectTest.ForeColor = System.Drawing.Color.Black;
            this.cb_analysis_dots_selectTest.FormattingEnabled = true;
            this.cb_analysis_dots_selectTest.Items.AddRange(new object[] {
            "모두보기"});
            this.cb_analysis_dots_selectTest.Location = new System.Drawing.Point(80, 160);
            this.cb_analysis_dots_selectTest.Margin = new System.Windows.Forms.Padding(2);
            this.cb_analysis_dots_selectTest.Name = "cb_analysis_dots_selectTest";
            this.cb_analysis_dots_selectTest.Size = new System.Drawing.Size(188, 27);
            this.cb_analysis_dots_selectTest.TabIndex = 1;
            // 
            // label_2
            // 
            this.label_2.AutoSize = true;
            this.label_2.Font = new System.Drawing.Font("굴림", 18F);
            this.label_2.ForeColor = System.Drawing.Color.Black;
            this.label_2.Location = new System.Drawing.Point(80, 117);
            this.label_2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_2.Name = "label_2";
            this.label_2.Size = new System.Drawing.Size(130, 24);
            this.label_2.TabIndex = 0;
            this.label_2.Text = "테스트선택";
            // 
            // panel_analysis_picture
            // 
            this.panel_analysis_picture.Controls.Add(this.panel_analysis_picture_dot);
            this.panel_analysis_picture.Location = new System.Drawing.Point(328, 105);
            this.panel_analysis_picture.Margin = new System.Windows.Forms.Padding(2);
            this.panel_analysis_picture.Name = "panel_analysis_picture";
            this.panel_analysis_picture.Size = new System.Drawing.Size(302, 411);
            this.panel_analysis_picture.TabIndex = 3;
            // 
            // panel_analysis_picture_dot
            // 
            this.panel_analysis_picture_dot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_analysis_picture_dot.Location = new System.Drawing.Point(0, 0);
            this.panel_analysis_picture_dot.Margin = new System.Windows.Forms.Padding(2);
            this.panel_analysis_picture_dot.Name = "panel_analysis_picture_dot";
            this.panel_analysis_picture_dot.Size = new System.Drawing.Size(302, 411);
            this.panel_analysis_picture_dot.TabIndex = 0;
            // 
            // Dots
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 739);
            this.Controls.Add(this.panel_analysis_dot_main);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Dots";
            this.Text = "Dots";
            this.SizeChanged += new System.EventHandler(this.Dots_SizeChanged);
            this.panel_analysis_dot_main.ResumeLayout(false);
            this.panel_analysis_dots_right.ResumeLayout(false);
            this.panel_detail_info_label.ResumeLayout(false);
            this.panel_detail_info.ResumeLayout(false);
            this.panel_detail_info.PerformLayout();
            this.panel_analysis_dots_left.ResumeLayout(false);
            this.panel_analysis_dots_left.PerformLayout();
            this.panel_analysis_picture.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_analysis_dot_main;
        private System.Windows.Forms.Panel panel_analysis_dots_right;
        private System.Windows.Forms.Panel panel_analysis_dots_left;
        private System.Windows.Forms.ComboBox cb_analysis_dots_selectScenario;
        private System.Windows.Forms.Label label_1;
        private System.Windows.Forms.Button btn_analysis_show_dot;
        private System.Windows.Forms.ComboBox cb_analysis_dots_selectImage;
        private System.Windows.Forms.Label label_3;
        private System.Windows.Forms.ComboBox cb_analysis_dots_selectTest;
        private System.Windows.Forms.Label label_2;
        private System.Windows.Forms.Panel panel_analysis_picture;
        private System.Windows.Forms.Panel panel_analysis_picture_dot;
        private System.Windows.Forms.Label label_detail_info_title;
        private System.Windows.Forms.Panel panel_detail_info_label;
        private System.Windows.Forms.Panel panel_detail_info;
        private System.Windows.Forms.Label label_testdate;
        private System.Windows.Forms.Label label_longest;
        private System.Windows.Forms.Label label_shortest;
        private System.Windows.Forms.Label label_visit;
        private System.Windows.Forms.Label label_time;
        private System.Windows.Forms.Label label_click;
    }
}