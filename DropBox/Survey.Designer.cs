namespace DropBox
{
    partial class Survey
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
            this.panel_analysis_survey_main = new System.Windows.Forms.Panel();
            this.fpanel_analysis_survey = new System.Windows.Forms.FlowLayoutPanel();
            this.panel_analysis_survey_left = new System.Windows.Forms.Panel();
            this.cb_analysis_survey_selectScenario = new System.Windows.Forms.ComboBox();
            this.label_1 = new System.Windows.Forms.Label();
            this.btn_analysis_show_survey = new System.Windows.Forms.Button();
            this.cb_analysis_survey_selectTest = new System.Windows.Forms.ComboBox();
            this.label_2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label_result = new System.Windows.Forms.Label();
            this.panel_analysis_survey_main.SuspendLayout();
            this.panel_analysis_survey_left.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_analysis_survey_main
            // 
            this.panel_analysis_survey_main.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel_analysis_survey_main.Controls.Add(this.panel1);
            this.panel_analysis_survey_main.Controls.Add(this.panel_analysis_survey_left);
            this.panel_analysis_survey_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_analysis_survey_main.Location = new System.Drawing.Point(0, 0);
            this.panel_analysis_survey_main.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel_analysis_survey_main.Name = "panel_analysis_survey_main";
            this.panel_analysis_survey_main.Size = new System.Drawing.Size(1108, 652);
            this.panel_analysis_survey_main.TabIndex = 7;
            // 
            // fpanel_analysis_survey
            // 
            this.fpanel_analysis_survey.AutoScroll = true;
            this.fpanel_analysis_survey.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.fpanel_analysis_survey.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.fpanel_analysis_survey.Location = new System.Drawing.Point(0, 93);
            this.fpanel_analysis_survey.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.fpanel_analysis_survey.Name = "fpanel_analysis_survey";
            this.fpanel_analysis_survey.Size = new System.Drawing.Size(803, 559);
            this.fpanel_analysis_survey.TabIndex = 3;
            this.fpanel_analysis_survey.WrapContents = false;
            // 
            // panel_analysis_survey_left
            // 
            this.panel_analysis_survey_left.BackColor = System.Drawing.Color.White;
            this.panel_analysis_survey_left.Controls.Add(this.cb_analysis_survey_selectScenario);
            this.panel_analysis_survey_left.Controls.Add(this.label_1);
            this.panel_analysis_survey_left.Controls.Add(this.btn_analysis_show_survey);
            this.panel_analysis_survey_left.Controls.Add(this.cb_analysis_survey_selectTest);
            this.panel_analysis_survey_left.Controls.Add(this.label_2);
            this.panel_analysis_survey_left.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_analysis_survey_left.Location = new System.Drawing.Point(0, 0);
            this.panel_analysis_survey_left.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel_analysis_survey_left.Name = "panel_analysis_survey_left";
            this.panel_analysis_survey_left.Size = new System.Drawing.Size(305, 652);
            this.panel_analysis_survey_left.TabIndex = 2;
            // 
            // cb_analysis_survey_selectScenario
            // 
            this.cb_analysis_survey_selectScenario.Font = new System.Drawing.Font("굴림", 14F);
            this.cb_analysis_survey_selectScenario.ForeColor = System.Drawing.Color.Black;
            this.cb_analysis_survey_selectScenario.FormattingEnabled = true;
            this.cb_analysis_survey_selectScenario.Location = new System.Drawing.Point(80, 81);
            this.cb_analysis_survey_selectScenario.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cb_analysis_survey_selectScenario.Name = "cb_analysis_survey_selectScenario";
            this.cb_analysis_survey_selectScenario.Size = new System.Drawing.Size(188, 27);
            this.cb_analysis_survey_selectScenario.TabIndex = 8;
            this.cb_analysis_survey_selectScenario.SelectedIndexChanged += new System.EventHandler(this.cb_analysis_survey_selectScenario_SelectedIndexChanged);
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
            // btn_analysis_show_survey
            // 
            this.btn_analysis_show_survey.BackgroundImage = global::DropBox.Properties.Resources._8_2_show_box;
            this.btn_analysis_show_survey.FlatAppearance.BorderSize = 0;
            this.btn_analysis_show_survey.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_analysis_show_survey.ForeColor = System.Drawing.Color.White;
            this.btn_analysis_show_survey.Location = new System.Drawing.Point(80, 483);
            this.btn_analysis_show_survey.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_analysis_show_survey.Name = "btn_analysis_show_survey";
            this.btn_analysis_show_survey.Size = new System.Drawing.Size(188, 63);
            this.btn_analysis_show_survey.TabIndex = 6;
            this.btn_analysis_show_survey.Text = "S H O W";
            this.btn_analysis_show_survey.UseVisualStyleBackColor = true;
            this.btn_analysis_show_survey.Click += new System.EventHandler(this.btn_analysis_show_survey_Click);
            // 
            // cb_analysis_survey_selectTest
            // 
            this.cb_analysis_survey_selectTest.Font = new System.Drawing.Font("굴림", 14F);
            this.cb_analysis_survey_selectTest.ForeColor = System.Drawing.Color.Black;
            this.cb_analysis_survey_selectTest.FormattingEnabled = true;
            this.cb_analysis_survey_selectTest.Location = new System.Drawing.Point(80, 165);
            this.cb_analysis_survey_selectTest.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cb_analysis_survey_selectTest.Name = "cb_analysis_survey_selectTest";
            this.cb_analysis_survey_selectTest.Size = new System.Drawing.Size(188, 27);
            this.cb_analysis_survey_selectTest.TabIndex = 1;
            // 
            // label_2
            // 
            this.label_2.AutoSize = true;
            this.label_2.Font = new System.Drawing.Font("굴림", 18F);
            this.label_2.ForeColor = System.Drawing.Color.Black;
            this.label_2.Location = new System.Drawing.Point(80, 121);
            this.label_2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_2.Name = "label_2";
            this.label_2.Size = new System.Drawing.Size(130, 24);
            this.label_2.TabIndex = 0;
            this.label_2.Text = "테스트선택";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.label_result);
            this.panel1.Controls.Add(this.fpanel_analysis_survey);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(305, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(803, 652);
            this.panel1.TabIndex = 4;
            // 
            // label_result
            // 
            this.label_result.AutoSize = true;
            this.label_result.Font = new System.Drawing.Font("굴림", 18F);
            this.label_result.ForeColor = System.Drawing.Color.Black;
            this.label_result.Location = new System.Drawing.Point(30, 44);
            this.label_result.Name = "label_result";
            this.label_result.Size = new System.Drawing.Size(73, 24);
            this.label_result.TabIndex = 4;
            this.label_result.Text = "Result";
            // 
            // Survey
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1108, 652);
            this.Controls.Add(this.panel_analysis_survey_main);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Survey";
            this.Text = "Survey";
            this.SizeChanged += new System.EventHandler(this.Survey_SizeChanged);
            this.panel_analysis_survey_main.ResumeLayout(false);
            this.panel_analysis_survey_left.ResumeLayout(false);
            this.panel_analysis_survey_left.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_analysis_survey_main;
        private System.Windows.Forms.FlowLayoutPanel fpanel_analysis_survey;
        private System.Windows.Forms.Panel panel_analysis_survey_left;
        private System.Windows.Forms.ComboBox cb_analysis_survey_selectScenario;
        private System.Windows.Forms.Label label_1;
        private System.Windows.Forms.Button btn_analysis_show_survey;
        private System.Windows.Forms.ComboBox cb_analysis_survey_selectTest;
        private System.Windows.Forms.Label label_2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label_result;
    }
}