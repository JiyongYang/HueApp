namespace DropBox
{
    partial class Graph
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Graph));
            this.nChartControl1 = new Nevron.Chart.WinForm.NChartControl();
            this.chartType = new System.Windows.Forms.ComboBox();
            this.taskNameBox = new System.Windows.Forms.ComboBox();
            this.personalBox = new System.Windows.Forms.ComboBox();
            this.panel_analysis_graph_left = new System.Windows.Forms.Panel();
            this.label_1 = new System.Windows.Forms.Label();
            this.btn_analysis_show_dot = new System.Windows.Forms.Button();
            this.label_3 = new System.Windows.Forms.Label();
            this.label_2 = new System.Windows.Forms.Label();
            this.panel_analysis_graph_left.SuspendLayout();
            this.SuspendLayout();
            // 
            // nChartControl1
            // 
            this.nChartControl1.AutoRefresh = false;
            this.nChartControl1.BackColor = System.Drawing.SystemColors.Control;
            this.nChartControl1.InputKeys = new System.Windows.Forms.Keys[0];
            this.nChartControl1.Location = new System.Drawing.Point(390, 44);
            this.nChartControl1.Name = "nChartControl1";
            this.nChartControl1.Size = new System.Drawing.Size(909, 566);
            this.nChartControl1.State = ((Nevron.Chart.WinForm.NState)(resources.GetObject("nChartControl1.State")));
            this.nChartControl1.TabIndex = 0;
            this.nChartControl1.Text = "nChartControl1";
            // 
            // chartType
            // 
            this.chartType.BackColor = System.Drawing.Color.White;
            this.chartType.Font = new System.Drawing.Font("굴림", 14F);
            this.chartType.ForeColor = System.Drawing.Color.Black;
            this.chartType.FormattingEnabled = true;
            this.chartType.Items.AddRange(new object[] {
            "Cumulative Chart",
            "Low Hight Chart"});
            this.chartType.Location = new System.Drawing.Point(80, 82);
            this.chartType.Name = "chartType";
            this.chartType.Size = new System.Drawing.Size(188, 27);
            this.chartType.TabIndex = 5;
            this.chartType.SelectedIndexChanged += new System.EventHandler(this.chartType_SelectedIndexChanged);
            this.chartType.SelectionChangeCommitted += new System.EventHandler(this.chartType_SelectionChangeCommitted);
            // 
            // taskNameBox
            // 
            this.taskNameBox.BackColor = System.Drawing.Color.White;
            this.taskNameBox.Font = new System.Drawing.Font("굴림", 14F);
            this.taskNameBox.ForeColor = System.Drawing.Color.Black;
            this.taskNameBox.FormattingEnabled = true;
            this.taskNameBox.Location = new System.Drawing.Point(80, 166);
            this.taskNameBox.Name = "taskNameBox";
            this.taskNameBox.Size = new System.Drawing.Size(188, 27);
            this.taskNameBox.TabIndex = 9;
            this.taskNameBox.SelectedIndexChanged += new System.EventHandler(this.taskNameBox_SelectedIndexChanged);
            // 
            // personalBox
            // 
            this.personalBox.BackColor = System.Drawing.Color.White;
            this.personalBox.Font = new System.Drawing.Font("굴림", 14F);
            this.personalBox.ForeColor = System.Drawing.Color.Black;
            this.personalBox.FormattingEnabled = true;
            this.personalBox.Location = new System.Drawing.Point(80, 249);
            this.personalBox.Name = "personalBox";
            this.personalBox.Size = new System.Drawing.Size(188, 27);
            this.personalBox.TabIndex = 10;
            this.personalBox.SelectedIndexChanged += new System.EventHandler(this.personalBox_SelectedIndexChanged);
            // 
            // panel_analysis_graph_left
            // 
            this.panel_analysis_graph_left.BackColor = System.Drawing.Color.White;
            this.panel_analysis_graph_left.Controls.Add(this.label_1);
            this.panel_analysis_graph_left.Controls.Add(this.btn_analysis_show_dot);
            this.panel_analysis_graph_left.Controls.Add(this.label_3);
            this.panel_analysis_graph_left.Controls.Add(this.chartType);
            this.panel_analysis_graph_left.Controls.Add(this.personalBox);
            this.panel_analysis_graph_left.Controls.Add(this.label_2);
            this.panel_analysis_graph_left.Controls.Add(this.taskNameBox);
            this.panel_analysis_graph_left.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_analysis_graph_left.Location = new System.Drawing.Point(0, 0);
            this.panel_analysis_graph_left.Margin = new System.Windows.Forms.Padding(2);
            this.panel_analysis_graph_left.Name = "panel_analysis_graph_left";
            this.panel_analysis_graph_left.Size = new System.Drawing.Size(305, 769);
            this.panel_analysis_graph_left.TabIndex = 14;
            // 
            // label_1
            // 
            this.label_1.AutoSize = true;
            this.label_1.Font = new System.Drawing.Font("굴림", 18F);
            this.label_1.ForeColor = System.Drawing.Color.Black;
            this.label_1.Location = new System.Drawing.Point(80, 44);
            this.label_1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_1.Name = "label_1";
            this.label_1.Size = new System.Drawing.Size(130, 24);
            this.label_1.TabIndex = 7;
            this.label_1.Text = "그래프선택";
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
            this.btn_analysis_show_dot.Visible = false;
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
            this.label_3.Text = "테스트선택";
            // 
            // label_2
            // 
            this.label_2.AutoSize = true;
            this.label_2.Font = new System.Drawing.Font("굴림", 18F);
            this.label_2.ForeColor = System.Drawing.Color.Black;
            this.label_2.Location = new System.Drawing.Point(80, 117);
            this.label_2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_2.Name = "label_2";
            this.label_2.Size = new System.Drawing.Size(154, 24);
            this.label_2.TabIndex = 0;
            this.label_2.Text = "시나리오선택";
            // 
            // Graph
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1364, 769);
            this.Controls.Add(this.panel_analysis_graph_left);
            this.Controls.Add(this.nChartControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Graph";
            this.Text = "Graph";
            this.panel_analysis_graph_left.ResumeLayout(false);
            this.panel_analysis_graph_left.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Nevron.Chart.WinForm.NChartControl nChartControl1;
        private System.Windows.Forms.ComboBox chartType;
        private System.Windows.Forms.ComboBox taskNameBox;
        private System.Windows.Forms.ComboBox personalBox;
        private System.Windows.Forms.Panel panel_analysis_graph_left;
        private System.Windows.Forms.Label label_1;
        private System.Windows.Forms.Button btn_analysis_show_dot;
        private System.Windows.Forms.Label label_3;
        private System.Windows.Forms.Label label_2;
    }
}